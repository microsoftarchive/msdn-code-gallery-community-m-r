

Param( 
    [string] [Parameter(Mandatory=$true)] $ClusterName,
    [string][Parameter(Mandatory=$true)] $StorageAccountName
)


Switch-AzureMode AzureServiceManagement

# Parameters
$HqlScriptFile = "wasb://myshuttledata@" + $StorageAccountName + ".blob.core.windows.net/scripts/AggregatedAccelerometerTable.hql"
$InputParameter = "wasb://myshuttledata@" + $StorageAccountName + ".blob.core.windows.net/accelerometer"

Write-Output "Verifying that Windows Azure credentials in the Windows PowerShell session have not expired."
Get-AzureWebsite | Out-Null

# Mark the start time of the script execution
$startTime = Get-Date

Use-AzureHDInsightCluster $ClusterName
Invoke-Hive -File $HqlScriptFile -Defines @{"INPUT"=$InputParameter}

# Mark the finish time of the script execution
$finishTime = Get-Date

# Output the time consumed in seconds
$TotalTime = ($finishTime - $startTime).TotalSeconds
Write-Output "The script completed in $TotalTime seconds."
