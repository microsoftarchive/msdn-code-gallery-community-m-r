//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "stdafx.h"
#include "RDGPluginUtils.h"

// Helper function to create a DOM instance. 
//  The purpose of this function is to read XML for authentication policy.
//  As this is sample authentication plugin, store the authentication policy in an XML file
//  and use the same for authentication validation purpose.
IXMLDOMDocument* DomFromCOM()
{
    HRESULT hr = S_OK;
    IXMLDOMDocument *pXmlDoc = NULL;

    hr = CoCreateInstance(__uuidof(DOMDocument30),
                          NULL,
                          CLSCTX_INPROC_SERVER,
                          __uuidof(IXMLDOMDocument),
                          (void**)&pXmlDoc);
    if(FAILED(hr))
    {
        return NULL;
    }

    hr = pXmlDoc->put_async(VARIANT_FALSE);
    if(FAILED(hr))
    {
        pXmlDoc->Release();
        return NULL;
    }

    hr = pXmlDoc->put_validateOnParse(VARIANT_FALSE);
    if(FAILED(hr))
    {
        pXmlDoc->Release();
        return NULL;
    }

    hr = pXmlDoc->put_resolveExternals(VARIANT_FALSE);
    if(FAILED(hr))
    {
        pXmlDoc->Release();
        return NULL;
    }

    hr = pXmlDoc->put_preserveWhiteSpace(VARIANT_TRUE);
    if(FAILED(hr))
    {
        pXmlDoc->Release();
        return NULL;
    }

    return pXmlDoc;
}

// Helper function to create variant string.
VARIANT VariantString(
    BSTR str
    )
{
    VARIANT var;

    VariantInit(&var);
    V_BSTR(&var) = SysAllocString(str);
    V_VT(&var) = VT_BSTR;

    return var;
}

// Helper function to append a whitespace text node to a specified element.
void AddWhiteSpaceToNode(
    IXMLDOMDocument *pDom,
    BSTR bstrValue,
    IXMLDOMNode *pNode
    )
{
    HRESULT hr = S_OK ;
    IXMLDOMText *pws = NULL;
    IXMLDOMNode *pBuf = NULL;

    if( (NULL == pDom) || (NULL == bstrValue) || (NULL == pNode))
    {
        return;
    }

    hr = pDom->createTextNode(bstrValue,&pws);
    if(SUCCEEDED(hr))
    {
         pNode->appendChild(pws, &pBuf);
         if (NULL != pws)
         {
             pws->Release();
             pws=NULL;
         }
        if (NULL != pBuf)
        {
            pBuf->Release();
            pBuf=NULL;
        }
    }
}

// Helper function to append a child to a parent node:
void AppendChildToParent(
    IXMLDOMNode *pChild,
    IXMLDOMNode *pParent
    )
{
    IXMLDOMNode *pNode = NULL;

    if(NULL == pParent)
    {
        return;
    }

    pParent->appendChild(pChild, &pNode);
    if (NULL != pNode)
    {
        pNode->Release();
        pNode=NULL;
    }
}

// Helper function to create a node with some text value to the IXMLDOMDocument
void CreateNode(
    IXMLDOMDocument *pXMLDom,
    IXMLDOMElement *pRoot,
    BSTR nodeName,
    BSTR nodeText
    )
{
    HRESULT hr = S_OK;
    IXMLDOMElement *pElement = NULL;

    if( (NULL == pXMLDom) || (NULL == pRoot) || (NULL == nodeText) || (NULL == nodeName))
    {
        return;
    }

    hr = pXMLDom->createElement(nodeName, &pElement);
    if(FAILED(hr))
    {
        return;
    }

    hr = pElement->put_text(nodeText);
    if(FAILED(hr))
    {
        pElement->Release();
        return;
    }

    AppendChildToParent(pElement, pRoot);

    if(NULL != pElement)
    {
        pElement->Release();
        pElement = NULL;
    }
}

