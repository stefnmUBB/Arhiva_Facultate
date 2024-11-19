#pragma once

#define COLOR_RED 1.0f, 0.0f, 0.0f, 1.0f
#define COLOR_GREEN 0.0f, 1.0f, 0.0f, 1.0f
#define COLOR_BLUE 0.0f, 0.0f, 1.0f, 1.0f
#define COLOR_WHITE 1.0f, 1.0f, 1.0f, 1.0f

#define TEXEL(u,v,tu,tv,tw,th) 1.0f*((tu)+(u))/(tw), 1.0f*((tv)+(v))/(th)

static constexpr float vertices[] = {
	 -1.0f,  -1.0f,  1.0f, COLOR_WHITE,  TEXEL(0.0f, 0.0f,0,0,2,2),
	 -1.0f,  -1.0f, -1.0f, COLOR_RED,  TEXEL(0.0f, 1.0f,0,0,2,2),
	  1.0f,  -1.0f, -1.0f, COLOR_RED,  TEXEL(1.0f, 1.0f,0,0,2,2),
	  1.0f,  -1.0f,  1.0f, COLOR_BLUE,  TEXEL(1.0f, 0.0f,0,0,2,2),	 
	
	-1.0f,  1.0f,  1.0f, COLOR_WHITE,  TEXEL(0.0f, 0.0f,0,1,2,2),
	 -1.0f,  1.0f, -1.0f, COLOR_GREEN,  TEXEL(0.0f, 1.0f,0,1,2,2),
	  1.0f,  1.0f, -1.0f, COLOR_GREEN,  TEXEL(1.0f, 1.0f,0,1,2,2),
	  1.0f,  1.0f,  1.0f, COLOR_BLUE,  TEXEL(1.0f, 0.0f,0,1,2,2),	 
	 
	 -1.0f, -1.0f, -1.0f, COLOR_RED,  TEXEL(0.0f, 0.0f,1,0,2,2),
	 -1.0f,  1.0f, -1.0f, COLOR_GREEN,  TEXEL(0.0f, 1.0f,1,0,2,2),
	 -1.0f,  1.0f,  1.0f, COLOR_BLUE,  TEXEL(1.0f, 1.0f,1,0,2,2),
	 -1.0f, -1.0f,  1.0f, COLOR_WHITE,  TEXEL(1.0f, 0.0f,1,0,2,2),
	 
	  1.0f, -1.0f, -1.0f, COLOR_RED,  TEXEL(0.0f, 0.0f,1,1,2,2),
	  1.0f,  1.0f, -1.0f, COLOR_GREEN,  TEXEL(0.0f, 1.0f,1,1,2,2),
	  1.0f,  1.0f,  1.0f, COLOR_BLUE,  TEXEL(1.0f, 1.0f,1,1,2,2),
	  1.0f, -1.0f,  1.0f, COLOR_WHITE,  TEXEL(1.0f, 0.0f,1,1,2,2),
	  
	 -1.0f, -1.0f, -1.0f, COLOR_RED,  TEXEL(0.0f, 0.0f,0,0,2,2),
	  1.0f, -1.0f, -1.0f, COLOR_GREEN,  TEXEL(0.0f, 1.0f,0,0,2,2),
	  1.0f,  1.0f, -1.0f, COLOR_BLUE,  TEXEL(1.0f, 1.0f,0,0,2,2),
	 -1.0f,  1.0f, -1.0f, COLOR_WHITE,  TEXEL(1.0f, 0.0f,0,0,2,2),
	 
	 -1.0f, -1.0f,  1.0f, COLOR_RED,  TEXEL(0.0f, 0.0f,0,1,2,2),
	  1.0f, -1.0f,  1.0f, COLOR_GREEN,  TEXEL(0.0f, 1.0f,0,1,2,2),
	  1.0f,  1.0f,  1.0f, COLOR_BLUE,  TEXEL(1.0f, 1.0f,0,1,2,2),
	 -1.0f,  1.0f,  1.0f, COLOR_WHITE,  TEXEL(1.0f, 0.0f,0,1,2,2),	
};

#define QFACEcw(a,b,c,d) a,b,d, b,c,d
#define QFACEccw(a,b,c,d) a,d,b, b,d,c

static constexpr unsigned int triangles[] = {
	QFACEccw(0,1,2,3),	
	QFACEccw(4,5,6,7),	
	
	QFACEcw(8,9,10,11),	
	QFACEcw(12,13,14,15),	
	
	QFACEcw(16,17,18,19),	
	QFACEcw(20,21,22,23),		
};