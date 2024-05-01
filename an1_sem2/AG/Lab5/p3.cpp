#include <bits/stdc++.h>

using namespace std;

class Graph
{
public:
    vector<set<int>> lst;
    Graph(int n)
    {
        lst = vector<set<int>>(n);
    }

    void add_edge(int u,int v)
    {
        lst[u].insert(v);
        lst[v].insert(u);
    }

    void rem_edge(int u,int v)
    {
        lst[u].erase(v);
        lst[v].erase(u);
    }

    vector<int> eulerian_cycle()
    {
        vector<int> result;
        stack<int> Q;
        Q.push(0);

        while(!Q.empty())
        {
            int k=Q.top();
            if(!lst[k].empty())
            {
                int x=*lst[k].begin();
                lst[k].erase(lst[k].begin());
                rem_edge(k,x);
                Q.push(x);
            }
            else
            {
                Q.pop();
                result.push_back(k);
            }

        }
        return result;
    }
};

int main(int argc, char** argv)
{
    ifstream f(argv[1]);

    int n, m; f>>n>>m;
    Graph G(n);

    for(int u,v; f>>u>>v;)
    {
        G.add_edge(u,v);
    }

    vector<int> cycle=G.eulerian_cycle();
    for(auto x:cycle) cout<<x<<' ';
    cout<<'\n';

    f.close();
    return 0;
}
