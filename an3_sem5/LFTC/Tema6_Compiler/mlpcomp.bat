@echo off
main.exe test\main.asm <examples\%1.mlpc || goto :END
pushd test
call compile.bat 
popd

test\main.exe

:END