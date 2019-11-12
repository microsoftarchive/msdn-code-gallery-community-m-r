
// HelloWorldDoc.h : interface of the CHelloWorldDoc class
//


#pragma once


class CHelloWorldDoc : public CDocument
{
protected: // create from serialization only
	CHelloWorldDoc();
	DECLARE_DYNCREATE(CHelloWorldDoc)

// Attributes
public:

// Operations
public:

// Overrides
public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);

// Implementation
public:
	virtual ~CHelloWorldDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	DECLARE_MESSAGE_MAP()
};
