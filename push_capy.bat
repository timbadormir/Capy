@echo off
chcp 65001 >nul
title 🚀 Auto Push - Proyecto Capy (eteches)
color 0a

echo ==================================================
echo        🚀 Subiendo cambios a la rama eteches 🚀
echo ==================================================
echo.

:: Ir a la carpeta del proyecto
cd /d "C:\Users\Drxco_rk\Desktop\sena\NUEVO CAPY"

:: Asegurar que siempre esté en la rama eteches
git checkout eteches

:: Verificar si hay cambios
git status --porcelain > temp_changes.txt
for /f %%i in (temp_changes.txt) do set hasChanges=true
del temp_changes.txt

if not defined hasChanges (
    echo ⚠️ No hay cambios para subir.
    pause
    exit /b
)

:: Pedir mensaje de commit
set /p msg="💬 Escribe el mensaje del commit: "
if "%msg%"=="" set msg=Actualización rápida

echo.
echo 🔄 Agregando archivos...
git add -A

echo.
echo 📝 Creando commit: "%msg%"...
git commit -m "%msg%"

echo.
echo 📡 Subiendo cambios a GitHub (rama eteches)...
git push origin eteches

echo.
echo ✅ ¡Listo, Capy está actualizado en GitHub en la rama eteches! 🦫🚀
pause
