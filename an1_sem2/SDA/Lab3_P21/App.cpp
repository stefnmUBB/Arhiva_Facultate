#include <iostream>

#include "TestScurt.h"
#include "TestExtins.h"

#include "LO.h"
#include <cassert>

bool cmp(int a, int b) { return a < b; };

void test_ultimulIndex();

int main(){
    test_ultimulIndex();
    testAll();
    testAllExtins();
    std::cout << "\nFinished LI Tests!" << std::endl;
}

void test_ultimulIndex()
{
    LO lo = LO(cmp);    
    lo.adauga(1);   
    lo.adauga(2);
    lo.adauga(5);
    lo.adauga(3);
    lo.adauga(1);
    lo.adauga(3); 
    // 1,1,2,3,3,5
    assert(lo.ultimulIndex(1) == 1);
    assert(lo.ultimulIndex(2) == 2);
    assert(lo.ultimulIndex(3) == 4);
    assert(lo.ultimulIndex(5) == 5);
    assert(lo.ultimulIndex(10) == -1);

}