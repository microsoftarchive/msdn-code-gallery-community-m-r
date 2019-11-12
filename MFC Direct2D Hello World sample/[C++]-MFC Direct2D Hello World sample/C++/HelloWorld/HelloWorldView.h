
// HelloWorldView.h : interface of the CHelloWorldView class
//

#pragma once


class CHelloWorldView : public CView
{
protected: // create from serialization only
	CHelloWorldView();
	DECLARE_DYNCREATE(CHelloWorldView)

// Attributes
public:
	CHelloWorldDoc* GetDocument() const;

// Operations
public:

// Overrides
public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:

// Implementation
public:
	virtual ~CHelloWorldView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:
	CD2DTextFormat*    m_pTextFormat;
	CD2DSolidColorBrush* m_pBlueBrush;

// Generated message map functions
protected:
	DECLARE_MESSAGE_MAP()
	afx_msg LRESULT OnDraw2d(WPARAM wParam, LPARAM lParam);
};

#ifndef _DEBUG  // debug version in HelloWorldView.cpp
inline CHelloWorldDoc* CHelloWorldView::GetDocument() const
   { return reinterpret_cast<CHelloWorldDoc*>(m_pDocument); }
#endif

