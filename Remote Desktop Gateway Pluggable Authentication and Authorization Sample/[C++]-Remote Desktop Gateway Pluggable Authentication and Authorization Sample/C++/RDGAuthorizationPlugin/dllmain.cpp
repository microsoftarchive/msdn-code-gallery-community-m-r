//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "stdafx.h"
#include "RDGTestAuthorizationEngineClassFactory.h"
#include "RDGTestAuthorizationEngineImpl.h"

// Global declarations
HINSTANCE g_hDllInstance = NULL;
LONG volatile g_uDllLockCount = 0;
TCHAR szTestAuthZPluginName[] = _T("RdgTestAuthorizationPlugin");

// DllMain - entry point
BOOL APIENTRY DllMain(
    HINSTANCE hModule,
    DWORD     ul_reason_for_call,
    LPVOID    lpReserved
    )
{
    switch (ul_reason_for_call)
    {
        case DLL_PROCESS_ATTACH:
        {
            g_hDllInstance = hModule;
            DisableThreadLibraryCalls(hModule);
        }
        break;

        case DLL_THREAD_ATTACH:
            break;

        case DLL_THREAD_DETACH:
            break;

        case DLL_PROCESS_DETACH:
            break;
    }

    return TRUE;
}

// Retrieves the class object from a DLL object handler or object application.
STDAPI DllGetClassObject(
    REFCLSID rclsid,
    REFIID riid,
    void** ppv
    )
{
    HRESULT hr = S_OK;
    CAATestPolicyEngineClassFactory* pFactory = NULL;

    // Check that the client is asking for the CSimpleMsgBoxImpl factory.
    if (!InlineIsEqualGUID(rclsid, __uuidof(CRDGTestPolicyEngineImpl)))
    {
        return CLASS_E_CLASSNOTAVAILABLE;
    }

    // Check that ppv really points to a void*.
    if ((NULL == ppv) || IsBadWritePtr(ppv, sizeof(void*)))
    {
        return E_POINTER;
    }

    *ppv = NULL;

    // Construct a new class factory object.
    pFactory = new CAATestPolicyEngineClassFactory();
    if (NULL == pFactory)
    {
        return E_OUTOFMEMORY;
    }

    // AddRef() the factory since we're using it.
    pFactory->AddRef();

    // QI() the factory for the interface the client wants. 
    hr = pFactory->QueryInterface(riid, ppv);

    // We're done with the factory, so Release() it.
    pFactory->Release();

    return hr;
}

// DllCanUnloadNow() is called when COM wants to unload our DLL from memory.
// We check our lock count, which will be nonzero if there are any COM objects still in memory.
// Return S_FALSE to prevent the DLL from being unloaded, or S_OK to let it be unloaded.
STDAPI DllCanUnloadNow()
{
    return (g_uDllLockCount > 0) ? S_FALSE : S_OK;
}

