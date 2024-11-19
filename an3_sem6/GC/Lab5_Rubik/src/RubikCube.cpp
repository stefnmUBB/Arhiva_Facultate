#include "RubikCube.hpp"

RubikCube::RubikCube()
{
	for(int i=0;i<3;i++)
		for(int j=0;j<3;j++)
			for(int k=0;k<3;k++)
			{								
				cubes[i][j][k] = new RubikCubeComponent({i,j,k});					
				cubes[i][j][k]->move(d*(i-1),d*(j-1),d*(k-1));
				if(i==1 && j==1 && k==1)
					cubes[i][j][k]->set_scale(1.2,1.2,1.2);				
				cubes[i][j][k]->update_model();				
				add_component(cubes[i][j][k]);
			}			
}