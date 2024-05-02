#include <iostream>
#include <filesystem>
#include <fstream>
#include <string>
#include <set>
#include <chrono>

#include <windows.h>

using namespace std;

void generate()
{
    filesystem::create_directory("data");
    
    for (int c = 1; c <= 5; c++)
    {
        int parts_count = 80 + rand() % 20;
        for (int i = 1; i <= 10; i++)
        {
            ofstream f("data\\C" + to_string(c) + "_P" + to_string(i) + ".txt");

            for (int k = 1; k <= parts_count; k++)
            {
                int score = (unsigned)rand() % 50 == 4 ? -1 : (unsigned)rand() % 11;
                f << 100 * c + k << " " << score << "\n";
            }
            
            f.close();
        }        
    }
}


class PartsList
{
private:
    struct Node
    {
        int part_id, score = 0;
        Node* next;
    };
    Node* head = nullptr;    

    Node* retrieve_or_create_node(int participant)
    {        
        Node* n = head, *prev = nullptr;
        while (n != nullptr)
        {
            if (n->part_id == participant)
            {
                if (prev != nullptr)
                    prev->next = n->next;
                else
                    head = n->next;
                n->next = nullptr;
                return n;
            }
            prev = n, n = n->next;
        }

        return new Node{ participant, 0, nullptr };
    }

    bool order(Node* n1, Node* n2)
    {
        return n1->score > n2->score || (n1->score == n2->score && n1->part_id <= n2->part_id);
    }

    void insert_node(Node* target)
    {
        if (head == nullptr)
        {            
            head = target;
            return;
        }
        if (order(target, head))
        {
            target->next = head;
            head = target;
            return;
        }

        Node* n = head;
        while (n->next != nullptr)
        {            
            if (order(n, target) && order(target, n->next))
            {
                target->next = n->next;
                n->next = target;
                return;
            }
            n = n->next;
        }

        n->next = target;
    }

    set<int> disqualified;
    mutex mutex;

    bool is_parallel;

public:
    PartsList(bool is_parallel=false) : is_parallel(is_parallel) { }

    void add_score(int participant, int score)
    {
        if(is_parallel) mutex.lock();
        ([&]()
            {
                if (disqualified.contains(participant)) return;
                Node* n = retrieve_or_create_node(participant);
                if (score < 0)
                {
                    delete n;
                    disqualified.insert(participant);
                    return;
                }
                n->score += score;
                insert_node(n);
            }
        )();
        if(is_parallel) mutex.unlock();
    }

    friend ostream& operator << (ostream& o, const PartsList& pl)
    {
        for (Node* n = pl.head; n; n = n->next)
            o << n->part_id << " " << n->score << "\n";
        o << "Eliminati:\n";
        for (auto d : pl.disqualified) o << d << "\n";
        return o;
    }
};

void sequencial()
{
    PartsList parts_list;

    for (const auto& entry : filesystem::directory_iterator("data"))
    {
        cout << entry.path() << "\n";
        ifstream f(entry.path());
        for (int part, score; f >> part >> score; parts_list.add_score(part, score));
        f.close();        
    }
    
    ofstream g("result.txt");
    g << parts_list;
    g.close();
}


struct lock_holder
{
private:
    bool is_locked = false;    
    mutex mutex;
public:
    void wait()
    {
        mutex.lock();
        is_locked = true;
        mutex.unlock();

        for (bool running = true; running;)
        {
            mutex.lock();
            Sleep(1);
            running = is_locked;
            mutex.unlock();
        }                
    }   

    void notify()
    {
        mutex.lock();
        is_locked = false;
        mutex.unlock();       
    }       
};

template<typename T>
class Queue
{
private:
    struct Node
    {
        T item;
        Node* next;
    };
    Node* head = nullptr;
    Node* last = nullptr;    

    mutex mutex;    
    lock_holder empty_lock;    
    bool is_parallel;

public:    
    Queue(bool is_parallel=false) : is_parallel(is_parallel) { }

    void enqueue(const T& item)
    {                      
        Node* n = new Node{ item, nullptr };
        if (head == nullptr)
        {
            head = last = n;
            return;
        }
        last->next = n;
        last = n;    
    }

