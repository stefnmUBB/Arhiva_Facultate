nasm.exe -fobj main.asm -l main.lst -I.\ || goto :END

ALINK.EXE -oPE -subsys console -entry start main.obj || goto :END

:END

echo Done.