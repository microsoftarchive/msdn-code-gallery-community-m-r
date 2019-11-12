
Param( 
    [string][Parameter(Mandatory=$true)] $ResourceGroupName,
    [string][Parameter(Mandatory=$true)] $StorageAccountKey,
    [string][Parameter(Mandatory=$true)] $StorageAccountName,
    [string][Parameter(Mandatory=$true)] $EventHubNamespace,
    [string][Parameter(Mandatory=$true)] $SharedAccessPolicyName,
    [string][Parameter(Mandatory=$true)] $SharedAccessPolicyKey,
    [string][Parameter(Mandatory=$true)] $EventHubName,
    [string][Parameter(Mandatory=$true)] $Name,
    [string][Parameter(Mandatory=$true)] $Location
)

Switch-AzureMode AzureServiceManagement

# Set the output level to verbose and make the script stop on error
$VerbosePreference = "Continue"
$ErrorActionPreference = "Stop"

Write-Output "Verifying that Windows Azure credentials in the Windows PowerShell session have not expired."
Get-AzureWebsite | Out-Null

# Mark the start time of the script execution
$startTime = Get-Date
$StorageAccountContext = New-AzureStorageContext $StorageAccountName (Get-AzureStorageKey $StorageAccountName).Primary

if (!(Get-AzureStorageContainer -Context $StorageAccountContext | Where-Object { $_.Name -eq "myshuttledata" }))
{
    New-AzureStorageContainer -Permission Container -Name "myshuttledata" -Context $StorageAccountContext
}

Set-AzureStorageBlobContent -Blob "vehicles\vehicles.csv" -Container "myshuttledata" -File ".\StreamAnalytics\Vehicles.csv" -Context $StorageAccountContext -Force

Switch-AzureMode AzureResourceManager

function ParseAndCreate-AzureStreamAnalyticsJob {
  param
  (
    [System.String]$file,
    [System.String]$name
  )  
 
  (Get-Content $file) `
    -replace '\(\$storageaccountkey\)', $StorageAccountKey `
    -replace '\(\$storageaccountname\)', $StorageAccountName `
    -replace '\(\$eventhubnamespace\)', $EventHubNamespace `
    -replace '\(\$sharedAccessPolicyName\)', $SharedAccessPolicyName `
    -replace '\(\$sharedAccessPolicyKey\)', $SharedAccessPolicyKey `
    -replace '\(\$location\)', $Location `
    -replace '\(\$eventhubname\)', $EventHubName |
  Out-File ".\StreamAnalytics\temp.json"
 
  New-AzureStreamAnalyticsJob -ResourceGroupName $ResourceGroupName –File ".\StreamAnalytics\temp.json" -Name $name -Force
  Start-AzureStreamAnalyticsJob -ResourceGroupName $ResourceGroupName -Name $name
  
  
  Remove-Item ".\StreamAnalytics\temp.json"

}

Try
{
  ParseAndCreate-AzureStreamAnalyticsJob ".\StreamAnalytics\MyShuttle-Accelerometer.json" "$Name-Accelerometer"
  ParseAndCreate-AzureStreamAnalyticsJob ".\StreamAnalytics\MyShuttle-Compass.json" "$Name-Compass"
  ParseAndCreate-AzureStreamAnalyticsJob ".\StreamAnalytics\MyShuttle-OBD.json" "$Name-OBD"
  ParseAndCreate-AzureStreamAnalyticsJob ".\StreamAnalytics\MyShuttle-OBD-SecurityBelt.json" "$Name-OBD-SecurityBelt"
  ParseAndCreate-AzureStreamAnalyticsJob ".\StreamAnalytics\MyShuttle-Rfid.json" "$Name-Rfid"
}
Catch
{
 
}

# Mark the finish time of the script execution
$finishTime = Get-Date

# Output the time consumed in seconds
$TotalTime = ($finishTime - $startTime).TotalSeconds
Write-Output "The script completed in $TotalTime seconds."
