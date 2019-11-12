#Requires -Version 3.0

Param( 
  [string][Parameter(Mandatory=$true)] $AzureSubscriptionName,
  [string][Parameter(Mandatory=$true)] $StorageAccountName,
  [string][Parameter(Mandatory=$true)] $ResourceGroupLocation,
  [string][Parameter(Mandatory=$true)] $WebSiteNameASPNET5,
  [string][Parameter(Mandatory=$true)] $WebSiteNameWebJob,
  [string][Parameter(Mandatory=$true)] $WebsiteNameMobileService,
  [string][Parameter(Mandatory=$true)] $SqlServerName,
  [string][Parameter(Mandatory=$true)] $SqlServerAdminLogin,
  [string][Parameter(Mandatory=$true)] $SqlServerAdminPassword,
  [string][Parameter(Mandatory=$true)] $SharepointName,
  [string][Parameter(Mandatory=$true)] $SharepointUsername,
  [string][Parameter(Mandatory=$true)] $SharepointPassword,
  [string][Parameter(Mandatory=$true)] $ResourceGroupName,
  [string][Parameter(Mandatory=$true)] $SqlDbName,
  [string] $WebSiteLocation = $ResourceGroupLocation,
  [string] $WebSiteHostingPlanName = 'MyShuttleHostingPlan',
  [string] $SqlServerLocation = $WebSiteLocation,
  [string] $StorageContainerName = $ResourceGroupName.ToLowerInvariant(),
  [string] $TemplateFile = '.\Templates\Microsoft.WebSiteSQLDatabase.0.2.6-preview.json',
  [string] $TemplateFileWebJob = '.\Templates\Microsoft.WebSite.0.2.14-preview.json',
  [string][Parameter(Mandatory=$true)] $EventHubName,
  [string] $ConsumerGroup = 'EventProcessor', 
  [string] $SignalRHubName = 'MyShuttleHub' 
)

$SqlServerName = $SqlServerName.ToLowerInvariant()

if ($SqlServerAdminPassword) {
    $SqlServerAdminSecretPassword = $SqlServerAdminPassword | ConvertTo-SecureString -AsPlainText -Force
}

Switch-AzureMode AzureServiceManagement

Write-Output "Verifying that Windows Azure credentials in the Windows PowerShell session have not expired."
Get-AzureWebsite | Out-Null

# Create Storage Account
if (!(Test-AzureName -Storage $StorageAccountName))
{  
    Write-Output "Creating Storage Account $StorageAccountName."
    New-AzureStorageAccount -StorageAccountName $StorageAccountName -Label $StorageAccountName -Location $ResourceGroupLocation
    Write-Output "Uploading Azure Storage Blob Content."
    Set-AzureSubscription -SubscriptionName $AzureSubscriptionName -CurrentStorageAccount $StorageAccountName
    $storageAccountKey = (Get-AzureStorageKey -StorageAccountName $StorageAccountName).Primary
    $storageAccountContext = New-AzureStorageContext $StorageAccountName (Get-AzureStorageKey $StorageAccountName).Primary
    $newContainer = New-AzureStorageContainer -Permission Container -Name "myshuttleinvoice"
    Set-AzureStorageBlobContent -Blob "invoiceform.pdf" -Container "myshuttleinvoice" -File ".\Documents\invoiceform.pdf" -Context $storageAccountContext -Force
}

# Convert relative paths to absolute paths if needed
$TemplateFile = [System.IO.Path]::Combine($PSScriptRoot, $TemplateFile)
$TemplateFileWebJob = [System.IO.Path]::Combine($PSScriptRoot, $TemplateFileWebJob)

$storageAccountKey = (Get-AzureStorageKey -StorageAccountName $StorageAccountName).Primary
$storageAccountContext = New-AzureStorageContext $StorageAccountName (Get-AzureStorageKey $StorageAccountName).Primary

