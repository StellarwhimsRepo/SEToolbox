@echo off
setlocal
call "%VS160COMNTOOLS%vsvars32.bat"
"MSBuild.exe" /fl "%~dp0Main\SEToolbox\BuildScripts\Build.proj" %* "/property:ReleaseRoot=%~dp0bin;Configuration=Release"
IF NOT [%NOPAUSE%] == [Y] PAUSE
