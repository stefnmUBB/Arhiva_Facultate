@echo off

set PROJPATH=%~dp0
set ASSETSPATH=%PROJPATH%assets
set BUILDPATH=%PROJPATH%build
set SHADERSPATH=%PROJPATH%shaders
set TOOLSPATH=%PROJPATH%tools
:: echo %ASSETSPATH%

if not exist "%BUILDPATH%" mkdir %BUILDPATH%
if not exist "%BUILDPATH%\assets" mkdir %BUILDPATH%\assets
if not exist "%BUILDPATH%\shaders" mkdir %BUILDPATH%\shaders

set LIBDIRS=-LC:/GL/glfw/lib
set LIBS=-lglfw3 -lopengl32 -lgdi32
set GCC=C:/mingw33/bin/g++.exe

set INCLUDES=-Iinclude -IC:/GL/freeglut/include -IC:/GL/glew/include -IC:/GL/glfw/include -IC:/GL/glm/include -I%BUILDPATH%
set CXXFLAGS=-std=c++11 -Wall -static-libgcc -static-libstdc++

set GRIT=D:\Software\Programs\grit\grit.exe

for /R %ASSETSPATH% %%f in (*) do ( 
	echo %%f
	%GRIT% %%f -gb -gu16 -p! -gB16 -ftc -o"%BUILDPATH%\assets\%%~nf"
	%TOOLSPATH%\put_extern.py %BUILDPATH%\assets\%%~nf.c	
)

for /R %SHADERSPATH% %%f in (*) do ( 
	echo %%f
	%TOOLSPATH%\shadmk.py %%f %BUILDPATH%\shaders
)

setlocal enabledelayedexpansion
set BUILD_FILES=
for /R %BUILDPATH% %%f in (*.c) do ( 
	echo %%f
	set "BUILD_FILES=!BUILD_FILES! %%f"
)

echo %BUILD_FILES%

set BP=%BUILDPATH%

%GCC% -c src/glad.c %INCLUDES% %LIBDIRS% %LIBS% %CXXFLAGS% -o %BP%/glad.o
%GCC% -c src/main.cpp %INCLUDES% %LIBDIRS% %LIBS% %CXXFLAGS% -o %BP%/main.o
%GCC% -c src/RubikCube.cpp %INCLUDES% %LIBDIRS% %LIBS% %CXXFLAGS% -o %BP%/RubikCube.o
%GCC% -c src/GraphicObject.cpp %INCLUDES% %LIBDIRS% %LIBS% %CXXFLAGS% -o %BP%/GraphicObject.o
%GCC% -c src/window.cpp %INCLUDES% %LIBDIRS% %LIBS% %CXXFLAGS% -o %BP%/window.o
%GCC% -c src/textures.cpp %INCLUDES% %LIBDIRS% %LIBS% %CXXFLAGS% -o %BP%/textures.o
%GCC% -c src/RubikMove.cpp %INCLUDES% %LIBDIRS% %LIBS% %CXXFLAGS% -o %BP%/RubikMove.o


%GCC% %BUILD_FILES% %BP%/glad.o %BP%/main.o %BP%/RubikCube.o %BP%/GraphicObject.o %BP%/window.o %BP%/textures.o %BP%/RubikMove.o %INCLUDES% %LIBDIRS% %LIBS% %CXXFLAGS% -o main.exe