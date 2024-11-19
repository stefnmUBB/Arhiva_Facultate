#pragma once

#include<glad/glad.h>
#include<GLFW/glfw3.h>

GLuint load_texture32(const char* data, int width, int height, GLuint* texId=nullptr)
{
	GLuint rezTextura;    
	
	if(texId==nullptr)
		glGenTextures(1, &rezTextura);
	else
		rezTextura = *texId;

	glBindTexture(GL_TEXTURE_2D, rezTextura);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, (const char*)data);
	// generam mipmap -uri pentru aceasta textura
	glGenerateMipmap(GL_TEXTURE_2D);	
	return rezTextura;
}

#include "grit32.hpp"

GLuint load_texture16(const unsigned short* data, int width, int height, GLuint* texId=nullptr)
{
	char* data32 = new char[width*height*3];
	rgb16_to_rgb32(data, data32, width*height);
	GLuint res = load_texture32(data32, width, height, texId);
	delete[] data32;	
	return res;
}
