// PubSubTestDlg.h : 헤더 파일
//

#pragma once

#include "PubSubManager.h"

class CPubSubTestDlg : public CDialog
{
public:
	CPubSubTestDlg(CWnd* pParent = NULL);	// 표준 생성자입니다.

	enum { IDD = IDD_PUBSUBTEST_DIALOG };

	static void DebugLog(const char *fmt);

protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 지원입니다.

protected:
	HICON m_hIcon;

	// 생성된 메시지 맵 함수
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()

	PubSubManager PubSubMan_;

	static CPubSubTestDlg *instance;

public:
	DWORD ipaddr_;
	int port_;
	CString channel_;
	CString message_;

	afx_msg void OnBnClickedBtnConnect();
	afx_msg void OnBnClickedBtnSubscribe();
	afx_msg void OnBnClickedBtnUnsubscribe();
	afx_msg void OnBnClickedBtnPublish();
	afx_msg void OnTimer(UINT_PTR nIDEvent);
};
