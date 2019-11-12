// Document.cpp

#include <vector>
#include <memory>

#include <Inspectable.h>
#include <Robuffer.h>
#include <collection.h>
#include <ppltasks.h>

#include "Document.h"
#include "Utilities.h"

using namespace MuPDFWinRT;
using namespace concurrency;

Document::Document()
	: m_doc(nullptr), m_buffer(nullptr)
{
}

Document::~Document()
{
	if (m_doc)
	{
		delete m_doc;
		m_doc = nullptr;
	}
	if (m_buffer)
	{
		m_buffer = nullptr;
	}
}

Document^ Document::Create(Windows::Storage::Streams::IBuffer^ buffer, DocumentType documentType, int32 resolution)
{
	if (!buffer)
	{
		throw ref new Platform::InvalidArgumentException(L"buffer");
	}
	Document^ document = ref new Document();
	document->Init(buffer, documentType, resolution);
	return document;
}

Platform::Boolean Document::AuthenticatePassword(Platform::String^ password)
{
	std::lock_guard<std::mutex> lock(m_lock);
	int ansiLength =  WideCharToMultiByte(
		CP_ACP, 
		0, 
		password->Data(), 
		-1, 
		nullptr, 
		0, 
		nullptr, 
		nullptr);
	if (ansiLength == 0)
		throw ref new Platform::FailureException();
	std::unique_ptr<char[]> ansiPassword(new char[ansiLength]);
	ansiLength =  WideCharToMultiByte(
		CP_ACP, 
		0, 
		password->Data(), 
		-1, 
		ansiPassword.get(),
		ansiLength,
		nullptr, 
		nullptr);
	if (ansiLength == 0)
		throw ref new Platform::FailureException();
	return m_doc->AuthenticatePassword(ansiPassword.get());
}

Point Document::GetPageSize(int pageNumber)
{
	//std::lock_guard<std::mutex> lock(m_lock);
	Utilities::ThrowIfFailed(m_doc->GotoPage(pageNumber));
	Point size;
	size.X = m_doc->GetPageWidth();
	size.Y = m_doc->GetPageHeight();
	return size;
}

Windows::Foundation::Collections::IVector<ILinkInfo^>^ Document::GetLinks(int32 pageNumber)
{
	std::lock_guard<std::mutex> lock(m_lock);
	Utilities::ThrowIfFailed(m_doc->GotoPage(pageNumber));
	auto links = m_doc->GetLinks();
	auto items = ref new Platform::Collections::Vector<ILinkInfo^>();
	for(size_t i = 0; i < links->size(); i++)
	{
		auto linkInfo = CreateLinkInfo(links->at(i));
		items->InsertAt(i, linkInfo);
	}
	return items;
}

void Document::DrawPage(
	int32 pageNumber, 
	Platform::WriteOnlyArray<int>^ pixels, 
	int32 x, 
	int32 y, 
	int32 width, 
	int32 height,
	Platform::Boolean invert)
{
	std::lock_guard<std::mutex> lock(m_lock);
	Utilities::ThrowIfFailed(m_doc->GotoPage(pageNumber));
	Utilities::ThrowIfFailed(m_doc->DrawPage((unsigned char *)pixels->Data, x, y, width, height, invert));
}

void Document::DrawPage(
	int32 pageNumber, 
	Windows::Storage::Streams::IBuffer^ pixels, 
	int32 x, 
	int32 y, 
	int32 width, 
	int32 height,
	Platform::Boolean invert)
{
	std::lock_guard<std::mutex> lock(m_lock);
	unsigned char *data = GetPointerToData(pixels);
	Utilities::ThrowIfFailed(m_doc->GotoPage(pageNumber));
	Utilities::ThrowIfFailed(m_doc->DrawPage(data, x, y, width, height, invert));
}

