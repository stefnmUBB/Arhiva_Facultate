#include <iostream>

#include "TestScurt.h"
#include "TestExtins.h"
#include "DO.h"

#include <iostream>
#include <cassert>

bool relatieR(TCheie cheie1, TCheie cheie2) {
    if (cheie1 <= cheie2) {
        return true;
    }
    else {
        return false;
    }
}


void testDiferentaValoareMaxMin()
{
    DO d(relatieR);
    assert(d.diferentaValoareMaxMin() == -1); // 0 elemente, => -1
    
    d.adauga(1, 3);
    assert(d.diferentaValoareMaxMin() == 0); // 1 element, max=min => 0

    d.adauga(2, 3);
    assert(d.diferentaValoareMaxMin() == 0); // 1 element, max=min => 0

    d.adauga(3, 5);
    std::cout << d.diferentaValoareMaxMin()<<'\n';
    assert(d.diferentaValoareMaxMin() == 2); 

    d.adauga(4, 7);
    assert(d.diferentaValoareMaxMin() == 4); // max=7, min==3

    d.adauga(5, 4);
    assert(d.diferentaValoareMaxMin() == 4); // elemente redundante

    d.sterge(1);
    assert(d.diferentaValoareMaxMin() == 4); // min inca e 3

    d.sterge(2);
    assert(d.diferentaValoareMaxMin() == 3); // min = 4

}

int main(){
    testDiferentaValoareMaxMin();
    testAll();
    testAllExtins();
    std::cout<<"Finished Tests!"<<std::endl;
}
