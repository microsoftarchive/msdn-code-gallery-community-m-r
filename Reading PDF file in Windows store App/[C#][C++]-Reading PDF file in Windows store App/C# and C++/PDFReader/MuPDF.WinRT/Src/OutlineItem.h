#pragma once

namespace MuPDFWinRT
{
	public ref class OutlineItem sealed
	{
	private:
		int32 m_level;
		int32 m_pageNumber;
		Platform::String^ m_title;
	public:
		OutlineItem(int32 pageNumber, int32 level, Platform::String^ title);
		property int32 PageNumber
		{
			int32 get()
			{
				return m_pageNumber;
			}
		}
		property Platform::String^ Title
		{
			Platform::String^ get()
			{
				return m_title;
			}
		}
		property int32 Level
		{
			int32 get()
			{
				return m_level;
			}
		}
	};
}