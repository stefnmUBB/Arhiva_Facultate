#include "GraphicObject.hpp"

#include <glad/glad.h>

GraphicObject::GraphicObject(const void* vertices_data, int vertices_size, const void* triangles_data, int triangles_size) : triangles_count(triangles_size)
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

	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 12 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(0);		
	
	glVertexAttribPointer(1, 4, GL_FLOAT, GL_FALSE, 12 * sizeof(float), (void*)(3*sizeof(float)));
	glEnableVertexAttribArray(1);	
	
	glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 12 * sizeof(float), (void*)(7*sizeof(float)));
	glEnableVertexAttribArray(2);

	glVertexAttribPointer(3, 3, GL_FLOAT, GL_FALSE, 12 * sizeof(float), (void*)(9*sizeof(float)));
	glEnableVertexAttribArray(3);

	glBindBuffer(GL_ARRAY_BUFFER, 0);

	// se face unbind pentru VAO
	glBindVertexArray(0);
}

void GraphicObject::draw(unsigned int shaderProgram, glm::mat4 model, glm::mat4 view, glm::mat4 projection, float cameraPos[3])
{		
	glUseProgram(shaderProgram);
	// obtinem locatiile variabilelor uniforms in program
	unsigned int modelLoc = glGetUniformLocation(shaderProgram, "model");
	unsigned int viewLoc  = glGetUniformLocation(shaderProgram, "view");
	unsigned int projectionLoc = glGetUniformLocation(shaderProgram, "projection");
	unsigned int cameraPosLoc = glGetUniformLocation(shaderProgram, "cameraPos");

	// transmitem valorile lor catre shadere (2 metode)
	glUniformMatrix4fv(modelLoc, 1, GL_FALSE, glm::value_ptr(model));
	glUniformMatrix4fv(viewLoc, 1, GL_FALSE, &view[0][0]);
	glUniformMatrix4fv(projectionLoc, 1, GL_FALSE, &projection[0][0]);
	glUniformMatrix4fv(cameraPosLoc, 1, GL_FALSE, &cameraPos[0]);

	// specificam modul in care vrem sa desenam -- aici din spate in fata, si doar contur
	// implicit pune fete, dar cum nu avem lumini si umbre deocamdata cubul nu va arata bine
	//glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);

	glBindVertexArray(VAO);
	//glDrawArrays(GL_TRIANGLES, 0, 8); - folosim alta functie pentru ca avem si EBO
	// desenam elementele
	glDrawElements(GL_TRIANGLES, triangles_count, GL_UNSIGNED_INT, 0);
}

GraphicObject::~GraphicObject()
{
	glDeleteVertexArrays(1, &VAO);
	glDeleteBuffers(1, &VBO);
	glDeleteBuffers(1, &EBO);
}	