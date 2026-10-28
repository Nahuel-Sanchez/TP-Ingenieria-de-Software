@echo off
rem Compila el instalador de Horizon Hotel & Resort con Inno Setup 6.
rem Antes: compilar la solucion en Release (GUI\bin\Release).
setlocal
set ISCC=%ProgramFiles(x86)%\Inno Setup 6\ISCC.exe
if not exist "%ISCC%" set ISCC=%ProgramFiles%\Inno Setup 6\ISCC.exe
if not exist "%ISCC%" (
  echo No se encontro Inno Setup 6. Descargarlo de https://jrsoftware.org/isdl.php
  exit /b 1
)
if not exist "%~dp0..\GUI\bin\Release\GUI_08YS.exe" (
  echo Falta GUI\bin\Release\GUI_08YS.exe: compilar la solucion en Release primero.
  exit /b 1
)
"%ISCC%" "%~dp0HorizonHotel.iss"
if errorlevel 1 exit /b 1
echo.
echo Instalador generado en %~dp0Salida\HorizonHotel_Setup.exe