    T dequeue()
    {                          
        if (head == nullptr)
        {            
            throw exception("Nothing to dequeue");
        }
        T result = head->item;
        head = head->next;
        if (head == nullptr) last = nullptr;                                       
        return result;
    }

    void try_enqueue(const T& item)
    {
        if (is_parallel) mutex.lock();
        //printf("Qput %i %i\n", ((int*)&item)[0], ((int*)&item)[1]);

        Node* n = new Node{ item, nullptr };
        if (head == nullptr)
        {
            head = last = n;
            goto _end;
        }
        last->next = n;
        last = n;

    _end:
        if (is_parallel) mutex.unlock();
    }

    bool try_dequeue(T& result)
    {        
        mutex.lock();
        if (head == nullptr)
        {
            mutex.unlock();
            return false;
        }                   

        if (head == nullptr)
        {
            printf("Oare aici aici aici????\n");
            mutex.unlock();
            throw exception("Nothing to dequeue");
        }
        result = head->item;
        head = head->next;
        if (head == nullptr) last = nullptr;

        mutex.unlock();
        return true;
    }

    bool is_empty()
    {        
        if (is_parallel) mutex.lock();
        bool result = head == nullptr;
        if (is_parallel) mutex.unlock();
        return result;
    }    
};

struct RowData
{
    int part_id;
    int score;
};



void producer(Queue<RowData>* queue, int pr, int pid)
{
    int q = 50 / pr, r = 50 % pr;
    int start = pid * q + (r <= pid ? r : pid);
    int end = (pid + 1) * q + (r <= (pid + 1) ? r : pid + 1);
    printf("Started producer %i %i\n", start, end);

    //cout << start << " " << end << "\n";
    for (int k = start; k < end; k++)
    {
        int country = k / 10 + 1, part = k % 10 + 1;
        ifstream f("data\\C" + to_string(country) + "_P" + to_string(part) + ".txt");
                
        //printf("Reading C%i_P%i=> %i\n", country, part, f.is_open() ? 1 : 0);        


        for (int part, score, k = 0; k < 50000 && (f >> part >> score); k++)
        {            
            queue->try_enqueue({ part, score });
        }
        f.close();        
    }    
    printf("Producer %i finished.\n", pid);
}

void consumer(Queue<RowData>* queue, std::atomic<bool>* work_needed, PartsList* parts_list, int pid)
{
    RowData x;
    while (work_needed->load())
    {                
        while (!queue->is_empty())
        {
            if (queue->try_dequeue(x))
            {
                //printf("Qget %i %i\n", x.part_id, x.score);
                parts_list->add_score(x.part_id, x.score);
            }
            else
                Sleep(1);
        }        
    }
    printf("Consumer %i finished.\n", pid);
}


void parallel(int pr, int pw)
{
    PartsList parts_list(true);
    Queue<RowData> q(true);

    std::atomic<bool> work_needed(true);

    thread* consumers = new thread[pw]{};
    for (int p = 0; p < pw; p++) consumers[p] = thread(consumer, &q, &work_needed, &parts_list, p);


    thread* producers = new thread[pr]{};
    for (int p = 0; p < pr; p++) producers[p] = thread(producer, &q, pr, p);    

    for (int p = 0; p < pr; p++) producers[p].join();

    work_needed.store(false);
    for (int p = 0; p < pw; p++) consumers[p].join();

    delete[] producers;       

    ofstream g("result.txt");
    g << parts_list;
    g.close();
}

int main(int argc, char** argv)
{    
    auto t_start = std::chrono::high_resolution_clock::now();
    // generate();
    if (argc > 1 && strcmp(argv[1], "seq") == 0)
    {
        sequencial();
    }
    else
    {
        int pw = argc > 2 ? atoi(argv[2]) : 16;
        int pr = argc > 3 ? atoi(argv[3]) : 1;
        parallel(pr, pw);
    }   
    auto t_end = std::chrono::high_resolution_clock::now();
    double elapsed_time_ms = std::chrono::duration<double, std::milli>(t_end - t_start).count();
    printf(">>Measured time = %f\n", elapsed_time_ms);

    return 0;
}