// Helper function to add a comment to IXMLDOMDocument
void AddComment(
    IXMLDOMDocument *pXMLDom,
    BSTR commentString
    )
{
    HRESULT hr = S_OK;
    IXMLDOMComment *pComment=NULL;

    if( (NULL == pXMLDom) || (NULL == commentString))
    {
        return;
    }

    hr = pXMLDom->createComment(commentString, &pComment);
    if(FAILED(hr))
    {
        return;
    }

    AppendChildToParent(pComment, pXMLDom);
    if(NULL != pComment)
    {
        pComment->Release();
        pComment = NULL;
    }
}

// Helper function to create a root element with an attribute and an attribute value
void CreateRootElementWithAttribute(
    IXMLDOMDocument *pXMLDom,
    IXMLDOMElement **pRoot,
    BSTR rootElementName,
    BSTR attributeName,
    BSTR attributeValue
    )
{
    IXMLDOMAttribute *pAttribute = NULL;
    IXMLDOMAttribute *pAttribute1 = NULL;
    VARIANT var;

    if ((NULL == pXMLDom) || (NULL == pRoot)|| (NULL == rootElementName)|| (NULL == attributeName)|| (NULL == attributeValue))
    {
        return;
    }

    pXMLDom->createElement(rootElementName, pRoot);
    var = VariantString(attributeValue);
    pXMLDom->createAttribute(attributeName, &pAttribute);
    pAttribute->put_value(var);
    (*pRoot)->setAttributeNode(pAttribute, &pAttribute1);
    AppendChildToParent(*pRoot, pXMLDom);

    if (NULL != pAttribute1)
    {
        pAttribute1->Release();
        pAttribute1 = NULL;
    }

    if (NULL != pAttribute)
    {
        pAttribute->Release();
        pAttribute = NULL;
    }

    VariantClear(&var);
}

// Helper function to get the text value for a node in the IXMLDOMDocument
void GetNodeValue(
    IXMLDOMDocument *pXMLDom,
    BSTR nodeName,
    BSTR *nodeValue
    )
{
    WCHAR logtext[MAX_PATH] = {0};
    HRESULT hr = S_OK;
    IXMLDOMNodeList *pXMLNodeList=NULL;
    IXMLDOMNode *pXMLNode=NULL;

    if ((NULL == pXMLDom) || (NULL == nodeName) || (NULL == nodeValue))
    {
        return;
    }

    hr = pXMLDom->getElementsByTagName(nodeName, &pXMLNodeList);
    if(FAILED(hr))
    {
        return;
    }

    hr = pXMLNodeList->get_item(0, &pXMLNode);
    if(SUCCEEDED(hr))
    {
        pXMLNode->get_text(nodeValue);
    }

    if(NULL != pXMLNodeList)
    {
        pXMLNodeList->Release();
        pXMLNodeList = NULL;
    }

    if (NULL != pXMLNode)
    {
        pXMLNode->Release();
        pXMLNode = NULL;
    }
}

// Read the connection policy from xml
void GetConnectionPolicyParams(
    BSTR *userName,
    BSTR *clientMachine,
    BSTR *smartcardAllowed,
    BSTR *passwordAllowed
    )
{
    IXMLDOMDocument *pXMLDom=NULL;
    IXMLDOMParseError *pXMLErr=NULL;

    BSTR bstr = NULL;
    VARIANT_BOOL status;
    VARIANT var;
    HRESULT hr = S_OK;

    CoInitialize(NULL);
    VariantInit(&var);
  
    pXMLDom = DomFromCOM();
    if (!pXMLDom)
    {
        goto clean;
    }

    WCHAR buffer[MAX_PATH];
    GetEnvironmentVariable(SYSTEM_DRIVE, buffer, MAX_PATH);
    wcscat_s(buffer, RDG_PLUGIN_POLICY_XML);
    var = VariantString(buffer);

    hr = pXMLDom->load(var, &status);
    if(FAILED(hr))
    {
        goto clean;
    }

    if (status!=VARIANT_TRUE)
    {
        pXMLDom->get_parseError(&pXMLErr);
        pXMLErr->get_reason(&bstr);
        goto clean;
    }
    GetNodeValue(pXMLDom, XML_USERNAME, userName);
    GetNodeValue(pXMLDom, XML_CLIENTMACHINE, clientMachine);
    GetNodeValue(pXMLDom, XML_SMARTCARD_ALLOWED, smartcardAllowed);
    GetNodeValue(pXMLDom, XML_PASSWORD_ALLOWED, passwordAllowed);

clean:
    if (NULL != bstr)
    {
        SysFreeString(bstr);
        bstr = NULL;
    }

    if (&var)
    {
        VariantClear(&var);
    }

    if (NULL != pXMLDom)
    {
        pXMLDom->Release();
        pXMLDom = NULL;
    }

    if (NULL != pXMLErr)
    {
        pXMLErr->Release();
        pXMLErr = NULL;
    }

    CoUninitialize();
}

