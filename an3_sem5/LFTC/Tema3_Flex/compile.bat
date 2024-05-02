@echo off
set "mypath=%~dp0"
set "mypath=%mypath:\=/%"

GnuWin32\bin\flex -omain.cpp main.l.cpp
mingw\bin\g++ main.cpp -std=c++11 -I"%mypath%GnuWin32/include" -L"%mypath%GnuWin32/lib" -lfl -static -static-libgcc -static-libstdc++ -o main.exe