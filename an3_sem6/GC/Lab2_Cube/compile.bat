@echo off

set LIBDIRS=-LC:/GL/glfw/lib
set LIBS=-lglfw3 -lopengl32 -lgdi32
set GCC=C:/mingw32/bin/g++.exe

set INCLUDES=-Iinclude -IC:/GL/freeglut/include -IC:/GL/glew/include -IC:/GL/glfw/include -IC:/GL/glm/include
set CXXFLAGS=-std=c++11 -Wall -static-libgcc -static-libstdc++

%GCC% src/main.cpp src/glad.c %INCLUDES% %LIBDIRS% %LIBS% %CXXFLAGS% -o main.exe