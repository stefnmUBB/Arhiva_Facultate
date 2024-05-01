#pragma once

#include <type_traits>
#include "vector.h"
#include "../Domain/list_item.h"

#include <functional>
#include <utility>
#include <vector>


/// <summary>
/// Function that executes an action on a list instance. Used when iteratively processing a list
/// </summary>
/// <typeparam name="TItem">list item</typeparam>
template<typename TItem> using ParseItemFunc = std::function<void(const TItem&)>;

template<typename TItem> class List;

template<typename T> using CallbackFunc = std::function<void(T)>;

template<typename TItem> class List
{
	static_assert(std::is_base_of<ListItem, TItem>::value, "TItem must inherit from ListItem");

public:
	std::vector<TItem> container;
	CallbackFunc<void*> callback = nullptr;
public:	
	std::vector<TItem>& get_container() noexcept;
	const std::vector<TItem>& get_container_const() const noexcept; 

	void set_callback(CallbackFunc<void*> cb) { callback = cb; }

	/// <summary>
	/// Adds new item to list
	/// </summary>
	/// <typeparam name="TItem">new element to add</typeparam>
	void add(TItem& element);

	/// <summary>
	/// finds the container position of element with a specified id
	/// </summary>	
	int find_by_id(int id) const;

	const TItem& find(std::function<bool(const TItem&)>) const;

	/// <summary>
	/// Updates element based on its id
	/// </summary>	
	void update(int id, const TItem& item);

	/// <summary>
	/// Removes element with a specified id
	/// </summary>
	bool remove_by_id(int id);

	/// <summary>
	/// Removes element at certain container position 
	/// </summary>	
	void remove_at(int index) noexcept;

	/// <summary>
	/// Accesses the index-th element in list
	/// </summary>	
	const TItem& operator[](int index) const;

	/// <summary>
	/// Gets list number of elements
	/// </summary>	
	size_t size() const noexcept;

	/// <summary>
	/// Automatically parses the list and performs a certain action for each item
	/// </summary>	
	void for_each(ParseItemFunc<const TItem&> do_with_item);	

	//void custom_delete();
};



#include <algorithm>

template<typename TItem>
void List<TItem>::add(TItem& element)
{
	element.set_id(container.size() == 0 ? 0 : container.back().get_id() + 1);
	container.push_back(element);
	if (callback != nullptr) { callback(this); }
}

template<typename TItem>
int List<TItem>::find_by_id(int id) const
{
	int left = 0, right = container.size();
	while (left < right)
	{
		const int mid = (left + right) / 2;
		if (container[mid].get_id() == id)
		{
			return mid;
		}

		if (id < container[mid].get_id())
		{
			right = mid;
		}
		else
		{
			left = mid + 1;
		}
	}
	return -1;
}

template<typename TItem>
void List<TItem>::update(int id, const TItem& item)
{
	const int index = find_by_id(id);
	if (index >= 0)
	{
		container[index] = item;
		container[index].set_id(id);
		if (callback != nullptr) { callback(this); }
	}
}

template<typename TItem>
bool List<TItem>::remove_by_id(int id)
{
	const int index = find_by_id(id);
	if (index < 0)
		return false;
	remove_at(index);
	if (callback != nullptr) { callback(this); }
	return true;
}

template<typename TItem>
void List<TItem>::remove_at(int index) noexcept
{
	container.erase(container.begin() + index);
	if (callback != nullptr) { callback(this); }
}

template<typename TItem>
const TItem& List<TItem>::operator[](int index) const
{
	return container[index];
}

template<typename TItem>
size_t List<TItem>::size() const noexcept
{
	return container.size();
}


#include <iostream>

template<typename TItem>
void List<TItem>::for_each(ParseItemFunc<const TItem&> do_with_item)
{
	if (size() == 0)
	{
		return;
		//throw std::out_of_range("No records to perform action on.");
	}

	std::for_each(container.begin(), container.end(), do_with_item);
}

template<typename TItem>
std::vector<TItem>& List<TItem>::get_container() noexcept
{
	return container;
}

template<typename TItem>
const std::vector<TItem>& List<TItem>::get_container_const() const noexcept
{
	return container;
}

template<typename TItem>
const TItem& List<TItem>::find(std::function<bool(const TItem&)> prop) const
{
	for (const TItem& item : container)
	{
		if (prop(item))
			return item;
	}
	throw std::exception("Item not found");
}
