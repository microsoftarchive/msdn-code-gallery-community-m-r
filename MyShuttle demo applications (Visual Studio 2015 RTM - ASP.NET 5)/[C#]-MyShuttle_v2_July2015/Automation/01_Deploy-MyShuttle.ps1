# The script has been tested on Powershell 3.0
Set-StrictMode -Version 3

# Set the output level to verbose and make the script stop on error
$VerbosePreference = "Continue"
$ErrorActionPreference = "Stop"

$startTime = Get-Date

Write-Output "Deploy started."

[Xml]$xml = Get-Content .\environment.xml

$azuresubscriptionname = $xml.env.azuresubscription.name

Select-AzureSubscription -SubscriptionName $azuresubscriptionname
azure account set $azuresubscriptionname

./Publish-MyShuttleResources.ps1 -AzureSubscriptionName $azuresubscriptionname  `
                                  -StorageAccountName $xml.env.storageaccount.name `
                                  -ResourceGroupName $xml.env.resourcegroup.name `
                                  -ResourceGroupLocation $xml.env.resourcegroup.location `
                                  -WebSiteNameASPNET5 $xml.env.websiteaspnet.name `
                                  -WebSiteNameMobileService $xml.env.websitemobileservice.name `
                                  -SqlServerName $xml.env.sqlserver.name `
                                  -SqlServerAdminLogin $xml.env.sqlserver.login `
                                  -SqlServerAdminPassword $xml.env.sqlserver.password `
                                  -WebSiteNameWebJob $xml.env.websitewebjob.name `
                                  -SharepointName $xml.env.sharepoint.name `
                                  -SharepointUsername $xml.env.sharepoint.username `
                                  -SharepointPassword $xml.env.sharepoint.password `
                                  -EventHubName $xml.env.eventhub.name `
                                  -SqlDbName $xml.env.sqlserver.dbname

Write-Output "Script is complete."

# Mark the finish time of the script execution
$finishTime = Get-Date
# Output the time consumed in seconds
$TotalTime = ($finishTime - $startTime).TotalSeconds
Write-Output "Total time used (seconds): $TotalTime"