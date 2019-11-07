#include "OutlineItem.h"

using namespace MuPDFWinRT;

OutlineItem::OutlineItem(int32 pageNumber, int32 level, Platform::String^ title)
	: m_pageNumber(pageNumber), m_level(level), m_title(title)
{
}
