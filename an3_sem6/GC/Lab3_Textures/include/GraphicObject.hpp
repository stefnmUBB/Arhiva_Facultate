#pragma once

class GraphicObject
{
public:
	unsigned int VBO, VAO, EBO;
public:	
	GraphicObject(const void* vertices_data, int vertices_size, const void* triangles_data, int triangles_size)
	{		
		// se initializeaza vertex data (si buffer-ele) si se configureaza
		// atributele vertex-ului   
		glGenVertexArrays(1, &VAO);
		glGenBuffers(1, &VBO);
		glGenBuffers(1, &EBO);
		// se face bind a obiectului Vertex Array, apoi se face bind si se stabilesc
		// vertex buffer(ele), si apoi se configureaza vertex attributes.
		glBindVertexArray(VAO);

		glBindBuffer(GL_ARRAY_BUFFER, VBO);
		glBufferData(GL_ARRAY_BUFFER, vertices_size, vertices_data, GL_STATIC_DRAW);

		glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
		glBufferData(GL_ELEMENT_ARRAY_BUFFER, triangles_size, triangles_data, GL_STATIC_DRAW);

		glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 9 * sizeof(float), (void*)0);
		glEnableVertexAttribArray(0);		
		
		glVertexAttribPointer(1, 4, GL_FLOAT, GL_FALSE, 9 * sizeof(float), (void*)(3*sizeof(float)));
		glEnableVertexAttribArray(1);	
		
		glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 9 * sizeof(float), (void*)(7*sizeof(float)));
		glEnableVertexAttribArray(2);

		glBindBuffer(GL_ARRAY_BUFFER, 0);

		// se face unbind pentru VAO
		glBindVertexArray(0);
	}
	
	template<int N, int M>	
	GraphicObject(const float (&vertices)[N], const unsigned int (&triangles)[M]) 
		: GraphicObject((const void*) vertices, sizeof(vertices), (const void*) triangles, sizeof(triangles)) { }
	
	void draw(unsigned int shaderProgram, glm::mat4 model, glm::mat4 view, glm::mat4 projection)
	{		
		// obtinem locatiile variabilelor uniforms in program
		unsigned int modelLoc = glGetUniformLocation(shaderProgram, "model");
		unsigned int viewLoc  = glGetUniformLocation(shaderProgram, "view");
		unsigned int projectionLoc = glGetUniformLocation(shaderProgram, "projection");

		// transmitem valorile lor catre shadere (2 metode)
		glUniformMatrix4fv(modelLoc, 1, GL_FALSE, glm::value_ptr(model));
		glUniformMatrix4fv(viewLoc, 1, GL_FALSE, &view[0][0]);
		glUniformMatrix4fv(projectionLoc, 1, GL_FALSE, &projection[0][0]);

		// specificam modul in care vrem sa desenam -- aici din spate in fata, si doar contur
		// implicit pune fete, dar cum nu avem lumini si umbre deocamdata cubul nu va arata bine
		//glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);

		glBindVertexArray(VAO);
		//glDrawArrays(GL_TRIANGLES, 0, 8); - folosim alta functie pentru ca avem si EBO
		// desenam elementele
		glDrawElements(GL_TRIANGLES, 36, GL_UNSIGNED_INT, 0);
	}
	
	~GraphicObject()
	{
		glDeleteVertexArrays(1, &VAO);
		glDeleteBuffers(1, &VBO);
		glDeleteBuffers(1, &EBO);
	}	
};

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

class IDrawable
{
protected:
	glm::mat4 model = glm::mat4(1.0f);
	float pos[3] = {0,0,0};	
	float rot[3] = {0,0,0};		
public:
	void rotate(float dx, float dy, float dz) { rot[0]+=dx, rot[1]+=dy, rot[2]+=dz; }
	void move(float dx, float dy, float dz) { pos[0]+=dx, pos[1]+=dy, pos[2]+=dz; }
	
	void set_pos(float dx, float dy, float dz) { pos[0]=dx, pos[1]=dy, pos[2]=dz; }
	
	void update_model()
	{
		model = glm::mat4(1.0f);
		model = glm::translate(model, glm::vec3(pos[0], pos[1], pos[2]));
		model = glm::rotate(model, glm::radians(rot[0]), glm::vec3(1.0f, 0.0f, 0.0f));
		model = glm::rotate(model, glm::radians(rot[1]), glm::vec3(0.0f, 1.0f, 0.0f));
		model = glm::rotate(model, glm::radians(rot[2]), glm::vec3(0.0f, 0.0f, 1.0f));		
	}	

	glm::mat4 get_model() const { return model; }
	virtual void draw(glm::mat4 model, glm::mat4 view, glm::mat4 projection) = 0;
	
	void draw(glm::mat4 view, glm::mat4 projection) { draw(model, view, projection); }	
};

class GraphicObjectInstance : public IDrawable
{
private:
	GraphicObject* obj;		
	int shaderProgram;
public:
	GraphicObjectInstance(GraphicObject* obj) : obj(obj) { }	
	
	void set_shader(int shaderProgram) { this->shaderProgram = shaderProgram; }	
	
	virtual void draw(glm::mat4 model, glm::mat4 view, glm::mat4 projection) override
	{
		obj->draw(shaderProgram, model, view, projection);
	}	
};

#include <vector>

class GraphicsGroup : public IDrawable
{
private:
	std::vector<IDrawable*> components;
public:
	void add_component(IDrawable* obj) { components.push_back(obj); }

	virtual void draw(glm::mat4 model, glm::mat4 view, glm::mat4 projection) override
	{
		for(auto* obj : components)
		{
			obj->draw(model * obj->get_model(), view, projection);
		}	
	}		
};

class Scene
{
private:
	std::vector<IDrawable*> objects;
	glm::mat4 view = glm::mat4(1.0f);	
	glm::mat4 proj = glm::mat4(1.0f);	
	
	float camera_pos[3] = {0,0,0};
	float camera_rot[3] = {0,0,0};	
public:
	Scene(int width, int height)
	{
		proj = glm::perspective(glm::radians(60.0f), (float)width / (float)height, 0.1f, 100.0f);
	}
	
	void camera_rotate(float dx, float dy, float dz) { camera_rot[0]+=dx, camera_rot[1]+=dy, camera_rot[2]+=dz; }
	void camera_move(float dx, float dy, float dz) { camera_pos[0]+=dx, camera_pos[1]+=dy, camera_pos[2]+=dz; }
	
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
		update_view();
		for(auto* obj: objects)
		{
			obj->draw(view, proj);
		}
	}			

};