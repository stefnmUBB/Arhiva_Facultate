#include<glad/glad.h>
#include<GLFW/glfw3.h>
#include <stdio.h>
#include <math.h>

// Vertex Shader source code
const char* vertexShaderSource = "#version 330 core\n"
"layout (location = 0) in vec3 aPos;\n"
"void main(){gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0);}\0";
//Fragment Shader source code
const char* fragmentShaderSource = "#version 330 core\n"
"layout (location = 0) in vec3 aPos;\n"
"out vec4 FragColor;\n"
"void main(){ FragColor = vec4(0.5f, 1.0f, 1.0f, 1.0f);}\0";

GLuint shaderProgram;

void initializeShaderProgram()
{
	// Create Vertex Shader Object and get its reference
	GLuint vertexShader = glCreateShader(GL_VERTEX_SHADER);
	// Attach Vertex Shader source to the Vertex Shader Object
	glShaderSource(vertexShader, 1, &vertexShaderSource, NULL);
	// Compile the Vertex Shader into machine code
	glCompileShader(vertexShader);

	// Create Fragment Shader Object and get its reference
	GLuint fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
	// Attach Fragment Shader source to the Fragment Shader Object
	glShaderSource(fragmentShader, 1, &fragmentShaderSource, NULL);
	// Compile the Vertex Shader into machine code
	glCompileShader(fragmentShader);

	// Create Shader Program Object and get its reference
	shaderProgram = glCreateProgram();
	// Attach the Vertex and Fragment Shaders to the Shader Program
	glAttachShader(shaderProgram, vertexShader);
	glAttachShader(shaderProgram, fragmentShader);
	// Wrap-up/Link all the shaders together into the Shader Program
	glLinkProgram(shaderProgram);

	// Delete the now useless Vertex and Fragment Shader objects
	glDeleteShader(vertexShader);
	glDeleteShader(fragmentShader);
}


void drawPixel(float x, float y)
{
	float r = 0.005f;
	// Vertices coordinates
	GLfloat vertices[] =
	{
		x-r, y-r * (float)(sqrt(3)) / 3, 0.0f, // Lower left corner
		x+r, y-r * (float)(sqrt(3)) / 3, 0.0f, // Lower right corner
		x+0.0f, y+r * (float)(sqrt(3)) * 2 / 3, 0.0f // Upper corner
	};

	// Create reference containers for the Vartex Array Object and the Vertex Buffer Object
	GLuint VAO, VBO;

	// Generate the VAO and VBO with only 1 object each
	glGenVertexArrays(1, &VAO);
	glGenBuffers(1, &VBO);

	// Make the VAO the current Vertex Array Object by binding it
	glBindVertexArray(VAO);

	// Bind the VBO specifying it's a GL_ARRAY_BUFFER
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	// Introduce the vertices into the VBO
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

	// Configure the Vertex Attribute so that OpenGL knows how to read the VBO
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);
	// Enable the Vertex Attribute so that OpenGL knows to use it
	glEnableVertexAttribArray(0);

	// Bind both the VBO and VAO to 0 so that we don't accidentally modify the VAO and VBO we created
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glBindVertexArray(0);	
	
	// render
	glUseProgram(shaderProgram);
	// Bind the VAO so OpenGL knows to use it
	glBindVertexArray(VAO);
	// Draw the triangle using the GL_TRIANGLES primitive
	glDrawArrays(GL_TRIANGLES, 0, 3);
	
	// Delete all the objects we've created
	glDeleteVertexArrays(1, &VAO);
	glDeleteBuffers(1, &VBO);	
}


float absf32(float x) { return (x<0)?-x:x; }

