#include <bits/stdc++.h>

using namespace std;

#define f first
#define c second
typedef pair<int,int> fc;

class Graph
{
public:
    vector<map<int,fc>> lst;

    Graph(int n)
    {
        lst = vector<map<int,fc>>(n);
    }

    void add_edge(int u,int v, int c)
    {
        lst[u][v]={c,c};
    }

    friend ostream& operator <<(ostream& o, const Graph& gr)
    {
        gr.for_each_edge([&o](int u,int v,int c)
            {
                o<<"("<<u<<", "<<v<<", "<<c <<")\n";
            });
        return o;
    }

    void for_each_edge(function<void(int,int,int)> action) const
    {
        for(int u=0;u<lst.size();u++)
        {
            const map<int,fc>& l=lst[u];
            for(auto p:l)
            {
                int v=p.first;
                int c=p.second.c;
                action(u,v,c);
            }
        }
    }

    vector<pair<int,int>> find_residual_path(int src, map<int,bool>& visited)
    {
        visited[src]=true;
        if(src==lst.size()-1)
            return vector<pair<int,int>>();
        for(auto _: lst[src])
        {
            int v=_.first;
            if(visited[v]) continue;
            int f=_.second.f;
            int c=_.second.c;
            if(c>0)
            {
                vector<pair<int,int>> result;
                result.push_back({src, v});
                vector<pair<int,int>> further=find_residual_path(v, visited);
                result.insert(result.end(), further.begin(), further.end());
                if(result.back().second==lst.size()-1)
                    return result;
            }
        }
        return vector<pair<int,int>>();
    }

    int FordFulkerson()
    {
        Graph R=*this; // residual

        int t=lst.size()-1;

        map<int,bool> visited;

        visited.clear();
        vector<pair<int,int>> p = R.find_residual_path(0, visited);

        int result=0;
        while(p.size()>0 && p.back().second==t)
        {
            //for(auto x:p)
              //  cout<<x.first<<' '<<x.second<<'\n';
            //cout<<'\n';
            int cfp = INT_MAX;
            for(auto e:p)
            {
                int u=e.first;
                int v=e.second;
                int c=R.lst[u][v].c;
                cfp=min(cfp,c);
            }
            for(auto e:p)
            {
                int u=e.first;
                int v=e.second;
                R.lst[u][v].c-=cfp;
                R.lst[v][u].c+=cfp;
                /*if(R.lst[u].find(v)==R.lst[u].end())
                {
                    R.lst[u][v].c+=cfp;
                }
                else
                {
                    R.lst[v][u].c-=cfp;
                }*/
            }
            //cout<<R<<'\n';
            result+=cfp;
            visited.clear();
            p = R.find_residual_path(0,visited);
        }

        return result;
    }
};

int main(int argc, char** argv)
{
    ifstream f(argv[1]);
    ofstream g(argv[2]);

    int n, m;
    f>>n>>m;
    Graph G(n);
    for(int u,v,c;f>>u>>v>>c;)
    {
        G.add_edge(u,v,c);
    }
    //cout<<G<<'\n';

    cout<<G.FordFulkerson()<<'\n';
    f.close();
    g.close();
    return 0;
}