// Read the resource authorization policy from xml
void GetResourcePolicyParams(
    BSTR *userName,
    BSTR *resourceMachine,
    BSTR *portNumber
    )
{
    IXMLDOMDocument *pXMLDom = NULL;
    IXMLDOMParseError *pXMLErr = NULL;

    BSTR bstr = NULL;
    VARIANT_BOOL status;
    VARIANT var;
    HRESULT hr = S_OK;

    CoInitialize(NULL);
    VariantInit(&var);
  
    pXMLDom = DomFromCOM();
    if (!pXMLDom)
    {
        goto clean;
    }

    WCHAR buffer[MAX_PATH];
    GetEnvironmentVariable(SYSTEM_DRIVE, buffer, MAX_PATH);
    wcscat_s(buffer, RDG_PLUGIN_POLICY_XML);
    var = VariantString(buffer);
    
    hr = pXMLDom->load(var, &status);
    if(FAILED(hr))
    {
        goto clean;
    }

    if (status!=VARIANT_TRUE)
    {
        pXMLDom->get_parseError(&pXMLErr);
        pXMLErr->get_reason(&bstr);
        goto clean;
    }
    GetNodeValue(pXMLDom, XML_USERNAME, userName);
    GetNodeValue(pXMLDom, XML_RESOURCEMACHINE, resourceMachine);
    GetNodeValue(pXMLDom, XML_PORTNUMBER, portNumber);
    
clean:
    if (NULL != bstr)
    {
        SysFreeString(bstr);
        bstr = NULL;
    }

    if (&var)
    {
        VariantClear(&var);
    }

    if (NULL != pXMLDom)
    {
        pXMLDom->Release();
        pXMLDom = NULL;
    }

    if (NULL != pXMLErr)
    {
        pXMLErr->Release();
        pXMLErr = NULL;
    }

    CoUninitialize();
}

// Check machine ID.
void GetMainSessionId(
    BSTR *expectedMainSessionId
    )
{
    IXMLDOMDocument *pXMLDom=NULL;
    IXMLDOMParseError *pXMLErr=NULL;

    BSTR bstr = NULL;
    VARIANT_BOOL status;
    VARIANT var;
    HRESULT hr = S_OK;

    CoInitialize(NULL);
    VariantInit(&var);
  
    pXMLDom = DomFromCOM();
    if (!pXMLDom)
    {
        goto clean;
    }

    WCHAR buffer[MAX_PATH];
    GetEnvironmentVariable(SYSTEM_DRIVE, buffer, MAX_PATH);
    wcscat_s(buffer, RDG_PLUGIN_SESSIONID_XML);
    var = VariantString(buffer);
    
    hr = pXMLDom->load(var, &status);
    if(FAILED(hr))
        goto clean;

    if (status!=VARIANT_TRUE)
    {
        pXMLDom->get_parseError(&pXMLErr);
        pXMLErr->get_reason(&bstr);
        goto clean;
    }

    GetNodeValue(pXMLDom, XML_ID, expectedMainSessionId);

clean:
    if (NULL != bstr)
    {
        SysFreeString(bstr);
        bstr = NULL;
    }

    if (&var)
    {
        VariantClear(&var);
    }

    if (NULL != pXMLDom)
    {
        pXMLDom->Release();
        pXMLDom = NULL;
    }

    if (NULL != pXMLErr)
    {
        pXMLErr->Release();
        pXMLErr = NULL;
    }

    CoUninitialize();
}

