//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "stdafx.h"
#include "RDGTestAuthorizationEngineImpl.h"

CRDGTestPolicyEngineImpl::CRDGTestPolicyEngineImpl()
{
    m_uRefCount = 0;
    InterlockedIncrement(&g_uDllLockCount);
}

CRDGTestPolicyEngineImpl::~CRDGTestPolicyEngineImpl()
{
    InterlockedDecrement(&g_uDllLockCount);
}

STDMETHODIMP_(ULONG) CRDGTestPolicyEngineImpl::AddRef()
{
    InterlockedIncrement(&m_uRefCount);
    return m_uRefCount;
}

STDMETHODIMP_(ULONG) CRDGTestPolicyEngineImpl::Release()
{
    InterlockedDecrement(&m_uRefCount);
    ULONG uRet = m_uRefCount;

    if (0 == m_uRefCount)
    {
        delete this;
    }

    return uRet;
}

STDMETHODIMP CRDGTestPolicyEngineImpl::QueryInterface(
    REFIID riid,
    void** ppv
    )
{
    HRESULT hr = S_OK;

    // Check that ppv really points to a void*.
    if (IsBadWritePtr(ppv, sizeof(void*)))
    {
        return E_POINTER;
    }

    // Standard QI initialization - set *ppv to NULL.
    *ppv = NULL;

    // If the client is requesting an interface we support, set *ppv.
    if (InlineIsEqualGUID(riid, IID_IUnknown))
    {
        *ppv = (ITSGPolicyEngine*)this;
    }
    else if (InlineIsEqualGUID(riid, __uuidof(ITSGPolicyEngine)))
    {
        *ppv = (ITSGPolicyEngine*)this;
    }
    else if (InlineIsEqualGUID(riid, __uuidof(ITSGAccountingEngine)))
    {
        *ppv = (ITSGAccountingEngine*)this;
    }
    else
    {
        hr = E_NOINTERFACE;
    }

    // If we're returning an interface pointer, AddRef() it.
    if (S_OK == hr)
    {
        ((IUnknown*) *ppv)->AddRef();
    }

    return hr;
}

