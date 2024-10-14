# PowerShell script that recursively deletes all 'bin' and 'obj' (or any other specified) folders inside current folder

$CurrentPath = (Get-Location -PSProvider FileSystem).ProviderPath

# recursively get all folders matching given includes, except ignored folders
$FoldersToRemove = Get-ChildItem .\ -include bin,obj,build,node_modules,.next,.reports -Recurse   | where {$_ -notmatch '_tools' -and $_ -notmatch '_build'} | foreach {$_.fullname}

# recursively get all folders matching given includes
$AllFolders = Get-ChildItem .\ -include bin,obj,build,node_modules,.next,.reports -Recurse | foreach {$_.fullname}

# subtract arrays to calculate ignored ones
$IgnoredFolders = $AllFolders | where {$FoldersToRemove -notcontains $_}

# remove folders and print to output
if($FoldersToRemove -ne $null)
{
    Write-Host
	foreach ($item in $FoldersToRemove)
	{
		remove-item $item -Force -Recurse;
		Write-Host "Removed: ." -nonewline;
		Write-Host $item.replace($CurrentPath, "");
	}
}

# print ignored folders	to output
if($IgnoredFolders -ne $null)
{
    Write-Host
	foreach ($item in $IgnoredFolders)
	{
		Write-Host "Ignored: ." -nonewline;
		Write-Host $item.replace($CurrentPath, "");
	}

	Write-Host
	Write-Host $IgnoredFolders.count "folders ignored" -foregroundcolor yellow
}

# print summary of the operation
Write-Host
if($FoldersToRemove -ne $null)
{
	Write-Host $FoldersToRemove.count "folders removed" -foregroundcolor green
}
else { 	Write-Host "No folders to remove" -foregroundcolor green }

Write-Host