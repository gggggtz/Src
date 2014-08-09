Param
(
    
    [String] $LogName,
    [int] $MaxSize,    
    [Boolean] $Overwrite,
    [String] $BackupDest,
    [String] $BackupUser,
    [String] $BackupPwd,
    [String] $StartTime

)

$BasePath = "HKLM:\SYSTEM\CurrentControlSet\Services\EventLog\"
$LogPath = $BasePath + $LogName
$Retention = 0xffffffff
$Backup = 0

$BackupSrc = "C:\Windows\System32\winevt\Logs\Archive*.evtx"
$BackupPSFile = "C:\Windows\System32\winevt\Logs\Backup.ps1"

Copy-Item -Path Backup.ps1 -Destination $BackupPSFile -Force

if($BackupDest)
{
    $Backup = 1
}

if($MaxSize -gt 0)
{
    $MaxSize = $MaxSize * 1024
    Set-ItemProperty -Path $LogPath -Name MaxSize -Value $MaxSize
}
if($Overwrite)
{
    $Retention = 0
    Set-ItemProperty -Path $LogPath -Name Retention $Retention
}

Set-ItemProperty -Path $LogPath -Name AutoBackupLogFiles $Backup

if($Backup)
{
    $ScheduleTask = New-Object -Com("Schedule.Service")
    $ScheduleTask.Connect($env:COMPUTERNAME)
    $WindowsFolder = $ScheduleTask.GetFolder("Microsoft\Windows")
    $EventLogFolder = $WindowsFolder.GetFolder("EventLog")
    if(! $EventLogFolder)
    {
        $EventLogFolder = $WindowsFolder.CreateFolder("EventLog")
    }
    $BackupTaskDef = $ScheduleTask.NewTask(0)
    
    $TaskRegInfo = $BackupTaskDef.RegistrationInfo
    #settings
    $TaskSettings =$BackupTaskDef.Settings
    $TaskSettings.StartWhenAvailable = $true
    $TaskSettings.Hidden = $false
    #trigger
    $TaskTriggers = $BackupTaskDef.Triggers
    $Trigger = $TaskTriggers.Create(2)
    #$Trigger.StartBoundary = "2014-08-09T16:00:00"
    $Trigger.StartBoundary = $StartTime
    $Trigger.DaysInterval = 1
    $Trigger.Enabled=$true
    #action
    $Action = $BackupTaskDef.Actions.Create(0)
    $Action.Path = "powershell.exe"
    $Action.Arguments = $BackupPSFile + " -src " + $BackupSrc + " -dest " + $BackupDest
    $EventLogFolder.RegisterTaskDefinition( 'Back up Event Logs', $BackupTaskDef, 6, $BackupUser , $BackupPwd , 1)
}




