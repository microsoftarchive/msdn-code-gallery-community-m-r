//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "stdafx.h"
#include "RDGTestAuthenticationEngineImpl.h"

#define ReAuthenticateUserValue     L"0"
#define DisconnectUserValue         L"1"
#define CancelAuthenticationValue   L"2"
#define NoTimeout                   L"0"

WCHAR* m_userName = NULL;
WCHAR* m_userDomain = NULL;
ULONG_PTR m_context = NULL;
int m_timeout = 0;
HANDLE g_CancelAuthenticationCalledEvent = NULL;

// Default constructor CRDGTestAuthenticationEngineImpl
CRDGTestAuthenticationEngineImpl::CRDGTestAuthenticationEngineImpl()
{
    m_uRefCount = 0;
    m_hThread = NULL;
    InterlockedIncrement(&g_uDllLockCount);
}

// Default destructor CRDGTestAuthenticationEngineImpl
CRDGTestAuthenticationEngineImpl::~CRDGTestAuthenticationEngineImpl()
{
    InterlockedDecrement(&g_uDllLockCount);
    if (m_hThread)
    {
        CloseHandle(m_hThread);
    }
}

// Implementation of IUnknown methods
STDMETHODIMP_(ULONG) CRDGTestAuthenticationEngineImpl::AddRef()
{
    InterlockedIncrement(&m_uRefCount);
    return m_uRefCount;
}

STDMETHODIMP_(ULONG) CRDGTestAuthenticationEngineImpl::Release()
{
    ULONG uRet = InterlockedDecrement(&m_uRefCount);
    if (0 == m_uRefCount)
    {
        delete this;
    }

    return uRet;
}

STDMETHODIMP CRDGTestAuthenticationEngineImpl::QueryInterface(
    REFIID riid,
    void** ppv
    )
{
    HRESULT hr = S_OK;

    // Check that ppv really points to a void*.
    if (IsBadWritePtr (ppv, sizeof(void*)))
    {
        return E_POINTER;
    }

    // Standard QI initialization - set *ppv to NULL.
    *ppv = NULL;

    // If the client is requesting an interface we support, set *ppv.
    if (InlineIsEqualGUID(riid, IID_IUnknown))
    {
        *ppv = (ITSGAuthenticationEngine*)this;
    }
    else if (InlineIsEqualGUID(riid, __uuidof(ITSGAuthenticationEngine)))
    {
        *ppv = (ITSGAuthenticationEngine*)this;
    }
    else
    {
        hr = E_NOINTERFACE;
    }

    // If we're returning an interface pointer, AddRef() it.
    if (SUCCEEDED(hr))
    {
        ((IUnknown*) *ppv)->AddRef();
    }

    return hr;
}

