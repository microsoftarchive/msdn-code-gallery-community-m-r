//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

// RDGPluginUtils - Common utilities for reading/writing xml and global declarations
#include <Windows.h>
#include <tchar.h>
#include <strsafe.h>
#include <msxml2.h>

// Common global declarations
#define RDG_REG_AUTHORIZATION_PLUG_INS  L"Software\\Microsoft\\Terminal Server Gateway\\Authorization plug-ins"
#define RDG_REG_AUTHENTICATION_PLUG_IN  L"Software\\Microsoft\\Terminal Server Gateway\\Authentication plug-ins"
#define RDG_NATIVE_PLUG_IN_NAME         L"native"
#define RDG_PLUGIN_POLICY_XML           L"\\RDGPlugins\\RDGPluginPolicy.xml"
#define RDG_PLUGIN_SESSIONID_XML        L"\\RDGPlugins\\MainSessionID.xml"
#define E_PROXY_RAP_ACCESSDENIED        HRESULT_FROM_WIN32(23002L)
#define SYSTEM_DRIVE                    L"SystemDrive"

// xml tags
#define XML                             L"xml"
#define XML_VERSION                     L"version='1.0'"
#define XML_ID                          L"ID"
#define XML_MAIN_SESSION_ID             L"MainSessionID"
#define XML_USERNAME                    L"UserName"
#define XML_CLIENTMACHINE               L"ClientMachine"
#define XML_SMARTCARD_ALLOWED           L"SmartcardAllowed"
#define XML_PASSWORD_ALLOWED            L"PasswordAllowed"
#define XML_RESOURCEMACHINE             L"ResourceMachine"
#define XML_PORTNUMBER                  L"PortNumber"
#define XML_COOKIEDATA                  L"CookieValue"
#define XML_TIMEOUT                     L"Timeout"
#define XML_TIMEOUTACTION               L"TimeoutAction"

// Read authentication policy from xml
void GetAuthenticationParams(
    BSTR *cookie,
    BSTR *timeout,
    BSTR *timeoutAction
    );

// Read connection policy from xml
void GetConnectionPolicyParams(
    BSTR *userName, 
    BSTR *clientMachine,
    BSTR *smartcardAllowed,
    BSTR *passwordAllowed
    );

// Read Resource policy from xml
void GetResourcePolicyParams(
    BSTR *userName,
    BSTR *resourceMachine,
    BSTR *portNumber
    );

// Read mainSessionID in authorization from xml which created at the time of authenthencation
void GetMainSessionId(
    BSTR *expectedMainSessionId
    );

// Check whether authentication is native or custom in authorization
BOOL IsAuthenticationEngineNative();

// Read mainSessionID from xml and use the same to make sure same user requested for authentication and authorization.
BOOL CheckForMainSessionId(
    GUID mainSessionId
    );

// Write mainSessionID in xml that received in authentication and later use in authorization to confirm the request from same user session.
void StoreMainSessionId(
    GUID mainSessionId
    );
