@echo off

set LIBDIRS=-LC:/GL/glfw/lib
set LIBS=-lglfw3 -lopengl32 -lgdi32
set GCC=C:/mingw32/bin/gcc.exe

set INCLUDES=-Iinclude -IC:/GL/freeglut/include -IC:/GL/glew/include -IC:/GL/glfw/include -IC:/GL/glm/include

%GCC% src/main.c src/glad.c %INCLUDES% %LIBDIRS% %LIBS% -o main.exe