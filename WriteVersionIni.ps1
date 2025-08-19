$OutputDirRaw = $args[0]
$OutputDir = $OutputDirRaw.Trim('"').Trim()

Write-Host "OutputDir: $OutputDir"

$VersionIni = Join-Path $OutputDir "version.ini"

if (-not (Test-Path $OutputDir)) {
    Write-Host "ERROR: Output directory does not exist!"
    exit 1
}

$files = @()
$files = Get-ChildItem -Path $OutputDir -File -Filter *.dll
$files += Get-ChildItem -Path $OutputDir -File -Filter *.exe

Write-Host "Files found: $($files.Count)"

if ($files.Count -eq 0) {
    Write-Host "WARNING: No exe or dll files found in the output directory."
}

$lines = foreach ($file in $files) {
    $ver = $file.VersionInfo.FileVersion
    "$($file.Name)=$ver"
}

if ($lines.Count -eq 0) {
    $lines = "No exe or dll files found."
}

$lines | Out-File -FilePath $VersionIni -Encoding UTF8
