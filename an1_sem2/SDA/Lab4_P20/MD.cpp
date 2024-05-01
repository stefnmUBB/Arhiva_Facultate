#include "MD.h"
#include "IteratorMD.h"
#include <exception>
#include <iostream>

using namespace std;


MD::MD() {	
	vals_count = 0;
}

// cu oepratiile din LDI: O(K) cautarea cheii, θ(1) adaugarea => O(K)
void MD::adauga(TCheie c, TValoare v) {
	try
	{
		//cout << "keys = " << list.dim() << '\n';
		auto entry = list.search([&c](pair<TCheie, LDI<TValoare>*>& item)
			{
				return item.first == c;
			});
		entry.second->add(v);
	}
	catch (list_item_not_found_exception)
	{
		LDI<TValoare>* new_key_list = new LDI<TValoare>();
		new_key_list->add(v);
		list.add(make_pair(c, new_key_list));
	}
	vals_count++;
}

// O(K) cautarea cheii, O(M) stergerea v, M=max valori/cheie => O(K+M) => O(N), N=total elem.
bool MD::sterge(TCheie c, TValoare v) {
	try
	{
		LDI<TValoare>* key_list = list.search([&c](pair<TCheie, LDI<TValoare>*>& item)
			{
				return item.first == c;
			}).second;
		if (key_list->remove(v))
		{
			if (key_list->dim() == 0)
			{
				list.remove(make_pair(c, key_list));
			}
			vals_count--;
			return true;
		}
		return false;
	}
	catch (list_item_not_found_exception)
	{
		return false;
	}
}

// O(K)
vector<TValoare> MD::cauta(TCheie c) const {
	try
	{
		LDI<TValoare>* key_list = list.search([&c](pair<TCheie, LDI<TValoare>*>& item)
			{
				return item.first == c;
			}).second;
		return key_list->to_vector();
	}
	catch (list_item_not_found_exception) 
	{
		return vector<TValoare>();
	}	
}

// θ(1)
int MD::dim() const {
	return vals_count;
}

// θ(1)
bool MD::vid() const {	
	return vals_count==0;
}

IteratorMD MD::iterator() const {
	return IteratorMD(*this);
}


// θ(N+K)
MD::~MD() {
	list.parse([](pair<TCheie, LDI<TValoare>*>& item)
		{
			item.second->free();
			delete item.second;
			return 1;
		});

	list.free();
}

