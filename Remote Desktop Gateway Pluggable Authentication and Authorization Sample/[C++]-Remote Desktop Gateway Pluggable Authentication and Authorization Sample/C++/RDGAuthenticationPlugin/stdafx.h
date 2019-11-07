//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

// stdafx.h : include file for standard system include files, 
//  or project specific include files that are used frequently, but are changed infrequently
#ifndef AFX_STDAFX_H__1aa791bb_66d1_425c_99bc_68ff7a7b795b__INCLUDED_
#define AFX_STDAFX_H__1aa791bb_66d1_425c_99bc_68ff7a7b795b__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "..\RDGAuthorizationPlugin\RDGPluginUtils.h"

// Authentication specific
extern HINSTANCE     g_hDllInstance;
extern LONG volatile g_uDllLockCount;

#endif // AFX_STDAFX_H__1aa791bb_66d1_425c_99bc_68ff7a7b795b__INCLUDED_
