#pragma once

#include<GLFW/glfw3.h>

GLuint load_texture32(const char* data, int width, int height, GLuint* texId=nullptr);

GLuint load_texture16(const unsigned short* data, int width, int height, GLuint* texId=nullptr);