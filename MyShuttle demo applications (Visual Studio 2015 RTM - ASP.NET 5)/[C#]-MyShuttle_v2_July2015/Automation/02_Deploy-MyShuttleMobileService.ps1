Param( 
  [string][Parameter(Mandatory=$true)] $PublishSettingsFile
)

# The script has been tested on Powershell 3.0
Set-StrictMode -Version 3

# Set the output level to verbose and make the script stop on error
$VerbosePreference = "Continue"
$ErrorActionPreference = "Stop"

$startTime = Get-Date

Write-Output "Deploy started."

[Xml]$xml = Get-Content .\environment.xml
[Xml]$xmlsettings = Get-Content $PublishSettingsFile

$azuresubscriptionname = $xml.env.azuresubscription.name

Select-AzureSubscription -SubscriptionName $azuresubscriptionname
azure account set $azuresubscriptionname

WAWSDeploy .\WebPackages\MyShuttleMobileService.zip $PublishSettingsFile

Write-Output "Script is complete."

# Mark the finish time of the script execution
$finishTime = Get-Date
# Output the time consumed in seconds
$TotalTime = ($finishTime - $startTime).TotalSeconds
Write-Output "Total time used (seconds): $TotalTime"