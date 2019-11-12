// LinkInfoRemote.cpp

#include "LinkInfo.h"

using namespace MuPDFWinRT;

LinkInfoRemote::LinkInfoRemote(RectF rect, Platform::String^ fileSpec, int32 pageNumber, Platform::Boolean newWindow)
{
	m_rect = rect;
	m_fileSpec = fileSpec;
	m_pageNumber = pageNumber;
	m_newWindow = newWindow;
}

void LinkInfoRemote::AcceptVisitor(LinkInfoVisitor^ visitor)
{
	visitor->VisitRemote(this);
}