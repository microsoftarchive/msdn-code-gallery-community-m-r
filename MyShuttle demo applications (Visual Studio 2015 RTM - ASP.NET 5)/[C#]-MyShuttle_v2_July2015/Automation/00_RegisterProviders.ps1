
[Xml]$xml = Get-Content .\environment.xml

$azuresubscriptionname = $xml.env.azuresubscription.name
$subscription = (Get-AzureSubscription -SubscriptionName $azuresubscriptionname)
$subscriptionId = $subscription.SubscriptionId
$tenantId = $subscription.TenantId

# Set well-known client ID and redirectUrl for Azure PowerShell
$clientId = '1950a258-227b-4e31-a9cf-717495945fc2'
$redirectUri = "urn:ietf:wg:oauth:2.0:oob"

$authUrl = "https://login.windows.net/${tenantId}"
$AuthContext = [Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext]$authUrl

$result = $AuthContext.AcquireToken("https://management.core.windows.net/",
        $clientId,
        [Uri]$redirectUri,
        [Microsoft.IdentityModel.Clients.ActiveDirectory.PromptBehavior]::Auto)
 
$authHeader = @{
'Content-Type'='application\json'
'Authorization'=$result.CreateAuthorizationHeader()
}

Invoke-RestMethod -Uri "https://management.azure.com/subscriptions/$subscriptionId/providers/Microsoft.DataFactory/register?api-version=2014-04-01-preview" -Method Post -Headers $authHeader -Verbose
Invoke-RestMethod -Uri "https://management.azure.com/subscriptions/$subscriptionId/providers/Microsoft.DocumentDB/register?api-version=2014-04-01-preview" -Method Post -Headers $authHeader -Verbose

Write-Output (Invoke-RestMethod -Uri "https://management.azure.com/subscriptions/${subscriptionId}/providers?api-version=2014-04-01-preview" -Headers $authHeader -Method Get -Verbose).Value
