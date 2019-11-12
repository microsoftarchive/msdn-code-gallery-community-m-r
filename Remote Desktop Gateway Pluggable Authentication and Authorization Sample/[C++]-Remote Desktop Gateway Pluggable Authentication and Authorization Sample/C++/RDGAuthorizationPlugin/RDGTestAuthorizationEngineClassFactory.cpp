//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "stdafx.h"
#include "RDGTestAuthorizationEngineClassFactory.h"
#include "RDGTestAuthorizationEngineImpl.h"

CAATestPolicyEngineClassFactory::CAATestPolicyEngineClassFactory()
{
    m_uRefCount = 0;
    InterlockedIncrement(&g_uDllLockCount);
}

CAATestPolicyEngineClassFactory::~CAATestPolicyEngineClassFactory()
{
    InterlockedDecrement(&g_uDllLockCount);
}

STDMETHODIMP_(ULONG) CAATestPolicyEngineClassFactory::AddRef()
{
    InterlockedIncrement(&m_uRefCount);
    return m_uRefCount;
}

STDMETHODIMP_(ULONG) CAATestPolicyEngineClassFactory::Release()
{
    InterlockedDecrement(&m_uRefCount);

    ULONG uRet = m_uRefCount;
    if (0 == m_uRefCount)
    {
        delete this;
    }

    return uRet;
}

STDMETHODIMP CAATestPolicyEngineClassFactory::QueryInterface(
    REFIID riid,
    void** ppv
    )
{
    HRESULT hr = S_OK;

    // Check that ppv really points to a void*.
    if ( IsBadWritePtr(ppv, sizeof(void*)))
    {
        return E_POINTER;
    }

    // Standard QI initialization - set *ppv to NULL.
    *ppv = NULL;

    // If the client is requesting an interface we support, set *ppv.
    if (InlineIsEqualGUID(riid, IID_IUnknown))
    {
        *ppv = (IUnknown*)this;
    }
    else if (InlineIsEqualGUID(riid, IID_IClassFactory))
    {
        *ppv = (IClassFactory*)this;
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

STDMETHODIMP CAATestPolicyEngineClassFactory::CreateInstance(
    IUnknown* pUnkOuter,
    REFIID riid,
    void** ppv
    )
{
    HRESULT hr = S_OK;
    CRDGTestPolicyEngineImpl* pPolicyEngine = NULL;

    // We don't support aggregation, so pUnkOuter must be NULL.
    if (NULL != pUnkOuter)
    {
        return CLASS_E_NOAGGREGATION;
    }

    // Check that ppv really points to a void*.
    if (IsBadWritePtr(ppv, sizeof(void*)))
    {
        return E_POINTER;
    }

    *ppv = NULL;

    // Create a new COM object!
    pPolicyEngine = new CRDGTestPolicyEngineImpl();
    if (NULL == pPolicyEngine)
    {
        return E_OUTOFMEMORY;
    }

    // QI the object for the interface the client is requesting.
    hr = pPolicyEngine->QueryInterface(riid, ppv);

    // If the QI failed, delete the COM object since the client isn't able
    // to use it (the client doesn't have any interface pointers on the object).
    if (FAILED(hr))
    {
        delete pPolicyEngine;
        pPolicyEngine = NULL;
    }

    return hr;
}

STDMETHODIMP CAATestPolicyEngineClassFactory::LockServer(
    BOOL fLock
    )
{
    // Increase/decrease the DLL ref count, according to the fLock param.
    fLock ? InterlockedIncrement(&g_uDllLockCount) : InterlockedDecrement(&g_uDllLockCount);

    return S_OK;
}
