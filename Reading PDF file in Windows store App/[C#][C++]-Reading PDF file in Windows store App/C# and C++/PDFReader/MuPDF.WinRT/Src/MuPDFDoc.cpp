#include <cmath>
#include <cctype>

#include "MuPDFDoc.h"

MuPDFDoc::MuPDFDoc(int resolution)
	: m_context(nullptr), m_document(nullptr), m_resolution(resolution), m_currentPage(-1), m_cts(nullptr)
{
	for(int i = 0; i < NUM_CACHE; i++)
	{
		m_pages[i].number = -1;
		m_pages[i].width = 0;
		m_pages[i].height = 0;
		m_pages[i].page = nullptr;
		m_pages[i].hqPage = nullptr;
		m_pages[i].pageList = nullptr;
		m_pages[i].annotList = nullptr;
	}
}

MuPDFDoc::~MuPDFDoc()
{
	CoTaskMemFree(m_cts);
	if (m_outline)
	{
		fz_free_outline(m_context, m_outline);
		m_outline = nullptr;
	}
	if (m_document)
	{
		ClearPages();
		fz_close_document(m_document);
		m_document = nullptr;
	}
	if (m_context)
	{
		fz_free_context(m_context);
		m_context = nullptr;
	}
}

HRESULT MuPDFDoc::Create(unsigned char *buffer, int bufferLen, const char *mimeType, int resolution, MuPDFDoc **obj)
{
	MuPDFDoc *doc = new MuPDFDoc(resolution);
	HRESULT result = doc->Init(buffer, bufferLen, mimeType);
	if (FAILED(result))
	{
		delete doc;
		return result;
	}
	else
	{
		*obj = doc;
		return S_OK;
	}
}

HRESULT MuPDFDoc::GotoPage(int pageNumber)
{
	int index = FindPageInCache(pageNumber);
	if (index >= 0)
	{
		m_currentPage = index;
		return S_OK;
	}
	index = GetPageCacheIndex(pageNumber);
	m_currentPage = index;
	PageCache *pageCache = &m_pages[m_currentPage];
	ClearPageCache(pageCache);
	/* In the event of an error, ensure we give a non-empty page */
	pageCache->width = 100;
	pageCache->height = 100;
	pageCache->number = pageNumber;
	fz_try(m_context)
	{
		pageCache->page = fz_load_page(m_document, pageCache->number);
		pageCache->mediaBox = fz_bound_page(m_document, pageCache->page);
		// fz_bound_page determine the size of a page at 72 dpi.
		fz_matrix ctm = CalcConvertMatrix();
		fz_bbox bbox = fz_round_rect(fz_transform_rect(ctm, pageCache->mediaBox));
		pageCache->width = bbox.x1 - bbox.x0;
		pageCache->height = bbox.y1 - bbox.y0;
	}
	fz_catch(m_context)
	{
		return E_FAIL;
	}
	return S_OK;
}

HRESULT MuPDFDoc::DrawPage(unsigned char *bitmap, int x, int y, int width, int height, bool invert)
{
	m_cts->abort = 0;
	fz_device *dev = nullptr;
	fz_pixmap *pixmap = nullptr;
	fz_var(dev);
	fz_var(pixmap);
	PageCache *pageCache = &m_pages[m_currentPage];
	fz_try(m_context)
	{
		fz_interactive *idoc = fz_interact(m_document);
		// Call fz_update_page now to ensure future calls yield the
		// changes from the current state
		if (idoc)
			fz_update_page(idoc, pageCache->page);

		if (!pageCache->pageList)
		{
			/* Render to list */
			pageCache->pageList = fz_new_display_list(m_context);
			dev = fz_new_list_device(m_context, pageCache->pageList);

			fz_run_page_contents(m_document, pageCache->page, dev, fz_identity, *&m_cts);
		}
		if (!pageCache->annotList)
		{
			if (dev)
			{
				fz_free_device(dev);
				dev = nullptr;
			}
			pageCache->annotList = fz_new_display_list(m_context);
			dev = fz_new_list_device(m_context, pageCache->annotList);
			for (fz_annot *annot = fz_first_annot(m_document, pageCache->page); annot; annot = fz_next_annot(m_document, annot))
				fz_run_annot(m_document, pageCache->page, annot, dev, fz_identity, *&m_cts);
		}
		
			fz_bbox rect;
			rect.x0 = x;
			rect.y0 = y;
			rect.x1 = x + width;
			rect.y1 = y + height;
			pixmap = fz_new_pixmap_with_bbox_and_data(m_context, fz_device_bgr, rect, bitmap);
			if (!pageCache->pageList && !pageCache->annotList)
			{
				fz_clear_pixmap_with_value(m_context, pixmap, 0xd0);
				break;
			}
			fz_clear_pixmap_with_value(m_context, pixmap, 0xff);
			//
			fz_matrix ctm = CalcConvertMatrix();
			fz_bbox bbox = fz_round_rect(fz_transform_rect(ctm, pageCache->mediaBox));
			/* Now, adjust ctm so that it would give the correct page width
			 * heights. */
			float xscale = (float)width/(float)(bbox.x1-bbox.x0);
			float yscale = (float)height/(float)(bbox.y1-bbox.y0);
			ctm = fz_concat(ctm, fz_scale(xscale, yscale));
			bbox = fz_round_rect(fz_transform_rect(ctm, pageCache->mediaBox));
			if (dev)
			{
				fz_free_device(dev);
				dev = nullptr;
			}
			dev = fz_new_draw_device(m_context, pixmap);
			if (pageCache->pageList)
				fz_run_display_list(pageCache->pageList, dev, ctm, bbox, *&m_cts);
			about = m_cts->abort;
			if (pageCache->annotList)
				fz_run_display_list(pageCache->annotList, dev, ctm, bbox, *&m_cts);
			if (invert)
				fz_invert_pixmap(m_context, pixmap);
	}
	fz_always(m_context)
	{
		if (dev)
		{
			fz_free_device(dev);
			dev = nullptr;
		}
		if (pixmap)
		{
			fz_drop_pixmap(m_context, pixmap);
		}
	}
	fz_catch(m_context)
	{
		return E_FAIL;
	}

	return S_OK;
}

HRESULT MuPDFDoc::LoadPage(unsigned char *bitmap, int pageNum, int width, int height, Data **data)
{
	GotoPage(pageNum);

	fz_device *dev = nullptr;
	fz_pixmap *pixmap = nullptr;
	fz_var(dev);
	fz_var(pixmap);
	PageCache *pageCache = &m_pages[m_currentPage];
	fz_try(m_context)
	{
		m_cts->abort = 0;
		fz_interactive *idoc = fz_interact(m_document);
		// Call fz_update_page now to ensure future calls yield the
		// changes from the current state
		if (idoc)
			fz_update_page(idoc, pageCache->page);

		if (!pageCache->pageList)
		{
			/* Render to list */
			pageCache->pageList = fz_new_display_list(m_context);
			dev = fz_new_list_device(m_context, pageCache->pageList);

			fz_run_page_contents(m_document, pageCache->page, dev, fz_identity, nullptr);
		}
		if (!pageCache->annotList)
		{
			if (dev)
			{
				fz_free_device(dev);
				dev = nullptr;
			}
			pageCache->annotList = fz_new_display_list(m_context);
			dev = fz_new_list_device(m_context, pageCache->annotList);
			for (fz_annot *annot = fz_first_annot(m_document, pageCache->page); annot; annot = fz_next_annot(m_document, annot))
				fz_run_annot(m_document, pageCache->page, annot, dev, fz_identity, nullptr);
		}

		fz_bbox rect;
		rect.x0 = 0;
		rect.y0 = 0;
		rect.x1 = width;
		rect.y1 = height;
		pixmap = fz_new_pixmap_with_bbox_and_data(m_context, fz_device_bgr, rect, bitmap);
		if (!pageCache->pageList && !pageCache->annotList)
		{
			fz_clear_pixmap_with_value(m_context, pixmap, 0xd0);
			break;
		}
		fz_clear_pixmap_with_value(m_context, pixmap, 0xff);

		*data = (Data*)CoTaskMemAlloc(sizeof(Data));

		(*data)->pagenumber = pageNum;
		(*data)->cacheNumber = m_currentPage;
		(*data)->ctx = m_context;
		(*data)->list = pageCache->pageList;
		(*data)->annotList = pageCache->annotList;
		(*data)->bbox = rect;
		(*data)->rect = pageCache->mediaBox;
		(*data)->pix = pixmap;
		(*data)->width = width;
		(*data)->height = height;
	}
	fz_catch(m_context)
	{
		return E_FAIL;
	}

	return S_OK;
}

HRESULT MuPDFDoc::LoadTwoPages(unsigned char *bitmap1, int pageNum1, unsigned char *bitmap2, int pageNum2, int width, int height, Data **data1, Data **data2)
{
	GotoPage(pageNum1);

	fz_device *dev = nullptr;
	fz_pixmap *pixmap = nullptr;
	fz_var(dev);
	fz_var(pixmap);
	PageCache *pageCache = &m_pages[m_currentPage];
	fz_try(m_context)
	{
		m_cts->abort = 0;

		fz_interactive *idoc = fz_interact(m_document);
		// Call fz_update_page now to ensure future calls yield the
		// changes from the current state
		if (idoc)
			fz_update_page(idoc, pageCache->page);

		if (!pageCache->pageList)
		{
			/* Render to list */
			pageCache->pageList = fz_new_display_list(m_context);
			dev = fz_new_list_device(m_context, pageCache->pageList);

			fz_run_page_contents(m_document, pageCache->page, dev, fz_identity, nullptr);
		}
		if (!pageCache->annotList)
		{
			if (dev)
			{
				fz_free_device(dev);
				dev = nullptr;
			}
			pageCache->annotList = fz_new_display_list(m_context);
			dev = fz_new_list_device(m_context, pageCache->annotList);
			for (fz_annot *annot = fz_first_annot(m_document, pageCache->page); annot; annot = fz_next_annot(m_document, annot))
				fz_run_annot(m_document, pageCache->page, annot, dev, fz_identity, nullptr);
		}

		fz_bbox rect;
		rect.x0 = 0;
		rect.y0 = 0;
		rect.x1 = width;
		rect.y1 = height;
		pixmap = fz_new_pixmap_with_bbox_and_data(m_context, fz_device_bgr, rect, bitmap1);
		if (!pageCache->pageList && !pageCache->annotList)
		{
			fz_clear_pixmap_with_value(m_context, pixmap, 0xd0);
			break;
		}
		fz_clear_pixmap_with_value(m_context, pixmap, 0xff);

		*data1 = (Data*)CoTaskMemAlloc(sizeof(Data));

		(*data1)->pagenumber = pageNum1;
		(*data1)->cacheNumber = m_currentPage;
		(*data1)->ctx = m_context;
		(*data1)->list = pageCache->pageList;
		(*data1)->annotList = pageCache->annotList;
		(*data1)->bbox = rect;
		(*data1)->rect = pageCache->mediaBox;
		(*data1)->pix = pixmap;
		(*data1)->width = width;
		(*data1)->height = height;
	}
	fz_catch(m_context)
	{
		return E_FAIL;
	}

	GotoPage(pageNum2);

	fz_device *dev1 = nullptr;
	fz_pixmap *pixmap1 = nullptr;
	fz_var(dev1);
	fz_var(pixmap1);
	PageCache *pageCache1 = &m_pages[m_currentPage];
	fz_try(m_context)
	{
		fz_interactive *idoc = fz_interact(m_document);
		// Call fz_update_page now to ensure future calls yield the
		// changes from the current state
		if (idoc)
			fz_update_page(idoc, pageCache1->page);

		if (!pageCache1->pageList)
		{
			/* Render to list */
			pageCache1->pageList = fz_new_display_list(m_context);
			dev1 = fz_new_list_device(m_context, pageCache1->pageList);

			fz_run_page_contents(m_document, pageCache1->page, dev1, fz_identity, nullptr);
		}
		if (!pageCache1->annotList)
		{
			if (dev1)
			{
				fz_free_device(dev1);
				dev1 = nullptr;
			}
			pageCache1->annotList = fz_new_display_list(m_context);
			dev1 = fz_new_list_device(m_context, pageCache1->annotList);
			for (fz_annot *annot = fz_first_annot(m_document, pageCache1->page); annot; annot = fz_next_annot(m_document, annot))
				fz_run_annot(m_document, pageCache1->page, annot, dev1, fz_identity, nullptr);
		}

		fz_bbox rect;
		rect.x0 = 0;
		rect.y0 = 0;
		rect.x1 = width;
		rect.y1 = height;
		pixmap1 = fz_new_pixmap_with_bbox_and_data(m_context, fz_device_bgr, rect, bitmap2);
		if (!pageCache1->pageList && !pageCache1->annotList)
		{
			fz_clear_pixmap_with_value(m_context, pixmap1, 0xd0);
			break;
		}
		fz_clear_pixmap_with_value(m_context, pixmap1, 0xff);

		*data2 = (Data*)CoTaskMemAlloc(sizeof(Data));

		(*data2)->pagenumber = pageNum2;
		(*data2)->cacheNumber = m_currentPage;
		(*data2)->ctx = m_context;
		(*data2)->list = pageCache1->pageList;
		(*data2)->annotList = pageCache->annotList;
		(*data2)->bbox = rect;
		(*data2)->rect = pageCache1->mediaBox;
		(*data2)->pix = pixmap1;
		(*data2)->width = width;
		(*data2)->height = height;
	}
	fz_catch(m_context)
	{
		return E_FAIL;
	}

	return S_OK;
}

HRESULT MuPDFDoc::Renderer(Data *data, int part, int numParts)
{
	if (m_cts->abort == 1) return S_OK;

	int pagenumber = data->pagenumber;
	fz_context *ctx = data->ctx;
	fz_display_list *list = data->list;
	fz_display_list *annotList = data->annotList;
	fz_rect rect = data->rect;
	fz_pixmap *pix = data->pix;
	fz_device *dev = nullptr;
	int width = data->width;
	int height = data->height / numParts;


	// The context pointer is pointing to the main thread's
	// context, so here we create a new context based on it for
	// use in this thread.


	ctx = fz_clone_context(ctx);

	fz_try(ctx)
	{
		if (part == 1 && numParts == 2)
		{
			rect.y1 = rect.y1/2;
		}
		if (part == 2 && numParts == 2)
		{
			rect.y0 = rect.y1/2;
		}
		fz_matrix ctm = CalcConvertMatrix();
		fz_bbox bbox = fz_round_rect(fz_transform_rect(ctm, rect));
		/* Now, adjust ctm so that it would give the correct page width
		* heights. */
		float xscale = (float)width/(float)(bbox.x1-bbox.x0);
		float yscale = (float)height/(float)(bbox.y1-bbox.y0);
		ctm = fz_concat(ctm, fz_scale(xscale, yscale));
		bbox = fz_round_rect(fz_transform_rect(ctm, rect));

		dev = fz_new_draw_device(ctx, pix);
		if (list)
			fz_run_display_list(list, dev, ctm, bbox, *&m_cts);
		if (m_cts->abort == 1) break;
		if (annotList)
			fz_run_display_list(annotList, dev, ctm, bbox, *&m_cts);
	}
	fz_always(ctx)
	{
		if (dev)
		{
			fz_free_device(dev);
			dev = nullptr;
		}

		if (pix)
		{
			fz_drop_pixmap(m_context, pix);
		}
	}
	fz_catch(ctx)
	{
		return E_FAIL;
	}

	fz_free_context(ctx);

	return S_OK;
}


HRESULT MuPDFDoc::UpdatePage(int pageNumber, unsigned char *bitmap, int x, int y, int width, int height, bool invert)
{
	int index = FindPageInCache(pageNumber);
	if (index < 0)
	{
		//TODO: get rid of this side effect!!!
		HRESULT result = GotoPage(pageNumber);
		if (FAILED(result))
		{
			return result;
		}
		return DrawPage(bitmap, x, y, width, height, invert);
	}
	fz_device *dev = nullptr;
	fz_pixmap *pixmap = nullptr;
	fz_var(dev);
	fz_var(pixmap);
	PageCache *pageCache = &m_pages[m_currentPage];
	fz_try(m_context)
	{
		fz_interactive *idoc = fz_interact(m_document);
		// Call fz_update_page now to ensure future calls yield the
		// changes from the current state
		if (idoc)
			fz_update_page(idoc, pageCache->page);

		if (!pageCache->pageList)
		{
			/* Render to list */
			pageCache->pageList = fz_new_display_list(m_context);
			dev = fz_new_list_device(m_context, pageCache->pageList);
			fz_run_page_contents(m_document, pageCache->page, dev, fz_identity, nullptr);
		}
		if (!pageCache->annotList)
		{
			if (dev)
			{
				fz_free_device(dev);
				dev = nullptr;
			}
			pageCache->annotList = fz_new_display_list(m_context);
			dev = fz_new_list_device(m_context, pageCache->annotList);
			for (fz_annot *annot = fz_first_annot(m_document, pageCache->page); annot; annot = fz_next_annot(m_document, annot))
				fz_run_annot(m_document, pageCache->page, annot, dev, fz_identity, nullptr);
		}
		fz_bbox rect;
		rect.x0 = x;
		rect.y0 = y;
		rect.x1 = x + width;
		rect.y1 = y + height;
		pixmap = fz_new_pixmap_with_bbox_and_data(m_context, fz_device_bgr, rect, bitmap);
		//
		fz_matrix ctm = CalcConvertMatrix();
		fz_bbox bbox = fz_round_rect(fz_transform_rect(ctm, pageCache->mediaBox));
		/* Now, adjust ctm so that it would give the correct page width
		 * heights. */
		float xscale = (float)width/(float)(bbox.x1-bbox.x0);
		float yscale = (float)height/(float)(bbox.y1-bbox.y0);
		ctm = fz_concat(ctm, fz_scale(xscale, yscale));
		bbox = fz_round_rect(fz_transform_rect(ctm, pageCache->mediaBox));
		if (dev)
		{
			fz_free_device(dev);
			dev = nullptr;
		}
		fz_annot *annot;
		while (idoc && (annot = fz_poll_changed_annot(idoc, pageCache->page)))
		{
			fz_bbox abox = fz_round_rect(fz_transform_rect(ctm, fz_bound_annot(m_document, annot)));
			abox = fz_intersect_bbox(abox, rect);

			if (!fz_is_empty_bbox(abox))
			{
				fz_clear_pixmap_rect_with_value(m_context, pixmap, 0xff, abox);
				dev = fz_new_draw_device_with_bbox(m_context, pixmap, abox);
				if (pageCache->pageList)
					fz_run_display_list(pageCache->pageList, dev, ctm, abox, nullptr);
				if (pageCache->annotList)
					fz_run_display_list(pageCache->annotList, dev, ctm, abox, nullptr);
				fz_free_device(dev);
				dev = nullptr;
				if (invert)
					fz_invert_pixmap_rect(pixmap, abox);
			}
		}
	}
	fz_always(m_context)
	{
		if (dev)
		{
			fz_free_device(dev);
			dev = nullptr;
		}
		if (pixmap)
		{
			fz_drop_pixmap(m_context, pixmap);
		}
	}
	fz_catch(m_context)
	{
		return E_FAIL;
	}
	return S_OK;
}

bool MuPDFDoc::AuthenticatePassword(char *password)
{
	return fz_authenticate_password(m_document, password) != 0;
}

int MuPDFDoc::GetPageWidth()
{
	return m_pages[m_currentPage].width;
}

int MuPDFDoc::GetPageHeight()
{
	return m_pages[m_currentPage].height;
}

static std::unique_ptr<char[]> CopyToUniqueStr(char *str)
{
	size_t len = strlen(str);
	std::unique_ptr<char[]> copyStr(new char[len + 1]);
	strcpy_s(copyStr.get(), len + 1, str);
	return copyStr;
}

static std::shared_ptr<MuPDFDocLink> CreateLink(const fz_rect &rect)
{
	std::shared_ptr<MuPDFDocLink> docLink(new MuPDFDocLink());
	docLink->left = rect.x0;
	docLink->top = rect.y0;
	docLink->right = rect.x1;
	docLink->bottom = rect.y1;
	return docLink;
}

static std::shared_ptr<MuPDFDocLink> CreateInternalLink(const fz_link *link, fz_rect &rect)
{
	std::shared_ptr<MuPDFDocLink> docLink(CreateLink(rect));
	docLink->type = INTERNAL;
	docLink->internalPageNumber = link->dest.ld.gotor.page;
	return docLink;
}

static std::shared_ptr<MuPDFDocLink> CreateRemoteLink(const fz_link *link, fz_rect &rect)
{
	std::shared_ptr<MuPDFDocLink> docLink(CreateLink(rect));
	docLink->type = REMOTE;
	docLink->remotePageNumber = link->dest.ld.gotor.page;
	docLink->newWindow = link->dest.ld.gotor.new_window != 0 ? true : false;
	docLink->fileSpec = CopyToUniqueStr(link->dest.ld.gotor.file_spec);
	return docLink;
}

std::shared_ptr<MuPDFDocLink> CreateURILink(const fz_link *link, fz_rect &rect)
{
	std::shared_ptr<MuPDFDocLink> docLink(CreateLink(rect));
	docLink->type = URI;
	docLink->uri = CopyToUniqueStr(link->dest.ld.uri.uri);	
	return docLink;
}

std::shared_ptr<std::vector<std::shared_ptr<MuPDFDocLink>>> MuPDFDoc::GetLinks()
{
	PageCache *pageCache = &m_pages[m_currentPage];
	std::shared_ptr<std::vector<std::shared_ptr<MuPDFDocLink>>> links(new std::vector<std::shared_ptr<MuPDFDocLink>>());
	fz_matrix ctm = CalcConvertMatrix();
	fz_link* list = fz_load_links(m_document, pageCache->page);
	for(fz_link* link = list; link; link = link->next)
	{
		fz_rect rect = fz_transform_rect(ctm, link->rect);
		std::shared_ptr<MuPDFDocLink> docLink;
		switch (link->dest.kind)
		{
		case FZ_LINK_GOTO:
		{
			docLink = CreateInternalLink(link, rect);
			break;
		}

		case FZ_LINK_GOTOR:
		{
			docLink = CreateRemoteLink(link, rect);
			break;
		}

		case FZ_LINK_URI:
		{
			docLink = CreateURILink(link, rect);
			break;
		}

		default:
			continue;
		}
		links->push_back(docLink);
	}
	fz_drop_link(m_context, list);
	return links;
}

static int TextLen(fz_text_page *page)
{
	int len = 0;
	for (fz_text_block *block = page->blocks; block < page->blocks + page->len; block++)
	{
		for (fz_text_line *line = block->lines; line < block->lines + block->len; line++)
		{
			for (fz_text_span *span = line->spans; span < line->spans + line->len; span++)
				len += span->len;
			len++; /* pseudo-newline */
		}
	}
	return len;
}

static fz_text_char TextCharAt(fz_text_page *page, int idx)
{
	static fz_text_char emptychar = { {0,0,0,0}, ' ' };
	int ofs = 0;
	for (fz_text_block *block = page->blocks; block < page->blocks + page->len; block++)
	{
		for (fz_text_line *line = block->lines; line < block->lines + block->len; line++)
		{
			for (fz_text_span *span = line->spans; span < line->spans + line->len; span++)
			{
				if (idx < ofs + span->len)
					return span->text[idx - ofs];
				/* pseudo-newline */
				if (span + 1 == line->spans + line->len)
				{
					if (idx == ofs + span->len)
						return emptychar;
					ofs++;
				}
				ofs += span->len;
			}
		}
	}
	return emptychar;
}

static int CharAt(fz_text_page *page, int idx)
{
	return TextCharAt(page, idx).c;
}

static int Match(fz_text_page *page, const char *str, int n)
{
	int orig = n;
	int c;
	while (*str) 
	{
		str += fz_chartorune(&c, (char *)str);
		if (c == ' ' && CharAt(page, n) == ' ') 
		{
			while (CharAt(page, n) == ' ')
				n++;
		} 
		else 
		{
			if (tolower(c) != tolower(CharAt(page, n)))
				return 0;
			n++;
		}
	}
	return n - orig;
}

static fz_bbox BBoxCharAt(fz_text_page *page, int idx)
{
	return fz_round_rect(TextCharAt(page, idx).bbox);
}

std::shared_ptr<std::vector<std::shared_ptr<RectFloat>>> MuPDFDoc::SearchText(const char* searchText)
{
	fz_text_sheet *sheet = nullptr;
	fz_text_page *text = nullptr;
	fz_device *dev  = nullptr;
	PageCache *pageCache = &m_pages[m_currentPage];
	fz_var(sheet);
	fz_var(text);
	fz_var(dev);
	std::shared_ptr<std::vector<std::shared_ptr<RectFloat>>> hints(new std::vector<std::shared_ptr<RectFloat>>());
	fz_try(m_context)
	{
		int hitCount = 0;
		fz_matrix ctm = CalcConvertMatrix();
		fz_rect mbrect = fz_transform_rect(ctm, pageCache->mediaBox);
		sheet = fz_new_text_sheet(m_context);
		text = fz_new_text_page(m_context, mbrect);
		dev = fz_new_text_device(m_context, sheet, text);
		fz_run_page(m_document, pageCache->page, dev, ctm, nullptr);
		fz_free_device(dev);
		dev = nullptr;
		int len = TextLen(text);
		for (int pos = 0; pos < len; pos++)
		{
			fz_bbox rr = fz_empty_bbox;
			int n = Match(text, searchText, pos);
			for (int i = 0; i < n; i++)
				rr = fz_union_bbox(rr, BBoxCharAt(text, pos + i));

			if (!fz_is_empty_bbox(rr) && hitCount < MAX_SEARCH_HITS)
			{
				hints->push_back(std::shared_ptr<RectFloat>(new RectFloat(rr.x0, rr.y0, rr.x1, rr.y1)));
				if (++hitCount >= MAX_SEARCH_HITS)
					break;
			}
		}
	}
	fz_always(m_context)
	{
		fz_free_text_page(m_context, text);
		fz_free_text_sheet(m_context, sheet);
		fz_free_device(dev);
	}
	fz_catch(m_context)
	{
		return std::shared_ptr<std::vector<std::shared_ptr<RectFloat>>>(nullptr);
	}
	return hints;
}

std::shared_ptr<std::vector<std::shared_ptr<Outlineitem>>> MuPDFDoc::GetOutline()
{
	std::shared_ptr<std::vector<std::shared_ptr<Outlineitem>>> items(new std::vector<std::shared_ptr<Outlineitem>>());
	FillOutline(items, 0, m_outline, 0);
	return items;
}

HRESULT MuPDFDoc::Init(unsigned char *buffer, int bufferLen, const char *mimeType)
{
	HRESULT result = InitContext();
	if (FAILED(result))
	{
		return result;
	}
	else
	{
		result = InitDocument(buffer, bufferLen, mimeType);
		return result;
	}
}

// These are the two locking functions required by MuPDF when
// operating in a multi-threaded environment. They each take a user
// argument that can be used to transfer some state, in this case a
// pointer to the array of mutexes.

void lock_mutex(void *user, int lock)
{
	EnterCriticalSection((LPCRITICAL_SECTION)user);
}

void unlock_mutex(void *user, int lock)
{
	LeaveCriticalSection((LPCRITICAL_SECTION)user);
}

HRESULT MuPDFDoc::InitContext()
{
	InitializeCriticalSectionEx(&m_critSec, 0, 0);
	locks.user = &m_critSec;
	locks.lock = lock_mutex;
	locks.unlock = unlock_mutex;
	m_context = fz_new_context(nullptr, &locks, FZ_STORE_DEFAULT);
	if (!m_context)
	{
		return E_OUTOFMEMORY;
	}
	else
	{
		m_cts = (fz_cookie*)CoTaskMemAlloc(sizeof(fz_cookie));
		return S_OK;
	}
}

HRESULT MuPDFDoc::InitDocument(unsigned char *buffer, int bufferLen, const char *mimeType)
{
	fz_stream *stream = OpenStream(buffer, bufferLen);
	if (!stream)
	{
		return E_OUTOFMEMORY;
	}
	else
	{
		fz_try(m_context)
		{
			m_document = fz_open_document_with_stream(m_context, mimeType, stream);
			m_outline = fz_load_outline(m_document);
			//AlertsInit();
		}
		fz_always(m_context)
		{
			fz_close(stream);
		}
		fz_catch(m_context)
		{
			return E_INVALIDARG;
		}
		return S_OK;
	}
}

fz_stream *MuPDFDoc::OpenStream(unsigned char *buffer, int bufferLen)
{
	fz_stream *stream = nullptr;
	fz_try(m_context)
	{
		stream = fz_open_memory(m_context, buffer, bufferLen);
	}
	fz_catch(m_context)
	{
		return nullptr;
	}
	return stream;
}

void MuPDFDoc::ClearPageCache(PageCache *pageCache)
{
	fz_try(m_context)
	{
		fz_free_display_list(m_context, pageCache->pageList);
		pageCache->pageList = nullptr;
		fz_free_display_list(m_context, pageCache->annotList);
		pageCache->annotList = nullptr;
		fz_free_page(m_document, pageCache->page);
		pageCache->page = nullptr;
		fz_free_page(m_document, pageCache->hqPage);
		pageCache->hqPage = nullptr;
	}
	fz_catch(m_context)
	{
	}
}

void MuPDFDoc::ClearPages()
{
	for(int i = 0; i < NUM_CACHE; i++)
	{
		ClearPageCache(&m_pages[i]);
	}
}

int MuPDFDoc::FindPageInCache(int pageNumber)
{
	for(int i = 0; i < NUM_CACHE; i++)
	{
		if (m_pages[i].page && m_pages[i].number == pageNumber)
		{
			return i;
		}
	}
	return -1;
}

int MuPDFDoc::GetPageCacheIndex(int pageNumber)
{
	int furthest = 0;
	int furthestDist = -1;
	for (int i = 0; i < NUM_CACHE; i++)
	{
		if (!m_pages[i].page)
		{
			/* cache record unused, and so a good one to use */
			return i;
		}
		else
		{
			int dist = abs(m_pages[i].number - pageNumber);
			/* Further away - less likely to be needed again */
			if (dist > furthestDist)
			{
				furthestDist = dist;
				furthest = i;
			}
		}
	}
	return furthest;
}

int MuPDFDoc::FillOutline(
	std::shared_ptr<std::vector<std::shared_ptr<Outlineitem>>> items, 
	int position, 
	fz_outline *outline, 
	int level)
{
	while (outline)
	{
		if (outline->dest.kind == FZ_LINK_GOTO)
		{
			int pageNumber = outline->dest.ld.gotor.page;
			if (pageNumber >= 0 && outline->title)
			{
				std::shared_ptr<Outlineitem> item(new Outlineitem());
				item->level = level;
				item->pageNumber = pageNumber;
				item->title = CopyToUniqueStr(outline->title);
				items->push_back(item); 
				position++;
			}
		}
		position = FillOutline(items, position, outline->down, level + 1);
		if (position < 0) 
			return -1;
		outline = outline->next;
	}
	return position;
}

fz_matrix MuPDFDoc::CalcConvertMatrix()
{
	// fz_bound_page determine the size of a page at 72 dpi.
	float zoom = m_resolution / 72.0;
	return fz_scale(zoom, zoom);
}

bool MuPDFDoc::IsCached(int pageNumber)
{
	int x = -1;
	x = FindPageInCache(pageNumber);
	if (x < 0)
		return false;

	if (m_pages[x].pageList)
		return true;
	else return false;
}

void MuPDFDoc::CancelDraw() 
{ 
	fz_context *ctx = fz_clone_context(m_context);
	fz_try(ctx)
	{
	m_cts->abort = 1;
	}
	fz_catch(ctx)
	{
	}
	fz_free_context(ctx);
}

bool MuPDFDoc::IsCanceled() 
{ 
	bool f = false;
	fz_context *ctx = fz_clone_context(m_context);
	fz_try(ctx)
	{
		f = (m_cts->abort == 1);
	}
	fz_catch(ctx)
	{
	}
	fz_free_context(ctx);
	return f;
}