void Document::DrawTwoPagesConcurrent(
			int32 firstPage, 
			Windows::Storage::Streams::IBuffer^ pixels, 
			int32 width, 
			int32 height)
{
	std::lock_guard<std::mutex> lock(m_lock);
	unsigned char *bmp1 = (unsigned char*)CoTaskMemAlloc(4 * width * height * sizeof(unsigned char));
	unsigned char *bmp2 = (unsigned char*)CoTaskMemAlloc(4 * width * height * sizeof(unsigned char));
	unsigned char *bmp3 = (unsigned char*)CoTaskMemAlloc(4 * width * height * sizeof(unsigned char));
	unsigned char *bmp4 = (unsigned char*)CoTaskMemAlloc(4 * width * height * sizeof(unsigned char));
	Data *data1 = nullptr;
	Data *data2 = nullptr;
	Utilities::ThrowIfFailed(m_doc->LoadTwoPages(bmp1, firstPage, bmp2, firstPage + 1, width, height, &data1, &data2));
	Data *data3 = nullptr;
	Data *data4 = nullptr;
	Utilities::ThrowIfFailed(m_doc->LoadTwoPages(bmp3, firstPage, bmp4, firstPage + 1, width, height, &data3, &data4));

	auto task1 = create_task( [this, data1]
	{
		Utilities::ThrowIfFailed(this->m_doc->Renderer(data1, 1, 2));
	});

	auto task2 = create_task( [this, data2]
	{
		Utilities::ThrowIfFailed(this->m_doc->Renderer(data2, 1, 2));
	});

	auto task3 = create_task( [this, data3]
	{
		Utilities::ThrowIfFailed(this->m_doc->Renderer(data3, 2, 2));
	});

	auto task4 = create_task( [this, data4]
	{
		Utilities::ThrowIfFailed(this->m_doc->Renderer(data4, 2, 2));
	});

	std::vector<task<void>> tasks(4);
	tasks[0] = task1;
	tasks[1] = task2;
	tasks[2] = task3;
	tasks[3] = task4;
	concurrency::when_all(tasks.begin(), tasks.end()).then( [this, data1, data2, data3, data4, bmp1, bmp2, bmp3, bmp4, pixels, width, height]
	{
		CoTaskMemFree(data1);
		CoTaskMemFree(data2);
		CoTaskMemFree(data3);
		CoTaskMemFree(data4);

		if (!this->m_doc->IsCanceled() && pixels != nullptr && pixels->Length > 1000)
		{
			unsigned char *data = GetPointerToData(pixels);
			auto size = width * 4;
			for (int x = 0; x < height / 2; x++)
			{
				if (pixels == nullptr) break;
				if (pixels->Length < 1000) break;
				CopyMemory(data + (2 * (size * x)), bmp1 + (size * x), size);
				if (pixels == nullptr) break;
				if (pixels->Length < 1000) break;
				CopyMemory(data + (2 * (size * x) + size), bmp2 + (size * x), size);
			}
			for (int x = height / 2; x < height; x++)
			{
				if (pixels == nullptr) break;
				if (pixels->Length < 1000) break;
				CopyMemory(data + (2 * (size * x)), bmp3 + (size * x), size);
				if (pixels == nullptr) break;
				if (pixels->Length < 1000) break;
				CopyMemory(data + (2 * (size * x) + size), bmp4 + (size * x), size);
			}
		}

		CoTaskMemFree(bmp1);
		CoTaskMemFree(bmp2);
		CoTaskMemFree(bmp3);
		CoTaskMemFree(bmp4);

	}).then( [] (task<void> t) 
	{
		try
		{
			t.get();
		}
		catch (std::exception)
		{
		}
	}).wait();;
}