void bresenham_line(float x0, float y0, float x1, float y1, void (*set_pixel)(float, float))
{
	void line_low(float x0, float y0, float x1, float y1)
	{
		if(x0>x1) {
			printf("bresenham_line: invalid arguments\n");
			return;
		}
		
		//printf("LO %f %f %f %f\n", x0,y0,x1,y1);
		
		float dx = x1-x0, dy = y1-y0;
		float D = 2*dy-dx;
		float y=y0;
		float step = 0.005;
		float yi = 1.0;
		
		if(dy < 0)		 
			yi = -1, dy = -dy;
		
		for(float x=x0;x<x1+step;x+=step)
		{
			set_pixel(x,y);
			if(D>0)
			{
				y+=yi*step;
				D -= 2*dx;
			}
			D += 2*dy;
		}	
	}
	
	
	void line_high(float x0, float y0, float x1, float y1)
	{		
		if(y0>y1) {
			printf("bresenham_line: invalid arguments\n");
			return;
		}
		//printf("HI %f %f %f %f\n", x0,y0,x1,y1);
		
		float dx = x1-x0, dy = y1-y0;
		float D = 2*dx-dy;
		float x=x0;
		float step = 0.005;
		float xi = 1.0;
		
		if(dx < 0)		 
			xi = -1, dx = -dx;
		
		for(float y=y0;y<y1+step;y+=step)		
		{
			set_pixel(x,y);
			if(D>0)
			{
				x+=xi*step;
				D -= 2*dy;
			}
			D += 2*dx;
		}	
	}	
		
	if(absf32(y1-y0)<absf32(x1-x0))
	{
		if(x0>x1)
			line_low(x1, y1, x0, y0);
		else
			line_low(x0, y0, x1, y1);
	}
	else
	{
		if(y0>y1)
			line_high(x1, y1, x0, y0);
		else
			line_high(x0, y0, x1, y1);
	}			
}


void bresenham_circle(float xc, float yc, float r, void (*set_pixel)(float, float))
{
	void drawCircle(float xc, float yc, float x, float y) 
	{ 
		set_pixel(xc+x, yc+y); 
		set_pixel(xc-x, yc+y); 
		set_pixel(xc+x, yc-y); 
		set_pixel(xc-x, yc-y); 
		set_pixel(xc+y, yc+x); 
		set_pixel(xc-y, yc+x); 
		set_pixel(xc+y, yc-x); 
		set_pixel(xc-y, yc-x); 
	} 
	  
	float step = 0.005;
	float x = 0, y = r; 
    float d = 3 - 2 * r;
    drawCircle(xc, yc, x, y); 
    while (y >= x) 
    { 
        // for each pixel we will 
        // draw all eight pixels           
        x+=step; 
  
        // check for decision parameter 
        // and correspondingly  
        // update d, x, y 
        if (d > 0) 
        { 
            y-=step; 
            d = d + 4 * (x - y)/step + 10;
        } 
        else
            d = d + 4 * x/step + 6;
        drawCircle(xc, yc, x, y);         
    } 
}

int main()
{
	// Initialize GLFW
	glfwInit();

	// Tell GLFW what version of OpenGL we are using 
	// In this case we are using OpenGL 3.3
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	// Tell GLFW we are using the CORE profile
	// So that means we only have the modern functions
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);
	
	GLFWwindow* window = glfwCreateWindow(600, 600, "Bresenham", NULL, NULL);
	// Error check if the window fails to create
	if (window == NULL)
	{
		printf("Failed to create GLFW window\n");		
		glfwTerminate();
		return -1;
	}
	// Introduce the window into the current context
	glfwMakeContextCurrent(window);

	//Load GLAD so it configures OpenGL
	gladLoadGL();	
	
	initializeShaderProgram();
		
	float a = 0;
	float b = 0;
	float c = 0;	

	// Main while loop
	while (!glfwWindowShouldClose(window))
	{
		// Specify the color of the background
		glClearColor(0.7f, 0.0f, 0.0f, 1.0f);
		// Clean the back buffer and assign the new color to it
		glClear(GL_COLOR_BUFFER_BIT);		
		
		float cx = 0.1 * (cos(b) + 2*sin(c) -1);
		float cy = 0.2 * (3*sin(c) - cos(b) + 1);
		float r = 0.1+0.4*absf32(cos(a));
						
		bresenham_line(cx, cy, cx+r*cos(a), cy+r*sin(a), drawPixel);				
		bresenham_circle(cx, cy, r, drawPixel);
		
		a+=0.01;
		b+=0.02;
		c+=0.03;	
		
		// Swap the back buffer with the front buffer
		glfwSwapBuffers(window);
		// Take care of all GLFW events
		glfwPollEvents();
	}

	
	glDeleteProgram(shaderProgram);
	// Delete window before ending the program
	glfwDestroyWindow(window);
	// Terminate GLFW before ending the program
	glfwTerminate();
	return 0;
}