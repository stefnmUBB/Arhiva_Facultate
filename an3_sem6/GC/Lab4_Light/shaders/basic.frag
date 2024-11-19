#version 330 core
out vec4 FragColor;
in vec4 vertColor;
in vec2 TexCoord;
in vec4 vertPos;
in vec3 viewPos;
in vec3 nCoord;

uniform sampler2D texImage;
uniform sampler2D normalMap;

uniform mat4 model;

uniform ivec4 options;
uniform vec3 lightPos;

float abs(float x) { return x<0?-x:x; }

void main()
{				
	vec3 c1 = cross(nCoord, vec3(0.0, 0.0, 1.0));
	vec3 c2 = cross(nCoord, vec3(0.0, 1.0, 0.0));
	vec3 tangent = length(c1)>length(c2) ? c1:c2;	
	
	vec3 n0 = normalize(nCoord);
	vec3 n1 = normalize(tangent);
	vec3 n2 = normalize(cross(n0,n1));
	
	vec3 normalFromMap = normalize((texture(normalMap, TexCoord)*2-1).xyz);
	//normalFromMap = vec3(0,0,0); 
	normalFromMap = n0*normalFromMap.z  + n1*normalFromMap.x + n2*normalFromMap.y;
	
	FragColor = vertColor;
	
	vec3 lightP = lightPos;
	
	vec3 lightColor = vec3(1.0,1.0,1.0);
	float ambientStrength = 0.2;
	vec3 ambient = ambientStrength * lightColor;		
	
	vec3 bPos = vertPos.xyz;	
	
	vec3 norm = normalize(nCoord+normalFromMap);
	
	vec3 lightDir = normalize(lightP - bPos);
	float diff = max(dot(norm, lightDir), 0.0);
	vec3 diffuse = diff * lightColor;	
	
	vec3 specular = vec3(0,0,0);
	
	float NdotL = dot(norm, lightDir);
	if(NdotL>0)
	{
		float specularStrength = 0.5;
		vec3 viewDir = normalize(viewPos - bPos);
		vec3 reflectDir = reflect(-lightDir, norm);	
		float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
		vec3 specular = specularStrength * spec * lightColor;	
	}
	
	FragColor = vec4(ambient+diffuse+specular, 1.0)*texture(texImage, TexCoord);
}
