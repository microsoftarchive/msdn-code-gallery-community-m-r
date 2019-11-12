# The script has been tested on Powershell 3.0
Set-StrictMode -Version 3

# Set the output level to verbose and make the script stop on error
$VerbosePreference = "Continue"
$ErrorActionPreference = "Stop"

# Grant administrative privileges
If (-Not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) {
  Write-Verbose "Script is not run with administrative user"

  If ((Get-WmiObject Win32_OperatingSystem | select BuildNumber).BuildNumber -ge 6000) {
    $CommandLine = $MyInvocation.Line.Replace($MyInvocation.InvocationName, $MyInvocation.MyCommand.Definition)
    Write-Verbose "  $CommandLine"
 
    Start-Process -FilePath PowerShell.exe -Verb Runas -ArgumentList "$CommandLine"

  } else {
    Write-Verbose "System does not support UAC"
    Write-Warning "This script requires administrative privileges. Please re-run with administrative account."
  }
  Break
}

Set-ExecutionPolicy -Scope Process Undefined -Force
if ($(Get-ExecutionPolicy) -eq "Restricted")
{
    Set-ExecutionPolicy -Scope Process -ExecutionPolicy RemoteSigned -Force
}

Write-Verbose "Starting Chocolatey installation.."
iex ((new-object net.webclient).DownloadString('https://chocolatey.org/install.ps1'))

# Install NodeJS
Write-Verbose "Starting NodeJS installation..."
cinst NodeJs -y

# Install Azure PowerShell
# Write-Verbose "Starting Azure Web Platform Installer..."
# cinst webpicmd -y
# webpicmd /Install /Products:"Microsoft Azure Powershell with Microsoft Azure Sdk" /AcceptEula

# Install Microsoft Azure Cross Platform Command Line
Write-Verbose "Starting Microsoft Azure Cross Platform Command Line installation"
npm install azure-cli -g
$npmpath = npm config get prefix
setx PATH "$env:path;$npmpath" -m

# Install WAWSDeploy
cinst wawsdeploy -y

Write-Verbose "Prerequisites script finished."
Write-Host -NoNewLine 'Press any key to finish...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');