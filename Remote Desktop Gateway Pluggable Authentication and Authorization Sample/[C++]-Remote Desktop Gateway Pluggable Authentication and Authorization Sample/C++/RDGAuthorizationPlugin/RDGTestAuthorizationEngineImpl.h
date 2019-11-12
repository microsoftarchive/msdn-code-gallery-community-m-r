//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

// ITSGPolicyEngine, ITSGAccountEngine interfaces are from TSGPolicyEngine.idl
//  Exposes methods that authorize connections and resources. Implement this interface when you want to override 
//  the default authorization logic of Remote Desktop Gateway (RD Gateway).
//
// Note: Windows SDKs must be installed inorder to include TSGpolicyEngine.h
#ifndef AFX_POLICYENGINE_H__df5ac9a7_85ae_4de1_bd87_2b944085440f__INCLUDED_
#define AFX_POLICYENGINE_H__df5ac9a7_85ae_4de1_bd87_2b944085440f__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "TSGPolicyEngine.h"

class CRDGTestPolicyEngineImpl : public ITSGPolicyEngine, public ITSGAccountingEngine
{
public:
    CRDGTestPolicyEngineImpl();
    virtual ~CRDGTestPolicyEngineImpl();

    // IUnknown
    STDMETHOD_(ULONG, AddRef)();
    STDMETHOD_(ULONG, Release)();
    STDMETHOD(QueryInterface)(
        REFIID riid,
        void** ppv
        );

    // IPolicyEngine
    STDMETHOD(AuthorizeConnection)(
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
        );

    STDMETHOD(AuthorizeResource)(
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
        __in ITSGAuthorizeResourceSink* pSink);

    STDMETHOD(Refresh)();

    STDMETHOD(IsQuarantineEnabled)(
        BOOL *quarantineEnabled
        );

    STDMETHOD(DoAccounting)(
        AAAccountingDataType accountingDataType,
        AAAccountingData accountingData
        );

protected:
    LONG volatile m_uRefCount;
};

class  __declspec(uuid("{9ab7a2f3-e2d4-47ea-94b4-fd3c3c070335}")) CRDGTestPolicyEngineImpl;

#endif // !defined(AFX_POLICYENGINE_H__df5ac9a7_85ae_4de1_bd87_2b944085440f__INCLUDED_)
