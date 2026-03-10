REM Make a self-extraction installer using 7-zip.
REM This script will delete and re-create ExcludeDictSetup.7z & ExcludeDictSetup.exe
REM Ctrl-C if you don't want to proceed!!

pause

del ExcludeDictSetup.7z ExcludeDictSetup.exe
"C:\Program Files\7-Zip\7z.exe" a ExcludeDictSetup.7z ..\Release\*.* 
copy /b 7zSD.sfx + config.txt + ExcludeDictSetup.7z ExcludeDictSetup.exe
