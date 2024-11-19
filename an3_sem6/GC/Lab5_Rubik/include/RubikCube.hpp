#pragma once

#include "RubikCubeComponent.hpp"
#include <math.h>

#include "RubikMove.hpp"

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#include <iostream>

class RubikCube : public GraphicsGroup
{
private:
	static constexpr float pi = atan(1) * 4;
private:
	struct rot3 
	{
		float t[9] = {1,0,0,0,1,0,0,0,1};				
		float a,b,c;
		rot3() { compute_angles(); }						
		rot3(float t[9]) { for(int i=0;i<9;i++) { this->t[i]=t[i]; } compute_angles(); }		
		
		void compute_angles()
		{
			c = - acos(t[0]/sqrt(t[0]*t[0]+t[1]*t[1])) * (t[1]<0?-1:1);
			b = pi/2 - acos(t[2]);
			a = - acos(t[8]/sqrt(t[8]*t[8]+t[5]*t[5])) * (t[5]<0?-1:1);			
			a *= 180/pi, b *= 180/pi, c *= 180/pi;
			
			//std::cout<<"New angles: "<<a<<", "<<b<<", "<<c<<"\n";
		}
		
		
		rot3 mul(rot3 r)
		{
			float o[9];
			for(int i=0;i<9;i++) o[i]=0;
			for(int i=0;i<3;i++)
				for(int j=0;j<3;j++)
					for(int k=0;k<3;k++)
						o[3*i+k] += r.t[3*i+j] * t[3*j+k];
			/*std::cout<<r.t[0]<<" "<<r.t[1]<<" "<<r.t[2]<<" "<<r.t[3]<<" "<<r.t[4]<<" "<<r.t[5]<<" "<<r.t[6]<<" "<<r.t[7]<<" "<<r.t[8]<<"\n";
			std::cout<<t[0]<<" "<<t[1]<<" "<<t[2]<<" "<<t[3]<<" "<<t[4]<<" "<<t[5]<<" "<<t[6]<<" "<<t[7]<<" "<<t[8]<<"\n";
			std::cout<<o[0]<<" "<<o[1]<<" "<<o[2]<<" "<<o[3]<<" "<<o[4]<<" "<<o[5]<<" "<<o[6]<<" "<<o[7]<<" "<<o[8]<<"\n";*/
			return rot3(o);
		}
		
		
		rot3 rotx(float a) { float c = cos(a), s=sin(a); float r[9] = { 1, 0, 0, 0, c, -s, 0, s, c }; return mul(rot3(r)); }
		rot3 roty(float a) { float c = cos(a), s=sin(a); float r[9] = { c, 0, s, 0, 1, 0, -s, 0, c }; return mul(rot3(r)); }
		rot3 rotz(float a) { float c = cos(a), s=sin(a); float r[9] = { c, -s, 0, s, c, 0, 0, 0, 1 }; return mul(rot3(r)); }
		
		rot3 operator +(float f[3]) 
		{
			//std::cout<<"Rot "<<f[0]<<", "<<f[1]<<", "<<f[2]<<"\n";
			if(f[0]!=0) return rotx(f[0]*pi/180);
			if(f[1]!=0) return roty(f[1]*pi/180);
			if(f[2]!=0) return rotz(f[2]*pi/180);
			return *this;
		}		
		
	};
	rot3 rot_state[3][3][3];

	RubikCubeComponent* cubes[3][3][3];
	float d = 2.05;
	float a=0;	
public:	
	RubikCube();
		
	void rotate_face(int axis, int layer, float a)
	{
		float cosa = cos(a), sina = sin(a);
		float rot[3] = {0,0,0};
		rot[axis] = a*180/pi;
		int I[3];
		I[axis] = layer;		
		for(int p=0;p<3;p++)
		{
			for(int q=0;q<3;q++)
			{
				I[(axis+1)%3]=p, I[(axis+2)%3]=q;				
				int i=I[0], j=I[1], k=I[2];
				if(i==1 && j==1 && k==1) continue;				
				float pos[3] = { d*(i-1), d*(j-1), d*(k-1) };
				float rx = pos[(axis+1)%3]*cosa-pos[(axis+2)%3]*sina;
				float ry = pos[(axis+1)%3]*sina+pos[(axis+2)%3]*cosa;
				pos[(axis+1)%3]=rx;
				pos[(axis+2)%3]=ry;								
				cubes[i][j][k]->set_pos(pos[0],pos[1],pos[2]);
				rot3 rot0=rot_state[i][j][k] + rot;
				cubes[i][j][k]->set_rot(rot0.a,rot0.b,rot0.c);
				cubes[i][j][k]->update_model();																
			}
		}				
	}		
	
	void permute_face(int axis, int layer, int direction)
	{				
		int I0[3]; I0[axis] = layer;				
		int I1[3]; I1[axis] = layer;		
		float rot[3] = {0,0,0}; rot[axis] = direction * 90;
		
		rot3 tmp_rot[9];
		RubikCubeComponent* tmp[9];
		int t=0;		
		for(int p0=0;p0<3;p0++)
		{
			for(int q0=0;q0<3;q0++)
			{
				int p1 = direction==1 ? q0 : 2-q0;
				int q1 = direction==1 ? 2-p0 : p0;
				I0[(axis+1)%3]=p0, I0[(axis+2)%3]=q0;				
				I1[(axis+1)%3]=p1, I1[(axis+2)%3]=q1;
				int i=I1[0], j=I1[1], k=I1[2];	
				tmp_rot[t] = rot_state[i][j][k] + rot;
				tmp[t++] = cubes[i][j][k];		
			}
		}		

		t=0;
		for(int p0=0;p0<3;p0++)
		{
			for(int q0=0;q0<3;q0++)
			{				
				I0[(axis+1)%3]=p0, I0[(axis+2)%3]=q0;								
				int i=I0[0], j=I0[1], k=I0[2];		
				rot_state[i][j][k] = tmp_rot[t];
				cubes[i][j][k] = tmp[t++];				
				cubes[i][j][k]->update_model();
			}
		}		
	}
	
	RubikMove* create_move(int axis, int layer, int direction)
	{
		return new RubikMove(this, axis, layer, direction);
	}
	
	void set_shader(int shaderProgram)
	{
		for(int i=0;i<3;i++)
			for(int j=0;j<3;j++)
				for(int k=0;k<3;k++)				
					cubes[i][j][k]->set_shader(shaderProgram);				
	}	
};