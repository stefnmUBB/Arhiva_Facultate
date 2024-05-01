#include "Iterator.h"
#include "LO.h"

#include <iostream>
using namespace std;

#include <exception>

class Node
{
private:
	TElement elem;
	Node* next;
public:
	Node(TElement val);
	int get_value() const;
	Node* get_next() const;
	void set_next(Node* n);
	~Node();
};

LO::LO(Relatie r) {

	rel = r;
	first_node = NULL;
	count = 0;
}

// returnare dimensiune
// There's a variable that keeps track of the list dimension => Θ(1)
int LO::dim() const {
	return count;
	/*if (first_node == NULL) // Θ(n) approach : parse until NULL
		return 0; // no elems in list
	int count = 1;
	for (Node* nd = first_node; (nd = nd->get_next()) != NULL; count++);
	return count;*/
}

// verifica daca LO e vida
bool LO::vida() const {
	return dim() == 0;
}

// returnare element
//arunca exceptie daca i nu e valid
// Fav   : i=0 => Θ(1)
// Defav : i=dim()-1 => Θ(n)
// Avg : sum_{i=1,n}(1/n*T(i))=(n+1)/2 => Θ(n)
// Overall : O(n)
TElement LO::element(int i) const {
	if (i < 0 || i >= dim())
	{
		throw exception("Invalid index");
	}
	Node* nd = first_node;	
	if (i == 0) return nd->get_value();
	for (; i--; nd = nd->get_next());
	return nd->get_value();
}

// sterge element de pe o pozitie i si returneaza elementul sters
//arunca exceptie daca i nu e valid
// Removal : Θ(1)
// Pos search : O(n)
// Fav   : i=0 => Θ(1)
// Defav : i=dim()-1 => Θ(n)
// Avg : sum_{i=1,n}(1/n*T(i))=(n+1)/2 => Θ(n)
// Overall : O(n)
TElement LO::sterge(int i) {	
	int el = element(i);
	Node* nd = first_node;
	Node* prev = NULL;

	while (nd->get_value() != el)
	{
		prev = nd;
		nd = nd->get_next();
	}

	// delete nd
	if (prev != NULL)
		prev->set_next(nd->get_next());
	else
		first_node = nd->get_next();

	delete nd;
	count--;
	return el;
}

// cauta element si returneaza prima pozitie pe care apare (sau -1)
// Fav   : e is less than min(list) => Θ(1)
// Defav : e is greater than max(list) => Θ(n)
// Avg : sum_{i=1,n}(1/n*T(i))=(n+1)/2 => Θ(n)
// Overall : O(n)
int LO::cauta(TElement e) const {
	Node* nd = first_node;
	for (int i = 0; nd != NULL; nd = nd->get_next(), i++)
	{
		if (e == nd->get_value())
			return i;
		if (rel(e, nd->get_value())) // expected search interval exceeded
			return -1;
	}	
	return -1;
}

// adaugare element in LO
// addition : Θ(1)
// pos search: O(n)
// Fav   : e is less than min(list) => Θ(1)
// Defav : e is greater than max(list) => Θ(n)
// Avg : sum_{i=1,n}(1/n*T(i))=(n+1)/2 => Θ(n)
// Overall : O(n)
void LO::adauga(TElement e) {
	count++;
	if (first_node == NULL)
	{
		first_node = new Node(e);
		return;
	}
	Node* nd = (Node*)first_node;
	if (rel(e, nd->get_value()))
	{
		// e is the first
		Node* new_node = new Node(e);
		new_node->set_next(nd);
		first_node = new_node;		
		return;
	}
	// find insert position
	Node* prev = NULL;
	while (nd != NULL && !rel(e, nd->get_value()))
	{
		prev = nd;
		nd = nd->get_next();
	}
	// add to the list
	Node* new_node = new Node(e);
	new_node->set_next(nd);
	prev->set_next(new_node);	
}

// returnare iterator
Iterator LO::iterator(){
	return Iterator(*this);
}


// destructor
// Θ(n) : each elem destroyed individually
LO::~LO() {
	Node* nd = first_node;
	while (nd != NULL)
	{
		Node* nxt = nd->get_next();
		delete nd;
		nd = nxt;
	}
}

// Θ(n)
void LO::print()
{
	Node* nd = (Node*)first_node;
	for (; nd != NULL; nd = nd->get_next())
	{
		std::cout << nd->get_value() << ' ';
	}
}

/*			
	T(n)= k, where k is the index of the first element > elem
	
	Compl. fav : Θ(1) - elem==min(list) && number of {elem}s==1
	Compl. defav : Θ(n) - elem not found in list (entire list needs to be parsed)	
	Average: T_a(n) = sum{i=1..n} (1/n*i)=(n+1)/2 => Θ(n)

	Overall : O(n)
*/
/** 

Functia ultimulIndex(lista, elem)
pre: 
	lista : LO
	elem : TComparabil
post:
	index: TPozitie = {indexul ultimei aparitii a lui elem, sau -1 daca elementul nu exista}

	index <- -1
	nod <- lista.prim()
	k <- 0

	iterez <- true
	Cat timp (iterez) si nod <> NIL executa
	  Daca lista.rel(elem, [nod].value()) atunci	    
		iterez <- false
	  SfDac
	  Daca [nod].value() = elem atunci
	     index <- k
	  SfDaca
	  nd <- [nd].urm()
	  k <- k+1
	SfCatTimp

	ultimulIndex <- index
SfFunctia

*/
int LO::ultimulIndex(TElement elem) const
{
	int index = -1;
	Node* nd = (Node*)first_node;
	for (int k = 0; nd != NULL; nd = nd->get_next(), k++)
	{
		if (rel(elem, nd->get_value()))
		{
			break; // no need to search past this point
		}
		if (nd->get_value() == elem)
		{
			index = k;
		}
	}
	return index;
}

Node::Node(TElement val)
{
	elem = val;
	next = NULL;
}

int Node::get_value() const
{
	return elem;
}

void Node::set_next(Node* n)
{
	next = n;
}

Node* Node::get_next() const
{
	return next;
}

Node::~Node()
{
}