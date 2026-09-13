@echo off
setlocal
cd /d "%~dp0"
dotnet restore
if errorlevel 1 exit /b %errorlevel%
dotnet build -c Release
if errorlevel 1 exit /b %errorlevel%
echo.
echo Build complete. Output: bin\Release\net8.0-windows\
pause
