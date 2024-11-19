#pragma once
#include <math.h>

class RubikCube;

class RubikMove
{
private:
	RubikCube* cube;
	float a = 0;
	const float pi = atan(1)*4;	
	int axis, layer;
	int direction;
public:
	RubikMove(RubikCube* cube, int axis, int layer, int direction) : cube(cube), axis(axis), layer(layer), direction(direction) {}	
	bool update();				
};