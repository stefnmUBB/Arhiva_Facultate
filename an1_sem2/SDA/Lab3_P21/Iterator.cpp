#include "Iterator.h"
#include "LO.h"
#include <exception>

Iterator::Iterator(const LO& lo) : lista(lo){
	index = 0;
}

void Iterator::prim() {
	index = 0;
}

void Iterator::urmator(){
	if (index > lista.dim() - 1)
		throw std::exception("Invalid next iterator");
	index++;	
}

bool Iterator::valid() const{	
	return 0 <= index && index < lista.dim();
}

TElement Iterator::element() const{	
	return lista.element(index);
}