// Determines whether the specified connection is authorized to connect to Remote Desktop Gateway (RD Gateway).
// RD Gateway calls this method after a user has been successfully authenticated. The authorization plug-in should then 
//  use the ITSGAuthorizeConnectionSink interface to notify RD Gateway about the result of authorization.
STDMETHODIMP CRDGTestPolicyEngineImpl::AuthorizeConnection(
    __in GUID mainSessionId,
    __in BSTR username,
    __in AAAuthSchemes authType,
    __in BSTR clientMachineIP,
    __in BSTR clientMachineName,
    __in BYTE* sohData,
    __in ULONG numSOHBytes,
    __in BYTE* cookieData,
    __in ULONG numCookieBytes,
    __in HANDLE_PTR userToken,
    __in ITSGAuthorizeConnectionSink* pSink
    )
{
    DWORD dwRet = 0;
    HRESULT hr = S_OK;
    AATrustClassID trustClass = AA_TRUSTEDUSER_TRUSTEDCLIENT;
    BSTR expectedUserName = NULL;
    BSTR expectedClientMachine = NULL;
    BSTR expectedSmartcardAllowed = NULL;
    BSTR expectedPasswordAllowed = NULL;
    BSTR expectedCookie = NULL;
    BSTR timeout = NULL;
    BSTR timeoutAction = NULL;

    if( (NULL == username) || (NULL == clientMachineName))
    {
        hr = E_INVALIDARG;
        goto FINISHED_COMPARISON;
    }

    //Test that mainSessionId is same as that in the Authentication Plugin in case custom Authentication Plugin is in use
    if (!IsAuthenticationEngineNative())
    {
        if (!CheckForMainSessionId(mainSessionId))
        {
            trustClass = AA_UNTRUSTED;
        }
    }

    // Get the authorization policy from xml
    GetConnectionPolicyParams(&expectedUserName, &expectedClientMachine, &expectedSmartcardAllowed, &expectedPasswordAllowed);

    // Compare username with xml policy data
    if (_wcsicmp(username, expectedUserName))
    {
        trustClass = AA_UNTRUSTED;
        goto FINISHED_COMPARISON;
    }

    // Compare clientMachineName with xml policy data
    if (_wcsicmp(clientMachineName, expectedClientMachine))
    {
        trustClass = AA_TRUSTEDUSER_UNTRUSTEDCLIENT;
        goto FINISHED_COMPARISON;
    }

    // Compare authType with xml policy data
    if (authType == AA_AUTH_NTLM)
    {
        if(_wcsicmp(expectedPasswordAllowed, L"true"))
        {
            trustClass = AA_UNTRUSTED;
            goto FINISHED_COMPARISON;
        }
    }
    if (authType == AA_AUTH_SC)
    {
        if(_wcsicmp(expectedSmartcardAllowed,L"true"))
        {
            trustClass = AA_UNTRUSTED;
            goto FINISHED_COMPARISON;
        }
    }

FINISHED_COMPARISON:
    ULONG   idleTimeout = 0;
    ULONG   sessionTimeout = 0;
    SESSION_TIMEOUT_ACTION_TYPE sessionTimeoutAction = SESSION_TIMEOUT_ACTION_DISCONNECT;
    PolicyAttributes policyAttributes = {0};

    policyAttributes[DriveRedirectionDisabled] = TRUE;
    policyAttributes[ClipboardRedirectionDisabled] = TRUE;

    GetAuthenticationParams(&expectedCookie, &timeout, &timeoutAction);
    if (_wcsicmp((const WCHAR*)cookieData,(const WCHAR*)expectedCookie))
    {
        trustClass = AA_UNTRUSTED;
    }

    // Done with cookie data verification.
    pSink->OnConnectionAuthorized(hr,
                                  mainSessionId,
                                  0,
                                  NULL,
                                  idleTimeout,
                                  sessionTimeout,
                                  sessionTimeoutAction,
                                  trustClass,
                                  policyAttributes);

    if(NULL != expectedUserName)
    {
        ::SysFreeString(expectedUserName);
        expectedUserName = NULL;
    }

    if(NULL != expectedClientMachine)
    {
        ::SysFreeString(expectedClientMachine);
        expectedClientMachine = NULL;
    }

    if(NULL != expectedSmartcardAllowed)
    {
        ::SysFreeString(expectedSmartcardAllowed);
        expectedSmartcardAllowed = NULL;
    }

    if(NULL != expectedPasswordAllowed)
    {
        ::SysFreeString(expectedPasswordAllowed);
        expectedPasswordAllowed = NULL;
    }

    if (expectedCookie != NULL)
    {
        ::SysFreeString(expectedCookie);
        expectedCookie = NULL;
    }

    if (NULL != timeout)
    {
        ::SysFreeString(timeout);
        timeout = NULL;
    }

    if (NULL != timeoutAction)
    {
        ::SysFreeString(timeoutAction);
        timeoutAction = NULL;
    }

    return S_OK;
}

