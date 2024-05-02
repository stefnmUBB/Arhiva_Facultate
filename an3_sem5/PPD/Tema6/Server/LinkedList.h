#pragma once

#include <functional>

template<class T>
class LinkedList
{
	struct Node
	{
		T data;
		Node* next;
		std::mutex mutex;

		Node(const T& data, Node* next = nullptr) : data(data), next(next), mutex() { }
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

	Node* head;
	Node* tail;

public:
	LinkedList()
	{
		tail = new Node(T());
		head = new Node(T(), tail);
	}

	/*
	     (1,5) --> (2, 10) --> (4, 15) --> (3, 7)

		 ADD (5,6)

		 start --> (5,6) --> (1,5) --> (2, 10) --> (4, 15) --> (3, 7) --> end

		 ADD (4, 2) -> UPDATE()
		       *                                       +2 
		 start --> (5,6) --> (1,5) --> (2, 10) --> (4, 17) --> (3, 7) --> end

		 ADD (1, -1) -> DELETE()
		               |---------------v
		 start --> (5,6) x-> (1,-1) x-> (2, 10) --> (4, 17) --> (3, 7) --> end					 
	*/


	void insert(const T& data)
	{			
		Node* newNode = new Node(data);				
		Node* pred = nullptr, * succ = nullptr;
		lock_node(pred = head);
		lock_node(succ = head->next);
		pred->next = newNode;
		newNode->next = succ;
		unlock_node(pred, succ);
	}


	bool update(std::function<bool(const T&)> predicate, std::function<T(const T&)> newDataF) {		
		Node* pred = nullptr;
		Node* curr = nullptr;
		
		lock_node(pred = head);
		lock_node(curr = head->next);
		while (curr != tail) {
			if (predicate(curr->data)) {
				curr->data = newDataF(curr->data); // Update the data inside the existing node
				unlock_node(pred);
				unlock_node(curr);
				return true; // Break out of the loop after updating
			}
			unlock_node(pred);
			pred = curr;
			lock_node(curr->next);
			curr = curr->next;
		}
		unlock_node(pred, curr);
		return false;
	}

	void remove(std::function<bool(const T&)> predicate) {
		Node* pred = nullptr, * curr = nullptr;
		lock_node(pred = head);
		lock_node(curr = head->next);
		while (curr != tail) {
			if (predicate(curr->data)) {
				pred->next = curr->next;
				unlock_node(pred, curr);				
				return;
			}
			unlock_node(pred);
			pred = curr;
			lock_node(curr->next);
			curr = curr->next;
		}
		unlock_node(pred, curr);		
	}	

	void sort(std::function<int(const T& x, const T& y)> is_sorted) {
		bool sorted = true;
		Node* crt = head->next;
		if (crt == tail) return;
		do {
			crt = head->next;
			sorted = true;
			while (crt->next != tail) {
				if (is_sorted(crt->data, crt->next->data) > 0) {
					sorted = false;
					T aux = crt->data;
					crt->data = crt->next->data;
					crt->next->data = aux;
					break;
				}
				crt = crt->next;
			}
		} while (!sorted);
	}


	void iterate_sync(std::function<void(const T&)> callback) const
	{
		for (Node* n = head->next; n != tail; n = n->next)
			callback(n->data);
	}


	~LinkedList()
	{
		for (Node* n = head, *tmp; n != nullptr; )
		{
			tmp = n;
			n = n->next;
			delete tmp;
		}
	}
};