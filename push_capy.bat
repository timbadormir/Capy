@echo off
title Auto Push - Proyecto Capy
color 0a

echo ==================================================
echo        🚀 Subiendo cambios del proyecto Capy 🚀
echo ==================================================
echo.

:: Ir a la carpeta del proyecto
cd /d "C:\Users\Drxco_rk\Desktop\sena\proyect capy"

:: Configurar Git para Windows (solo la primera vez, por si acaso)
git config core.autocrlf true

:: Mostrar estado actual
git status
echo.

:: Preguntar mensaje de commit
set /p msg="💬 Escribe el mensaje del commit: "
if "%msg%"=="" set msg=Actualización rápida

:: Agregar todos los cambios
echo.
echo 🔄 Agregando archivos...
git add .

:: Crear commit
echo.
echo 📝 Creando commit: "%msg%"...
git commit -m "%msg%"

:: Hacer push a GitHub
echo.
echo 📡 Subiendo cambios a GitHub...
git push origin main

:: Confirmación final
echo.
echo ✅ ¡Listo, Capy está actualizado en GitHub!
pause
