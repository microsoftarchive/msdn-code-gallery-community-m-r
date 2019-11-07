'''' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
'''' ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
'''' THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'''' PARTICULAR PURPOSE.
''''
'''' Copyright (c) Microsoft Corporation. All rights reserved

On Error Resume Next

Dim PluginName
Set objArgs = WScript.Arguments

' Check for arguments
if objArgs.Count <> 0 then
    PluginName = objArgs(0)
else
    wscript.echo "Argument not specified"
    wscript.echo "Example to set native plugin:"
    wscript.echo "    cscript SetAuthenticationPlugin.vbs native"
    wscript.quit(1)
end if
Err.Clear

' Connect to WMI on Gateway server
ServerName = "."
set serv = GetObject("winmgmts:{authenticationLevel=pktPrivacy}!" & "\\" & ServerName & "\root\cimv2\TerminalServices")

if Err.Number <> 0 then
    wscript.echo "Could not connect " & CStr(Err.Number)
    wscript.quit(Err.Number)
end if

' Create a gateway instance
set objs = serv.InstancesOf("Win32_TSGatewayServerSettings")
Err.Clear

for each obj in objs
    if Err.Number <> 0 then
        wscript.echo "Err number = " & CStr(Err.Number)
        wscript.quit(Err.Number)
    end if

    ' Register the authentication plugin name with RD Gateway
    if obj.SetAuthenticationPluginAndRecycleRpcApplicationPools(PluginName) = 0 then
        wscript.echo "SetAuthenticationPlugin successful"
        wscript.quit(0)
    else
        wscript.echo "Error in SetAuthenticationPlugin " & CStr(Err.Number)
        wscript.quit(-1)
    end if

Next
wscript.echo "Failed to get an instance of Win32_TSGatewayServerSettings class"
wscript.quit(1)