// Read the authentication policy from xml file which is stored in the hard coded location.
void GetAuthenticationParams(
    BSTR *cookie,
    BSTR *timeout,
    BSTR *timeoutAction
    )
{
    IXMLDOMDocument *pXMLDom = NULL;
    IXMLDOMParseError *pXMLErr = NULL;

    BSTR bstr = NULL;
    VARIANT_BOOL status;
    VARIANT var;
    HRESULT hr = S_OK;

    CoInitialize(NULL);
    VariantInit(&var);
  
    pXMLDom = DomFromCOM();
    if (!pXMLDom)
    {
        goto clean;
    }

    WCHAR buffer[MAX_PATH];
    GetEnvironmentVariable(SYSTEM_DRIVE, buffer, MAX_PATH);
    wcscat_s(buffer, RDG_PLUGIN_POLICY_XML);
    var = VariantString(buffer);
    
    hr = pXMLDom->load(var, &status);
    if(FAILED(hr))
        goto clean;

    if (status!=VARIANT_TRUE)
    {
        pXMLDom->get_parseError(&pXMLErr);
        pXMLErr->get_reason(&bstr);
        goto clean;
    }
    GetNodeValue(pXMLDom, XML_COOKIEDATA, cookie);
    GetNodeValue(pXMLDom, XML_TIMEOUT, timeout);
    GetNodeValue(pXMLDom, XML_TIMEOUTACTION, timeoutAction);

clean:
    if (NULL != bstr)
    {
        SysFreeString(bstr);
        bstr = NULL;
    }

    if (&var)
    {
        VariantClear(&var);
    }

    if (NULL != pXMLDom)
    {
        pXMLDom->Release();
        pXMLDom = NULL;
    }

    if (NULL != pXMLErr)
    {
        pXMLErr->Release();
        pXMLErr = NULL;
    }

    CoUninitialize();
}

// Check whether the current plugin is native or custom
BOOL IsAuthenticationEngineNative()
{
    BOOL bNative = TRUE;
    LONG lRet = ERROR_SUCCESS;
    HKEY hTSGPlugInKey = NULL;
    WCHAR szPluginName [MAX_PATH] = L"";
    DWORD dwPluginName = sizeof(szPluginName);

    lRet = RegOpenKeyEx(HKEY_LOCAL_MACHINE,
                        RDG_REG_AUTHENTICATION_PLUG_IN,
                        0L, 
                        KEY_READ,
                        &hTSGPlugInKey);
    if (lRet != ERROR_SUCCESS)
    {
        goto EXIT_POINT;
    }

    lRet = RegQueryValueEx(hTSGPlugInKey,
                           NULL,
                           0,
                           NULL,
                           (LPBYTE)szPluginName,
                           &dwPluginName);
    if (lRet != ERROR_SUCCESS)
    {
        goto EXIT_POINT;
    }

    if (_wcsicmp(szPluginName, RDG_NATIVE_PLUG_IN_NAME))
    {
        bNative = FALSE;
    }

EXIT_POINT:
    if (NULL != hTSGPlugInKey)
    {
        RegCloseKey(hTSGPlugInKey);
        hTSGPlugInKey = NULL;
    }

    return bNative;
}

