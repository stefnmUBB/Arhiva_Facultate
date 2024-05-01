#pragma once
#include <climits>


#define NULL_TCHEIE INT_MAX
#define NULL_TVALOARE -1
typedef int TCheie;
typedef int TValoare;

class IteratorDictionar;

#include <utility>
#include <functional>
typedef std::pair<TCheie,TValoare> TElem;
typedef std::function<int(int, int)> QuadProbHash;


const TElem NONE = { INT_MAX ,0 };
const TElem DELETED = { INT_MAX - 1 ,0 };
#define elem_key first
#define elem_value second

class Dictionar {
	friend class IteratorDictionar;

private:	
	QuadProbHash d;

	TElem* table;
	int magnitude;

	int count;

	// separate add method for inserting and rehashing purposes
	// assumes key foes not already exist in table
	// returns false if table is full
	static bool add_elem(TElem* table, int cap, QuadProbHash d, TElem e);

	static bool is_free(TElem elem);

	int get_pos_of_key(TCheie elem_key) const;
public:

	// constructorul implicit al dictionarului
	Dictionar();

	// adauga o pereche (cheie, valoare) in dictionar	
	//daca exista deja cheia in dictionar, inlocuieste valoarea asociata cheii si returneaza vechea valoare
	// daca nu exista cheia, adauga perechea si returneaza null: NULL_TVALOARE
	TValoare adauga(TCheie c, TValoare v);

	//cauta o cheie si returneaza valoarea asociata (daca dictionarul contine cheia) sau null: NULL_TVALOARE
	TValoare cauta(TCheie c) const;

	//sterge o cheie si returneaza valoarea asociata (daca exista) sau null: NULL_TVALOARE
	TValoare sterge(TCheie c);

	//returneaza numarul de perechi (cheie, valoare) din dictionar 
	int dim() const;

	//verifica daca dictionarul e vid 
	bool vid() const;

	// se returneaza iterator pe dictionar
	IteratorDictionar iterator() const;

	TCheie cheieMaxima() const;


	// destructorul dictionarului	
	~Dictionar();

};


