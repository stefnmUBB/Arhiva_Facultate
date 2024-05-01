#include <bits/stdc++.h>

using namespace std;

class Graph
{
private:
    vector<vector<pair<int,int>>> lst;
public:
    Graph(int v)
    {
        lst = vector<vector<pair<int,int>>>(v+1);
        for(int i=1;i<=v;i++)
        {
            lst[i]=vector<pair<int,int>>();
        }
    }
    void add_edge(int x,int y,int w)
    {
        lst[x].push_back(make_pair(y,w));
    }

    vector<int> BellmanFord(int s)
    {
        vector<int> d(nodes_count()+1,INT_MAX/2);
        vector<int> p(nodes_count()+1,-1);
        d[s]=0;

        for(int i=1;i<nodes_count();i++)
        {
            for(int u=0;u<nodes_count();u++)
            {
                for(auto e:lst[u])
                {
                    int v=e.first, w=e.second;
                    // relax(u,v,w);
                    if(d[v]>d[u]+w)
                    {
                        d[v]=d[u]+w;
                        p[v]=p[u];
                    }
                }
            }
        }

        for(int u=0;u<=nodes_count();u++)
        {
            for(auto e:lst[u])
            {
                int v=e.first, w=e.second;
                if(d[v]>d[u]+w)
                    return vector<int>();
            }
        }

        return d;
    }

    int nodes_count() const { return lst.size()-1; }

};

int main(int argc, char** argv)
{
    ifstream f(argv[1]);
    ofstream g(argv[2]);

    int V, E, S;
    f>>V>>E>>S;

    Graph G(V);

    for(int x,y,w;E--;)
    {
        f>>x>>y>>w;
        G.add_edge(x,y,w);
    }

    vector<int> d=G.BellmanFord(S);

    for(int i=0;i<G.nodes_count();i++)
        if(d[i]==INT_MAX/2)
        {
            g<<"INF ";
            cout<<"INF ";
        }
        else
        {
            g<<d[i]<<' ';
            cout<<d[i]<<' ';
        }

    f.close();
    g.close();
    return 0;
}
