#include <iostream>
#include <filesystem>
#include <fstream>
#include <string>
#include <set>
#include <chrono>
#include <queue>
#include <map>

#include <windows.h>

#include "SortedLinkedList.h"
#include "ProducerConsumerPipe.h"

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

struct RowData
{
    int part_id;
    int score;
    int country;
};

class MyPartsList : SortedLinkedList<RowData>
{
private:
    class RemovePartsList : public SortedLinkedList<int>
    {
        virtual bool is_sorted(const int& x, const int& y) override { return x < y; }
    } removed_parts;

    //std::mutex partid_mx[10000];
    
    std::map<int, std::mutex> partid_mx;
    std::mutex mx;

public:
    virtual bool is_sorted(const RowData& x, const RowData& y) override
    {
        return x.score > y.score || (x.score == y.score && x.part_id <= y.part_id);
    }    

    void update_or_insert(const RowData& x)
    {       
        mx.lock();
        std::lock_guard<mutex> lk(partid_mx[x.part_id]); // mutex for each id, prevent duplicates                
        mx.unlock();        
                
        if (removed_parts.find([&x](const int& r) {return r == x.part_id; }))
            return;

        RowData tmp = { x.part_id, 0, x.country };
        find_and_remove([&x](const RowData& r) {return r.part_id == x.part_id; }, tmp);

        if (x.score < 0)
        {
            removed_parts.insert(x.part_id);
            //printf("___________________Removed %i\n", x.part_id);
            return;
        }
        else
        {
            tmp.score += x.score;
            insert(tmp);
        }
    }

    friend ostream& operator <<(ostream& o, const MyPartsList& l)
    {
        l.iterate_sync([&o](const RowData& r) { o << r.part_id << " " << r.score << " " << r.country << "\n"; });
        o << "Eliminati:\n";
        l.removed_parts.iterate_sync([&o](const int& x) { o << x << "\n"; });
        return o;
    }
};

class PartsList
{
private:
    struct Node
    {
        int part_id, score = 0, country;
        Node* next;
    };
    Node* head = nullptr;

    Node* retrieve_or_create_node(int participant, int country)
    {
        Node* n = head, * prev = nullptr;
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

        return new Node{ participant, 0, country, nullptr };
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
    PartsList(bool is_parallel = false) : is_parallel(is_parallel) { }

    void add_score(int participant, int score, int country)
    {
        lock_guard<std::mutex> lk(mutex);        
        
        if (disqualified.contains(participant)) return;
        Node* n = retrieve_or_create_node(participant, country);
        if (score < 0)
        {
            delete n;
            disqualified.insert(participant);
            return;
        }
        n->score += score;
        insert_node(n);    
    }

    friend ostream& operator << (ostream& o, const PartsList& pl)
    {
        for (Node* n = pl.head; n; n = n->next)
            o << n->part_id << " " << n->score << " " << n->country << "\n";
        o << "Eliminati:\n";
        for (auto d : pl.disqualified) o << d << "\n";
        return o;
    }
};


class PartPipe : public ProducerConsumerPipe<RowData>
{
private:
    MyPartsList* parts_list;
    //PartsList* parts_list;
    atomic<int> k = 0;
public:
    PartPipe(MyPartsList* parts_list) : parts_list(parts_list) { }
    //PartPipe(PartsList* parts_list) : parts_list(parts_list) { }

    virtual void process(const RowData& x) override
    {
        //printf("P %i %i\n", x.part_id, x.score);
        parts_list->update_or_insert(x);
        //parts_list->add_score(x.part_id, x.score, x.country);
        k++;
    }

    ~PartPipe() 
    {
        printf("Total processed = %i\n", k.load());
    }
};

void producer(PartPipe* pipe, int pr, int pid)
{
    int q = 50 / pr, r = 50 % pr;
    int start = pid * q + (r <= pid ? r : pid);
    int end = (pid + 1) * q + (r <= (pid + 1) ? r : pid + 1);
    //printf("Started producer %i %i\n", start, end);

    //cout << start << " " << end << "\n";
    for (int k = start; k < end; k++)
    {
        int country = k / 10 + 1, part = k % 10 + 1;
        ifstream f("data\\C" + to_string(country) + "_P" + to_string(part) + ".txt");

        //printf("Reading C%i_P%i=> %i\n", country, part, f.is_open() ? 1 : 0);        


        for (int part, score, k = 0; k < 50000 && (f >> part >> score); k++)
        {
            pipe->put({ part, score, country });            
        }
        f.close();        

    }
    //printf("Producer %i finished.\n", pid);
}

void consumer(PartPipe* pipe, int pid)
{
    //printf("Consumer %i started\n", pid);
    pipe->run();
    //printf("Consumer %i finished\n", pid);
}

void parallel(int pr, int pw)
{
    MyPartsList parts_list;
    //PartsList parts_list(true);
    PartPipe pipe(&parts_list);
            
    thread* consumers = new thread[pw]{};
    for (int p = 0; p < pw; p++) consumers[p] = thread(consumer, &pipe, p);


    thread* producers = new thread[pr]{};
    for (int p = 0; p < pr; p++) producers[p] = thread(producer, &pipe, pr, p);

    for (int p = 0; p < pr; p++) producers[p].join();   

    delete[] producers;

    pipe.stop();

    for (int p = 0; p < pw; p++) consumers[p].join();

    ofstream g("result.txt");
    g << parts_list;
    g.close();
}


int main(int argc, char** argv)
{   
    // generate();              

    //while (1)
    {
        auto t_start = std::chrono::high_resolution_clock::now();

        int pw = argc > 2 ? atoi(argv[2]) : 16;
        int pr = argc > 3 ? atoi(argv[3]) : 1;

        parallel(pr, pw);
        auto t_end = std::chrono::high_resolution_clock::now();
        double elapsed_time_ms = std::chrono::duration<double, std::milli>(t_end - t_start).count();
        printf(">>Measured time = %f\n", elapsed_time_ms);
    }
    return 0;
}
