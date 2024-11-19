#version 330 core
out vec4 FragColor;
in vec4 vertColor;
in vec2 TexCoord;
in vec4 vertPos;
uniform sampler2D texImage;

uniform ivec4 options;

float abs(float x) { return x<0?-x:x; }

void main()
{
	float t = cos(vertPos.x) * sin(vertPos.y + vertPos.z);
	t = 1 - abs(t) / (abs(t)+3);
	
	float w = sin(vertPos.x + t*vertPos.y + vertPos.x);
	w = w - abs(w) / (abs(w)+1);
	
	vec4 color = {0.7,0.7,0,1};	
	bool hasColor=true;
	
	if(options.y==1 && options.z==1)	
		color =  (1-w)*color + w*vertColor;
	else if(options.y==1)
		color =  vertColor;
	else if(options.z==0)
		color = vec4(0,0,0,1);
		
	
	if(options.x!=0)
	{
		if(options[3]==0)
			FragColor = color*(1-t) + texture(texImage, TexCoord)*t;
		else
			FragColor = color*texture(texImage, TexCoord);
	}
	else
		FragColor = color;
}
