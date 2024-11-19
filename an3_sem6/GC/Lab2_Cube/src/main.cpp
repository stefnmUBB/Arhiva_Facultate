#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#include <windows.h>

#include "buffers.hpp"
#include "shaders.hpp"
#include "GraphicObject.hpp"
#include "utils.hpp"
#include "window.hpp"        

int main()
{		    
	GLFWwindow* window = init_window("Cubes");
	if(window == nullptr)
		return -1;
    // glad: incarcam referintele la functiile OpenGL

    if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress))
    {
        std::cout << "Failed to initialize GLAD" << std::endl;
        return -1;
    }
		
	int shaderProgram = load_shaders_program("data/cub.vert", "data/basic.frag");    
    
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
				cubes[i][j][k]->update_model();	
				group.add_component(cubes[i][j][k]);				
			}			
	
	scene.add_object(&group);
	scene.camera_move(0,0,-10);	

	auto processInput = [](GLFWwindow *window, Scene* scene)
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
		
	};
	
	int n=200, k=0;
	double pi = atan(1)*4;

    while (!glfwWindowShouldClose(window))
    {
        // input
        processInput(window, &scene);
        // render
        glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT);
							
		double d1 = 1.7*cos(2*pi*k/n);
		double d2 = 2.0*cos(3*(2*pi*k/n));
		double d3 = 1.5*cos(0.5*(2*pi*k/n)+0.1);
		k = (k+1)%n;
		
		for(int i=0;i<3;i+=2)
			for(int j=0;j<3;j+=2)
				for(int k=0;k<3;k+=2)
				{					
					cubes[i][j][k]->set_pos(3.2*d1*(i-1),3.2*d2*(j-1),3.2*d3*(k-1));
					cubes[i][j][k]->rotate(i+1,j+1,k+1);
					cubes[i][j][k]->update_model();
				}
		
		//scene.camera_rotate(2,0,0);		
		group.rotate(1,0,0);	
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