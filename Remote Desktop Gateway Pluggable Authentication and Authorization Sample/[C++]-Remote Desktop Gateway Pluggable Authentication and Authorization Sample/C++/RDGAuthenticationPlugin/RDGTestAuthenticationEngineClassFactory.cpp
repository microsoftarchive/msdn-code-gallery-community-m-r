//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "stdafx.h"
#include "RDGTestAuthenticationEngineClassFactory.h"
#include "RDGTestAuthenticationEngineImpl.h"

// Default constructor for authentication engine class factory
CRDGTestAuthenticationEngineClassFactory::CRDGTestAuthenticationEngineClassFactory()
{
    m_uRefCount = 0;
    InterlockedIncrement(&g_uDllLockCount);
}

// Default destructor for authentication engine class factory
CRDGTestAuthenticationEngineClassFactory::~CRDGTestAuthenticationEngineClassFactory()
{
    InterlockedDecrement(&g_uDllLockCount);
}

// Reference count of object
STDMETHODIMP_(ULONG) CRDGTestAuthenticationEngineClassFactory::AddRef()
{
    InterlockedIncrement(&m_uRefCount);
    return m_uRefCount;
}

// Reference count of object
STDMETHODIMP_(ULONG) CRDGTestAuthenticationEngineClassFactory::Release()
{
    InterlockedDecrement(&m_uRefCount);

    ULONG uRet = m_uRefCount;
    if (0 == m_uRefCount)
    {
        delete this;
    }

    return uRet;
}

// Query interface
STDMETHODIMP CRDGTestAuthenticationEngineClassFactory::QueryInterface(
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
        *ppv = (IUnknown*)this;
    }
    else if(InlineIsEqualGUID(riid, IID_IClassFactory))
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

// IClassFactory methods
STDMETHODIMP CRDGTestAuthenticationEngineClassFactory::CreateInstance(
    IUnknown* pUnkOuter,
    REFIID riid,
    void** ppv
    )
{
    HRESULT hr = S_OK;
    CRDGTestAuthenticationEngineImpl* pAuthenticationEngine = NULL;

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
    pAuthenticationEngine = new CRDGTestAuthenticationEngineImpl();
    if (NULL == pAuthenticationEngine)
    {
        return E_OUTOFMEMORY;
    }

    // QI the object for the interface the client is requesting.
    hr = pAuthenticationEngine->QueryInterface(riid, ppv);

    // If the QI failed, delete the COM object since the client isn't able
    // to use it (the client doesn't have any interface pointers on the object).
    if (FAILED(hr))
    {
        delete pAuthenticationEngine;
        pAuthenticationEngine = NULL;
    }

    return hr;
}

STDMETHODIMP CRDGTestAuthenticationEngineClassFactory::LockServer(
    BOOL fLock
    )
{
    // Increase/decrease the DLL ref count, according to the fLock param.
    fLock ? InterlockedIncrement(&g_uDllLockCount) : InterlockedDecrement(&g_uDllLockCount);

    return S_OK;
}
