#pragma once

#include <GLFW/glfw3.h>

// configurari
extern const unsigned int SCR_WIDTH;
extern const unsigned int SCR_HEIGHT;

// glfw: ce se intampla la o redimensionalizare a ferestrei
void framebuffer_size_callback(GLFWwindow* window, int width, int height);


GLFWwindow* init_window(const char* title);