# Create or update the resource group using the specified template file and template parameters file
Switch-AzureMode AzureResourceManager
Write-Output "Creating $WebSiteNameASPNET5 WebSite & SQL in Resource Group $ResourceGroupName"
New-AzureResourceGroup -Name $ResourceGroupName `
                       -Location $ResourceGroupLocation `
                       -TemplateFile $TemplateFile `
                       -siteLocation $WebSiteLocation `
                       -hostingPlanName $WebSiteHostingPlanName `
                       -serverLocation $SqlServerLocation `
                       -siteName $WebSiteNameASPNET5 `
                       -serverName $SqlServerName `
                       -administratorLogin $SqlServerAdminLogin `
                       -administratorLoginPassword $SqlServerAdminSecretPassword `
                       -databaseName $SqlDbName `
                       -Force
                       
# Create or update the resource group

Write-Output "Creating WebSiteNameWebJob WebSite in Resource Group $ResourceGroupName"
New-AzureResourceGroup -Name $ResourceGroupName `
                       -Location $ResourceGroupLocation `
                       -TemplateFile $TemplateFileWebJob `
                       -siteName $WebSiteNameWebJob `
                       -hostingPlanName $WebSiteHostingPlanName `
                       -siteLocation $WebSiteLocation `
                       -Force

Switch-AzureMode AzureServiceManagement

Write-Output "Publishing Azure Website Project $WebSiteNameASPNET5"
Publish-AzureWebsiteProject -Name $WebSiteNameASPNET5 -Package .\WebPackages\MyShuttleWeb.zip
Publish-AzureWebsiteProject -Name $WebSiteNameWebJob -Package .\WebPackages\WebJob.zip

# Adding Web Site app settings & connection strings
Switch-AzureMode AzureServiceManagement
Write-Output "Adding App settings to $WebSiteNameASPNET5"

$dbconnection = "data source=" + $SqlServerName + ".database.windows.net;initial catalog=MyShuttle;persist security info=True;user id=" + $SqlServerAdminLogin + "@" + $SqlServerName + ";password=" + $SqlServerAdminPassword + ";MultipleActiveResultSets=True"

$settings = @{ 
    "EventHubName" = $EventHubName;
    "ConsumerGroup" = $ConsumerGroup;
    "SignalRHubName" = $SignalRHubName;
    "SignalRUrl" = "http://" + $WebSiteNameASPNET5 + ".azurewebsites.net/web";
    } 

$StorageConnectionString = 'DefaultEndpointsProtocol=https;AccountName=' + $StorageAccountName + ';AccountKey=' + $storageAccountKey

$ConnStringInfoDashboard = New-Object Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.ConnStringInfo
$ConnStringInfoDashboard.Name = "AzureWebJobsDashboard"
$ConnStringInfoDashboard.ConnectionString = $StorageConnectionString
$ConnStringInfoDashboard.Type = [Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.DatabaseType]::Custom

$ConnStringInfoStorage = New-Object Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.ConnStringInfo
$ConnStringInfoStorage.Name = "AzureWebJobsStorage"
$ConnStringInfoStorage.ConnectionString = $StorageConnectionString
$ConnStringInfoStorage.Type = [Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.DatabaseType]::Custom

$ConnStringDefaultConnection = New-Object Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.ConnStringInfo
$ConnStringDefaultConnection.Name = "DefaultConnection"
$ConnStringDefaultConnection.ConnectionString = $dbconnection
$ConnStringDefaultConnection.Type = [Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.DatabaseType]::SQLAzure

$CspBag = (Get-AzureWebsite -Name $WebSiteNameASPNET5).ConnectionStrings
if ($CspBag.Count -eq 0)
{
    $CspBag.Add($ConnStringDefaultConnection)
    $CspBag.Add($ConnStringInfoDashboard)
    $CspBag.Add($ConnStringInfoStorage)
}

$error.clear() 
Set-AzureWebsite -Name $WebSiteNameASPNET5 -AppSettings $settings -ConnectionStrings $CspBag
if ($error) {throw "Error: Call to Set-AzureWebsite with app settings failed."}

# Add app WebJob app setting & connection strings
Write-Output "Adding App settings to $WebSiteNameWebJob"
$settings = @{ 
    "pdf::invoiceform" = "http://" + $StorageAccountName + ".blob.core.windows.net/myshuttleinvoice/invoiceform.pdf"; 
    "invoicequeuename" = "invoicequeue";
	"SharePoint::webUrl" = "https://" + $SharepointName + ".sharepoint.com/";
	"SharePoint::userName" = $SharepointUsername;
	"SharePoint::password" = $SharepointPassword;
	 } 

# Add connection strings
$StorageConnectionString = 'DefaultEndpointsProtocol=https;AccountName=' + $StorageAccountName + ';AccountKey=' + $storageAccountKey

$CspBag = (Get-AzureWebsite -Name $WebSiteNameWebJob).ConnectionStrings
if ($CspBag.Count -eq 0)
{
    $CspBag.Add($ConnStringInfoDashboard)
    $CspBag.Add($ConnStringInfoStorage)
}
 
$error.clear()
Set-AzureWebsite -Name $WebSiteNameWebJob -AppSettings $settings -ConnectionStrings $CspBag
if ($error) {throw "Error: Call to Set-AzureWebsite with database connection strings failed."}

Write-Output "Creating mobile service"
azure config mode asm
try {
azure mobile create $WebsiteNameMobileService $SqlServerAdminLogin $SqlServerAdminPassword -r $SqlServerName -d $SqlDbName -b dotnet
$settingmobiledb = "Data Source=" + $SqlServerName + ".database.windows.net;Initial Catalog=MyShuttle;User ID=" + $SqlServerAdminLogin + ";Password=" + $SqlServerAdminPassword + ";Asynchronous Processing=True;TrustServerCertificate=False;"
azure mobile appsetting add $WebsiteNameMobileService "MS_TableConnectionString" $settingmobiledb
azure mobile appsetting add $WebsiteNameMobileService "AzureWebJobsStorage" $StorageConnectionString
}
catch {}