BOOL CheckForMainSessionId(
    GUID mainSessionId
    )
{
    BSTR expectedMainSessionId = NULL;
    BSTR actualMainSessionId = NULL;
    BOOL ret = TRUE;

    //Convert mainSessionId from GUID to WCHAR
    WCHAR   actualMainSessionId_WChar[MAX_PATH] = L"";
    StringFromGUID2(mainSessionId, actualMainSessionId_WChar, sizeof(actualMainSessionId_WChar)/sizeof(WCHAR));

    //Convert WCHAR to BSTR
    actualMainSessionId = SysAllocString(actualMainSessionId_WChar);

    //GetMain Session ID
    GetMainSessionId(&expectedMainSessionId);
    if(_wcsicmp(actualMainSessionId,expectedMainSessionId))
    {
        ret = FALSE;
    }

    if (NULL != expectedMainSessionId)
    {
        ::SysFreeString(expectedMainSessionId);
        expectedMainSessionId = NULL;
    }
    if (NULL != actualMainSessionId)
    {
        ::SysFreeString(actualMainSessionId);
        actualMainSessionId = NULL;
    }

    return ret;
}

// Store the main session ID given by RD Gateway and use the same for validation in authorization.
void StoreMainSessionId(
    GUID mainSessionId
    )
{
    IXMLDOMDocument *pXMLDom = NULL;
    IXMLDOMProcessingInstruction *pi = NULL;
    IXMLDOMElement *pRoot = NULL;
    BSTR bstr = NULL;
    BSTR bstr1 = NULL;
    BSTR bstr_wsn = SysAllocString(L"\n");
    BSTR bstr_wsnt= SysAllocString(L"\n\t");
    BSTR idBstr = NULL;
    VARIANT var;
    HRESULT hr = S_OK;
    WCHAR filePath[MAX_PATH] = {0};

    CoInitialize(NULL);
    VariantInit(&var);

    pXMLDom = DomFromCOM();
    if (!pXMLDom) goto clean;

    // Create a processing instruction element.
    bstr = SysAllocString(XML);
    bstr1 = SysAllocString(XML_VERSION);
    hr = pXMLDom->createProcessingInstruction(bstr, bstr1, &pi);
    if(FAILED(hr))
        goto clean;

    AppendChildToParent(pi, pXMLDom);

    // Create a comment element.
    AddComment(pXMLDom, L"This file stores the mainSessionId for the current RD Connection");

    // Create the root element.
    CreateRootElementWithAttribute(pXMLDom, &pRoot, XML_MAIN_SESSION_ID, XML_ID, L"Current Session ID");

    // Add NEWLINE+TAB for identation before <node1>.
    AddWhiteSpaceToNode(pXMLDom, bstr_wsnt, pRoot);

    //Convert GUID to WCHAR before storing
    WCHAR id[MAX_PATH];
    StringFromGUID2(mainSessionId, id, sizeof(id)/sizeof(WCHAR));

    //Convert WCHAR to BSTR
    idBstr = SysAllocString(id);

    // Create a node to hold CookieValue.
    CreateNode(pXMLDom, pRoot, XML_ID, idBstr);

    hr = pXMLDom->get_xml(&bstr);
    if(FAILED(hr))
    {
        goto clean;
    }

    GetEnvironmentVariable(SYSTEM_DRIVE, filePath, MAX_PATH);
    wcscat_s(filePath, RDG_PLUGIN_SESSIONID_XML);
    var = VariantString(filePath);
    pXMLDom->save(var);

clean:
    if (NULL != bstr)
    {
        SysFreeString(bstr);
        bstr = NULL;
    }

    if (NULL != bstr1)
    {
        SysFreeString(bstr1);
        bstr1 = NULL;
    }

    if (NULL != bstr_wsn)
    {
        SysFreeString(bstr_wsn);
        bstr_wsn = NULL;
    }

    if (NULL != bstr_wsnt)
    {
        SysFreeString(bstr_wsnt);
        bstr_wsnt = NULL;
    }

    if (NULL != idBstr)
    {
        SysFreeString(idBstr);
        idBstr = NULL;
    }

    if (&var)
    {
        VariantClear(&var);
    }

    if (NULL != pXMLDom)
    {
        pXMLDom->Release();
        pXMLDom = NULL;
    }

    if (NULL != pRoot)
    {
        pRoot->Release();
        pRoot = NULL;
    }

    if (NULL != pi)
    {
        pi->Release();
        pi = NULL;
    }

    CoUninitialize();
}
