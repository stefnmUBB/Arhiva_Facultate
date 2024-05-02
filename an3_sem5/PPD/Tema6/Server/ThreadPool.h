#pragma once
#include <functional>
#include <thread>
#include <vector>
#include "ProducerConsumerPipe.h"


class ThreadPool : public ProducerConsumerPipe<std::function<void()>> // "thread pool"
{
private:
    int p_r = 2;

    std::atomic<int> crt_r = 0; // <=p_r
    std::atomic<int> total_count = 0;
    std::atomic<int> finished_count = 0;

    std::condition_variable cv_can_read;
    std::mutex lk_can_read;

    void run_function_in_thread(std::function<void()> f) // guard
    {
        std::unique_lock<std::mutex> lock(lk_can_read);

        while (crt_r.load() == p_r) // limiteaza nr de threaduri care ruleaza simultan (p_r)
            cv_can_read.wait(lock);
        crt_r++;
        lock.unlock();
        f();
        lock.lock();
        crt_r--;
        cv_can_read.notify_one();
        lock.unlock();
    }

    std::vector<std::thread*> threads;

public:
    ThreadPool(int p_r=2) : p_r(p_r) { }

    virtual void process(const std::function<void()>& f) override
    {
        std::thread* t = new std::thread(&ThreadPool::run_function_in_thread, this, f);
        threads.push_back(t);        
    }

    ~ThreadPool()
    {
        printf("~ThreadPool start\n");
        for (int i = 0; i < threads.size(); i++)
        {
            threads[i]->join();
            delete threads[i];
        }
        printf("~ThreadPool end\n");
    }
};