// DllRegisterServer() creates the registy entries that tells COM where this
// server is located and its threading model. It's a COM registration.
STDAPI DllRegisterServer()
{
    HKEY hCLSIDKey = NULL;
    HKEY hInProcSvrKey = NULL;
    HKEY hTSGPlugInKey = NULL;
    HKEY hTSGTestPlugInKey = NULL;
    LONG  lResult = ERROR_SUCCESS;
    TCHAR szModulePath[MAX_PATH]= L"";
    TCHAR szClassDescription[] = _T("RD Gateway Test Authorization Engine");
    TCHAR szThreadingModel[] = _T("Both");
    TCHAR szCLSIDKey[] = _T("CLSID");
    TCHAR szDescriptionKey[] = _T("Description");
    TCHAR szCLSIDValue[] = _T("{9ab7a2f3-e2d4-47ea-94b4-fd3c3c070335}");
    HRESULT hr = S_OK;

    try
    {
        // Create a key under CLSID for our COM server.
        lResult = RegCreateKeyEx(HKEY_CLASSES_ROOT,
                                 _T("CLSID\\{9ab7a2f3-e2d4-47ea-94b4-fd3c3c070335}"),
                                 0,
                                 NULL,
                                 REG_OPTION_NON_VOLATILE,
                                 KEY_SET_VALUE | KEY_CREATE_SUB_KEY,
                                 NULL,
                                 &hCLSIDKey,
                                 NULL);
        if (ERROR_SUCCESS != lResult)
        {
            throw lResult;
        }

        // The default value of the key is a human-readable description of the coclass.
        lResult = RegSetValueEx(hCLSIDKey,
                                NULL,
                                0,
                                REG_SZ,
                                (const BYTE*) szClassDescription,
                                sizeof(szClassDescription));
        if (ERROR_SUCCESS != lResult)
        {
            throw lResult;
        }

        // Create the InProcServer32 key, which holds info about our coclass.
        lResult = RegCreateKeyEx(hCLSIDKey,
                                 _T("InProcServer32"),
                                 0,
                                 NULL,
                                 REG_OPTION_NON_VOLATILE,
                                 KEY_SET_VALUE,
                                 NULL,
                                 &hInProcSvrKey,
                                 NULL);
        if (ERROR_SUCCESS != lResult)
        {
            throw lResult;
        }

        // The default value of the InProcServer32 key holds the full path to our DLL.
        GetModuleFileName(g_hDllInstance, szModulePath, MAX_PATH);

        lResult = RegSetValueEx(hInProcSvrKey,
                                NULL,
                                0,
                                REG_SZ,
                                (const BYTE*) szModulePath, 
                               sizeof(TCHAR) * (lstrlen(szModulePath)+1));
        if (ERROR_SUCCESS != lResult)
        {
            throw lResult;
        }

        // The ThreadingModel value tells COM how it should handle threads in this DLL.
        lResult = RegSetValueEx(hInProcSvrKey,
                                _T("ThreadingModel"),
                                0,
                                REG_SZ,
                               (const BYTE*) szThreadingModel,
                               sizeof(szThreadingModel) );
        if (ERROR_SUCCESS != lResult)
        {
            throw lResult;
        }

        // Open/create the main key for AA. 
        lResult = RegOpenKeyEx(HKEY_LOCAL_MACHINE,
                               RDG_REG_AUTHORIZATION_PLUG_INS,
                               0L, 
                               KEY_WRITE,
                               &hTSGPlugInKey);
        if (ERROR_FILE_NOT_FOUND == lResult)
        {
            lResult = RegCreateKeyEx(HKEY_LOCAL_MACHINE,
                                     RDG_REG_AUTHORIZATION_PLUG_INS,
                                     0,
                                     NULL,
                                     REG_OPTION_NON_VOLATILE,
                                     KEY_SET_VALUE,
                                     NULL,
                                     &hTSGPlugInKey,
                                     NULL);
        }
        if (ERROR_SUCCESS != lResult)
        {
            throw lResult;
        }

        lResult = RegSetValueEx(hTSGPlugInKey,
                                NULL,
                                0,
                                REG_SZ,
                                (LPBYTE)szTestAuthZPluginName,
                                sizeof(szTestAuthZPluginName));
        if (ERROR_SUCCESS != lResult)
        {
            throw lResult;
        }

        lResult = RegCreateKeyEx(hTSGPlugInKey,
                                 szTestAuthZPluginName,
                                 0,
                                 NULL,
                                 REG_OPTION_NON_VOLATILE,
                                 KEY_SET_VALUE,
                                 NULL,
                                 &hTSGTestPlugInKey,
                                 NULL);
        if (ERROR_SUCCESS != lResult)
        {
            throw lResult;
        }

        lResult = RegSetValueEx(hTSGTestPlugInKey,
                                szCLSIDKey,
                                0,
                                REG_SZ,
                                (LPBYTE)szCLSIDValue,
                                sizeof(szCLSIDValue));
        if (ERROR_SUCCESS != lResult)
        {
            throw lResult;
        }

        lResult = RegSetValueEx(hTSGTestPlugInKey,
                                szDescriptionKey,
                                0,
                                REG_SZ,
                                (LPBYTE)szClassDescription,
                                sizeof(szClassDescription));
        if (ERROR_SUCCESS != lResult)
        {
            throw lResult;
        }
    }
    catch(LONG error)
    {
        hr = HRESULT_FROM_WIN32(error);
    }

    if (NULL != hCLSIDKey)
    {
        RegCloseKey(hCLSIDKey);
    }

    if (NULL != hInProcSvrKey)
    {
        RegCloseKey(hInProcSvrKey);
    }

    return hr;
}