// Determines which resources the specified connection is authorized to connect to.
// Remote Desktop Gateway (RD Gateway) calls this method after a user has been successfully authenticated.
// The authorization plug-in should then use the ITSGAuthorizeConnectionSink interface to notify RD Gateway about the result of authorization.
STDMETHODIMP CRDGTestPolicyEngineImpl::AuthorizeResource(
    __in GUID mainSessionId,
    __in int subSessionId,
    __in BSTR username,
    __in BSTR* resourceNames,
    __in ULONG numResources,
    __in BSTR* alternateResourceNames,
    __in ULONG numAlternateResourceName,
    __in ULONG portNumber,
    __in BSTR operation,
    __in BYTE* cookie,
    __in ULONG numBytesInCookie,
    __in ITSGAuthorizeResourceSink* pSink
    )
{
    DWORD dwRet = 0;
    HRESULT hr = S_OK;
    BSTR expectedUserName = NULL;
    BSTR expectedResourceMachine = NULL;
    BSTR expectedPortNumber = NULL;
    BSTR expectedCookie = NULL;
    BSTR timeout = NULL;
    BSTR timeoutAction = NULL;

    // Get the authorization policy from xml.
    GetResourcePolicyParams(&expectedUserName, &expectedResourceMachine, &expectedPortNumber);
    ULONG expectedPortNumberInUlong = _wtoi(expectedPortNumber);

    // Test that mainSessionId is same as that in the Authentication Plugin in case custom Authentication Plugin is in use
    if (!IsAuthenticationEngineNative())
    {
        if (!CheckForMainSessionId(mainSessionId))
        {
            hr = E_FAIL;
        }
    }

    // Compare username with authorization policy in xml
    if (_wcsicmp(username, expectedUserName))
    {
        hr = E_PROXY_RAP_ACCESSDENIED;
    }

    // Compare resourceName with authorization policy in xml
    if(_wcsicmp(resourceNames[0], expectedResourceMachine))
    {
        hr = E_PROXY_RAP_ACCESSDENIED;
    }

    // Compare portNumber with authorization policy in xml
    if (portNumber != expectedPortNumberInUlong)
    {
        hr = E_PROXY_RAP_ACCESSDENIED;
    }

    // Check for cookie data
    GetAuthenticationParams(&expectedCookie, &timeout, &timeoutAction);
    if (_wcsicmp((const WCHAR*)cookie, (const WCHAR*)expectedCookie))
    {
        hr = E_PROXY_RAP_ACCESSDENIED;
    }

    if (FAILED(hr))
    {
        pSink->OnChannelAuthorized(hr,
                                   mainSessionId,
                                   subSessionId,
                                   NULL,
                                   0,
                                   resourceNames,
                                   numResources);
    }
    else
    {
        pSink->OnChannelAuthorized(hr,
                                   mainSessionId,
                                   subSessionId,
                                   resourceNames,
                                   numResources,
                                   NULL,
                                   0);
    }

    if (expectedUserName != NULL)
    {
        ::SysFreeString(expectedUserName);
        expectedUserName = NULL;
    }

    if (expectedResourceMachine != NULL)
    {
        ::SysFreeString(expectedResourceMachine);
        expectedResourceMachine = NULL;
    }

    if (expectedPortNumber != NULL)
    {
        ::SysFreeString(expectedPortNumber);
        expectedPortNumber = NULL;
    }

    if (expectedCookie != NULL)
    {
        ::SysFreeString(expectedCookie);
    }

    if (NULL != timeout)
    {
        ::SysFreeString(timeout);
        timeout = NULL;
    }

    if (NULL != timeoutAction)
    {
        ::SysFreeString(timeoutAction);
        timeoutAction = NULL;
    }

    return S_OK;
}

// This method is reserved. It should always return S_OK.
STDMETHODIMP CRDGTestPolicyEngineImpl::Refresh()
{
    return S_OK;
}

// Indicates whether the authorization plug-in requires a statement of health (SoH) from the user's computer.
//  This method is not implemented in this sample code.
STDMETHODIMP CRDGTestPolicyEngineImpl::IsQuarantineEnabled(
    BOOL *quarantineEnabled
    )
{
    return E_NOTIMPL;
}

// Provides information about the creation or closing of sessions for a connection.
// Remote Desktop Gateway (RD Gateway) calls this method to pass information to an authorization plug-in.
// This method is not implemented in this sample code.
STDMETHODIMP CRDGTestPolicyEngineImpl::DoAccounting(
    AAAccountingDataType accountingDataType,
    AAAccountingData accountingData
    )
{
    return E_NOTIMPL;
}
