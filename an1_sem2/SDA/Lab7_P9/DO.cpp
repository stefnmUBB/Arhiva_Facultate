#include "Iterator.h"
#include "DO.h"
#include <iostream>

#include <exception>
#include <cassert>
using namespace std;

#define next_free left
#define ekey first
#define evalue second

DO::DO(Relatie r) : r(r) {
	cap = 128;
	elems = new TElem[cap];
	left = new int[cap];
	right = new int[cap];
	parent = new int[cap];
	root = -1;
	first_free = 0;
	for (int i = 0; i < cap - 1; i++)
	{
		next_free[i] = i + 1;
	}
	next_free[cap - 1] = -1;
}

// θ(1) am.
int DO::create_node(TElem e)
{
	if (first_free == -1)
	{
		int new_cap = 2 * cap;
		TElem* new_elems = new TElem[new_cap];
		int* new_left = new int[new_cap];
		int* new_right = new int[new_cap];
		int* new_parent = new int[new_cap];

		for (int i = 0; i < cap; i++)
		{
			new_elems[i] = elems[i];
			new_left[i] = left[i];
			new_right[i] = right[i];
			new_parent[i] = parent[i];
		}

		delete[] elems;
		delete[] left;
		delete[] right;
		delete[] parent;

		elems = new_elems;
		left = new_left;
		right = new_right;
		parent = new_parent;

		first_free = cap;
		cap = new_cap;
		for (int i = first_free; i < cap - 1; i++)
			next_free[i] = i + 1;
		next_free[cap - 1] = -1;
	}

	int pos = first_free;
	first_free = next_free[first_free];

	elems[pos] = e;
	left[pos] = -1;
	right[pos] = -1;
	parent[pos] = -1;

	return pos;
}

// O(h)
int DO::add_rec(int upper, int p, TElem e, int& old_value)
{
	if (p == -1)
	{
		p = create_node(e);
		parent[p] = upper;		
		return p;
	}
	if (e.ekey == elems[p].ekey) // 
	{
		old_value = elems[p].evalue;
		elems[p].evalue = e.evalue;		
		return p;
	}
	if (r(e.ekey, elems[p].ekey))
	{
		left[p] = add_rec(p, left[p], e, old_value);
	}
	else
	{
		right[p] = add_rec(p, right[p], e, old_value);
	}	
	return p;
}

//adauga o pereche (cheie, valoare) in dictionar
//daca exista deja cheia in dictionar, inlocuieste valoarea asociata cheii si returneaza vechea valoare
//daca nu exista cheia, adauga perechea si returneaza null
// O(h), fav θ(1), defav O(h)
TValoare DO::adauga(TCheie c, TValoare v) {	
	if (root == -1)
	{
		root = create_node({ c,v });
		size++;
		return NULL_TVALOARE;
	}

	int old_value = INT_MAX;
	root = add_rec(-1, root, { c,v }, old_value);
	size += (old_value == INT_MAX);
	return old_value == INT_MAX ? -1 : old_value;
}

// O(h), fav θ(1), defav O(h)
TValoare DO::find_rec(int p, int key) const
{
	if (p == -1)
	{		
		return NULL_TVALOARE;
	}
	if (key == elems[p].ekey) 
	{
		return elems[p].evalue;			
	}
	if (r(key, elems[p].ekey))
	{
		return find_rec(left[p], key);	
	}
	else
	{
		return find_rec(right[p], key);
	}		
}

//cauta o cheie si returneaza valoarea asociata (daca dictionarul contine cheia) sau null
// O(h), fav θ(1), defav O(h)
TValoare DO::cauta(TCheie c) const {
	return find_rec(root, c);
}

