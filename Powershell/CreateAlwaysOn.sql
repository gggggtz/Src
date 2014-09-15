--- YOU MUST EXECUTE THE FOLLOWING SCRIPT IN SQLCMD MODE.
:Connect 10.0.0.50

IF (SELECT state FROM sys.endpoints WHERE name = N'AlwaysOn_EndPoint') <> 0
BEGIN
	ALTER ENDPOINT [AlwaysOn_EndPoint] STATE = STARTED
END


GO

use [master]

GO

GRANT CONNECT ON ENDPOINT::[AlwaysOn_EndPoint] TO [LAB\admin]

GO

:Connect 10.0.0.58\SQLINSTANCE1

IF (SELECT state FROM sys.endpoints WHERE name = N'AlwaysOn_EndPoint') <> 0
BEGIN
	ALTER ENDPOINT [AlwaysOn_EndPoint] STATE = STARTED
END


GO

use [master]

GO

GRANT CONNECT ON ENDPOINT::[AlwaysOn_EndPoint] TO [LAB\admin]

GO

:Connect 10.0.0.54\SQLINSTANCE1

IF (SELECT state FROM sys.endpoints WHERE name = N'AlwaysOn_EndPoint') <> 0
BEGIN
	ALTER ENDPOINT [AlwaysOn_EndPoint] STATE = STARTED
END


GO

use [master]

GO

GRANT CONNECT ON ENDPOINT::[AlwaysOn_EndPoint] TO [LAB\admin]

GO

:Connect 10.0.0.50

IF EXISTS(SELECT * FROM sys.server_event_sessions WHERE name='AlwaysOn_health')
BEGIN
  ALTER EVENT SESSION [AlwaysOn_health] ON SERVER WITH (STARTUP_STATE=ON);
END
IF NOT EXISTS(SELECT * FROM sys.dm_xe_sessions WHERE name='AlwaysOn_health')
BEGIN
  ALTER EVENT SESSION [AlwaysOn_health] ON SERVER STATE=START;
END

GO

:Connect 10.0.0.58\SQLINSTANCE1

IF EXISTS(SELECT * FROM sys.server_event_sessions WHERE name='AlwaysOn_health')
BEGIN
  ALTER EVENT SESSION [AlwaysOn_health] ON SERVER WITH (STARTUP_STATE=ON);
END
IF NOT EXISTS(SELECT * FROM sys.dm_xe_sessions WHERE name='AlwaysOn_health')
BEGIN
  ALTER EVENT SESSION [AlwaysOn_health] ON SERVER STATE=START;
END

GO

:Connect 10.0.0.54\SQLINSTANCE1

IF EXISTS(SELECT * FROM sys.server_event_sessions WHERE name='AlwaysOn_health')
BEGIN
  ALTER EVENT SESSION [AlwaysOn_health] ON SERVER WITH (STARTUP_STATE=ON);
END
IF NOT EXISTS(SELECT * FROM sys.dm_xe_sessions WHERE name='AlwaysOn_health')
BEGIN
  ALTER EVENT SESSION [AlwaysOn_health] ON SERVER STATE=START;
END

GO

:Connect 10.0.0.50

USE [master]

GO

CREATE AVAILABILITY GROUP [MyAlwaysOnGroup]
WITH (AUTOMATED_BACKUP_PREFERENCE = SECONDARY)
FOR DATABASE [AdventureWorks2012]
REPLICA ON N'NODE3\SQLINSTANCE1' WITH (ENDPOINT_URL = N'TCP://Node3.lab.pmc.com:5022', FAILOVER_MODE = MANUAL, AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT, BACKUP_PRIORITY = 50, SECONDARY_ROLE(ALLOW_CONNECTIONS = NO)),
	N'NODE4\SQLINSTANCE1' WITH (ENDPOINT_URL = N'TCP://Node4.lab.pmc.com:5022', FAILOVER_MODE = MANUAL, AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT, BACKUP_PRIORITY = 50, SECONDARY_ROLE(ALLOW_CONNECTIONS = NO)),
	N'SQLFCI' WITH (ENDPOINT_URL = N'TCP://SQLFCI.lab.pmc.com:5022', FAILOVER_MODE = MANUAL, AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT, BACKUP_PRIORITY = 50, SECONDARY_ROLE(ALLOW_CONNECTIONS = NO));

