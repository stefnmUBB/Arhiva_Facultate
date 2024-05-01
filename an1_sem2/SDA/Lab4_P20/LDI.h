#pragma once

#include <iostream>
#include <functional>
#include <utility>
#include <vector>

template<typename TItem> using ParseItemFunc = std::function<int(TItem&)>;
template<typename TItem> using SearchItemFunc = std::function<bool(TItem&)>;

class list_item_not_found_exception : public std::exception
{
public: list_item_not_found_exception(const char* msg) : std::exception(msg) { }
};

template<typename TItem> class IteratorLDI;

template<typename TItem> class LDI
{
	friend class IteratorLDI<TItem>;
private:
	TItem* items;
	int* prev;
	int* next;
	int cap;

	int size;

	int first, last;

	int first_free;

private:
	int get_free_pos();
	void add_free_pos(int i);

	void redim();
public:
	LDI();

	void add(TItem item);

	bool remove(TItem item);

	void parse(const ParseItemFunc<TItem>& parse_fnc);

	const TItem& search(const SearchItemFunc<TItem>& search_func) const;

	friend std::ostream& operator << (std::ostream& o, LDI<TItem>& lst)
	{
		lst.parse([&o](TItem& item) { o << item << ' '; return 0; });
		return o;
	}

	size_t dim() const;

	std::vector<TItem> to_vector() const;

	void free();
};

template<typename TItem> int LDI<TItem>::get_free_pos()
{
	int i = first_free;
	//std::cout << "nxt : " << i << '\n';
	first_free = next[first_free];
	return i;
}

template<typename TItem> void LDI<TItem>::add_free_pos(int i)
{
	next[i] = first_free;
	first_free = i;
}

// T(n)=2*n => θ(n)
template<typename TItem> void LDI<TItem>::redim()
{
	TItem* new_items = new TItem[2*cap];
	int* new_prev = new int[2*cap];
	int* new_next = new int[2*cap];

	for (int i = 0; i < cap; i++)
	{
		new_items[i] = items[i];
		new_prev[i] = prev[i];
		new_next[i] = next[i];
	}
	cap *= 2;

	delete[] items;
	delete[] prev;
	delete[] next;

	items = new_items;
	prev = new_prev;
	next = new_next;
}

template<typename TItem> LDI<TItem>::LDI()
{
	cap = 256;
	items = new TItem[cap];
	prev = new int[cap];
	next = new int[cap];

	first_free = 0;
	for (int i = 0; i < cap - 1; i++)
	{
		next[i] = i + 1;
	}
	next[cap - 1] = -1;

	first = last = -1;
	size = 0;
}

// θ(1) amortizat
template<typename TItem> void LDI<TItem>::add(TItem item)
{
	if (first_free == -1)
	{
		redim();
		first_free = cap/2;
		for (int i = first_free; i < cap - 1; i++)
		{
			next[i] = i + 1;
		}
		next[cap - 1] = -1;		
	}	
	int index = get_free_pos();

	if (size == 0)
	{
		items[index] = item;
		next[index] = -1;
		prev[index] = -1;
		first = last = index;
	}
	else
	{
		// add to list's back
		items[index] = item;
		next[index] = -1;
		prev[index] = last;
		next[last] = index;
		last = index;
	}
	size++;
}

// fav θ(1), defav θ(n), avg O(n)
// ovrall: O(n) cautarea item, θ(1) stergerea in sine => O(n)
template<typename TItem> bool LDI<TItem>::remove(TItem item)
{
	int i = first;
	while (i != -1)
	{
		if (items[i] == item)
		{
			int p = prev[i];
			int n = next[i];
			if(p!=-1)
				next[p] = n;
			else			
				first = n;			
			if (n != -1)
				prev[n] = p;
			else
				last = p;
			add_free_pos(i);
			size--;
			return true;
		}
		i = next[i];
	}
	return false;
}

// // θ(n) parcurgere generica 
template<typename TItem> void LDI<TItem>::parse(const ParseItemFunc<TItem>& parse_fnc)
{
	int i = first;
	while (i != -1)
	{
		if (parse_fnc(items[i]) != 0) break;
		i = next[i];
	}
}

// fav θ(1), defav θ(n), avg O(n)
// overall O(n)
template<typename TItem> const TItem& LDI<TItem>::search(const SearchItemFunc<TItem>& search_func) const
{	
	int i = first;
	while (i != -1)
	{
		if (search_func(items[i]))
		{
			return items[i];
		}
		i = next[i];
	}	
	throw list_item_not_found_exception("Item not found in list");
}

template<typename TItem> void LDI<TItem>::free()
{
	delete[] items;
	delete[] prev;
	delete[] next;
}

// θ(1)
template<typename TItem> size_t LDI<TItem>::dim() const
{
	return this->size;
}

// θ(n) 
template<typename TItem> std::vector<TItem> LDI<TItem>::to_vector() const
{
	std::vector<TItem> vec;
	int i = first;
	while (i != -1)
	{
		vec.push_back(items[i]);
		i = next[i];
	}
	return vec;
}


template<typename TItem> class IteratorLDI
{
private:
	const LDI<TItem>* list = NULL;
	int crt = -1;
public:	
	IteratorLDI() = default;
	IteratorLDI(const LDI<TItem>& list);

	void first();

	void next();

	bool valid() const;

	const TItem& elem() const;

	//int skip_list();
};

template<typename TItem> IteratorLDI<TItem>::IteratorLDI(const LDI<TItem>& list)
{
	this->list = &list;
	first(); // optional
}


template<typename TItem> void IteratorLDI<TItem>::first()
{
	if (list != NULL)
		crt = this->list->first;
}

template<typename TItem> void IteratorLDI<TItem>::next()
{
	if (valid())
		crt = list->next[crt];
}

template<typename TItem> bool IteratorLDI<TItem>::valid() const
{
	return list != NULL && crt != -1;
}

template<typename TItem> const TItem& IteratorLDI<TItem>::elem() const
{
	return list->items[crt];
}

/*// skips whole list if iterator is on the first position
template<typename TItem> int IteratorLDI<TItem>::skip_list()
{
	if (valid())
	{
		if (crt == list->first)
		{
			crt = -1;
			return list->dim();
		}
	}
	return 0;
}*/