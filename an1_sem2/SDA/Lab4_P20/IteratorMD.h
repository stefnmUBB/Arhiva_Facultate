#pragma once
#include "MD.h"

class MD;

class IteratorMD
{
	friend class MD;

private:

	//constructorul primeste o referinta catre Container
	//iteratorul va referi primul element din container
	IteratorMD(const MD& c);

	//contine o referinta catre containerul pe care il itereaza
	const MD& md;

	IteratorLDI<pair<TCheie, LDI<TValoare>*>> keys_iter;
	IteratorLDI<TValoare> vals_iter;

public:

		//reseteaza pozitia iteratorului la inceputul containerului
		void prim();

		//muta iteratorul in container
		// arunca exceptie daca iteratorul nu e valid
		void urmator();

		//verifica daca iteratorul e valid (indica un element al containerului)
		bool valid() const;

		//returneaza valoarea elementului din container referit de iterator
		//arunca exceptie daca iteratorul nu e valid
		TElem element() const;

		// mută cursorul iteratorului a.î. să refere a k-a pereche începând de la cea curentă. 
		//Iteratorul devine nevalid în cazul în care există mai puțin de k perechi rămase în multidicționar.  
		// aruncă excepție în cazul în care iteratorul este nevalid sau k este zero ori negativ.
		void avanseazaKPasi(int k);
};

