@echo off
chcp 65001 >nul
title 🚀 Auto Push - Proyecto Capy
color 0a

echo ==================================================
echo        🚀 Subiendo cambios del proyecto Capy 🚀
echo ==================================================
echo.

:: Ir a la carpeta del proyecto
cd /d "C:\Users\Drxco_rk\Desktop\sena\proyect capy"

:: Configurar saltos de línea correctos (evita warnings LF/CRLF)
git config core.autocrlf true

:: Verificar si hay cambios
for /f %%i in ('git status --porcelain ^| findstr /r "."') do set hasChanges=true

if not defined hasChanges (
    echo ⚠️ No hay cambios para subir.
    echo 💡 Consejo: Modifica algún archivo o agrega nuevos assets.
    pause
    exit /b
)

:: Pedir mensaje de commit
set /p msg="💬 Escribe el mensaje del commit: "
if "%msg%"=="" set msg=Actualización rápida

echo.
echo 🔄 Agregando archivos...
git add .

echo.
echo 📝 Creando commit: "%msg%"...
git commit -m "%msg%"

echo.
echo 📡 Subiendo cambios a GitHub...
git push origin main

echo.
echo ✅ ¡Listo, Capy está actualizado en GitHub! 🦫🚀
pause
