#pragma once
#include<vector>
#include<utility>
#include "LDI.h"

using namespace std;

typedef int TCheie;
typedef int TValoare;

typedef std::pair<TCheie, TValoare> TElem;

class IteratorMD;

class MD
{
	friend class IteratorMD;

private:
	/* aici e reprezentarea */

	LDI<pair<TCheie, LDI<TValoare>*>> list;
	int vals_count;
	
public:
	// constructorul implicit al MultiDictionarului
	MD();

	// adauga o pereche (cheie, valoare) in MD	
	void adauga(TCheie c, TValoare v);

	//cauta o cheie si returneaza vectorul de valori asociate
	vector<TValoare> cauta(TCheie c) const;

	//sterge o cheie si o valoare 
	//returneaza adevarat daca s-a gasit cheia si valoarea de sters
	bool sterge(TCheie c, TValoare v);

	//returneaza numarul de perechi (cheie, valoare) din MD 
	int dim() const;

	//verifica daca MultiDictionarul e vid 
	bool vid() const;

	// se returneaza iterator pe MD
	IteratorMD iterator() const;

	// destructorul MultiDictionarului	
	~MD();	

	friend std::ostream& operator<<(ostream& o, MD& md)
	{
		md.list.parse([&o](pair < TCheie, LDI<TValoare>*> p)
			{
				o << p.first << " : " << (*p.second) << '\n';
				return 1;
			});
		return o;
	}

};

