@echo off
setlocal

REM one way to get to the solution name :-D
for %%* in (.) do set BUILD_SOLUTION=%%~nx*

set BUILD_VSCMD=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsDevCmd.bat

if exist "%BUILD_VSCMD%" (call "%BUILD_VSCMD%")

pause

set BUILD_ROOT=%~dp0
set BUILD_BIN=%BUILD_ROOT%StfBin
set BUILD_DEPLOY_DIR=C:\Temp\Stf
set BUILD_CONFIGURATION_DIR=%BUILD_ROOT%%BUILD_SOLUTION%.core\Configuration\Config
set BUILD_SCRIPT_PS=%BUILD_ROOT%Build\BuildVsSolutions.ps1
set BUILD_SELENIUM_SERVERS=%BUILD_ROOT%Build\ToStfInstallDirectory\Selenium

REM Lets see what we have set up...
echo Building %BUILD_SOLUTION% with this setup
set BUILD

REM Build the solutions
powershell -ExecutionPolicy Bypass -file "%BUILD_SCRIPT_PS%" -buildRoot %BUILD_ROOT%

REM Make sure all the needed Selenium servers are present
robocopy "%BUILD_SELENIUM_SERVERS%" "%BUILD_DEPLOY_DIR%\Selenium" /MIR

REM Deploy the solutions and corresponding configuration
robocopy "%BUILD_BIN%"               "%BUILD_DEPLOY_DIR%\StfBin" /MIR
robocopy "%BUILD_CONFIGURATION_DIR%" "%BUILD_DEPLOY_DIR%\Config" StfConfiguration.xml

REM make sure we have a STF temp
if NOT exist "%BUILD_DEPLOY_DIR%\Temp" (
  mkdir "%BUILD_DEPLOY_DIR%\Temp"
)

rem Set the plugin folder to the build folder
powershell -ExecutionPolicy Bypass -file "%BUILD_ROOT%\tools\Stf-SetPluginPath.ps1" -PluginFolderType build
