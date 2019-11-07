
Param( 
    [string][Parameter(Mandatory=$true)] $DataFactoryName,
    [string][Parameter(Mandatory=$true)] $ResourceGroupName,
    [string][Parameter(Mandatory=$true)] $StorageAccountName,
    [string][Parameter(Mandatory=$true)] $SqlServerName,
    [string][Parameter(Mandatory=$true)] $SqlDatabaseName,
    [string][Parameter(Mandatory=$true)] $SqlServerAdminLogin,
    [string][Parameter(Mandatory=$true)] $SqlServerAdminPassword,
    [string][Parameter(Mandatory=$true)] $HDInsightClusterName,
    [string][Parameter(Mandatory=$true)] $HDInsightClusterUser,
    [string][Parameter(Mandatory=$true)] $HDInsightClusterPassword,
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
Set-AzureStorageBlobContent -Blob "scripts\AggregatedAccelerometerTable.hql" -Container "myshuttledata" -File ".\DataFactory\AggregatedAccelerometerTable.hql" -Context $StorageAccountContext -Force
Set-AzureStorageBlobContent -Blob "scripts\SecurityBeltWarnings.hql" -Container "myshuttledata" -File ".\DataFactory\SecurityBeltWarnings.hql" -Context $StorageAccountContext -Force

Switch-AzureMode AzureResourceManager

New-AzureDataFactory -ResourceGroupName $ResourceGroupName -Name $DataFactoryName -Location $Location -Force

Write-Host 'StorageLinkedService'

$StorageLinkedServiceFile = ".\DataFactory\StorageLinkedService.json"
$StorageLinkedServiceOutputFile = ".\DataFactory\StorageLinkedService_output.json"
(gc $StorageLinkedServiceFile).replace('[AccountName]',$StorageAccountName)|sc $StorageLinkedServiceOutputFile
(gc $StorageLinkedServiceOutputFile).replace('[AccountKey]', $StorageAccountKey)|sc $StorageLinkedServiceOutputFile
New-AzureDataFactoryLinkedService -ResourceGroupName $ResourceGroupName -DataFactoryName $DataFactoryName -File ".\DataFactory\StorageLinkedService_output.json" -Force

Write-Host 'AzureSqlLinkedService'

$AzureSqlLinkedServiceFile = ".\DataFactory\AzureSqlLinkedService.json"
$AzureSqlLinkedServiceOutputFile = ".\DataFactory\AzureSqlLinkedService_output.json"
(gc $AzureSqlLinkedServiceFile).replace('[Server]',$SqlServerName)|sc $AzureSqlLinkedServiceOutputFile
(gc $AzureSqlLinkedServiceOutputFile).replace('[DatabaseName]',$SqlDatabaseName)|sc $AzureSqlLinkedServiceOutputFile
(gc $AzureSqlLinkedServiceOutputFile).replace('[User]',$SqlServerAdminLogin)|sc $AzureSqlLinkedServiceOutputFile
(gc $AzureSqlLinkedServiceOutputFile).replace('[Password]',$SqlServerAdminPassword)|sc $AzureSqlLinkedServiceOutputFile
New-AzureDataFactoryLinkedService -ResourceGroupName $ResourceGroupName -DataFactoryName $DataFactoryName -File $AzureSqlLinkedServiceOutputFile -Force

New-AzureDataFactoryLinkedService -ResourceGroupName $ResourceGroupName -DataFactoryName $DataFactoryName -File ".\DataFactory\AzureSqlLinkedService_output.json" -Force


Write-Host 'MyHDInsightClusterLinkedService'

$MyHDInsightClusterLinkedServiceFile = ".\DataFactory\MyHDInsightClusterLinkedService.json"
$MyHDInsightClusterLinkedServiceOutputFile = ".\DataFactory\MyHDInsightClusterLinkedService_output.json"
(gc $MyHDInsightClusterLinkedServiceFile).replace('[Name]',$HDInsightClusterName)|sc $MyHDInsightClusterLinkedServiceOutputFile
(gc $MyHDInsightClusterLinkedServiceOutputFile).replace('[User]',$HDInsightClusterUser)|sc $MyHDInsightClusterLinkedServiceOutputFile
(gc $MyHDInsightClusterLinkedServiceOutputFile).replace('[Password]',$HDInsightClusterPassword)|sc $MyHDInsightClusterLinkedServiceOutputFile
New-AzureDataFactoryLinkedService -ResourceGroupName $ResourceGroupName -DataFactoryName $DataFactoryName -File $MyHDInsightClusterLinkedServiceOutputFile -Force

Write-Host 'HDInsightOnDemandClusterLinkedService'
New-AzureDataFactoryLinkedService -ResourceGroupName $ResourceGroupName -DataFactoryName $DataFactoryName -File ".\DataFactory\HDInsightOnDemandClusterLinkedService.json" -Force

Write-Host 'AzureSQLTableOutput'
New-AzureDataFactoryTable  -ResourceGroupName $ResourceGroupName -DataFactoryName $DataFactoryName –File ".\DataFactory\AzureSQLTableOutput.json" -Force

Write-Host 'HiveInputBlobTable'
New-AzureDataFactoryTable  -ResourceGroupName $ResourceGroupName -DataFactoryName $DataFactoryName –File ".\DataFactory\HiveInputBlobTable.json" -Force

Write-Host 'HiveOutputBlobTable'
New-AzureDataFactoryTable  -ResourceGroupName $ResourceGroupName -DataFactoryName $DataFactoryName –File ".\DataFactory\HiveOutputBlobTable.json" -Force

Write-Host 'HiveAccelerometerAggregateOutputBlobTable'
New-AzureDataFactoryTable  -ResourceGroupName $ResourceGroupName -DataFactoryName $DataFactoryName –File ".\DataFactory\HiveAccelerometerAggregateOutputBlobTable.json" -Force

Write-Host 'SecurityBeltWarningsPipeline'
$SecurityBeltWarningsPipelineFile = ".\DataFactory\SecurityBeltWarningsPipeline.json"
$SecurityBeltWarningsPipelineOutputFile = ".\DataFactory\SecurityBeltWarningsPipeline_output.json"
(gc $SecurityBeltWarningsPipelineFile).replace('[StorageName]',$StorageAccountName)|sc $SecurityBeltWarningsPipelineOutputFile
New-AzureDataFactoryPipeline  -ResourceGroupName $ResourceGroupName -DataFactoryName $DataFactoryName -File $SecurityBeltWarningsPipelineOutputFile -Force

Write-Host 'AccelerometerAggregatePipeline'
$AccelerometerAggregatePipelineFile = ".\DataFactory\AccelerometerAggregatePipeline.json"
$AccelerometerAggregatePipelineOutputFile = ".\DataFactory\AccelerometerAggregatePipeline_output.json"
(gc $AccelerometerAggregatePipelineFile).replace('[StorageName]',$StorageAccountName)|sc $AccelerometerAggregatePipelineOutputFile
New-AzureDataFactoryPipeline  -ResourceGroupName $ResourceGroupName -DataFactoryName $DataFactoryName -File $AccelerometerAggregatePipelineOutputFile -Force

$StarDateTime = (get-date).AddDays(-1).ToString("u")
$EndDateTime = (get-date).AddDays(30).ToString("u")

Write-Host 'AzureDataFactoryPipelineActivePeriod'
Set-AzureDataFactoryPipelineActivePeriod -ResourceGroupName $ResourceGroupName -DataFactoryName $DataFactoryName -StartDateTime $StarDateTime –EndDateTime $EndDateTime –Name SecurityBeltWarningsPipeline -Force
Set-AzureDataFactoryPipelineActivePeriod -ResourceGroupName $ResourceGroupName -DataFactoryName $DataFactoryName -StartDateTime $StarDateTime –EndDateTime $EndDateTime –Name AccelerometerAggregatePipeline -Force


# Mark the finish time of the script execution
$finishTime = Get-Date

# Output the time consumed in seconds
$TotalTime = ($finishTime - $startTime).TotalSeconds
Write-Output "The script completed in $TotalTime seconds."
