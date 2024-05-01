#include "Colectie.h"
#include "IteratorColectie.h"
#include <exception>
#include <iostream>

using namespace std;

void Colectie::redim_vec(TElem* &v, size_t &old_cap, size_t new_cap) const
{
	TElem* w = new TElem[new_cap];
	for (size_t i = 0; i < old_cap && i < new_cap; i++)
		w[i] = v[i];
	delete[] v;
	v = w;
	old_cap = new_cap;
}

size_t Colectie::in_set_index_of(TElem elem) const
{
	for (size_t i = 0; i < set_size; i++)
	{
		if (set[i] == elem)
			return i;
	}
	return set_size;
}

Colectie::Colectie() {
	set_capacity = 1;	
	set = new TElem[1];

	el_capacity = 1;
	elems = new TElem[1];

	set_size = 0;	
	el_size = 0;
}


/*
* Compl. fav.   : T(n)=1   => Θ(1) [ size+1 < capacity ], n =nr of elements in set
* Compl. defav  : T(n)=n+1 => Θ(n) [ size == capacity-1, realloc buffer ] 
* Compl. avg    : A(n) = 1/n * sum(for i=1..n: (i+1), if i==2^k, else 1)
*                      = 1/n * (sum(1 for i=1..n)+ sum(2^i for i in 1..log2(n)))
*                      = 1/n * (n+ (2^(1+log2(n)) -1))
*                      = 1/n * (n+ (2*n-1))
*                      = 3 - 1/n => Θ(1)
* Overall : O(n)
*/
void Colectie::adauga(TElem elem) {	
	size_t index = in_set_index_of(elem);
	if (index == set_size) // elem does not exist
	{
		if (set_size + 1 == set_capacity)
		{
			redim_vec(set, set_capacity, 2 * set_capacity);
		}
		set[set_size++] = elem;
	}
	
	if (el_size + 1 == el_capacity)
	{
		redim_vec(elems, el_capacity, 2 * el_capacity);
	}
	elems[el_size++] = index;
}

/*
* k-th element : T(n) = k (search) + (n-k) (shift) = n
* Complexity : Θ(n)
* Overall   : Θ(n)
*/
bool Colectie::sterge(TElem elem) {
	size_t index = in_set_index_of(elem);
	if (index == set_size)
		return false;
	int k = 0;
	for (; elems[k] != index; k++);

	for (size_t i = k + 1; i < el_size; i++)
		elems[i - 1] = elems[i];
	el_size--;

	if (nrAparitii(elem) == 0)
	{
		for (size_t i = index + 1; i < set_size; i++)
			set[i - 1] = set[i];
		set_size--;

		for (size_t i = 0; i < el_size; i++)
		{
			if (elems[i] >= index) elems[i]--;
		}
	}

	return true;
}

/*
* Compl. fav: T(n) = 1 => Θ(1) [elem is the first]
* Compl. def: T(n) = n => Θ(n) [elem is the last]
* Compl. avg: T(n) = 1/n*(1+2+...+n) = (n+1)/2 => Θ(n)
* Overall: O(n)
*/
bool Colectie::cauta(TElem elem) const {
	return in_set_index_of(elem) != set_size;
}

/*
* T(n) = n => Θ(n) [parse whole list one time]
* no fav/defav complexities
* Overall: Θ(n)
*/
int Colectie::nrAparitii(TElem elem) const {
	int count = 0;
	for (size_t p = 0; p < el_size; p++)
		count += (set[elems[p]] == elem);
	return count;
}

// Θ(1)
int Colectie::dim() const {
	return el_size;
}

// Θ(1)
bool Colectie::vida() const {	
	return dim() == 0;
}

// Θ(1)
IteratorColectie Colectie::iterator() const {
	return  IteratorColectie(*this);
}


Colectie::~Colectie() {
	delete[] set;
	delete[] elems;
}

void Colectie::print()
{
	IteratorColectie ic = iterator();
	ic.prim();
	while (ic.valid()) {
		TElem e = ic.element();
		std::cout << e << " ";
		ic.urmator();
	}
	std::cout << "\n";
}

