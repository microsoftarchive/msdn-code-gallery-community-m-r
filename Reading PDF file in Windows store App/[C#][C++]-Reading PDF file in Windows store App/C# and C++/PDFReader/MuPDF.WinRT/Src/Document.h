#pragma once

#include <memory>
#include <mutex>

#include "OutlineItem.h"
#include "MuPDFDoc.h"
#include "LinkInfo.h"
#include "RectF.h"

namespace MuPDFWinRT
{
	public enum class DocumentType : int { PDF, XPS, CBZ };

	public value struct Point
	{
		int32 X;
		int32 Y;
	};

	public ref class Document sealed
	{    
	private:
		std::mutex m_lock; 
		MuPDFDoc *m_doc;
		Windows::Storage::Streams::IBuffer^ m_buffer;
		Document();
		OutlineItem^ CreateOutlineItem(std::shared_ptr<Outlineitem> item);
		ILinkInfo^ CreateLinkInfo(std::shared_ptr<MuPDFDocLink> link);
		void Init(Windows::Storage::Streams::IBuffer^ buffer, DocumentType documentType, int resolution);
		unsigned char *GetPointerToData(Windows::Storage::Streams::IBuffer^ buffer);
		const char *GetMIMEType(DocumentType documentType);
	public:
		static Document^ Create(Windows::Storage::Streams::IBuffer^ buffer, DocumentType documentType, int32 resolution);
		virtual ~Document();
		Platform::Boolean AuthenticatePassword(Platform::String^ password);
		Point GetPageSize(int32 pageNumber);
		Windows::Foundation::Collections::IVector<ILinkInfo^>^ GetLinks(int32 pageNumber);	
		[Windows::Foundation::Metadata::DefaultOverload]
		void DrawPage(
			int32 pageNumber, 
			Platform::WriteOnlyArray<int>^ pixels, 
			int32 x, 
			int32 y, 
			int32 width, 
			int32 height,
			Platform::Boolean invert);
		void DrawPage(
			int32 pageNumber, 
			Windows::Storage::Streams::IBuffer^ pixels, 
			int32 x, 
			int32 y, 
			int32 width, 
			int32 height,
			Platform::Boolean invert);
		void DrawTwoPagesConcurrent(
			int32 firstPage, 
			Windows::Storage::Streams::IBuffer^ pixels, 
			int32 width, 
			int32 height);
		void DrawFirtPageConcurrent(
			int32 firstPage, 
			Windows::Storage::Streams::IBuffer^ pixels, 
			int32 width, 
			int32 height);
		void DrawFirtPageConcurrent(
			int32 firstPage, 
			Windows::Storage::Streams::IBuffer^ pixels, 
			Windows::Storage::Streams::IBuffer^ bitmap, 
			int32 width, 
			int32 height);
		void DrawSecondPageConcurrent(
			int32 firstPage, 
			Windows::Storage::Streams::IBuffer^ pixels, 
			int32 width, 
			int32 height);
		void DrawSecondPageConcurrent(
			int32 firstPage, 
			Windows::Storage::Streams::IBuffer^ pixels, 
			Windows::Storage::Streams::IBuffer^ bitmap, 
			int32 width, 
			int32 height);
		bool IsCached(int32 pageNumber);
		void CancelDraw();
		Windows::Foundation::Collections::IVector<RectF>^ SearchText(int32 pageNumber, Platform::String^ text);
		Windows::Foundation::Collections::IVector<OutlineItem^>^ GetOutline();
		property int32 PageCount
		{
			int32 get()
			{
				std::lock_guard<std::mutex> lock(m_lock);
				return m_doc->GetPageCount();
			}
		}
		property Platform::Boolean NeedsPassword
		{
			Platform::Boolean get()
			{
				std::lock_guard<std::mutex> lock(m_lock);
				return m_doc->NeedsPassword();
			}
		}
		property Platform::Boolean JavaSciptSupported
		{
			Platform::Boolean get()
			{
				std::lock_guard<std::mutex> lock(m_lock);
				return m_doc->JavaScriptSupported();
			}
		}
		property Platform::Boolean HasOutline
		{
			Platform::Boolean get()
			{
				std::lock_guard<std::mutex> lock(m_lock);
				return m_doc->HasOutline();
			}
		}
	};
}