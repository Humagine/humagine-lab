echo "App Harbour Build Thingy"
chdir /D ..\..\..\..
echo %cd%

o.exe update-wrap
o.exe build-wrap
