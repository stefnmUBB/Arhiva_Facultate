#pragma once

#include "Queue.h"

template<typename T>
class ProducerConsumerPipe
{
private:
	Queue<T>* queue = nullptr;
	std::atomic<int> kput = 0;
public:
	ProducerConsumerPipe(int queue_capacity=50)
	{
		queue = new Queue<T>(queue_capacity);
	}

	void put(const T& item)
	{
		queue->enqueue(item);
		kput++;
	}

	virtual void process(const T& item) = 0;

	void run()
	{
		T item = T();
		while (queue->dequeue(item))
		{
			process(item);			
		}
	}

	void stop()
	{
		printf("Stopping pipe...\n");
		queue->shutdown();
	}

	virtual ~ProducerConsumerPipe()
	{
		printf("Total put = %i\n", kput.load());
		delete queue;
		printf("~ProducerConsumerPipe end\n");
	}
};
