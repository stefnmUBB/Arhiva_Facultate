#include "IteratorDictionar.h"
#include "Dictionar.h"

using namespace std;

// θ(1)
IteratorDictionar::IteratorDictionar(const Dictionar& d) : dict(d){
	index = 0;
	prim();
}

// Fav/med θ(1) [pozitia 0 este ocupata de o cheie], defav O(n)
void IteratorDictionar::prim() {
	int cap = 1 << dict.magnitude;
	for (index = 0; index<cap && Dictionar::is_free(dict.table[index]); index++);
}

// Fav/med θ(1), defav O(n)
void IteratorDictionar::urmator() {
	int cap = 1 << dict.magnitude;
	for (++index; index < cap && Dictionar::is_free(dict.table[index]); index++);
}

// θ(1)
TElem IteratorDictionar::element() const{	
	return dict.table[index];
}

// θ(1)
bool IteratorDictionar::valid() const {	
	return index < (1 << dict.magnitude) && !Dictionar::is_free(dict.table[index]);
}

