//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#ifndef AFX_AAPOLICYENGINECLASSFACTORY_H__1a24174e_061c_4c74_ad8f_08e950cb9fe6__INCLUDED_
#define AFX_AAPOLICYENGINECLASSFACTORY_H__1a24174e_061c_4c74_ad8f_08e950cb9fe6__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CAATestPolicyEngineClassFactory : public IClassFactory
{
public:
    CAATestPolicyEngineClassFactory();
    virtual ~CAATestPolicyEngineClassFactory();

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
#endif // !defined(AFX_AAPOLICYENGINECLASSFACTORY_H__1a24174e_061c_4c74_ad8f_08e950cb9fe6__INCLUDED_)
