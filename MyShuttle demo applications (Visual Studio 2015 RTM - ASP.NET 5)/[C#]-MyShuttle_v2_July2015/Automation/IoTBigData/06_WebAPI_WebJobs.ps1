#Requires -Version 3.0

Param( 
  [string][Parameter(Mandatory=$true)] $ResourceGroupLocation,
  [string][Parameter(Mandatory=$true)] $ResourceGroupName,
  [string][Parameter(Mandatory=$true)] $SiteName,
  [string][Parameter(Mandatory=$true)] $SqlServerName,
  [string][Parameter(Mandatory=$true)] $SqlServerAdminLogin,
  [string][Parameter(Mandatory=$true)] $SqlServerAdminPassword,
  [string][Parameter(Mandatory=$true)] $DocumentDBId,
  [string][Parameter(Mandatory=$true)] $DocumentDBAccessKey,
  [string][Parameter(Mandatory=$true)] $HDInsightClusterName,
  [string][Parameter(Mandatory=$true)] $HDInsightClusterUser,
  [string][Parameter(Mandatory=$true)] $HDInsightClusterPassword,
  [string][Parameter(Mandatory=$true)] $StorageAccountKey,
  [string][Parameter(Mandatory=$true)] $StorageAccountName,
  [string] $TemplateFile = '.\WebAPI\Microsoft.WebSite.0.2.6-preview.json'
)

Switch-AzureMode AzureServiceManagement

#Write-Output "Verifying that Windows Azure credentials in the Windows PowerShell session have not expired."
#Get-AzureWebsite | Out-Null

# Create or update the resource group using the specified template file and template parameters file
Switch-AzureMode AzureResourceManager
Write-Output "Creating $SiteName website in Resource Group $ResourceGroupName"
New-AzureResourceGroup -Name $ResourceGroupName `
                       -Location $ResourceGroupLocation `
                       -TemplateFile $TemplateFile `
                       -siteLocation $ResourceGroupLocation `
                       -hostingPlanName "hostingPlanName" `
                       -siteName $SiteName `
                       -Force

Switch-AzureMode AzureServiceManagement

Write-Output "Publishing Azure Website Project $SiteName"
Publish-AzureWebsiteProject -Name $SiteName -Package .\WebAPI\webapi.zip

# Adding Web Site app settings & connection strings
Switch-AzureMode AzureServiceManagement
Write-Output "Adding App settings to $SiteName"

$dbconnection = "data source=" + $SqlServerName + ".database.windows.net;initial catalog=MyShuttle;persist security info=True;user id=" + $SqlServerAdminLogin + "@" + $SqlServerName + ";password=" + $SqlServerAdminPassword + ";MultipleActiveResultSets=True"

function Get-Key {
  param
  (
    [System.String]
    $Verb,
 
    [System.String]
    $ResourceId = '',
 
    [System.String]
    $ResourceType,
 
    [HashTable]
    $Headers
  )
 
  $message = $($Verb + '\n' + $ResourceType + '\n' + $ResourceId +'\n' + $Headers.'x-ms-date' + '\n\n')
 
  $key = [System.Convert]::FromBase64String($DocumentDbKey)
 
  $hmacsha = new-object -TypeName System.Security.Cryptography.HMACSHA256 -ArgumentList (,$key) 
 
  $messageBytes =[Text.Encoding]::UTF8.GetBytes($message.ToLowerInvariant())
  $hash = $hmacsha.ComputeHash($messageBytes)
  $signature = [System.Convert]::ToBase64String($hash)
 
  return [System.Web.HttpUtility]::UrlEncode($('type=master&ver=1.0&sig=' + $signature))
}

$verb = 'POST'
$resourceType ='dbs'
$resourceId = [String]::Empty

$settings = @{ 
    "DocumentDb:EndpointUrl" = "https://" + $DocumentDBId + ".documents.azure.com:443/";
    "DocumentDb:AccessKey" = $DocumentDBAccessKey;
    "DocumentDb:DatabaseId" = $DocumentDBId;
    "Settings:DeleteDocuments" = "true";
    "HDinsight:EndpointUrl" = "https://" + $HDInsightClusterName + ".azurehdinsight.net";
    "HDinsight:Login" = $HDInsightClusterUser;
    "HDinsight:Password" = $HDInsightClusterPassword;
    } 

$website = Get-AzureWebSite -Name $SiteName 
$appSettingsHash = $website.AppSettings
if ($appSettingsHash.Count -lt 7)
{
    $ConnStringDefaultConnection = New-Object Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.ConnStringInfo
    $ConnStringDefaultConnection.Name = "MyShuttleDashboardContext"
    $ConnStringDefaultConnection.ConnectionString = $dbconnection
    $ConnStringDefaultConnection.Type = [Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.DatabaseType]::SQLAzure

    $StorageConnectionString = 'DefaultEndpointsProtocol=https;AccountName=' + $StorageAccountName + ';AccountKey=' + $StorageAccountKey
    $ConnStringInfoDashboard = New-Object Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.ConnStringInfo
    $ConnStringInfoDashboard.Name = "AzureWebJobsDashboard"
    $ConnStringInfoDashboard.ConnectionString = $StorageConnectionString
    $ConnStringInfoDashboard.Type = [Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.DatabaseType]::Custom

    $ConnStringStorage = New-Object Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.ConnStringInfo
    $ConnStringStorage.Name = "AzureWebJobsStorage"
    $ConnStringStorage.ConnectionString = $StorageConnectionString
    $ConnStringStorage.Type = [Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.DatabaseType]::Custom


    $CspBag = (Get-AzureWebsite -Name $SiteName).ConnectionStrings
    $CspBag.Add($ConnStringDefaultConnection)
    $CspBag.Add($ConnStringInfoDashboard)
    $CspBag.Add($ConnStringStorage)

    $error.clear() 
    Set-AzureWebsite -Name $SiteName -AppSettings $settings -ConnectionStrings $CspBag
    if ($error) {throw "Error: Call to Set-AzureWebsite with app settings failed."}
}

$StorageAccountContext = New-AzureStorageContext $StorageAccountName (Get-AzureStorageKey $StorageAccountName).Primary

if (!(Get-AzureStorageContainer -Context $StorageAccountContext | Where-Object { $_.Name -eq "drivingstyle-output" }))
{
    New-AzureStorageContainer -Permission Container -Name "drivingstyle-output" -Context $StorageAccountContext
}

Set-AzureStorageBlobContent -Blob "sample.csv" -Container "drivingstyle-output" -File ".\webapi\sample.csv" -Context $StorageAccountContext -Force