// Remote Desktop Gateway (RD Gateway) calls this method when it receives a new connection request.
//  The authentication plug-in should authenticate the user based on the cookie referenced by the cookieData parameter.
//  The authentication plug-in should then use the ITSGAuthenticateUserSink interface to notify RD Gateway about the result of authentication.
STDMETHODIMP CRDGTestAuthenticationEngineImpl::AuthenticateUser(
    __in GUID mainSessionId,
    __in LPBYTE cookieData,
    __in ULONG numCookieBytes,
    __in ULONG_PTR context,
    __in ITSGAuthenticateUserSink *pSink
    )
{
    HRESULT hr = S_OK;
    BSTR expectedCookie = NULL;
    BSTR timeout = NULL;
    BSTR timeoutAction = NULL;

    // Get the authentication policy from XML file.  As this is sample plug-in, creating authentication policy temporarily in xml and use the same for authentication.
    // In the real time scenario, we should use the actual authentication system.
    GetAuthenticationParams(&expectedCookie, &timeout, &timeoutAction);

    m_context = context;
    m_timeout = _wtoi(timeout);

    //timeoutAction = 2 means that CancelAuthentication would be called. Hence put a sleep to the current thread
    if (!_wcsicmp(timeoutAction, CancelAuthenticationValue))
    {
        HANDLE m_SleepThreadHnd = CreateThread(NULL,
                                               0,
                                               (LPTHREAD_START_ROUTINE) this->SleepThread,
                                               NULL,
                                               0,
                                               NULL);
        return S_OK;
    }
    else
    {
        // Check whether the expected cookie recieved or not
        if (_wcsicmp((const WCHAR*)cookieData, (const WCHAR*)expectedCookie))
        {
            hr = E_ACCESSDENIED;

            // Notifies Remote Desktop Gateway (RD Gateway) that the authentication plug-in failed authentication.
            pSink->OnUserAuthenticationFailed(m_context, hr, hr);
        }
        else
        {
            // Got the expected cookie.  In the real scenario, need to get Username and Domain.
            //   As this is sample authentication plugin, use the cookie to carry this information.
            //   Let us parse the cookie to get Username and Domain.
            WCHAR* ptr = wcschr(expectedCookie, L'\\');
            if(ptr != NULL)
            {
                m_userName = ptr + 1;
                *ptr = L'\0';
                m_userDomain = (WCHAR *)expectedCookie;
             }

            // Store the mainSessionID.  Use this info to validate in authorization.  Make sure same session user
            //  requested authentication and authorization.
            StoreMainSessionId(mainSessionId);

            // Notifies Remote Desktop Gateway (RD Gateway) that the authentication plug-in has successfully authenticated the user.
            pSink->OnUserAuthenticated(m_userName,
                                       m_userDomain,
                                       m_context,
                                       NULL);
        }

        if (_wcsicmp(timeout, NoTimeout))
        {
            // timeoutAction = 0 means ReauthenticateUser after "timeout" period
            pSink->AddRef();
            if(!_wcsicmp(timeoutAction, ReAuthenticateUserValue))
            {
                m_hThread = CreateThread(NULL,
                                       0,
                                       (LPTHREAD_START_ROUTINE) this->ReauthenticateUserThread,
                                       (LPVOID) pSink,
                                       0,
                                       NULL);
            }
            else if (!_wcsicmp(timeoutAction, DisconnectUserValue)) // timeoutAction = 1 means DisconnectUser after "timeout" period
            {
                m_hThread = CreateThread(NULL,
                                       0,
                                       (LPTHREAD_START_ROUTINE) this->DisconnectUserThread,
                                       (LPVOID) pSink,
                                       0,
                                       NULL);
            }
        }
    }

    if(NULL != expectedCookie)
    {
        ::SysFreeString(expectedCookie);
        expectedCookie = NULL;
    }

    if(NULL != timeout)
    {
        ::SysFreeString(timeout);
        timeout = NULL;
    }

    if(NULL != timeoutAction)
    {
        ::SysFreeString(timeoutAction);
        timeoutAction = NULL;
    }

    return S_OK;
}

// Notifies Remote Desktop Gateway (RD Gateway) that it should disconnect the client.
DWORD CRDGTestAuthenticationEngineImpl::DisconnectUserThread(
    LPVOID lpParameter
    )
{
    ITSGAuthenticateUserSink *pSink = (ITSGAuthenticateUserSink *) lpParameter;

    Sleep(m_timeout * 60 * 1000);
    if (pSink != NULL && m_context != NULL)
    {
        pSink->DisconnectUser(m_context);
        pSink->Release();
    }
    return S_OK;
}

// Notifies Remote Desktop Gateway (RD Gateway) that it should silently reauthenticate and reauthorize the user.
DWORD CRDGTestAuthenticationEngineImpl::ReauthenticateUserThread(
    LPVOID lpParameter
    )
{
    ITSGAuthenticateUserSink *pSink = (ITSGAuthenticateUserSink *) lpParameter;

    Sleep(m_timeout * 60 * 1000);
    if (pSink != NULL && m_context != NULL)
    {
        pSink->ReauthenticateUser(m_context);
        pSink->Release();
    }
    return S_OK;
}

// Wait for cancel authentication if we create an event in CancelAuthentication.
DWORD CRDGTestAuthenticationEngineImpl::SleepThread(
    LPVOID lpParameter
    )
{
    DWORD dwRet = 0;

    dwRet = WaitForSingleObject(g_CancelAuthenticationCalledEvent, 600000);
    if (NULL != g_CancelAuthenticationCalledEvent)
    {
       CloseHandle(g_CancelAuthenticationCalledEvent);
    }

    return S_OK;
}

// Remote Desktop Gateway (RD Gateway) calls this method when the user who initiated the connection terminates the connection,
//  or when the connection fails.
//  This method is not implemented in this sample code.
STDMETHODIMP CRDGTestAuthenticationEngineImpl::CancelAuthentication(
    __in GUID mainSessionId, 
    __in ULONG_PTR context
    )
{
    return E_NOTIMPL;
}
