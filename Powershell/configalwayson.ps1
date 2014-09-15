###########################################################
# SCRIPT Parameters
###########################################################

Param
(
    # Name of the server instances that will participate in the availability group
    [Parameter(Mandatory=$true)]
    [string[]] $ServerList,

    # The default port used by the endpoints, if we need to create them
    [string] $EndpointPort = 5022,

    # The name of the endpoint created on each server, if we need to create one
    [string] $EndpointName = "AlwaysOn_Endpoint"
)

###########################################################
# SCRIPT BODY
###########################################################

foreach ($server in $ServerList)
{
    # Connection to the server instance, using Windows authentication
    Write-Verbose "Creating SMO Server object for server: $server"
    $serverObject = New-Object Microsoft.SQLServer.Management.SMO.Server($server)

    # Enable AlwaysOn. We use the -Force option to force a server restart without confirmation.
    # This WILL result in your SQL Server instance restarting.
    Write-Verbose "Enabling AlwaysOn on server instance: $server"
    Enable-SqlAlwaysOn -InputObject $serverObject -Force

    # Check if the server already has a mirroring endpoint (note: a server can only have one)
    $endpointObject = $serverObject.Endpoints |
        Where-Object { $_.EndpointType -eq "DatabaseMirroring" } |
        Select-Object -First 1

    # Create an endpoint if one doesn't exist
    if($endpointObject -eq $null)
    {
        Write-Verbose "Creating endpoint '$EndpointName' on server instance: $server"
        $endpointObject = New-SqlHadrEndpoint -InputObject $serverObject -Name $EndpointName -Port $EndpointPort
    }
    else
    {
        Write-Verbose "An endpoint already exists on '$server', skipping endpoint creation."
    }

    # Start the endpoint
    Write-Verbose "Starting endpoint on server instance: $server"
    Set-SqlHadrEndpoint -InputObject $endpointObject -State "Started" | Out-Null
}