GO

:Connect 10.0.0.50

USE [master]

GO

ALTER AVAILABILITY GROUP [MyAlwaysOnGroup]
ADD LISTENER N'MyAGListener' (
WITH IP
((N'10.0.0.48', N'255.255.255.0')
)
, PORT=56789);

GO

:Connect 10.0.0.58\SQLINSTANCE1

ALTER AVAILABILITY GROUP [MyAlwaysOnGroup] JOIN;

GO

:Connect 10.0.0.54\SQLINSTANCE1

ALTER AVAILABILITY GROUP [MyAlwaysOnGroup] JOIN;

GO

:Connect 10.0.0.58\SQLINSTANCE1


-- Wait for the replica to start communicating
begin try
declare @conn bit
declare @count int
declare @replica_id uniqueidentifier 
declare @group_id uniqueidentifier
set @conn = 0
set @count = 30 -- wait for 5 minutes 

if (serverproperty('IsHadrEnabled') = 1)
	and (isnull((select member_state from master.sys.dm_hadr_cluster_members where upper(member_name COLLATE Latin1_General_CI_AS) = upper(cast(serverproperty('ComputerNamePhysicalNetBIOS') as nvarchar(256)) COLLATE Latin1_General_CI_AS)), 0) <> 0)
	and (isnull((select state from master.sys.database_mirroring_endpoints), 1) = 0)
begin
    select @group_id = ags.group_id from master.sys.availability_groups as ags where name = N'MyAlwaysOnGroup'
	select @replica_id = replicas.replica_id from master.sys.availability_replicas as replicas where upper(replicas.replica_server_name COLLATE Latin1_General_CI_AS) = upper(@@SERVERNAME COLLATE Latin1_General_CI_AS) and group_id = @group_id
	while @conn <> 1 and @count > 0
	begin
		set @conn = isnull((select connected_state from master.sys.dm_hadr_availability_replica_states as states where states.replica_id = @replica_id), 1)
		if @conn = 1
		begin
			-- exit loop when the replica is connected, or if the query cannot find the replica status
			break
		end
		waitfor delay '00:00:10'
		set @count = @count - 1
	end
end
end try
begin catch
	-- If the wait loop fails, do not stop execution of the alter database statement
end catch
ALTER DATABASE [AdventureWorks2012] SET HADR AVAILABILITY GROUP = [MyAlwaysOnGroup];

GO

:Connect 10.0.0.54\SQLINSTANCE1


-- Wait for the replica to start communicating
begin try
declare @conn bit
declare @count int
declare @replica_id uniqueidentifier 
declare @group_id uniqueidentifier
set @conn = 0
set @count = 30 -- wait for 5 minutes 

if (serverproperty('IsHadrEnabled') = 1)
	and (isnull((select member_state from master.sys.dm_hadr_cluster_members where upper(member_name COLLATE Latin1_General_CI_AS) = upper(cast(serverproperty('ComputerNamePhysicalNetBIOS') as nvarchar(256)) COLLATE Latin1_General_CI_AS)), 0) <> 0)
	and (isnull((select state from master.sys.database_mirroring_endpoints), 1) = 0)
begin
    select @group_id = ags.group_id from master.sys.availability_groups as ags where name = N'MyAlwaysOnGroup'
	select @replica_id = replicas.replica_id from master.sys.availability_replicas as replicas where upper(replicas.replica_server_name COLLATE Latin1_General_CI_AS) = upper(@@SERVERNAME COLLATE Latin1_General_CI_AS) and group_id = @group_id
	while @conn <> 1 and @count > 0
	begin
		set @conn = isnull((select connected_state from master.sys.dm_hadr_availability_replica_states as states where states.replica_id = @replica_id), 1)
		if @conn = 1
		begin
			-- exit loop when the replica is connected, or if the query cannot find the replica status
			break
		end
		waitfor delay '00:00:10'
		set @count = @count - 1
	end
end
end try
begin catch
	-- If the wait loop fails, do not stop execution of the alter database statement
end catch
ALTER DATABASE [AdventureWorks2012] SET HADR AVAILABILITY GROUP = [MyAlwaysOnGroup];

GO


GO

