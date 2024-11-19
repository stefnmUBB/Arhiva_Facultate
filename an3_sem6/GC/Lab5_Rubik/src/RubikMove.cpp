#include "RubikMove.hpp"
#include "RubikCube.hpp"

bool RubikMove::update()
{
	bool result=true;
	a+=0.1;
	if(a>pi/2)	
		a=pi/2, result=false;			
	cube->rotate_face(axis, layer, direction*a);
	if(!result)
	{
		cube->permute_face(axis, layer, direction);
	}	
	return result;	
}