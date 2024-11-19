#pragma once

#include "GraphicObject.hpp"
#include "buffers.hpp"
#include <stdlib.h>

struct RubikCubeId 
{ 
	int i,j,k; 
	int to_buffer_id() { return i*9+j*3+k; }
};

class RubikCubeComponent : public GraphicObjectInstance
{
private:
	RubikCubeId id;	
	int buffer_id;
public:
	RubikCubeComponent(RubikCubeId id) : GraphicObjectInstance(create_graphics(id.to_buffer_id())), id(id), buffer_id(id.to_buffer_id())
	{ }
	
private:	
	GraphicObject* create_graphics(int id)
	{
		int verts_size = sizeof(vertices);
		int triangles_size = sizeof(triangles);
		float* verts = new float[verts_size/sizeof(float)];
		memcpy(verts, vertices, sizeof(vertices));
		
		for(int i=0;i<6;i++)
		{
			if(!shown_faces[6*id+i])
			{
				for(int j=0;j<4;j++)
					verts[4*12*i + 12*j+ 3] = verts[4*12*i + 12*j + 4] = verts[4*12*i + 12*j + 5] = 0;
			}
		}
		
		return new GraphicObject(verts, verts_size, triangles, triangles_size);	
	}
};