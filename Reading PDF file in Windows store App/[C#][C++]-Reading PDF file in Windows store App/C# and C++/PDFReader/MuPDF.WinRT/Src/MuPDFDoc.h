#pragma once

#include <memory>
#include <functional>
#include <vector>

#include <windows.h>
#include <Winerror.h>

extern "C" {
	#include "fitz.h"
	#include "mupdf.h"
}


#define NUM_CACHE (15)
#define MAX_SEARCH_HITS (500)

//TODO: Maybe I should change this to class/struct with default constructor
typedef struct
{
	int number;
	int width;
	int height;
	fz_rect mediaBox;
	fz_page *page;
	fz_page *hqPage;
	fz_display_list *pageList;
	fz_display_list *annotList;
} PageCache;

typedef struct
{
	int level;
	//UTF-8
	std::unique_ptr<char[]> title;
	int pageNumber;
} Outlineitem;

template<class T>
struct Rect
{
    Rect() : left(), top(), right(), bottom() {}
    Rect(T left, T top, T right, T bottom) : 
        left(left), top(top), right(right), bottom(bottom) {}

    T left;
    T top;
    T right;
    T bottom;
};

typedef Rect<float> RectFloat;

/* Possible values of link type */
enum MuPDFDocLinkType { INTERNAL, URI, REMOTE };

typedef struct
{
	MuPDFDocLinkType type;
	float left; 
	float top;
	float right; 
	float bottom;
	// URI
	std::unique_ptr<char[]> uri;
	// INTERNAL
	int internalPageNumber;
	// REMOTE
	std::unique_ptr<char[]> fileSpec;
	int remotePageNumber;
	bool newWindow;	
} MuPDFDocLink;



struct Data {
	// A pointer to the original context in the main thread sent
	// from main to rendering thread. It will be used to create
	// each rendering thread's context clone.
	fz_context *ctx;


	// Page number sent from main to rendering thread for printing
	int pagenumber;

	//Cache index
	int cacheNumber;

	// The display list as obtained by the main thread and sent
	// from main to rendering thread. This contains the drawing
	// commands (text, images, etc.) for the page that should be
	// rendered.
	fz_display_list *list;

	fz_display_list *annotList;


	// The area of the page to render as obtained by the main
	// thread and sent from main to rendering thread.
	fz_bbox bbox;

	fz_rect rect;

	// This is the result, a pixmap containing the rendered page.
	// It is passed first from main thread to the rendering
	// thread, then its samples are changed by the rendering
	// thread, and then back from the rendering thread to the main
	// thread.
	fz_pixmap *pix;

	int width;
	int height;
};


class MuPDFDoc
{
private:
	CRITICAL_SECTION m_critSec;

	fz_locks_context locks;
	fz_context *m_context;
	fz_document *m_document;
	fz_outline *m_outline;
	fz_cookie *m_cts;
	int m_currentPage;
	int m_resolution;
	int about;
	PageCache m_pages[NUM_CACHE];
	MuPDFDoc(int resolution);
	//MuPDFDoc(const MuPDFDoc& that) = delete;
	HRESULT Init(unsigned char *buffer, int bufferLen, const char *mimeType);
	HRESULT InitContext();
	HRESULT InitDocument(unsigned char *buffer, int bufferLen, const char *mimeType);
	fz_stream *OpenStream(unsigned char *buffer, int bufferLen);
	void ClearPageCache(PageCache *pageCache);
	void ClearPages();
	int FindPageInCache(int pageNumber);
	int GetPageCacheIndex(int pageNumber);
	fz_matrix CalcConvertMatrix();
	int FillOutline(std::shared_ptr<std::vector<std::shared_ptr<Outlineitem>>> items, int position, fz_outline *outline, int level);
public:
	static HRESULT Create(unsigned char *buffer, int bufferLen, const char *mimeType, int resolution, MuPDFDoc **obj);
	~MuPDFDoc(void);
	HRESULT GotoPage(int pageNumber);
	HRESULT DrawPage(unsigned char *bitmap, int x, int y, int width, int height, bool invert);
	HRESULT UpdatePage(int pageNumber, unsigned char *bitmap, int x, int y, int width, int height, bool invert);
	bool AuthenticatePassword(char *password);
	inline int GetPageCount() { return fz_count_pages(m_document); }
	inline bool JavaScriptSupported() { return fz_javascript_supported() != 0; }
	inline bool NeedsPassword() { return fz_needs_password(m_document) != 0; }
	inline bool HasOutline() { return m_outline != nullptr; }
	bool IsCached(int pageNumber);
	void CancelDraw();
	bool IsCanceled();
	int GetPageWidth();
	int GetPageHeight();
	std::shared_ptr<std::vector<std::shared_ptr<MuPDFDocLink>>> GetLinks();
	std::shared_ptr<std::vector<std::shared_ptr<RectFloat>>> SearchText(const char* text);
	std::shared_ptr<std::vector<std::shared_ptr<Outlineitem>>> GetOutline();

	HRESULT LoadPage(unsigned char *bitmap, int pageNum, int width, int height, Data **data);
	HRESULT LoadTwoPages(unsigned char *bitmap1, int pageNum1, unsigned char *bitmap2, int pageNum2, int width, int height, Data **data1, Data **data2);
	HRESULT Renderer(Data *data, int part, int numParts);
};