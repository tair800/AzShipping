# Run Settings API - Build first, then run (avoids "stuck on building")
$ErrorActionPreference = "Stop"
Set-Location $PSScriptRoot

Write-Host "Building Settings API..." -ForegroundColor Cyan
dotnet build services/Settings/Settings.API/Settings.API.csproj --verbosity minimal
if ($LASTEXITCODE -ne 0) { exit 1 }

Write-Host "Starting Settings API (no rebuild)..." -ForegroundColor Green
dotnet run --project services/Settings/Settings.API/Settings.API.csproj --no-build
