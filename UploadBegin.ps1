param (
    [string]$scriptPath,
    [string]$targetDir
)

$scriptPath = $scriptPath.Trim('"').TrimEnd('\')
$targetDir  = $targetDir.Trim('"').TrimEnd('\')

$argList = "-NoProfile -ExecutionPolicy Bypass -File `"$scriptPath`" `"$targetDir`" -NoExit"

# Асинхронний запуск
Start-Process -FilePath "powershell.exe" -ArgumentList $argList -WindowStyle Normal
