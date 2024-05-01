#include "IteratorColectie.h"
#include "Colectie.h"

IteratorColectie::IteratorColectie(const Colectie& c): col(c), current(0) {

}

// Θ(1)
void IteratorColectie::prim() {
	current = 0;
}

// Θ(1)
void IteratorColectie::urmator() {
	current++;
}

// Θ(1)
bool IteratorColectie::valid() const {
	return current < col.dim();
}


// Θ(1)
TElem IteratorColectie::element() const {	
	return col.set[col.elems[current]];
}
