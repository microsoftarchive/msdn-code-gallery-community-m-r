// LinkInfoInternal.cpp

#include "LinkInfo.h"

using namespace MuPDFWinRT;

LinkInfoInternal::LinkInfoInternal(RectF rect, int32 pageNumber)
{
	m_rect = rect;
	m_pageNumber = pageNumber;
}

void LinkInfoInternal::AcceptVisitor(LinkInfoVisitor^ visitor)
{
	visitor->VisitInternal(this);
}