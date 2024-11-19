#version 330 core
layout (location = 0) in vec3 vertexPos;
layout (location = 1) in vec4 vertexColor;
layout (location = 2) in vec2 texCoord;
layout (location = 3) in vec3 normal;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform vec3 cameraPos;

out vec4 vertColor;
out vec2 TexCoord;
out vec4 vertPos;
out vec3 viewPos;
out vec3 nCoord;

void main()
{    
	vertColor = vertexColor;
    TexCoord = texCoord;		
	nCoord =  mat3(transpose(inverse(model)))*normal;	
	viewPos = cameraPos;		
	
	vec4 pos = model*vec4(vertexPos, 1.0);
	
	gl_Position = projection*view*(vec4(pos.xyz,1));
			
	vertPos = model*vec4(vertexPos, 1.0);	
	
}
