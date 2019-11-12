//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "stdafx.h"
#include "RDGTestAuthenticationEngineImpl.h"
#include "RDGTestAuthenticationEngineClassFactory.h"

// Global declarations
HINSTANCE g_hDllInstance = NULL;
LONG volatile g_uDllLockCount = 0;
TCHAR szTestAuthNPluginName[] = _T("RdgTestAuthenticationPlugin");

// DllMain - entry point
BOOL APIENTRY DllMain(
    HINSTANCE hModule,
    DWORD ul_reason_for_call,
    LPVOID lpReserved
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
    CRDGTestAuthenticationEngineClassFactory* pFactory = NULL;

    // Check that the client is asking for the CRDGTestAuthenticationEngineImpl factory.
    if (!InlineIsEqualGUID(rclsid, __uuidof(CRDGTestAuthenticationEngineImpl)))
    {
        return CLASS_E_CLASSNOTAVAILABLE;
    }

    // Check whether it's a bad pointer.
    if ((NULL == ppv) || IsBadWritePtr(ppv, sizeof(void*)))
    {
        return E_POINTER;
    }

    *ppv = NULL;

    // Construct a new class factory object.
    pFactory = new CRDGTestAuthenticationEngineClassFactory();
    if (NULL == pFactory)
    {
        return E_OUTOFMEMORY;
    }

    // AddRef() the factory since we're using it.
    pFactory->AddRef();

    // QeuryInterface to the class factory for the interface the client wants.
    hr = pFactory->QueryInterface(riid, ppv);

    // We're done with the factory, so Release() it.
    pFactory->Release();

    return hr;
}

// DllCanUnloadNow() is called when COM wants to unload this DLL from memory.
// Check the dll lock count if there are any COM objects still in memory.
// Return S_FALSE to prevent the DLL from being unloaded, or S_OK to let it
// be unloaded.
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
    HKEY hRDGPlugInKey = NULL;
    HKEY hRDGTestPlugInKey = NULL;
    LONG lResult = ERROR_SUCCESS;
    HRESULT hr = S_OK;
    TCHAR szModulePath[MAX_PATH]= L"";
    TCHAR szClassDescription[] = _T("RD Gateway Test Authentication Engine");
    TCHAR szThreadingModel[] = _T("Both");
    TCHAR szCLSIDKey[] = _T("CLSID");
    TCHAR szDescriptionKey[] = _T("Description");
    TCHAR szCLSIDValue[] = _T("{f1400855-aec4-4696-b17c-6af75cf3f402}");

    try
    {
        // Create a key under CLSID for this COM server.
        lResult = RegCreateKeyEx(HKEY_CLASSES_ROOT,
                                 _T("CLSID\\{f1400855-aec4-4696-b17c-6af75cf3f402}"),
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

        // Create the InProcServer32 key, which holds information about this coclass.
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

        // The default value of the InProcServer32 key holds the full path to this DLL.
        GetModuleFileName (g_hDllInstance, szModulePath, MAX_PATH);

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
                               RDG_REG_AUTHENTICATION_PLUG_IN,
                               0L, 
                               KEY_WRITE,
                               &hRDGPlugInKey);
        if (lResult == ERROR_FILE_NOT_FOUND)
        {
            lResult = RegCreateKeyEx(HKEY_LOCAL_MACHINE,
                                     RDG_REG_AUTHENTICATION_PLUG_IN,
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
            throw lResult;
        }

        // set the plugin name
        lResult = RegSetValueEx(hRDGPlugInKey,
                                NULL,
                                0,
                                REG_SZ,
                                (LPBYTE)szTestAuthNPluginName,
                                sizeof(szTestAuthNPluginName));
        if (ERROR_SUCCESS != lResult)
        {
            throw lResult;
        }

        lResult = RegCreateKeyEx(hRDGPlugInKey,
                                 szTestAuthNPluginName,
                                 0,
                                 NULL,
                                 REG_OPTION_NON_VOLATILE,
                                 KEY_SET_VALUE,
                                 NULL,
                                 &hRDGTestPlugInKey,
                                 NULL);
        if (ERROR_SUCCESS != lResult)
        {
            throw lResult;
        }

        lResult = RegSetValueEx(hRDGTestPlugInKey,
                                szCLSIDKey,
                                0,
                                REG_SZ,
                                (LPBYTE)szCLSIDValue,
                                sizeof(szCLSIDValue));
        if (ERROR_SUCCESS != lResult)
        {
            throw lResult;
        }

        lResult = RegSetValueEx(hRDGTestPlugInKey,
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
    hr = StringCchCatW(strRegKeyPath, MAX_PATH, RDG_REG_AUTHENTICATION_PLUG_IN);
    if(FAILED(hr))
    {
        return hr;
    }

    hr = StringCchCatW(strRegKeyPath, MAX_PATH, _T("\\"));
    if(FAILED(hr))
    {
        return hr;
    }

    hr = StringCchCatW(strRegKeyPath, MAX_PATH, szTestAuthNPluginName);
    if(FAILED(hr))
    {
        return hr;
    }

    // Delete plugin name key
    lResult = RegDeleteKey(HKEY_LOCAL_MACHINE, strRegKeyPath);
    if (ERROR_SUCCESS != lResult)
    {
        return HRESULT_FROM_WIN32(lResult);
    }

    // Delete InprocServer32 key
    lResult = RegDeleteKey(HKEY_CLASSES_ROOT, _T("CLSID\\{f1400855-aec4-4696-b17c-6af75cf3f402}\\InProcServer32"));
    if (ERROR_SUCCESS != lResult)
    {
        return HRESULT_FROM_WIN32(lResult);
    }

    // Delete CLSID
    lResult = RegDeleteKey(HKEY_CLASSES_ROOT, _T("CLSID\\{f1400855-aec4-4696-b17c-6af75cf3f402}"));
    if (ERROR_SUCCESS != lResult)
    {
        return HRESULT_FROM_WIN32(lResult);
    }

    // Open/create the main key for AA. 
    lResult = RegOpenKeyEx(HKEY_LOCAL_MACHINE,
                           RDG_REG_AUTHENTICATION_PLUG_IN,
                           0L, 
                           KEY_WRITE,
                           &hRDGPlugInKey);
    if (lResult == ERROR_FILE_NOT_FOUND)
    {
        lResult = RegCreateKeyEx(HKEY_LOCAL_MACHINE,
                                 RDG_REG_AUTHENTICATION_PLUG_IN,
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
        if (NULL != hRDGPlugInKey)
        {
            RegCloseKey(hRDGPlugInKey);
        }
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
