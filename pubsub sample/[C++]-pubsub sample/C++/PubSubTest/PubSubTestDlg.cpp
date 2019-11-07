
// PubSubTestDlg.cpp : 구현 파일
//

#include "stdafx.h"
#include "PubSubTest.h"
#include "PubSubTestDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CPubSubTestDlg 대화 상자

CPubSubTestDlg *CPubSubTestDlg::instance = NULL;


CPubSubTestDlg::CPubSubTestDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CPubSubTestDlg::IDD, pParent)
	, ipaddr_(0)
	, port_(0)
	, channel_(_T(""))
	, message_(_T(""))
{
	instance = this;

	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);

	WSAData wsaData;
	WSAStartup(MAKEWORD(1, 1), &wsaData);
}

void CPubSubTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_IPAddress(pDX, IDC_IPADDRESS, ipaddr_);
	DDX_Text(pDX, IDC_EDIT_PORT, port_);
	DDX_Text(pDX, IDC_EDIT_KEY, channel_);
	DDX_Text(pDX, IDC_EDIT_MESSAGE, message_);
}

BEGIN_MESSAGE_MAP(CPubSubTestDlg, CDialog)
	ON_WM_PAINT()
	ON_WM_TIMER()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_BTN_CONNECT, &CPubSubTestDlg::OnBnClickedBtnConnect)
	ON_BN_CLICKED(IDC_BTN_SUBSCRIBE, &CPubSubTestDlg::OnBnClickedBtnSubscribe)
	ON_BN_CLICKED(IDC_BTN_UNSUBSCRIBE, &CPubSubTestDlg::OnBnClickedBtnUnsubscribe)
	ON_BN_CLICKED(IDC_BTN_PUBLISH, &CPubSubTestDlg::OnBnClickedBtnPublish)
END_MESSAGE_MAP()


// CPubSubTestDlg 메시지 처리기

BOOL CPubSubTestDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// 이 대화 상자의 아이콘을 설정합니다. 응용 프로그램의 주 창이 대화 상자가 아닐 경우에는
	//  프레임워크가 이 작업을 자동으로 수행합니다.
	SetIcon(m_hIcon, TRUE);			// 큰 아이콘을 설정합니다.
	SetIcon(m_hIcon, FALSE);		// 작은 아이콘을 설정합니다.

	SetTimer(2699, 100, NULL);

	CIPAddressCtrl *ipctrl = (CIPAddressCtrl *)GetDlgItem(IDC_IPADDRESS);
	ipctrl->SetAddress(192, 168, 25, 200);

	SetDlgItemText(IDC_EDIT_PORT, "6379");

	return TRUE;  // 포커스를 컨트롤에 설정하지 않으면 TRUE를 반환합니다.
}

// 대화 상자에 최소화 단추를 추가할 경우 아이콘을 그리려면
//  아래 코드가 필요합니다. 문서/뷰 모델을 사용하는 MFC 응용 프로그램의 경우에는
//  프레임워크에서 이 작업을 자동으로 수행합니다.

void CPubSubTestDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 그리기를 위한 디바이스 컨텍스트

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// 클라이언트 사각형에서 아이콘을 가운데에 맞춥니다.
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// 아이콘을 그립니다.
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// 사용자가 최소화된 창을 끄는 동안에 커서가 표시되도록 시스템에서
//  이 함수를 호출합니다.
HCURSOR CPubSubTestDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


void CPubSubTestDlg::OnBnClickedBtnConnect()
{
	UpdateData(TRUE);

	CString ipaddr;
	ipaddr.Format("%d.%d.%d.%d", (ipaddr_ >> 24) & 0xff, (ipaddr_ >> 16) & 0xff, (ipaddr_ >> 8) & 0xff, ipaddr_ & 0xff);

	PubSubMan_.Connect(ipaddr, port_);
}

void CPubSubTestDlg::OnBnClickedBtnSubscribe()
{
	UpdateData(TRUE);

	PubSubMan_.Subscribe(channel_);
}

void CPubSubTestDlg::OnBnClickedBtnUnsubscribe()
{
	UpdateData(TRUE);

	PubSubMan_.Unsubscribe(channel_);
}

void CPubSubTestDlg::OnBnClickedBtnPublish()
{
	UpdateData(TRUE);

	PubSubMan_.Publish(channel_, message_);
}

void CPubSubTestDlg::OnTimer( UINT_PTR nIDEvent )
{
	PubSubMan_.OnTimer();
}

void CPubSubTestDlg::DebugLog(const char *log)
{
	CEdit *edit = (CEdit *)instance->GetDlgItem(IDC_EDIT_STATUS);

	edit->SetSel(0x7fffffff, 0x7fffffff);
	edit->ReplaceSel(log);
	edit->LineScroll(0xffffffff);
}

void DEBUGLOG(const char *fmt, ...)
{
	char szTempBuf[4096] ;
	va_list vlMarker ;

	va_start(vlMarker,fmt) ;
	_vstprintf(szTempBuf,fmt,vlMarker) ;
	va_end(vlMarker) ;

	strcat_s(szTempBuf, "\r\n");
	CPubSubTestDlg::DebugLog(szTempBuf);
}