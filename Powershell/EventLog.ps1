Limit-EventLog -LogName Application -MaximumSize 1024 -OverflowAction DoNotOverwrite
$charlieAppLog = Get-EventLog -LogName Application -Source Charlie -Newest 1 -ErrorAction SilentlyContinue
if(!$charlieAppLog)
{   
    New-EventLog -LogName Application -Source Charlie
}

for($i = 1; $i -le 100000; $i++)
{
	Write-EventLog -LogName Application -Source Charlie -EntryType Information -Message "Test Message $i" -EventId $i -Category 1
}
