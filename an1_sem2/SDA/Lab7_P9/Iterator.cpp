#include "Iterator.h"
#include "DO.h"

#include <iostream>

using namespace std;

Iterator::Iterator(const DO& d) : dict(d){
	prim();
}

void Iterator::prim(){
	pos = dict.min(dict.root);
}

void Iterator::urmator() {
	pos = dict.next_ord(pos);
}

bool Iterator::valid() const{	
	return pos >= 0;
}

TElem Iterator::element() const{
	return dict.elems[pos];
}


