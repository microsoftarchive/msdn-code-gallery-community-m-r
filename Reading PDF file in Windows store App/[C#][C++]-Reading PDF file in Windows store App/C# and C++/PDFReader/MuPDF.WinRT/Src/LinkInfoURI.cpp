// LinkInfoURI.cpp

#include "LinkInfo.h"

using namespace MuPDFWinRT;

LinkInfoURI::LinkInfoURI(RectF rect, Platform::String^ uri)
{
	m_rect = rect;
	m_uri = uri;
}

void LinkInfoURI::AcceptVisitor(LinkInfoVisitor^ visitor)
{
	visitor->VisitURI(this);
}