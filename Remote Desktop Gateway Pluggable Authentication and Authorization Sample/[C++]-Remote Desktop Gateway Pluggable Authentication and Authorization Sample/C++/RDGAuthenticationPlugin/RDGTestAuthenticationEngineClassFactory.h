//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#ifndef AFX_AAAUTHENTICATIONENGINECLASSFACTORY_H__ed502b09_44a2_415a_b76a_7c332cd35436__INCLUDED_
#define AFX_AAAUTHENTICATIONENGINECLASSFACTORY_H__ed502b09_44a2_415a_b76a_7c332cd35436__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

// RD Gateway Authentication Engine Class Factory
class CRDGTestAuthenticationEngineClassFactory : public IClassFactory
{
public:
    CRDGTestAuthenticationEngineClassFactory();
    virtual ~CRDGTestAuthenticationEngineClassFactory();

    // IUnknown
    STDMETHOD_(ULONG, AddRef)();
    STDMETHOD_(ULONG, Release)();
    STDMETHOD(QueryInterface)(
        REFIID riid,
        void** ppv
        );

    // IClassFactory
    STDMETHOD(CreateInstance)(
        IUnknown* pUnkOuter,
        REFIID riid,
        void** ppv
        );
    STDMETHOD(LockServer)(
        BOOL fLock
        );

protected:
    LONG volatile m_uRefCount;
};

#endif // !defined(AFX_AAAUTHENTICATIONENGINECLASSFACTORY_H__ed502b09_44a2_415a_b76a_7c332cd35436__INCLUDED_)
