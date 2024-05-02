@echo off
set "mypath=%~dp0"
set "mypath=%mypath:\=/%"

set "flex=%mypath%/bin/flex/bin/flex.exe"
set "bison=%mypath%/bin/bison/bin/bison.exe"
set "gxx=%mypath%/bin/mingw/bin/g++.exe"

set "build=%mypath%/build/"
set "source=%mypath%/source/"

::@echo on

cd %flex%\..\
flex -o%build%/main.l.cpp %source%/main.l.cpp || goto :END 


cd %bison%\..\
bison -o%build%/main.tab.cpp %source%/main.y.cpp || goto :END 

cd %mypath%

%gxx% -std=c++11 -omain.exe -I"%mypath%/bin/flex/include" -I"%mypath%/include" -L"%mypath%/bin/flex/lib" %build%/main.l.cpp -static -static-libgcc -static-libstdc++ -lfl

::%gxx% -std=c++11 -L"%mypath%/bin/flex/lib" -L"%mypath%/bin/bison/lib" -lfl -ly -static -static-libgcc -static-libstdc++  -omain.exe %build%/main.l.o %build%/main.tab.o


:END
cd %mypath%



:: %gxx% 
:: GnuWin32\bin\flex -omain.cpp main.l.cpp
:: mingw\bin\g++ main.cpp -std=c++11 -I"%mypath%GnuWin32/include" -L"%mypath%GnuWin32/lib" -lfl -static -static-libgcc -static-libstdc++ -o main.exe