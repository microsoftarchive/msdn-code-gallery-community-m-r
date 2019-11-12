#Requires -Version 3.0

Param( 
  [string][Parameter(Mandatory=$true)] $ResourceGroupLocation,
  [string][Parameter(Mandatory=$true)] $DatabaseAccountName,
  [string][Parameter(Mandatory=$true)] $ResourceGroupName,
  [string] $TemplateFile = '.\DocumentDB\Microsoft.DocumentDB.0.4.0-preview.json'
)

Switch-AzureMode AzureServiceManagement

Write-Output "Verifying that Windows Azure credentials in the Windows PowerShell session have not expired."
Get-AzureWebsite | Out-Null

# Create or update the resource group using the specified template file and template parameters file
Switch-AzureMode AzureResourceManager
Write-Output "Creating $DatabaseAccountName DocumentDB in Resource Group $ResourceGroupName"
New-AzureResourceGroup -Name $ResourceGroupName `
                       -location $ResourceGroupLocation `
                       -TemplateFile $TemplateFile `
                       -databaseAccountName $DatabaseAccountName `
                       -locationFromTemplate $ResourceGroupLocation `
                       -capacityUnits 1 `
                       -Force
                       