void Document::DrawFirtPageConcurrent(
			int32 firstPage, 
			Windows::Storage::Streams::IBuffer^ pixels, 
			int32 width, 
			int32 height)
{
	std::lock_guard<std::mutex> lock(m_lock);
	unsigned char *bmp1 = (unsigned char*)CoTaskMemAlloc(4 * width * height * sizeof(unsigned char));
	unsigned char *bmp3 = (unsigned char*)CoTaskMemAlloc(4 * width * height * sizeof(unsigned char));
	Data *data1 = nullptr;
	Utilities::ThrowIfFailed(m_doc->LoadPage(bmp1, firstPage, width, height, &data1));
	Data *data3 = nullptr;
	Utilities::ThrowIfFailed(m_doc->LoadPage(bmp3, firstPage, width, height, &data3));

	auto task1 = create_task( [this, data1]
	{
		Utilities::ThrowIfFailed(this->m_doc->Renderer(data1, 1, 2));
	});

	auto task3 = create_task( [this, data3]
	{
		Utilities::ThrowIfFailed(this->m_doc->Renderer(data3, 2, 2));
	});

	std::vector<task<void>> tasks(2);
	tasks[0] = task1;
	tasks[1] = task3;
	concurrency::when_all(tasks.begin(), tasks.end()).then( [this, data1, data3, bmp1, bmp3, pixels, width, height]
	{
		CoTaskMemFree(data1);
		CoTaskMemFree(data3);

		if (!this->m_doc->IsCanceled() && pixels != nullptr && pixels->Length > 1000)
		{
			unsigned char *data = GetPointerToData(pixels);
			auto size = width * 4;
			for (int x = 0; x < height / 2; x++)
			{
				if (pixels == nullptr) break;
				if (pixels->Length < 1000) break;
				CopyMemory(data + (2 * (size * x)), bmp1 + (size * x), size);
			}
			for (int x = height / 2; x < height; x++)
			{
				if (pixels == nullptr) break;
				if (pixels->Length < 1000) break;
				CopyMemory(data + (2 * (size * x)), bmp3 + (size * x), size);
			}
		}

		CoTaskMemFree(bmp1);
		CoTaskMemFree(bmp3);

	}).then( [] (task<void> t) 
	{
		try
		{
			t.get();
		}
		catch (std::exception)
		{
		}
	}).wait();;
}

void Document::DrawFirtPageConcurrent(
			int32 firstPage, 
			Windows::Storage::Streams::IBuffer^ pixels, 
			Windows::Storage::Streams::IBuffer^ bitmap, 
			int32 width, 
			int32 height)
{
	std::lock_guard<std::mutex> lock(m_lock);
	unsigned char *bmp1 = (unsigned char*)CoTaskMemAlloc(4 * width * height * sizeof(unsigned char));
	unsigned char *bmp3 = (unsigned char*)CoTaskMemAlloc(4 * width * height * sizeof(unsigned char));
	Data *data1 = nullptr;
	Utilities::ThrowIfFailed(m_doc->LoadPage(bmp1, firstPage, width, height, &data1));
	Data *data3 = nullptr;
	Utilities::ThrowIfFailed(m_doc->LoadPage(bmp3, firstPage, width, height, &data3));

	auto task1 = create_task( [this, data1]
	{
		Utilities::ThrowIfFailed(this->m_doc->Renderer(data1, 1, 2));
	});

	auto task3 = create_task( [this, data3]
	{
		Utilities::ThrowIfFailed(this->m_doc->Renderer(data3, 2, 2));
	});

	std::vector<task<void>> tasks(2);
	tasks[0] = task1;
	tasks[1] = task3;
	concurrency::when_all(tasks.begin(), tasks.end()).then( [this, data1, data3, bmp1, bmp3, pixels, width, height, bitmap]
	{
		CoTaskMemFree(data1);
		CoTaskMemFree(data3);

		if (!this->m_doc->IsCanceled())
		{
			unsigned char *data = GetPointerToData(pixels);
			unsigned char *secondPage = GetPointerToData(bitmap);
			auto size = width * 4;
			for (int x = 0; x < height / 2; x++)
			{
				CopyMemory(data + (2 * (size * x)), bmp1 + (size * x), size);
				CopyMemory(data + (2 * (size * x) + size), secondPage + (size * x), size);
			}
			for (int x = height / 2; x < height; x++)
			{
				CopyMemory(data + (2 * (size * x)), bmp3 + (size * x), size);
				CopyMemory(data + (2 * (size * x) + size), secondPage + (size * x), size);
			}
		}

		CoTaskMemFree(bmp1);
		CoTaskMemFree(bmp3);

	}).then( [] (task<void> t) 
	{
		try
		{
			t.get();
		}
		catch (std::exception)
		{
		}
	}).wait();;
}

