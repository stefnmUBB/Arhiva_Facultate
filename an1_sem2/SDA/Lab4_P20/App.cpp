#include <iostream>
#include "TestExtins.h"
#include "TestScurt.h"

#include "LDI.h"
#include "IteratorMD.h"
#include <cassert>

using namespace std;

void testAvanseazaKPasi()
{
	MD m;
	m.adauga(1, 100);
	m.adauga(2, 200);
	m.adauga(3, 300);
	m.adauga(1, 500);
	m.adauga(2, 600);
	m.adauga(4, 800);

	// 1: 100, 500
	// 2: 200, 600
	// 3: 300
	// 4: 800

	IteratorMD im = m.iterator();

	try { im.avanseazaKPasi(-5); assert(false); }
	catch (const std::exception&) { assert(true); }

	try { im.avanseazaKPasi(0); assert(false); }
	catch (const std::exception&) { assert(true); }

	assert(im.element().second == 100);	

	im.avanseazaKPasi(2);
	assert(im.element().second == 200);

	im.avanseazaKPasi(3);
	assert(im.element().second == 800);

	try { im.avanseazaKPasi(1); assert(false); }
	catch (const std::exception&) { assert(true); }		
}

int main() 
{	
	testAvanseazaKPasi();


	testAll();
	testAllExtins();	

	std::cout<<"End";

}
