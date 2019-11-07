
Param( 
    [string][Parameter(Mandatory=$true)] $HDInsightClusterName,
    [int]   [Parameter(Mandatory=$true)] $ClusterNodes,
    [string][Parameter(Mandatory=$true)] $HDInsightClusterUser,
    [string][Parameter(Mandatory=$true)] $HDInsightClusterPassword,
    [string][Parameter(Mandatory=$true)] $StorageAccountName,
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


$StorageAccountKey = (Get-AzureStorageKey -StorageAccountName $StorageAccountName).Primary
$StorageAccountContext = New-AzureStorageContext $StorageAccountName (Get-AzureStorageKey $StorageAccountName).Primary
$ContainerName = $HDInsightClusterName
$HDInsightClusterScretPassword = $HDInsightClusterPassword | ConvertTo-SecureString -AsPlainText -Force
$Credentials = new-object  -TypeName System.Management.Automation.PSCredential -ArgumentList $HDInsightClusterUser,$HDInsightClusterScretPassword

Start-Sleep -s 10

if (!(Get-AzureHDInsightCluster -Name $HDInsightClusterName))
{
	# Create a new HDInsight cluster
	New-AzureHDInsightCluster -Name $HDInsightClusterName -Location $Location -DefaultStorageAccountName "$StorageAccountName.blob.core.windows.net" -DefaultStorageAccountKey $StorageAccountKey -DefaultStorageContainerName $ContainerName  -ClusterSizeInNodes $ClusterNodes -Credential $Credentials 
}

if (!(Get-AzureStorageContainer -Context $StorageAccountContext | Where-Object { $_.Name -eq "myshuttledata" }))
{
    New-AzureStorageContainer -Permission Container -Name "myshuttledata" -Context $StorageAccountContext
}

if (!(Get-AzureStorageContainer -Context $StorageAccountContext | Where-Object { $_.Name -eq "drivingstyle-output" }))
{
    New-AzureStorageContainer -Permission Container -Name "drivingstyle-output" -Context $StorageAccountContext
}


Set-AzureStorageBlobContent -Blob "tripdata\tripdatademo.csv" -Container "myshuttledata" -File ".\HDInsight\tripdatademo.csv" -Context $StorageAccountContext -Force
Set-AzureStorageBlobContent -Blob "OBD\OBD.csv" -Container "myshuttledata" -File ".\HDInsight\OBD.csv" -Context $StorageAccountContext -Force
Set-AzureStorageBlobContent -Blob "OBD-SecurityBelt\OBD-SecurityBelt.csv" -Container "myshuttledata" -File ".\HDInsight\OBD-SecurityBelt.csv" -Context $StorageAccountContext -Force
Set-AzureStorageBlobContent -Blob "rfid\rfid.csv" -Container "myshuttledata" -File ".\HDInsight\rfid.csv" -Context $StorageAccountContext -Force
Set-AzureStorageBlobContent -Blob "compass\compass.csv" -Container "myshuttledata" -File ".\HDInsight\compass.csv" -Context $StorageAccountContext -Force
Set-AzureStorageBlobContent -Blob "classification\classified.csv" -Container "myshuttledata" -File ".\HDInsight\classified.csv" -Context $StorageAccountContext -Force
Set-AzureStorageBlobContent -Blob "drivingstyle.csv" -Container "drivingstyle-output" -File ".\HDInsight\drivingstyle.csv" -Context $StorageAccountContext -Force

$blobs = (Get-AzureStorageBlob -Blob accelerometer* -Container "myshuttledata" -Context $StorageAccountContext)
if (!$blobs -or $blobs.Length -lt 10)
{
    Set-AzureStorageBlobContent -Blob "accelerometer\20150203 fake accelerometer G unique 22.csv" -Container "myshuttledata" -File ".\HDInsight\20150203 fake accelerometer G unique 22.csv" -Context $StorageAccountContext -Force
    Set-AzureStorageBlobContent -Blob "accelerometer\20150204 fake accelerometer G unique 23.csv" -Container "myshuttledata" -File ".\HDInsight\20150204 fake accelerometer G unique 23.csv" -Context $StorageAccountContext -Force
    Set-AzureStorageBlobContent -Blob "accelerometer\20150204 fake accelerometer G unique 24.csv" -Container "myshuttledata" -File ".\HDInsight\20150204 fake accelerometer G unique 24.csv" -Context $StorageAccountContext -Force
    Set-AzureStorageBlobContent -Blob "accelerometer\20150204 fake accelerometer G unique 25.csv" -Container "myshuttledata" -File ".\HDInsight\20150204 fake accelerometer G unique 25.csv" -Context $StorageAccountContext -Force
    Set-AzureStorageBlobContent -Blob "accelerometer\20150204 fake accelerometer G unique 26.csv" -Container "myshuttledata" -File ".\HDInsight\20150204 fake accelerometer G unique 26.csv" -Context $StorageAccountContext -Force
    Set-AzureStorageBlobContent -Blob "accelerometer\20150204 fake accelerometer G unique 27.csv" -Container "myshuttledata" -File ".\HDInsight\20150204 fake accelerometer G unique 27.csv" -Context $StorageAccountContext -Force
    Set-AzureStorageBlobContent -Blob "accelerometer\20150204 fake accelerometer G unique 28.csv" -Container "myshuttledata" -File ".\HDInsight\20150204 fake accelerometer G unique 28.csv" -Context $StorageAccountContext -Force
    Set-AzureStorageBlobContent -Blob "accelerometer\20150204 fake accelerometer G unique 29.csv" -Container "myshuttledata" -File ".\HDInsight\20150204 fake accelerometer G unique 29.csv" -Context $StorageAccountContext -Force
    Set-AzureStorageBlobContent -Blob "accelerometer\20150204 fake accelerometer G unique 30.csv" -Container "myshuttledata" -File ".\HDInsight\20150204 fake accelerometer G unique 30.csv" -Context $StorageAccountContext -Force
    Set-AzureStorageBlobContent -Blob "accelerometer\20150204 fake accelerometer G unique 31.csv" -Container "myshuttledata" -File ".\HDInsight\20150204 fake accelerometer G unique 31.csv" -Context $StorageAccountContext -Force
}


#Summit Hive Job
$CreateTablesFile = ".\HDInsight\CreateTables.hql"
$CreateTablesOutputFile = ".\HDInsight\CreateTables_output.hql"
(gc $CreateTablesFile).replace('[StorageAccountName]',$StorageAccountName)|sc $CreateTablesOutputFile
$hivequerys = Get-Content $CreateTablesOutputFile -Raw
Use-AzureHDInsightCluster -Name $HDInsightClusterName
Invoke-Hive $hivequerys


# Mark the finish Time of the script execution
$finishTime = Get-Date

# Output the time consumed in seconds
$TotalTime = ($finishTime - $startTime).TotalSeconds
Write-Output "The script completed in $TotalTime seconds."
