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

extern "C"
{	
	#include "assets/image.h"
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
		
	int shaderProgram = create_shader_program(cub_vert, basic_frag);
	
	GLuint texImage = load_texture16(imageBitmap, 512, 512);
	glUniform1i(glGetUniformLocation(shaderProgram, "texImage"), 0);	
	
		
	//glUniform1i(glGetUniformLocation(shaderProgram, "texImage"), 0);	
	
    
    GraphicObject cube(vertices, triangles);	
	
	glUseProgram(shaderProgram);
	
	GraphicObjectInstance* cubes[3][3][3];
	GraphicsGroup group;	
	
	Scene scene(SCR_WIDTH, SCR_HEIGHT);	
	
	for(int i=0;i<3;i++)
		for(int j=0;j<3;j++)
			for(int k=0;k<3;k++)
			{
				cubes[i][j][k] = new GraphicObjectInstance(&cube);
				cubes[i][j][k]->set_shader(shaderProgram);
				cubes[i][j][k]->move(2.2*(i-1),2.2*(j-1),2.2*(k-1));
				cubes[i][j][k]->rotate(90*(i*k+j),-90*(2*j-i+k),90*(i+j-k)+90*k*j);
				cubes[i][j][k]->update_model();				
				group.add_component(cubes[i][j][k]);
			}			
	
	scene.add_object(&group);
	scene.camera_move(0,0,-10);	

	int input_timeout = 0;
	int MAX_INPUT_TIMEOUT = 10;
	auto processInput = [&input_timeout, MAX_INPUT_TIMEOUT](GLFWwindow *window, Scene* scene)
	{
		if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS)
			glfwSetWindowShouldClose(window, true);
		if (glfwGetKey(window, GLFW_KEY_UP) == GLFW_PRESS)
			scene->camera_move(0,0,0.2);
		if (glfwGetKey(window, GLFW_KEY_DOWN) == GLFW_PRESS)
			scene->camera_move(0,0,-0.2);
		if (glfwGetKey(window, GLFW_KEY_LEFT) == GLFW_PRESS)
			scene->camera_rotate(0,3,0);
		if (glfwGetKey(window, GLFW_KEY_RIGHT) == GLFW_PRESS)
			scene->camera_rotate(0,-3,0);
		if (glfwGetKey(window, GLFW_KEY_T) == GLFW_PRESS && input_timeout==0)
		{
			opt_show_tex = 1-opt_show_tex;
			input_timeout = MAX_INPUT_TIMEOUT;
		}
		if (glfwGetKey(window, GLFW_KEY_Y) == GLFW_PRESS && input_timeout==0)
		{
			opt_show_overlay = 1-opt_show_overlay;
			input_timeout = MAX_INPUT_TIMEOUT;
		}
		if (glfwGetKey(window, GLFW_KEY_C) == GLFW_PRESS && input_timeout==0)
		{
			opt_show_colors = 1-opt_show_colors;
			input_timeout = MAX_INPUT_TIMEOUT;
		}
		if (glfwGetKey(window, GLFW_KEY_M) == GLFW_PRESS && input_timeout==0)
		{
			opt_multiply = 1-opt_multiply;
			input_timeout = MAX_INPUT_TIMEOUT;
		}						
		if (glfwGetKey(window, GLFW_KEY_A) == GLFW_PRESS && input_timeout==0)
		{
			opt_show_tex = opt_show_colors = opt_show_overlay = opt_multiply = 1;
			input_timeout = MAX_INPUT_TIMEOUT;
		}						
		if(input_timeout>0) input_timeout--;		
	};
	
	int n=256, k=0;
	double pi = atan(1)*4;

	glBindTexture(GL_TEXTURE_2D, texImage);


    while (!glfwWindowShouldClose(window))
    {
        // input
        processInput(window, &scene);
		
		glUniform4i(glGetUniformLocation(shaderProgram, "options"), options[0], options[1], options[2], options[3]);		
        // render
        glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT);
							
		double d1 = 1.7*cos(2000*k/n*0.001*pi);
		double d2 = 2.0*cos(3*(2000*k/n*0.001*pi));
		double d3 = 1.5*cos(0.5*(2000*k/n*0.001*pi)+0.1);
		k = (k+1)%(2*n);
		
		for(int i=0;i<3;i+=2)
			for(int j=0;j<3;j+=2)
				for(int k=0;k<3;k+=2)
				{					
					cubes[i][j][k]->set_pos(3.2*d1*(i+j-k-1),3.2*d2*(j+k-i-1),3.2*d3*(k+i-j-1));
					cubes[i][j][k]->rotate(i+1,j+1,k+1);
					cubes[i][j][k]->update_model();
				}
		
		//scene.camera_rotate(2,0,0);		
		group.rotate(1,0.5,0);	
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