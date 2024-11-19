#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#include <windows.h>

#include "buffers.hpp"
#include "shaders.hpp"
#include "GraphicObject.hpp"
#include "utils.hpp"
#include "window.hpp"        

#include "shaders/basic_frag.h"
#include "shaders/cub_vert.h"

#include "shaders/default_vert.h"
#include "shaders/default_frag.h"

#include <fstream>

extern "C"
{	
	#include "assets/image.h"
	#include "assets/brick_tex.h"
	#include "assets/brick_norm.h"
}
#include "textures.hpp"

int options[4]={0,0,0,0};
int& opt_show_tex = options[0];
int& opt_show_colors = options[1];
int& opt_show_overlay = options[2];
int& opt_multiply = options[3];

int main()
{		    
	opt_show_tex = opt_show_colors = opt_show_overlay = opt_multiply = 1;
	GLFWwindow* window = init_window("Cubes textured 100y");
	if(window == nullptr)
		return -1;
    // glad: incarcam referintele la functiile OpenGL

    if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress))
    {
        std::cout << "Failed to initialize GLAD" << std::endl;
        return -1;
    }
		

	glEnable(GL_DEPTH_TEST);
		
	GLuint texImage = load_texture16(brick_texBitmap, 256, 256);			
	GLuint normalMap = load_texture16(brick_normBitmap, 256, 256);		
		
	int shaderProgram = create_shader_program(cub_vert, basic_frag);	
	glUseProgram(shaderProgram);
	
	glActiveTexture(GL_TEXTURE0);			
	glBindTexture(GL_TEXTURE_2D, texImage);	
	glUniform1i(glGetUniformLocation(shaderProgram, "texImage"), 0);		
	
	glActiveTexture(GL_TEXTURE1);			
	glBindTexture(GL_TEXTURE_2D, normalMap);				
	glUniform1i(glGetUniformLocation(shaderProgram, "normalMap"), 1);	
	    
	float lightPos[3]={0.0,2.0,10.0};
		
	float lsRad=0.4;
	int lightSourceShaderProgram = create_shader_program(default_vert, default_frag);
	
	GraphicObject lightPoint = GraphicObject::SimpleTriangle(
		0.0*lsRad,0.34*lsRad,0.0, 
		-0.5*lsRad,-0.34*lsRad,0.0, 
		0.5*lsRad,-0.34*lsRad,0.0, 
		1.0, 1.0, 1.0);		
	GraphicObjectInstance lightInstance(&lightPoint);
	lightInstance.set_shader(lightSourceShaderProgram);
	
    GraphicObject cube(vertices, triangles);	
		
	
	GraphicObjectInstance* cubes[3][3][3];
	GraphicsGroup group;	
	
	Scene scene(SCR_WIDTH, SCR_HEIGHT);	
	
	for(int i=0;i<3;i++)
		for(int j=0;j<3;j++)
			for(int k=0;k<3;k++)
			{				
				//if(i!=1 || j!=1 || k!=1) continue;
				cubes[i][j][k] = new GraphicObjectInstance(&cube);
				cubes[i][j][k]->set_shader(shaderProgram);
				cubes[i][j][k]->move(2.2*(i-1),2.2*(j-1),2.2*(k-1));
				cubes[i][j][k]->rotate(90*(i*k+j),-90*(2*j-i+k),90*(i+j-k)+90*k*j);
				cubes[i][j][k]->update_model();				
				group.add_component(cubes[i][j][k]);
			}			
	
	scene.add_object(&lightInstance);
	scene.add_object(&group);
	//scene.camera_move(0,0,-10);	

	float cam_d = -10;
	float cam_a = 0;
	float cam_b = 0;

	auto updateCamera = [](Scene* scene, float cam_d, float cam_a, float cam_b){
		scene->camera_set_pos(cam_d*sin(-glm::radians(cam_a)),cam_b,cam_d*cos(-glm::radians(cam_a)));
		scene->camera_set_rotate(0,cam_a,0);
		
	};

	int input_timeout = 0;
	int MAX_INPUT_TIMEOUT = 10;
	auto processInput = [&input_timeout, MAX_INPUT_TIMEOUT, &cam_d, &cam_a, &cam_b, updateCamera](GLFWwindow *window, Scene* scene)
	{
		if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS)
			glfwSetWindowShouldClose(window, true);
		if (glfwGetKey(window, GLFW_KEY_UP) == GLFW_PRESS)
		{
			cam_d+=0.2;			
		}
		if (glfwGetKey(window, GLFW_KEY_DOWN) == GLFW_PRESS)
		{
			cam_d-=0.2;			
		}
		if (glfwGetKey(window, GLFW_KEY_LEFT) == GLFW_PRESS)
		{
			cam_a-=3;			
		}
		if (glfwGetKey(window, GLFW_KEY_RIGHT) == GLFW_PRESS)
		{
			cam_a+=3;			
		}				
		if (glfwGetKey(window, GLFW_KEY_W) == GLFW_PRESS)
		{
			cam_b-=0.1;
		}
		if (glfwGetKey(window, GLFW_KEY_S) == GLFW_PRESS)
		{
			cam_b+=0.1;
		}		
		updateCamera(scene, cam_d, cam_a, cam_b);
		
	};
	
	updateCamera(&scene, cam_d, cam_a, cam_b);
	
	int n=512, k=0;
	double pi = atan(1)*4;

	//glBindTexture(GL_TEXTURE_2D, texImage);	

    while (!glfwWindowShouldClose(window))
    {
        // input
        processInput(window, &scene);
					
		float lt = 2*k*pi/n;
		float lf = (k+n/2)*pi/n;
		
		lightPos[0]=16*cos(lf);
		lightPos[1]=0;
		lightPos[2]=16*sin(lf);
		
		if(abs(lightPos[1])>16*0.4) lightPos[1] = (lightPos[1]<0?-1:1)*16*0.4;
		
		glUseProgram(shaderProgram);
		glUniform4i(glGetUniformLocation(shaderProgram, "options"), options[0], options[1], options[2], options[3]);		
		glUniform3f(glGetUniformLocation(shaderProgram, "lightPos"), lightPos[0], lightPos[1], lightPos[2]);
        // render
        glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT);
							
		double d1 = 1.7*cos(2000*k/n*0.001*pi);
		double d2 = 2.0*cos(3*(2000*k/n*0.001*pi));
		double d3 = 1.5*cos(0.5*(2000*k/n*0.001*pi)+0.1);
		k = (k+1)%(2*n);		
		
		/*for(int i=0;i<3;i+=2)
			for(int j=0;j<3;j+=2)
				for(int k=0;k<3;k+=2)
				{					
					//if(i!=1 || j!=1 || k!=1) continue;
					cubes[i][j][k]->set_pos(3.2*d1*(i+j-k-1),3.2*d2*(j+k-i-1),3.2*d3*(k+i-j-1));
					cubes[i][j][k]->rotate(i+1,j+1,k+1);
					cubes[i][j][k]->update_model();
				}*/
				
		group.rotate(0.1,0.1,0.1);
		
		lightInstance.set_pos(lightPos[0],lightPos[1],lightPos[2]);
		lightInstance.update_model();
		
		group.update_model();		
		scene.update();
		
		glfwSwapBuffers(window);
        Sleep(10);
        glfwPollEvents();
    }    
    
    glDeleteProgram(shaderProgram);	    
    glfwTerminate();
    return 0;
}