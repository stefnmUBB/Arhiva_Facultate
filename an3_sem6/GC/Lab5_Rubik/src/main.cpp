#include <windows.h>
#include <vector>
#include <algorithm>

#include "buffers.hpp"
#include "shaders.hpp"
#include "GraphicObject.hpp"
#include "RubikCubeComponent.hpp"
#include "RubikCube.hpp"
#include "utils.hpp"
#include "window.hpp"        
#include "Scene.hpp"        

#include "shaders/basic_frag.h"
#include "shaders/cub_vert.h"

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
	GLFWwindow* window = init_window("Rubik's cube");
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
	RubikCube rubik_cube;	
	rubik_cube.set_shader(shaderProgram);
	
	Scene scene(SCR_WIDTH, SCR_HEIGHT, window);		
	scene.add_object(&rubik_cube);	

	float cam_d = -20,cam_a = 0, cam_b = 0;	

	auto updateCamera = [](Scene* scene, float cam_d, float cam_a, float cam_b){
		float px = cam_d*sin(-glm::radians(cam_a))*cos(-glm::radians(cam_b));
		float py = cam_d*sin(-glm::radians(cam_b));
		float pz = cam_d*cos(-glm::radians(cam_a))*cos(-glm::radians(cam_b));
		scene->camera_set_pos(px, py, pz);
		scene->camera_set_rotate(-cam_b, cam_a, 0);
	};	
	
	RubikMove* current_move = nullptr;
	
	scene.on_key_pressed(GLFW_KEY_ESCAPE, [](Scene* s){ s->send_close_signal(); });
	
	scene.on_key_pressed(GLFW_KEY_UP  , [&](Scene* s){ cam_b+=3; updateCamera(s, cam_d, cam_a, cam_b); });
	scene.on_key_pressed(GLFW_KEY_DOWN, [&](Scene* s){ cam_b-=3; updateCamera(s, cam_d, cam_a, cam_b); });
	scene.on_key_pressed(GLFW_KEY_LEFT, [&](Scene* s){ cam_a-=3; updateCamera(s, cam_d, cam_a, cam_b); });
	scene.on_key_pressed(GLFW_KEY_RIGHT, [&](Scene* s){ cam_a+=3; updateCamera(s, cam_d, cam_a, cam_b); });
	
	int game_state = 0;
	int direction = 1;
		
	scene.on_key_pressed(GLFW_KEY_0, [&](Scene* s){ if(game_state!=1) return; direction = -direction; std::cout<<"dir="<<direction<<"\n"; }, new Cooldown());
	scene.on_key_pressed(GLFW_KEY_1, [&](Scene* s){ if(game_state!=1) return; if(!current_move) {current_move = rubik_cube.create_move(0,0,direction); std::cout<<"move="<<direction*1<<"\n";} });	
	scene.on_key_pressed(GLFW_KEY_2, [&](Scene* s){ if(game_state!=1) return; if(!current_move) {current_move = rubik_cube.create_move(0,2,direction); std::cout<<"move="<<direction*2<<"\n";} });	
	scene.on_key_pressed(GLFW_KEY_3, [&](Scene* s){ if(game_state!=1) return; if(!current_move) {current_move = rubik_cube.create_move(1,0,direction); std::cout<<"move="<<direction*3<<"\n";} });	
	scene.on_key_pressed(GLFW_KEY_4, [&](Scene* s){ if(game_state!=1) return; if(!current_move) {current_move = rubik_cube.create_move(1,2,direction); std::cout<<"move="<<direction*4<<"\n";} });	
	scene.on_key_pressed(GLFW_KEY_5, [&](Scene* s){ if(game_state!=1) return; if(!current_move) {current_move = rubik_cube.create_move(2,0,direction); std::cout<<"move="<<direction*5<<"\n";} });	
	scene.on_key_pressed(GLFW_KEY_6, [&](Scene* s){ if(game_state!=1) return; if(!current_move) {current_move = rubik_cube.create_move(2,2,direction); std::cout<<"move="<<direction*6<<"\n";} });		
	
	scene.on_key_pressed(GLFW_KEY_A, [&](Scene* s){ if(game_state!=1) return; if(!current_move) { game_state=2; } });		
	
	updateCamera(&scene, cam_d, cam_a, cam_b);
	
	
	std::vector<int> shuffle_steps;
	int shuffle_count=10;
	int shuffle_it=0;
	
	auto inverse_move = [](int m) 
	{
		return -m;
		/*int d = m<0 ? 1 : -1;		
		if(m<0) m=-m;
		return d*(((m-1)^1)+1);*/
	};

    while (scene.running())
    {       		
		scene.process_input();		
		if(game_state==0 && current_move==nullptr)
		{
			int move = rand()%6;
			int dir = rand()%2 ? 1 : -1;			
			shuffle_steps.push_back(dir*(move+1));
			current_move = rubik_cube.create_move(move/2, 2*(move%2), dir);
			std::cout<<"MOVE: "<<move/2<<" "<<2*(move%2)<<" "<<dir<<" (move:"<<dir*(move+1)<<", inv:"<< inverse_move(dir*(move+1)) <<")\n";
			shuffle_it++;
			if(shuffle_it==shuffle_count)
			{
				game_state=1;				
			}
		}
		
		if(game_state==2 && current_move==nullptr)
		{
			int move = rand()%6;
			int dir = rand()%2 ? 1 : -1;						
			current_move = rubik_cube.create_move(move/2, 2*(move%2), dir);
		}
		
		
		if(current_move && !current_move->update())
		{			
			current_move = nullptr;
		}			
		lightPos[0]=-scene.camera_pos[0], lightPos[1]=-scene.camera_pos[1], lightPos[2]=-scene.camera_pos[2];		
		
		glUseProgram(shaderProgram);
		glUniform4i(glGetUniformLocation(shaderProgram, "options"), options[0], options[1], options[2], options[3]);		
		glUniform3f(glGetUniformLocation(shaderProgram, "lightPos"), lightPos[0], lightPos[1], lightPos[2]);
        // render
        glClearColor(0.8f, 0.8f, 0.8f, 1.0f);
        glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT);		
		
		rubik_cube.update_model();
		scene.update();
		
		glfwSwapBuffers(window);
        Sleep(10);
        glfwPollEvents();
    }
    
    glDeleteProgram(shaderProgram);	    
    glfwTerminate();
    return 0;
}