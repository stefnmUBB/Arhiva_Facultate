#version 330 core
layout (location = 0) in vec3 vertexPos;
layout (location = 1) in vec4 vertexColor;
layout (location = 2) in vec2 texCoord;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec4 vertColor;
out vec2 TexCoord;
out vec4 vertPos;

void main()
{	
    gl_Position = projection*view*model*vec4(vertexPos, 1.0);	
	vertColor = vertexColor;
    TexCoord = texCoord;	
	vertPos = gl_Position;	
}
