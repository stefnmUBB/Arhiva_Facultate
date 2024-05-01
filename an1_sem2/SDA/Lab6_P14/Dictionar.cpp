#include "Dictionar.h"
#include <iostream>
#include "IteratorDictionar.h"

class
{
public:	
	QuadProbHash operator [](int magnitude)
	{
		// m=2^magn, c1=c2=0.5
		return ([=](int c, int i)
			{
				int m = 1 << magnitude;
				m--;
				return ((c & m) + i * (i + 1) / 2) & m;
			});
	}
} hash_generator;

// Fav θ(1)/ Defav θ(N)
bool Dictionar::add_elem(TElem* table, int cap, QuadProbHash d, TElem e)
{
	for (int i = 0; i < cap; i++)
	{
		int pos = d(e.elem_key, i);
		if (is_free(table[pos]))
		{
			table[pos] = e;
			//std::cout << "Assigned " << pos << " to key " << e.elem_key << '\n';
			return true;
		}
	}
	return false;
}

// θ(1)
Dictionar::Dictionar() {
	magnitude = 4; // 16 entries in hash table	
	d = hash_generator[magnitude];
	table = new TElem[1 << magnitude];
	for (int i = 0, cap = (1 << magnitude); i < cap; i++)
	{
		table[i] = NONE;
	}
	count = 0;
}

Dictionar::~Dictionar() {
	delete[] table;
}

// Fav. θ(1) amortizat/ Defav O(N)
TValoare Dictionar::adauga(TCheie c, TValoare v){
	int pos = get_pos_of_key(c);
	//std::cout << c << ' ' << pos << '\n';
	if (pos >= 0) // key already exists
	{
		int old_val = table[pos].elem_value;
		table[pos].elem_value = v;
		return old_val;
	}
	// unknown key, add it to the table

	if (!add_elem(table, 1 << magnitude, d, { c,v }))
	{
		// table full, resize and rehash
		int old_cap = 1 << magnitude;
		magnitude++;
		d = hash_generator[magnitude];
		TElem* new_table = new TElem[1 << magnitude];
		for (int i = 0, cap = (1 << magnitude); i < cap; i++)
			new_table[i] = NONE;

		for (int i = 0; i < old_cap; i++)
		{
			if (table[i].elem_key != NONE.elem_key)
			{
				add_elem(new_table, 1 << magnitude, d, table[i]);
			}
		}
		delete[] table;
		table = new_table;
		// finally, retry adding the new element
		add_elem(table, 1 << magnitude, d, { c,v });
	}
	count++;
	return NULL_TVALOARE;
}

// Daca c nu exista: O(1/(1-a))), a=n/m
// c exista: O(1/a * ln(1/1-a)), a=n/m
// a = constant
// fav. θ(1)/ defav O(n)
int Dictionar::get_pos_of_key(TCheie elem_key) const
{
	for (int i = 0, cap = (1 << magnitude); i < cap; i++)
	{
		int pos = d(elem_key, i);
		if (table[pos].elem_key == elem_key)
			return pos; // element found
		if (table[pos].elem_key == NONE.elem_key)
			return -1; // no more collisions
	}
	return -1; // table full
}

//cauta o cheie si returneaza valoarea asociata (daca dictionarul contine cheia) sau null
// Th. fav θ(1), defav O(n)
TValoare Dictionar::cauta(TCheie c) const{
	int pos = get_pos_of_key(c);
	return pos >= 0 ? table[pos].elem_value : NULL_TVALOARE;
}

// presupune cautare O(S)--^ + stergere O(1) => aceeasi compl ca la cautare
// optimizare: inlocuirea flagurilor STERS cu NIL, atunci cand acest lucru nu 
// afecteaza corectitudinea tabelei θ(M)
TValoare Dictionar::sterge(TCheie c)
{
	int free_count = 0;
	for (int i = 0, cap = (1 << magnitude); i < cap; i++)
	{
		int pos = d(c, i);
		if (is_free(table[pos]))
			free_count++;
		if (table[pos].elem_key == NONE.elem_key)
		{
			return NULL_TVALOARE;
		}
		if (table[pos].elem_key == c)
		{
			int val = table[pos].elem_value;

			table[pos] = DELETED;
			count--;

			return val;
		}
	}
	if (free_count == (1 << magnitude))
	{
		for (int i = 0, cap = (1 << magnitude); i < cap; i++)
			table[i] = NONE;

	}
	return NULL_TVALOARE;
}

// θ(1)
int Dictionar::dim() const {	
	return count;
}

// θ(1)
bool Dictionar::vid() const{	
	return count == 0;
}

// θ(1)
IteratorDictionar Dictionar::iterator() const {
	return  IteratorDictionar(*this);
}

// θ(1)
// pentru o pozitie in tabela verificam daca nu este ocupata de elemente
bool Dictionar::is_free(TElem elem)
{
	return elem.elem_key >= INT_MAX - 1;
}

/*
*	C. liniara θ(N), N=nr.chei , dc. N==0, dict vid θ(1)
* 
	Functia cheieMaxima(d)
		Pre
			d : TDictionar
		Post
			cheieMaxima : TCheie, cheia maxima din d
		________

		Daca vid(d) atunci
			cheieMaxima <- NIL
		SfDaca

		iterator(d, it)
		prim(it)                  # stim ca exista un prim, pt ca d nu este vid
		max <- element(it).cheie
		urmator(it)

		CatTimp valid(it) executa # iteram restul cheilor
			elem <- element(it)
			max <- @maximulDintre(max, elem.cheie)
			urmator(it)
		SfCatTimp

		cheieMaxima <- max        # returnam max
	SfFunctia
*/

TCheie Dictionar::cheieMaxima() const
{
	if (vid()) return NULL_TCHEIE;
	IteratorDictionar it = iterator();
	it.prim();
	TCheie max = it.element().elem_key;
	it.urmator();
	while (it.valid())
	{
		TElem elem = it.element();
		if (max < elem.elem_key)
		{
			max = elem.elem_key;
		}
		it.urmator();
	}
	return max;
}