void Document::DrawSecondPageConcurrent(
			int32 firstPage, 
			Windows::Storage::Streams::IBuffer^ pixels, 
			int32 width, 
			int32 height)
{
	std::lock_guard<std::mutex> lock(m_lock);
	unsigned char *bmp2 = (unsigned char*)CoTaskMemAlloc(4 * width * height * sizeof(unsigned char));
	unsigned char *bmp4 = (unsigned char*)CoTaskMemAlloc(4 * width * height * sizeof(unsigned char));
	Data *data2 = nullptr;
	Utilities::ThrowIfFailed(m_doc->LoadPage( bmp2, firstPage + 1, width, height, &data2));
	Data *data4 = nullptr;
	Utilities::ThrowIfFailed(m_doc->LoadPage(bmp4, firstPage + 1, width, height, &data4));

	auto task2 = create_task( [this, data2]
	{
		Utilities::ThrowIfFailed(this->m_doc->Renderer(data2, 1, 2));
	});

	auto task4 = create_task( [this, data4]
	{
		Utilities::ThrowIfFailed(this->m_doc->Renderer(data4, 2, 2));
	});

	std::vector<task<void>> tasks(2);
	tasks[0] = task2;
	tasks[1] = task4;
	concurrency::when_all(tasks.begin(), tasks.end()).then( [this, data2, data4, bmp2, bmp4, pixels, width, height]
	{
		CoTaskMemFree(data2);
		CoTaskMemFree(data4);

		if (!this->m_doc->IsCanceled())
		{
			unsigned char *data = GetPointerToData(pixels);
			auto size = width * 4;
			for (int x = 0; x < height / 2; x++)
			{
				if (pixels == nullptr) break;
				if (pixels->Length < 1000) break;
				CopyMemory(data + (2 * (size * x) + size), bmp2 + (size * x), size);
			}
			for (int x = height / 2; x < height; x++)
			{
				if (pixels == nullptr) break;
				if (pixels->Length < 1000) break;
				CopyMemory(data + (2 * (size * x) + size), bmp4 + (size * x), size);
			}
		}

		CoTaskMemFree(bmp2);
		CoTaskMemFree(bmp4);

	}).then( [] (task<void> t) 
	{
		try
		{
			t.get();
		}
		catch (std::exception)
		{
		}
	}).wait();;
}

void Document::DrawSecondPageConcurrent(
			int32 firstPage, 
			Windows::Storage::Streams::IBuffer^ pixels, 
			Windows::Storage::Streams::IBuffer^ bitmap, 
			int32 width, 
			int32 height)
{
	std::lock_guard<std::mutex> lock(m_lock);
	unsigned char *bmp2 = (unsigned char*)CoTaskMemAlloc(4 * width * height * sizeof(unsigned char));
	unsigned char *bmp4 = (unsigned char*)CoTaskMemAlloc(4 * width * height * sizeof(unsigned char));
	Data *data2 = nullptr;
	Utilities::ThrowIfFailed(m_doc->LoadPage( bmp2, firstPage + 1, width, height, &data2));
	Data *data4 = nullptr;
	Utilities::ThrowIfFailed(m_doc->LoadPage(bmp4, firstPage + 1, width, height, &data4));

	auto task2 = create_task( [this, data2]
	{
		Utilities::ThrowIfFailed(this->m_doc->Renderer(data2, 1, 2));
	});

	auto task4 = create_task( [this, data4]
	{
		Utilities::ThrowIfFailed(this->m_doc->Renderer(data4, 2, 2));
	});

	std::vector<task<void>> tasks(2);
	tasks[0] = task2;
	tasks[1] = task4;
	concurrency::when_all(tasks.begin(), tasks.end()).then( [this, data2, data4, bmp2, bmp4, pixels, width, height, bitmap]
	{
		CoTaskMemFree(data2);
		CoTaskMemFree(data4);

		if (!this->m_doc->IsCanceled())
		{
			unsigned char *data = GetPointerToData(pixels);
			unsigned char *firstPage = GetPointerToData(bitmap);
			auto size = width * 4;
			for (int x = 0; x < height / 2; x++)
			{
				CopyMemory(data + (2 * (size * x)), firstPage + (size * x), size);
				CopyMemory(data + (2 * (size * x) + size), bmp2 + (size * x), size);
			}
			for (int x = height / 2; x < height; x++)
			{
				CopyMemory(data + (2 * (size * x)), firstPage + (size * x), size);
				CopyMemory(data + (2 * (size * x) + size), bmp4 + (size * x), size);
			}
		}

		CoTaskMemFree(bmp2);
		CoTaskMemFree(bmp4);

	}).then( [] (task<void> t) 
	{
		try
		{
			t.get();
		}
		catch (std::exception)
		{
		}
	}).wait();
}

