#pragma once

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

class GraphicObject
{
public:
	unsigned int VBO, VAO, EBO;
	int triangles_count;
public:	
	GraphicObject(const void* vertices_data, int vertices_size, const void* triangles_data, int triangles_size);
	
	template<int N, int M>	
	GraphicObject(const float (&vertices)[N], const unsigned int (&triangles)[M]) 
		: GraphicObject((const void*) vertices, sizeof(vertices), (const void*) triangles, sizeof(triangles)) { }
		
	static GraphicObject SimpleTriangle(float a0, float a1, float a2, float b0, float b1, float b2, float c0, float c1, float c2, float r, float g, float b, float a=1)
	{
		float buffer[] = 
		{
			a0, a1, a2, r,g,b,a, 0, 0, 0, 0, 0,
			b0, b1, b2, r,g,b,a, 0, 0, 0, 0, 0,
			c0, c1, c2, r,g,b,a, 0, 0, 0, 0, 0
		};
		unsigned int triangles[] = {0,1,2, 0,2,1};
		
		return GraphicObject(buffer, triangles);		
	}	
	
	void draw(unsigned int shaderProgram, glm::mat4 model, glm::mat4 view, glm::mat4 projection, float cameraPos[3]);		
	~GraphicObject();	
};

class IDrawable
{
protected:
	glm::mat4 model = glm::mat4(1.0f);
	float pos[3] = {0,0,0};	
	float rot[3] = {0,0,0};		
	float scale[3]= {1,1,1};
public:
	void rotate(float dx, float dy, float dz) { rot[0]+=dx, rot[1]+=dy, rot[2]+=dz; }	
	void move(float dx, float dy, float dz) { pos[0]+=dx, pos[1]+=dy, pos[2]+=dz; }
	
	void set_pos(float dx, float dy, float dz) { pos[0]=dx, pos[1]=dy, pos[2]=dz; }
	void set_rot(float dx, float dy, float dz) { rot[0]=dx, rot[1]=dy, rot[2]=dz; }
	
	void set_scale(float sx, float sy, float sz) { scale[0]=sx, scale[1]=sy, scale[2]=sz; }
	
	void update_model()
	{
		model = glm::mat4(1.0f);
		model = glm::translate(model, glm::vec3(pos[0], pos[1], pos[2]));
		model = glm::rotate(model, glm::radians(rot[0]), glm::vec3(1.0f, 0.0f, 0.0f));
		model = glm::rotate(model, glm::radians(rot[1]), glm::vec3(0.0f, 1.0f, 0.0f));
		model = glm::rotate(model, glm::radians(rot[2]), glm::vec3(0.0f, 0.0f, 1.0f));	
		model = glm::scale(model, glm::vec3(scale[0], scale[1], scale[2]));
	}	

	glm::mat4 get_model() const { return model; }
	virtual void draw(glm::mat4 model, glm::mat4 view, glm::mat4 projection, float cameraPos[3]) = 0;
	
	void draw(glm::mat4 view, glm::mat4 projection, float cameraPos[3]) { draw(model, view, projection, cameraPos); }	
};

class GraphicObjectInstance : public IDrawable
{
private:
	GraphicObject* obj;		
	int shaderProgram;
public:
	GraphicObjectInstance(GraphicObject* obj) : obj(obj) { }	
	
	void set_shader(int shaderProgram) { this->shaderProgram = shaderProgram; }	
	
	virtual void draw(glm::mat4 model, glm::mat4 view, glm::mat4 projection,  float cameraPos[3]) override
	{
		obj->draw(shaderProgram, model, view, projection, cameraPos);
	}	
};

#include <vector>

class GraphicsGroup : public IDrawable
{
private:
	std::vector<IDrawable*> components;
public:
	void add_component(IDrawable* obj) { components.push_back(obj); }

	virtual void draw(glm::mat4 model, glm::mat4 view, glm::mat4 projection, float cameraPos[3]) override
	{
		for(auto* obj : components)
		{
			obj->draw(model * obj->get_model(), view, projection, cameraPos);
		}	
	}		
};