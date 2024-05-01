#include "IteratorMD.h"
#include "MD.h"

using namespace std;

IteratorMD::IteratorMD(const MD& _md): md(_md) {
	keys_iter = IteratorLDI<pair<TCheie, LDI<TValoare>*>>(md.list);
	//std::cout << md.list.dim() << '\n';
	if (md.list.dim() > 0)
	{
		keys_iter.first();
		//std::cout << "k="<<keys_iter.elem().first << '\n';
		//std::cout << "l=" << *keys_iter.elem().second << '\n';
		vals_iter = IteratorLDI<TValoare>(*keys_iter.elem().second);
		vals_iter.first();
		//std::cout << vals_iter.elem() << '\n';
	}
}

TElem IteratorMD::element() const{
	TCheie c = keys_iter.elem().first;
	TValoare v = vals_iter.elem();
	return pair <TCheie, TValoare>(c, v);
}

bool IteratorMD::valid() const {
	return keys_iter.valid() && vals_iter.valid();
}

void IteratorMD::urmator() {
	vals_iter.next();
	if (!vals_iter.valid())
	{
		keys_iter.next();
		if (keys_iter.valid())
		{
			vals_iter = IteratorLDI<TValoare>(*keys_iter.elem().second);
		}
	}	
}

void IteratorMD::prim() {
	keys_iter.first();
	vals_iter = IteratorLDI<TValoare>(*keys_iter.elem().second);
}

/**

Functia avanseazaKPasi(iter, k)
pre:
	iter : TIteratorMD iterator la MD
	k    : Intreg nr de pozitii cu care avansam iter(>0)
post:
	iter : avansat cu k pozitii

	Daca k<=0 atunci
		@ arunca exceptie
	SfDaca

	i <- k
	Cat timp i>0 executa
		urmator(iter)
		i <- i-1
	SfCatTimp

	Daca !valid(iter) atunci
		@ arunca exceptie
	SfDaca
SfFunctia
*/
/// complexity: θ(k)
void IteratorMD::avanseazaKPasi(int k)
{
	if (k <= 0)
		throw std::exception("Step must be a non-zero positive");

	for (; k--;) urmator();

	if (!valid())
		throw std::exception("Iterator not valid");

}