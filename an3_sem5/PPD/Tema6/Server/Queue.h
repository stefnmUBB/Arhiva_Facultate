#pragma once

#include <mutex>

template<typename T>
class Queue
{
private:
	int capacity;

	T* items;
	int start = 0, end = 0;
	int size = 0;

	bool is_active = true;

	mutable std::mutex mx;
	std::condition_variable cv_empty;
	std::condition_variable cv_full;

	std::atomic<int> dequeueing = 0;

public:
	Queue(int capacity = 50) : capacity(capacity)
	{
		items = new T[capacity]();
	}

	void enqueue(const T& item)
	{
		std::unique_lock<std::mutex> lock(mx);

		while (size == capacity)
		{
			cv_full.wait(lock);
		}

		items[end] = item;
		end = (end + 1) % capacity;
		size++;

		cv_empty.notify_one();

	}

	bool dequeue(T& item)
	{
		std::unique_lock<std::mutex> lock(mx);
		dequeueing++;

		while (is_active && size == 0)
		{
			cv_empty.wait(lock);
		}
		dequeueing--;

		if (!is_active)
			if (size == 0) return false;

		item = items[start];
		start = (start + 1) % capacity;
		size--;

		cv_full.notify_one();

		return true;
	}

	void shutdown()
	{
		std::unique_lock<std::mutex> lock(mx);
		is_active = false;
		lock.unlock();


		while (dequeueing.load() != 0)
		{
			lock.lock();
			cv_empty.notify_all();
			lock.unlock();
		}
	}

	~Queue()
	{
		printf("~Queue start\n");
		delete[] items;
		printf("~Queue end\n");
	}
};
