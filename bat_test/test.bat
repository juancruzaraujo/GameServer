@echo off

cls

Set count=0
set arg1=%1
set arg2=%2

if [%arg1%]==[] goto :formato


if [%arg2%]==[] goto :formato

:startTelnet
start telnet %arg1% %arg2%
timeout /t 1 /nobreak
if %count% gtr 15 (goto :endTelnet) else (set /a count+=1)
echo "telnet iniciado"
goto :startTelnet

:endTelnet
echo fin
cls
exit /b

:formato
echo "faltan parametros"
echo "test.bat ip puerto"
echo "test.bat 127.0.0.1 1492
exit /b