bool Document::IsCached(int32 pageNumber)
{
	return m_doc->IsCached(pageNumber);
}

void Document::CancelDraw()
{ 
	m_doc->CancelDraw(); 
}

Windows::Foundation::Collections::IVector<RectF>^ Document::SearchText(int32 pageNumber, Platform::String^ text)
{
	std::lock_guard<std::mutex> lock(m_lock);
	Utilities::ThrowIfFailed(m_doc->GotoPage(pageNumber));
	auto ut8Text = Utilities::ConvertStringToUTF8(text);
	auto hints = m_doc->SearchText(ut8Text.get());
	if (!hints)
		throw ref new Platform::OutOfMemoryException();
	auto items = ref new Platform::Collections::Vector<RectF, RectFEqual>();
	for(size_t i = 0; i < hints->size(); i++)
	{
		auto hint = hints->at(i);
		RectF rect = Utilities::CreateRectF(hint->left, hint->top, hint->right, hint->bottom);
		items->InsertAt(i, rect);
	}
	return items;
}

Windows::Foundation::Collections::IVector<OutlineItem^>^ Document::GetOutline()
{
	std::lock_guard<std::mutex> lock(m_lock);
	auto items = m_doc->GetOutline();
	auto outlineItems = ref new Platform::Collections::Vector<MuPDFWinRT::OutlineItem^>();
	for(size_t i = 0; i < items->size(); i++)
	{
		auto outlineItem = CreateOutlineItem(items->at(i));
		outlineItems->InsertAt(i, outlineItem);
	}
	return outlineItems;
}

MuPDFWinRT::OutlineItem^ Document::CreateOutlineItem(std::shared_ptr<Outlineitem> item)
{
	auto title = Utilities::ConvertUTF8ToString(item->title.get());
	return ref new MuPDFWinRT::OutlineItem(item->pageNumber, item->level, title);
}

ILinkInfo^ Document::CreateLinkInfo(std::shared_ptr<MuPDFDocLink> link)
{
	ILinkInfo^ linkInfo;
	RectF rect = Utilities::CreateRectF(link->left, link->top, link->right, link->bottom);
	switch (link->type)
	{
	case INTERNAL:
		{
			linkInfo = ref new LinkInfoInternal(rect, link->internalPageNumber);
			break;
		}
	case URI:
		{
			auto uri = Utilities::ConvertUTF8ToString(link->uri.get());
			linkInfo = ref new LinkInfoURI(rect, uri);
			break;
		}
	case REMOTE:
		{
			auto fileSpec = Utilities::ConvertUTF8ToString(link->fileSpec.get());
			linkInfo = ref new LinkInfoRemote(rect, fileSpec, link->remotePageNumber, link->newWindow);
			break;
		}
	};
	return linkInfo;
}

void Document::Init(Windows::Storage::Streams::IBuffer^ buffer, DocumentType documentType, int resolution)
{
	m_buffer = buffer;
	unsigned char *data = GetPointerToData(buffer);
	const char *type = GetMIMEType(documentType);
	Utilities::ThrowIfFailed(MuPDFDoc::Create(data, buffer->Length, type, resolution, &m_doc));
}

unsigned char *Document::GetPointerToData(Windows::Storage::Streams::IBuffer^ buffer)
{
	// Cast to Object^, then to its underlying IInspectable interface.
	Object^ obj = buffer;
	Microsoft::WRL::ComPtr<IInspectable> insp(reinterpret_cast<IInspectable*>(obj));

	// Query the IBufferByteAccess interface.
	Microsoft::WRL::ComPtr<Windows::Storage::Streams::IBufferByteAccess> bufferByteAccess;
	Utilities::ThrowIfFailed(insp.As(&bufferByteAccess));

	// Retrieve the buffer data.
	unsigned char *data = nullptr;
	Utilities::ThrowIfFailed(bufferByteAccess->Buffer(&data));
	return data;
}

const char *Document::GetMIMEType(DocumentType documentType)
{
	switch (documentType)
	{
	case DocumentType::PDF:
		return "application/pdf";
	case DocumentType::XPS:
		return "application/vnd.ms-xpsdocument";
	case MuPDFWinRT::DocumentType::CBZ:
		return "application/x-cbz";
	default:
		return "application/pdf";
	}
}