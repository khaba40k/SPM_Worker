param (
    [string]$BuildOutputDir
)

# Кольори тексту та фону
$Host.UI.RawUI.ForegroundColor = "Blue"    # текст
$Host.UI.RawUI.BackgroundColor = "Yellow"  # фон
Clear-Host  # застосувати фон до всього вікна

# Розмір вікна (у символах, не пікселях)
$rawUI = $Host.UI.RawUI
$size = $rawUI.WindowSize
$size.Width  = 80  # близько до "широкого", 500 пікселів реально не працює
$size.Height = 25   # висота у рядках
$rawUI.WindowSize = $size

# Розмір буфера (щоб скрол не з’являвся)
$buffer = $rawUI.BufferSize
$buffer.Width  = 120
$buffer.Height = 300  # можеш зробити великим, якщо потрібно прокручування
$rawUI.BufferSize = $buffer

Write-Host "BUILDING PROCESS"

Start-Sleep -Seconds 2

do {
    $input = Read-Host "UPLOAD TO FTP? (Y/N)"
    $input = $input.Trim().ToUpper()
} while ($input -ne "Y" -and $input -ne "N")

if ($input -eq "N") {
    exit
}

Write-Host "Wait for *.msi"

# Таймаут
$timeout = 30   # секунд
$elapsed = 0

while (-not (Get-ChildItem -Path $BuildOutputDir -Filter *.msi -File -ErrorAction SilentlyContinue)) {
    Start-Sleep -Seconds 1
    $elapsed++
    if ($elapsed -ge $timeout) {
        Write-Host "NOT FOUND!"
        break
    }
}

Write-Host "Resume"

# ====== Налаштування FTP ======
$ftpServer   = "ftp://sholompromax.com:21/"
$ftpUser     = "spmsoft@sholompromax.com"
$ftpPassword = "R2bn=5?ALPz09z&J"

Write-Host "START UPLOADING: $BuildOutputDir" -ForegroundColor Yellow

# Отримуємо список файлів у папці збірки (без підпапок можна додати -File)
$files = Get-ChildItem -Path $BuildOutputDir -File

foreach ($file in $files) {
    $uri = $ftpServer + $file.Name
    Write-Host "$($file.Name) -> $uri"
    
    $webclient = New-Object System.Net.WebClient
    $webclient.Credentials = New-Object System.Net.NetworkCredential($ftpUser, $ftpPassword)
    
    try {
        $webclient.UploadFile($uri, $file.FullName)
    }
    catch {
        Write-Host "ERROR!!! -> $($file.Name): $_" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "!!! ALL FILES UPLOADED !!!"

Start-Sleep -Seconds 3