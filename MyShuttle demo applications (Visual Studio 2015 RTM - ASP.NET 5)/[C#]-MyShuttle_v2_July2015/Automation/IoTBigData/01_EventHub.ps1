   
Param( 
    [Parameter(Mandatory = $true)]
    [ValidatePattern("^[a-z0-9]*$")]
    [String]$Path,                                  # required    needs to be alphanumeric    
    [Int]$PartitionCount = 8,                      # optional    default to 8
    [Int]$MessageRetentionInDays = 1,               # optional    default to 1
    [Parameter(Mandatory = $true)]
    [ValidatePattern("^[a-z0-9]*$")]
    [String]$Namespace,                             # required    needs to be alphanumeric
    [Bool]$CreateACSNamespace = $False,             # optional    default to $false
    [String]$Location = "West Europe",               # optional    default to "West Europe"
    [string][Parameter(Mandatory=$true)] $StorageAccountName,
    [string][Parameter(Mandatory=$true)] $WebSiteNameASPNET5

)
 
Switch-AzureMode AzureServiceManagement


# Set the output level to verbose and make the script stop on error
$VerbosePreference = "Continue"
$ErrorActionPreference = "Stop"

Write-Host "Verifying that Windows Azure credentials in the Windows PowerShell session have not expired."
Get-AzureWebsite | Out-Null

try
{
    # WARNING: Make sure to reference the latest version of Microsoft.ServiceBus.dll
    Write-Host "Adding the [Microsoft.ServiceBus.dll] assembly to the script..."
    $scriptPath = Split-Path (Get-Variable MyInvocation -Scope 0).Value.MyCommand.Path
    $packagesFolder = (Split-Path $scriptPath -Parent) + "\tools"
    $assembly = Get-ChildItem $packagesFolder -Include "Microsoft.ServiceBus.dll" -Recurse
    Add-Type -Path $assembly.FullName

    Write-Host "The [Microsoft.ServiceBus.dll] assembly has been successfully added to the script."
}

catch [System.Exception]
{
    Write-Error("Could not add the Microsoft.ServiceBus.dll assembly to the script. Make sure you build the solution before running the provisioning script.")
}

# Mark the start time of the script execution
$startTime = Get-Date

# Create Azure Service Bus namespace
$CurrentNamespace = Get-AzureSBNamespace -Name $Namespace

# Check if the namespace already exists or needs to be created
if ($CurrentNamespace)
{
    Write-Host "The namespace [$Namespace] already exists in the [$($CurrentNamespace.Region)] region." 
}
else
{
    Write-Host "The [$Namespace] namespace does not exist."
    Write-Host "Creating the [$Namespace] namespace in the [$Location] region..."
    New-AzureSBNamespace -Name $Namespace -Location $Location -CreateACSNamespace $CreateACSNamespace -NamespaceType Messaging
    $CurrentNamespace = Get-AzureSBNamespace -Name $Namespace
    Write-Host "The [$Namespace] namespace in the [$Location] region has been successfully created."
}

# Create the NamespaceManager object to create the event hub
Write-Host "Creating a NamespaceManager object for the [$Namespace] namespace..."
$NamespaceManager = [Microsoft.ServiceBus.NamespaceManager]::CreateFromConnectionString($CurrentNamespace.ConnectionString);
Write-Host "NamespaceManager object for the [$Namespace] namespace has been successfully created."

Start-Sleep -s 10

# Check if the event hub already exists
if ($NamespaceManager.EventHubExists($Path))
{
    Write-Host "The [$Path] event hub already exists in the [$Namespace] namespace." 
}
else
{
    Write-Host "Creating the [$Path] event hub in the [$Namespace] namespace: PartitionCount=[$PartitionCount] MessageRetentionInDays=[$MessageRetentionInDays]..."

    $EventHubDescription = New-Object -TypeName Microsoft.ServiceBus.Messaging.EventHubDescription -ArgumentList $Path 
    $EventHubDescription.PartitionCount = $PartitionCount
    $EventHubDescription.MessageRetentionInDays = $MessageRetentionInDays
    $EventHubDescription.UserMetadata = 'This event hub is used by the devices of the IoT solution'

    $EventHubDescription.Authorization.Add($global:EventHubSendRule)
    $EventHubDescription.Authorization.Add($global:EventHubListenRule)
    $EventHubDescription.Authorization.Add($global:eventHubFullRule)

    
    $NamespaceManager.CreateEventHub($EventHubDescription);
    
    Write-Host "The [$Path] event hub in the [$Namespace] namespace has been successfully created."

    Write-Host "Adding App settings to $WebSiteNameASPNET5"
    $StorageAccountKey = (Get-AzureStorageKey -StorageAccountName $StorageAccountName).Primary

    $MicrosoftServiceBusConnectionString="Endpoint=sb://$Namespace.servicebus.windows.net/;SharedAccessKeyName=Full;SharedAccessKey=" + $global:EventHubFullSharedAccessPolicyKey
    $AzureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=$StorageAccountName;AccountKey=$StorageAccountKey"

    $website = Get-AzureWebSite -Name $WebSiteNameASPNET5
    $appSettingsHash = $website.AppSettings
    $appSettingsHash.Add("Microsoft.ServiceBus.ConnectionString",$MicrosoftServiceBusConnectionString)
    $appSettingsHash.Add("AzureStorageConnectionString",$AzureStorageConnectionString)

    Set-AzureWebsite -Name $WebSiteNameASPNET5 -AppSettings $appSettingsHash -WebSocketsEnabled $true
}


# Create the consumer group if not exists
$ConsumerGroupName = "demoreceiver"
Write-Host "Creating the consumer group [$ConsumerGroupName] for the [$Path] event hub..."
$ConsumerGroupDescription = New-Object -TypeName Microsoft.ServiceBus.Messaging.ConsumerGroupDescription -ArgumentList $Path, $ConsumerGroupName
$ConsumerGroupDescription.UserMetadata = ""
$NamespaceManager.CreateConsumerGroupIfNotExists($ConsumerGroupDescription) | Out-Null
Write-Host "The consumer group [$ConsumerGroupName] for the [$Path] event hub has been successfully created."

# Create the consumer group if not exists
$ConsumerGroupName = "eventprocessor"
Write-Host "Creating the consumer group [$ConsumerGroupName] for the [$Path] event hub..."
$ConsumerGroupDescription = New-Object -TypeName Microsoft.ServiceBus.Messaging.ConsumerGroupDescription -ArgumentList $Path, $ConsumerGroupName
$ConsumerGroupDescription.UserMetadata = ""
$NamespaceManager.CreateConsumerGroupIfNotExists($ConsumerGroupDescription) | Out-Null
Write-Host "The consumer group [$ConsumerGroupName] for the [$Path] event hub has been successfully created."



# Mark the finish time of the script execution
$finishTime = Get-Date

# Output the time consumed in seconds
$TotalTime = ($finishTime - $startTime).TotalSeconds
Write-Host "The script completed in $TotalTime seconds."