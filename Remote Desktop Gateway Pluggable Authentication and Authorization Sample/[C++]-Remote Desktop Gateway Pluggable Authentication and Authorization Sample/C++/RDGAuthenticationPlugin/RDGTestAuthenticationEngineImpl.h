//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

// ITSGAuthenticationEngine is from TSGAuthenticationEngine.idl
//  Exposes methods that authenticate users for Remote Desktop Gateway (RD Gateway).
//  Implement this interface when you want to override the default authentication process in RD Gateway.
//
//  Note: Windows SDKs must be installed inorder to include TSGAuthenticationEngine.h

#ifndef AFX_AUTHENTICATIONENGINE_H__08f03e5f_03cb_44a1_8fcf_3db1b599f37e__INCLUDED_
#define AFX_AUTHENTICATIONENGINE_H__08f03e5f_03cb_44a1_8fcf_3db1b599f37e__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "TSGAuthenticationEngine.h"

class CRDGTestAuthenticationEngineImpl : public ITSGAuthenticationEngine
{
public:
    CRDGTestAuthenticationEngineImpl();
    virtual ~CRDGTestAuthenticationEngineImpl();

    // IUnknown
    STDMETHOD_(ULONG, AddRef)();
    STDMETHOD_(ULONG, Release)();
    STDMETHOD(QueryInterface)(
        REFIID riid,
        void** ppv
        );

    // RDG calls this method on the authentication engine to authenticate the user
    STDMETHOD(AuthenticateUser)(
        __in GUID mainSessionId,                // A unique identifier to distinguish the connection
        __in BYTE *cookieData,                  // UAG cookie
        __in ULONG numCookieBytes,              // Number of cookie byte
        __in ULONG_PTR context,                 // Some context to call back on
        __in ITSGAuthenticateUserSink *pSink    // Sink object
        );

    // RDG calls this method to cancel an ongoing authentication
    STDMETHOD(CancelAuthentication)(
        __in GUID   mainSessionId,  // A unique identifier to distinguish the connection
        __in ULONG_PTR context      // Some context to call back on
        );

    DWORD static DisconnectUserThread(LPVOID lpParameter);
    DWORD static ReauthenticateUserThread(LPVOID lpParameter);
    DWORD static SleepThread(LPVOID lpParameter);

protected:
    LONG volatile m_uRefCount;
    HANDLE m_hThread;
};

class  __declspec(uuid("{f1400855-aec4-4696-b17c-6af75cf3f402}")) CRDGTestAuthenticationEngineImpl;

#endif // !defined(AFX_AUTHENTICATIONENGINE_H__08f03e5f_03cb_44a1_8fcf_3db1b599f37e__INCLUDED_)
