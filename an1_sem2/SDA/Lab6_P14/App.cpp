#include "TestExtins.h"
#include "TestScurt.h"


#include <iostream>
using namespace std;

#include "Dictionar.h"
#include <cassert>

void test_ch_max()
{
	Dictionar d;
	assert(d.cheieMaxima() == NULL_TCHEIE); // vid

	d.adauga(15,2);
	assert(d.cheieMaxima() == 15); // doar prim

	d.adauga(7, 3);	
	assert(d.cheieMaxima() == 15);

	d.adauga(18, 5);
	assert(d.cheieMaxima() == 18);

	d.sterge(18);
	assert(d.cheieMaxima() == 15);

	d.sterge(7);
	d.sterge(15);
	assert(d.cheieMaxima() == NULL_TCHEIE); // din nou vid
}

int main() {	
	test_ch_max();
	testAll();
	testAllExtins();

	cout << "That's all!" << endl;
	return 0;
}