// DllUnregisterServer() delete the registry entries at unregistration.
STDAPI DllUnregisterServer()
{
    HRESULT hr = S_OK;
    WCHAR strRegKeyPath[MAX_PATH] = L"";
    LONG lResult = ERROR_SUCCESS;
    HKEY hRDGPlugInKey = NULL;

    // Construct the registry path to be deleted.
    hr = StringCchCatW(strRegKeyPath, MAX_PATH, RDG_REG_AUTHORIZATION_PLUG_INS);
    if(FAILED(hr))
    {
        return hr;
    }

    hr = StringCchCatW(strRegKeyPath, MAX_PATH, _T("\\"));
    if(FAILED(hr))
    {
        return hr;
    }

    hr = StringCchCatW(strRegKeyPath, MAX_PATH, szTestAuthZPluginName);
    if(FAILED(hr))
    {
        return hr;
    }

    lResult = RegDeleteKey(HKEY_LOCAL_MACHINE, strRegKeyPath);
    if (ERROR_SUCCESS != lResult)
    {
        return HRESULT_FROM_WIN32(lResult);
    }

    lResult = RegDeleteKey(HKEY_CLASSES_ROOT, _T("CLSID\\{9ab7a2f3-e2d4-47ea-94b4-fd3c3c070335}\\InProcServer32") );
    if (ERROR_SUCCESS != lResult)
    {
        return HRESULT_FROM_WIN32(lResult);
    }

    lResult = RegDeleteKey(HKEY_CLASSES_ROOT, _T("CLSID\\{9ab7a2f3-e2d4-47ea-94b4-fd3c3c070335}") );
    if (ERROR_SUCCESS != lResult)
    {
        return HRESULT_FROM_WIN32(lResult);
    }

    // Open/create the main key for AA. 
    lResult = RegOpenKeyEx(HKEY_LOCAL_MACHINE,
                           RDG_REG_AUTHORIZATION_PLUG_INS,
                           0L, 
                           KEY_WRITE,
                           &hRDGPlugInKey);
    if (lResult == ERROR_FILE_NOT_FOUND)
    {
        lResult = RegCreateKeyEx(HKEY_LOCAL_MACHINE,
                                 RDG_REG_AUTHORIZATION_PLUG_INS,
                                 0,
                                 NULL,
                                 REG_OPTION_NON_VOLATILE,
                                 KEY_SET_VALUE,
                                 NULL,
                                 &hRDGPlugInKey,
                                 NULL);
    }
    if (ERROR_SUCCESS != lResult)
    {
        RegCloseKey(hRDGPlugInKey);
        return HRESULT_FROM_WIN32(lResult);
    }

    // set the default authentication plugin name
    lResult = RegSetValueEx(hRDGPlugInKey,
                            NULL,
                            0,
                            REG_SZ,
                            (LPBYTE)RDG_NATIVE_PLUG_IN_NAME,
                            sizeof(RDG_NATIVE_PLUG_IN_NAME));
    if (ERROR_SUCCESS != lResult)
    {
        if (NULL != hRDGPlugInKey)
        {
            RegCloseKey(hRDGPlugInKey);
        }
        return HRESULT_FROM_WIN32(lResult);
    }

    if (NULL != hRDGPlugInKey)
    {
        RegCloseKey(hRDGPlugInKey);
    }

    return hr;
}
