@echo off
setlocal

:: Obtiene la ruta del directorio donde se encuentra el .bat
set "currentDir=%~dp0"

:: Solicita al usuario que ingrese la llave a codificar
set /p "key=Ingrese la llave a codificar en Base64: "

:: Guarda la llave en un archivo temporal en la misma carpeta
echo %key%> "%currentDir%temp.txt"

:: Usa certutil para codificar el contenido del archivo en Base64 y guarda el resultado en temp.b64
certutil -encode "%currentDir%temp.txt" "%currentDir%temp.b64" >nul

:: Lee el contenido codificado excluyendo las líneas de encabezado y pie
for /f "skip=1 tokens=*" %%i in (%currentDir%temp.b64) do (
    if "%%i"=="-----END CERTIFICATE-----" goto done
    echo %%i
)

:done

:: Limpia archivos temporales
del "%currentDir%temp.txt"
del "%currentDir%temp.b64"

pause
endlocal
