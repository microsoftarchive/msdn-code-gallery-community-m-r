
// HelloWorldView.cpp : implementation of the CHelloWorldView class
//

#include "stdafx.h"
#include "HelloWorld.h"

#include "HelloWorldDoc.h"
#include "HelloWorldView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CHelloWorldView

IMPLEMENT_DYNCREATE(CHelloWorldView, CView)

BEGIN_MESSAGE_MAP(CHelloWorldView, CView)
	ON_REGISTERED_MESSAGE(AFX_WM_DRAW2D, &CHelloWorldView::OnDraw2d)
END_MESSAGE_MAP()

// CHelloWorldView construction/destruction

using namespace D2D1;

CHelloWorldView::CHelloWorldView()
{
	// Initialize Direct2D
	EnableD2DSupport();

	// Create D2D graphic resources
	m_pBlueBrush = new CD2DSolidColorBrush(GetRenderTarget(), ColorF(ColorF::RoyalBlue));

	m_pTextFormat = new CD2DTextFormat(GetRenderTarget(), _T("Gabriola"), 50);
	m_pTextFormat->Get()->SetTextAlignment(DWRITE_TEXT_ALIGNMENT_CENTER);
	m_pTextFormat->Get()->SetParagraphAlignment(DWRITE_PARAGRAPH_ALIGNMENT_CENTER);
}

CHelloWorldView::~CHelloWorldView()
{
}

BOOL CHelloWorldView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

// CHelloWorldView drawing

void CHelloWorldView::OnDraw(CDC* /*pDC*/)
{
	CHelloWorldDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	// TODO: add draw code for native data here
}

// CHelloWorldView diagnostics

#ifdef _DEBUG
void CHelloWorldView::AssertValid() const
{
	CView::AssertValid();
}

void CHelloWorldView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CHelloWorldDoc* CHelloWorldView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CHelloWorldDoc)));
	return (CHelloWorldDoc*)m_pDocument;
}
#endif //_DEBUG


// CHelloWorldView message handlers


// AFX_WM_DRAW2D event handler
afx_msg LRESULT CHelloWorldView::OnDraw2d(WPARAM wParam, LPARAM lParam)
{
	CHwndRenderTarget* pRenderTarget = (CHwndRenderTarget*)lParam;
	ASSERT_VALID(pRenderTarget);

	// Clear window background
	pRenderTarget->Clear(ColorF(ColorF::Beige));

	// Draw text
	CRect rect;
	GetClientRect(rect);
	pRenderTarget->DrawText(_T("Hello, World!"), rect, m_pBlueBrush, m_pTextFormat);
	return TRUE;
}
