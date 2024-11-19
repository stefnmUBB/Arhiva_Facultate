#pragma once

#include <GLFW/glfw3.h>
#include <map>
#include <set>
#include <functional>

class Cooldown
{
private:
	int frames, state=0;	
	bool busy = false;
public:
	Cooldown(int frames=10) : frames(frames) {}
	bool is_busy() const { return busy; }
	void start() { busy=true; }
	void update()
	{
		if(!busy) return;
		state++;
		if(state==frames)
		{			
			busy=false;	
			state=0;
		}
	}	
};

class Scene
{
private:
	std::vector<IDrawable*> objects;
	glm::mat4 view = glm::mat4(1.0f);	
	glm::mat4 proj = glm::mat4(1.0f);	
	GLFWwindow* window;
public:
	float camera_pos[3] = {0,0,0};
	float camera_rot[3] = {0,0,0};	
	
	std::map<int, std::function<void(Scene*)>> key_bindings;
	std::map<int, Cooldown*> key_cooldown;
	std::set<Cooldown*> key_cooldowns;
public:
	Scene(int width, int height, GLFWwindow* window) : window(window)
	{
		proj = glm::perspective(glm::radians(60.0f), (float)width / (float)height, 0.1f, 100.0f);
	}
	
	void camera_rotate(float dx, float dy, float dz) { camera_rot[0]+=dx, camera_rot[1]+=dy, camera_rot[2]+=dz; }
	void camera_set_rotate(float x, float y, float z) { camera_rot[0]=x, camera_rot[1]=y, camera_rot[2]=z; }	
	
	void camera_move(float dx, float dy, float dz) { camera_pos[0]+=dx, camera_pos[1]+=dy, camera_pos[2]+=dz; }
	void camera_set_pos(float x, float y, float z) { camera_pos[0]=x, camera_pos[1]=y, camera_pos[2]=z; }
	
	void update_view()
	{
		view = glm::mat4(1.0f);
		view = glm::rotate(view, glm::radians(camera_rot[0]), glm::vec3(1.0f, 0.0f, 0.0f));
		view = glm::rotate(view, glm::radians(camera_rot[1]), glm::vec3(0.0f, 1.0f, 0.0f));
		view = glm::rotate(view, glm::radians(camera_rot[2]), glm::vec3(0.0f, 0.0f, 1.0f));
		view = glm::translate(view, glm::vec3(camera_pos[0], camera_pos[1], camera_pos[2]));
	}
	
	void add_object(IDrawable* obj) { objects.push_back(obj); }	
	
	void update()
	{
		for(auto& cd : key_cooldowns)
			cd->update();
		
		update_view();
		for(auto* obj: objects)
		{
			obj->draw(view, proj, camera_pos);
		}
	}				
	
	void process_input()
	{
		for(const auto& kv : key_bindings)					
			if (glfwGetKey(window, kv.first) == GLFW_PRESS)			
			{
				if(key_cooldown[kv.first]!=nullptr && key_cooldown[kv.first]->is_busy())
					continue;
				if(key_cooldown[kv.first]!=nullptr) 
					key_cooldown[kv.first]->start();
				kv.second(this);		
			}
	}	
	
	void on_key_pressed(int key, std::function<void(Scene*)> ev, Cooldown* cooldown=nullptr) 
	{ 
		key_bindings[key]=ev; 
		key_cooldown[key]=cooldown;
		if(cooldown) key_cooldowns.insert(cooldown);
	}
	
	void send_close_signal() { glfwSetWindowShouldClose(window, true); }
	
	bool running() { return !glfwWindowShouldClose(window); }
};