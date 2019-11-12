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

Add-Type -Path ".\Tools\Microsoft.ServiceBus.dll"
Set-Location -Path .\IoTBigData
 
Switch-AzureMode AzureServiceManagement

$storageaccountname = $xml.env.storageaccount.name
$storageAccountKey = (Get-AzureStorageKey -StorageAccountName $storageaccountname).Primary

$global:EventHubFullSharedAccessPolicyKey = $null



[Microsoft.ServiceBus.Messaging.AccessRights[]]$AccessRights = New-Object -TypeName 'System.Collections.Generic.List[Microsoft.ServiceBus.Messaging.AccessRights]';
$AccessRights  += [Microsoft.ServiceBus.Messaging.AccessRights]::Send;
$Key = [Microsoft.ServiceBus.Messaging.SharedAccessAuthorizationRule]::GenerateRandomKey();
$eventHubSendRule = New-Object Microsoft.ServiceBus.Messaging.SharedAccessAuthorizationRule("Send", $Key, $AccessRights);

$global:EventHubSendRule = $eventHubSendRule

[Microsoft.ServiceBus.Messaging.AccessRights[]]$AccessRights = New-Object -TypeName 'System.Collections.Generic.List[Microsoft.ServiceBus.Messaging.AccessRights]';
$AccessRights  = $AccessRights + [Microsoft.ServiceBus.Messaging.AccessRights]::Listen;
$Key = [Microsoft.ServiceBus.Messaging.SharedAccessAuthorizationRule]::GenerateRandomKey();
$eventHubListenRule = New-Object Microsoft.ServiceBus.Messaging.SharedAccessAuthorizationRule("Listen", $Key, $AccessRights);

$global:EventHubListenRule = $eventHubListenRule


[Microsoft.ServiceBus.Messaging.AccessRights[]]$AccessRights = New-Object -TypeName 'System.Collections.Generic.List[Microsoft.ServiceBus.Messaging.AccessRights]';
$AccessRights  = $AccessRights + [Microsoft.ServiceBus.Messaging.AccessRights]::Send;
$AccessRights  = $AccessRights + [Microsoft.ServiceBus.Messaging.AccessRights]::Listen;
$AccessRights  = $AccessRights + [Microsoft.ServiceBus.Messaging.AccessRights]::Manage;
$Key = [Microsoft.ServiceBus.Messaging.SharedAccessAuthorizationRule]::GenerateRandomKey();
    
$global:EventHubFullSharedAccessPolicyKey = $Key
    
$eventHubFullRule = New-Object Microsoft.ServiceBus.Messaging.SharedAccessAuthorizationRule("Full", $Key, $AccessRights);
    
$global:EventHubFullRule = $eventHubFullRule

./01_EventHub.ps1 -Path $xml.env.eventhub.name `
                              -Namespace $xml.env.eventhub.namespace `
                              -Location $xml.env.resourcegroup.location  `
                              -WebSiteNameASPNET5 $xml.env.websiteaspnet.name `
                              -StorageAccountName $storageaccountname

./02_StreamAnalytics.ps1 -ResourceGroupName $xml.env.resourcegroup.name `
                          -StorageAccountKey $storageAccountKey `
                          -StorageAccountName $storageaccountname `
                          -EventHubNamespace $xml.env.eventhub.namespace `
                          -SharedAccessPolicyName "Full" `
                          -SharedAccessPolicyKey $global:EventHubFullSharedAccessPolicyKey `
                          -EventHubName $xml.env.eventhub.name `
                          -Name $xml.env.streamanalytics.name `
                          -Location $xml.env.streamanalytics.location

./03_HDInsight.ps1  -HDInsightClusterName $xml.env.hdinsight.name  -ClusterNodes $xml.env.hdinsight.clusternodes -HDInsightClusterUser $xml.env.hdinsight.username -HDInsightClusterPassword $xml.env.hdinsight.password -StorageAccountName $storageaccountname -Location $xml.env.resourcegroup.location

./04_DataFactory.ps1 -DataFactoryName $xml.env.datafactory.name -ResourceGroupName $xml.env.resourcegroup.name -StorageAccountName $storageaccountname -Location $xml.env.datafactory.location -SqlServerName $xml.env.sqlserver.name -SqlDatabaseName $xml.env.sqlserver.dbname -SqlServerAdminLogin $xml.env.sqlserver.login -SqlServerAdminPassword $xml.env.sqlserver.password -HDInsightClusterName $xml.env.hdinsight.name -HDInsightClusterUser $xml.env.hdinsight.username -HDInsightClusterPassword $xml.env.hdinsight.password

./05_DocumentDB.ps1 -ResourceGroupName $xml.env.resourcegroup.name -ResourceGroupLocation $xml.env.resourcegroup.location -DataBaseAccountName $xml.env.documentdb.name

./06_WebAPI_WebJobs.ps1 -ResourceGroupName $xml.env.resourcegroup.name -ResourceGroupLocation $xml.env.resourcegroup.location -SiteName $xml.env.websitewebapi.name -SqlServerName $xml.env.sqlserver.name -SqlServerAdminLogin $xml.env.sqlserver.login -SqlServerAdminPassword $xml.env.sqlserver.password -DocumentDBId $xml.env.documentdb.name -DocumentDBAccessKey "[KEY]" -HDInsightClusterName $xml.env.hdinsight.name -HDInsightClusterUser $xml.env.hdinsight.username -HDInsightClusterPassword $xml.env.hdinsight.password  -StorageAccountKey $storageAccountKey -StorageAccountName $storageaccountname `

Set-Location -Path ..

Write-Output "Script is complete."

# Mark the finish time of the script execution
$finishTime = Get-Date
# Output the time consumed in seconds
$TotalTime = ($finishTime - $startTime).TotalSeconds
Write-Output "Total time used (seconds): $TotalTime"