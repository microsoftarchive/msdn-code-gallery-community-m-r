#pragma once

#include "RectF.h"

namespace MuPDFWinRT
{
	ref class LinkInfoVisitor;

	public interface class ILinkInfo
	{
		void AcceptVisitor(LinkInfoVisitor^ visitor);
	};

	ref class LinkInfoInternal;
	ref class LinkInfoRemote;
	ref class LinkInfoURI;

	public delegate void InternalLinkEventHandler(LinkInfoVisitor^ sender, LinkInfoInternal^ linkInfo);

	public delegate void RemoteLinkEventHandler(LinkInfoVisitor^ sender, LinkInfoRemote^ linkInfo);

	public delegate void URILinkEventHandler(LinkInfoVisitor^ sender, LinkInfoURI^ linkInfo);

	public ref class LinkInfoVisitor sealed
	{
	internal:
		void VisitInternal(LinkInfoInternal^ linkInfoInternal);
		void VisitRemote(LinkInfoRemote^ linkInfoRemote);
		void VisitURI(LinkInfoURI^ linkInfoURI);
	public:
		event InternalLinkEventHandler^ OnInternalLink;
		event RemoteLinkEventHandler^ OnRemoteLink;
		event URILinkEventHandler^ OnURILink;
	};

	// Under WinRT, inheritance is only supported for Xaml classes, so I have to duplicate some code :(

	public ref class LinkInfoInternal sealed : public ILinkInfo
	{
	private:
		int32 m_pageNumber;
		RectF m_rect;
	internal:
		LinkInfoInternal(RectF rect, int32 pageNumber);
	public:
		virtual void AcceptVisitor(LinkInfoVisitor^ visitor);
		property RectF Rect
		{
			RectF get()
			{
				return m_rect;
			}
		}
		property int32 PageNumber
		{
			int32 get()
			{
				return m_pageNumber;
			}
		}
	};

	public ref class LinkInfoRemote sealed: public ILinkInfo
	{
	private:
		Platform::String^ m_fileSpec;
		int32 m_pageNumber;
		Platform::Boolean m_newWindow;	
		RectF m_rect;
	internal:
		LinkInfoRemote(RectF rect, Platform::String^ fileSpec, int32 pageNumber, Platform::Boolean newWindow);
	public:
		virtual void AcceptVisitor(LinkInfoVisitor^ visitor);
		property RectF Rect
		{
			RectF get()
			{
				return m_rect;
			}
		}
		property Platform::String^ FileSpec
		{
			Platform::String^ get()
			{
				return m_fileSpec;
			}
		}
		property int32 PageNumber
		{
			int32 get()
			{
				return m_pageNumber;
			}
		}
		property Platform::Boolean NewWindow
		{
			Platform::Boolean get()
			{
				return m_newWindow;
			}
		}
	};

	public ref class LinkInfoURI sealed: public ILinkInfo
	{
	private:
		Platform::String^ m_uri;
		RectF m_rect;
	internal:
		LinkInfoURI(RectF rect, Platform::String^ uri);
	public:
		virtual void AcceptVisitor(LinkInfoVisitor^ visitor);
		property RectF Rect
		{
			RectF get()
			{
				return m_rect;
			}
		}
		property Platform::String^ URI
		{
			Platform::String^ get()
			{
				return m_uri;
			}
		}
	};
}