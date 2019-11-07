// LinkInfoVisitor.cpp

#include "LinkInfo.h"

using namespace MuPDFWinRT;

void LinkInfoVisitor::VisitInternal(LinkInfoInternal^ linkInfoInternal)
{
	OnInternalLink(this, linkInfoInternal);
}

void LinkInfoVisitor::VisitRemote(LinkInfoRemote^ linkInfoRemote)
{
	OnRemoteLink(this, linkInfoRemote);
}

void LinkInfoVisitor::VisitURI(LinkInfoURI^ linkInfoURI)
{
	OnURILink(this, linkInfoURI);
}