// O(h), fav θ(1), defav O(h)
int DO::del_rec(int p, TCheie c, TValoare& res)
{
	if (p == -1)
	{
		return -1;
	}
	if (c == elems[p].ekey)
	{						
		res = elems[p].evalue;
		if (left[p] != -1 && right[p] != -1)
		{						
			cout << "LR\n";
			TElem min = elems[right[p]];
			int q = right[p];
			while (q != -1)
			{
				min = elems[q].evalue < min.evalue ? elems[q] : min;
				q = left[q];
			}			
			elems[p] = min;
			int _;
			right[p] = del_rec(right[p], min.ekey, _);
			parent[right[p]] = p;
			return p;
		}
		else
		{			
			int repl = -1;
			if (left[p] == -1)
			{
				cout << "R\n";
				repl = right[p];
			}
			else // if (right[p] == -1)
			{
				cout << "L\n";
				repl = left[p];
			}			

			elems[p] = { 0,0 };
			parent[p] = -1;
			right[p] = -1;
			next_free[p] = first_free;
			first_free = p;

			if(repl!=-1)
				parent[repl] = -1;
			
			return repl;
		}
	}
	if (r(c, elems[p].ekey))
	{	
		left[p] = del_rec(left[p], c, res);
		return p;
	}
	else
	{
		right[p] = del_rec(right[p], c, res);
		return p;
	}
}

//sterge o cheie si returneaza valoarea asociata (daca exista) sau null
TValoare DO::sterge(TCheie c) {

	TValoare val = INT_MAX;
	root = del_rec(root, c, val);
	size -= (val != INT_MAX);
	return val == INT_MAX ? NULL_TVALOARE : val;
}

//returneaza numarul de perechi (cheie, valoare) din dictionar
// θ(1)
int DO::dim() const {	
	return size;
}

//verifica daca dictionarul e vid
// θ(1)
bool DO::vid() const {	
	return size==0;
}

Iterator DO::iterator() const {
	return  Iterator(*this);
}

DO::~DO() {
	delete[] elems;
	delete[] left;
	delete[] right;
	delete[] parent;
}

void DO::print()
{
	cout << "----------------------------------------------------\n";
	print_rec(root, 0);
	cout << "----------------------------------------------------\n";
}

void DO::print_rec(int p, int lvl)
{
	cout << string(2 * lvl, ' ');
	if (p == -1)
	{
		cout << "*\n";
		return;
	}
	cout << elems[p].ekey << "  " << parent[p] <<" ("<< elems[parent[p]].ekey << ")\n";
	print_rec(left[p], lvl + 1);
	print_rec(right[p], lvl + 1);
}

// θ(h)
int DO::min(int p) const
{	
	if (size == 0) return -1;
	int mnp = p;
	int q = p;
	while (q != -1)
	{
		mnp = r(elems[q].evalue, elems[mnp].evalue) ? q : mnp;
		q = left[q];
	}
	return mnp;
}

// θ(h)
int DO::max(int p) const
{
	if (size == 0) return -1;
	int mxp = p;
	int q = p;
	while (q != -1)
	{
		mxp = r(elems[mxp].evalue, elems[q].evalue) ? q : mxp;
		q = right[q];
	}
	return mxp;
}

// O(h), sau θ(1) daca arborele e echilibrat
int DO::next_ord(int p) const
{
	if (right[p] != -1)
	{		
		return min(right[p]);
	}

	int prec = parent[p];
	
	while (prec != -1 && p == right[prec])
	{
		p = prec;
		prec = parent[p];
		cout << "P" << prec << '\n';
	}
	return prec;
}

/// θ(n), n=nr intrari in dictionar
/// Functia diferentaValoareMaxMin(d)
///		Pre:
///			d : DictionarOrdonat
///		Post
///			diferentaValoareMaxMin : Intreg === @{max_val(d) - min_val(d)}
/// 
///		Daca vid(d) atunci
///			diferentaValoareMaxMin <- -1
///		Altfel
///			iterator(d, it)
///			prim(it)
///			min <- element(it).val
///			max <- element(it).val
///			CatTimp valid(it) executa
///				v <- element(it).val
///				min <- @minimulDintre(min, v) cu relatia r(d)
///				max <- @maximulDintre(max, v) cu relatia r(d)
///				urmator(it)
///			SfCatTimp
///			result <- max - min
///			diferentaValoareMaxMin <- result
///		SfDaca
///		
int DO::diferentaValoareMaxMin() const
{
	if(root == -1) // vid
	{
		return -1;
	}	
		
	Iterator it = this->iterator();
	it.prim();
	TValoare min = it.element().evalue;
	TValoare max = it.element().evalue;
	while (it.valid())
	{
		TValoare v = it.element().evalue;
		cout << "C= " << it.element().ekey << "  Val " << v << '\n';
		min = r(v, min) ? v : min;
		max = r(max, v) ? v : max;
		it.urmator();
	}
	cout << max << ' ' << min << '\n';
	return max - min;
}