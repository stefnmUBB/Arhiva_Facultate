#pragma once
#include <exception>

template<typename T> class _iterator;

template<typename T> class _vector
{
private:
	T* container;
	size_t len, cap;

	void redim()
	{
		cap *= 2;
		T* new_container = new T[cap];
		for (size_t i = 0; i < len; i++)
			new_container[i] = container[i];
		delete[] container;
		container = new_container;
	}
public:
	_vector()
	{
		container = new T[1];
		len = 0;
		cap = 1;
	}

	void push_back(T item)
	{
		if (len == cap)
		{
			redim();
		}
		container[len++] = item;
	}

	T& operator[] (int index)  const noexcept
	{
		return container[index];
	}


	size_t size() const noexcept
	{
		return len;
	}

	void erase(size_t index) noexcept
	{
		for (size_t i = index; i < len - 1; i++)
			container[i] = container[i + 1];
		if (index < len) len--;
	}

	T back() const
	{
		if (len == 0) throw std::exception("Cannot get last element of empty _vector");
		return container[len - 1];
	}


	_iterator<T> get_iter() const
	{
		return _iterator<T>(*this);
	}

	void custom_delete()
	{
		delete[] container;
	}
};

template<typename T> class _iterator
{
private:
	size_t index = 0;
	_vector<T> v;
public:
	_iterator(const _vector<T>& v)
	{
		this->v = v;
	}


	void go_to_begin()
	{
		index = 0;
	}

	bool is_valid()
	{
		return index < v.size();
	}

	void next()
	{
		index++;
	}


	const T& get_item()
	{
		return v[index];
	}	
};

