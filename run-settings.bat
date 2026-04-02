@echo off
cd /d "%~dp0"
echo Building Settings API...
dotnet build services/Settings/Settings.API/Settings.API.csproj --verbosity minimal
if errorlevel 1 exit /b 1
echo.
echo Starting Settings API...
dotnet run --project services/Settings/Settings.API/Settings.API.csproj --no-build
