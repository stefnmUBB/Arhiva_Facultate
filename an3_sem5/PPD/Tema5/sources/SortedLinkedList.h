#pragma once

#include <mutex>
#include <functional>

template<typename T>
class SortedLinkedList
{
private:
	struct Node
	{
		T item;
		Node* next;
		std::mutex mutex;		

		Node(const T& item, Node* next=nullptr) : item(item), next(next), mutex() { }
	};

	Node* lock_node(Node* n) { if (n) n->mutex.lock(); return n; }

	void unlock_node() { }
	void unlock_node(Node* n) { if (n) n->mutex.unlock(); }

	template<typename... Args>
	void unlock_node(Node* n, Args... args)
	{
		unlock_node(n);
		unlock_node(args...);
	}	

	Node* start;
	Node* end;

public:
	SortedLinkedList()
	{
		end = new Node(T());
		start = new Node(T(), end);
	}

	virtual bool is_sorted(const T& x, const T& y) = 0;

	bool find(std::function<bool(const T&)> pred)
	{
		Node* n0 = lock_node(start);
		Node* n1 = lock_node(n0->next);

		if (n1 == end) // empty list
		{
			unlock_node(n0, n1);			
			return false;
		}

		Node* n2 = lock_node(n1->next);

		while (n2 != nullptr)
		{
			if (pred(n1->item))
			{				
				unlock_node(n0, n1, n2);								
				return true;
			}

			unlock_node(n0);
			n0 = n1;
			n1 = n2;
			n2 = lock_node(n2->next);
		}

		unlock_node(n0, n1, n2);		
		return false;
	}

	bool find_and_remove(std::function<bool(const T&)> pred, T& result)
	{
		Node* n0 = lock_node(start);
		Node* n1 = lock_node(n0->next);

		if (n1 == end) // empty list
		{
			unlock_node(n0, n1);			
			return false;
		}

		Node* n2 = lock_node(n1->next);

		while (n2 != nullptr)
		{
			if (pred(n1->item))
			{
				result = n1->item;
				n0->next = n2;
				unlock_node(n0, n1, n2);				
				delete n1;
				return true;
			}

			unlock_node(n0);
			n0 = n1;
			n1 = n2;
			n2 = lock_node(n2->next);
		}

		unlock_node(n0, n1, n2);		

		return false;
	}

	void insert(const T& item)
	{
		Node* target = new Node(item);

		Node* n0 = lock_node(start);
		Node* n1 = lock_node(n0->next);

		if (n1 == end) // empty list
		{
			n0->next = target;
			target->next = n1;
			unlock_node(n0, n1);			
			return;
		}

		if (is_sorted(target->item, n1->item))
		{
			// insert before
			n0->next = target;
			target->next = n1;
			unlock_node(n0, n1);
			return;
		}

		unlock_node(n0), n0 = n1, n1 = lock_node(n0->next);


		while (n1 != end)
		{
			if (is_sorted(n0->item, target->item) && is_sorted(target->item, n1->item))
			{
				n0->next = target;
				target->next = n1;
				unlock_node(n0, n1);
				return;
			}

			unlock_node(n0), n0 = n1, n1 = lock_node(n0->next);
		}

		// insert at end
		n0->next = target;
		target->next = n1;
		unlock_node(n0, n1);
	}

	~SortedLinkedList()
	{
		for (Node* n = start, *tmp; n != nullptr; )
		{			
			tmp = n;
			n = n->next;
			delete tmp;
		}
	}

	void iterate_sync(std::function<void(const T&)> callback) const
	{
		for (Node* n = start->next; n != end; n = n->next)
			callback(n->item);
	}


};
