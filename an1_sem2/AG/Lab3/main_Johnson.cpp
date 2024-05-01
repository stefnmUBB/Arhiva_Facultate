#include <bits/stdc++.h>

using namespace std;

class Graph
{
private:
    vector<vector<pair<int,int>>> lst;
    vector<int> d;
public:
    Graph(int v)
    {
        lst = vector<vector<pair<int,int>>>(v+1);
        for(int i=0;i<=v;i++)
        {
            lst[i]=vector<pair<int,int>>();
        }
        for(int i=1;i<=v;i++)
        {
            add_edge(0,i,0);
        }

        d = vector<int>(v+1,INT_MAX/2);
    }
    void add_edge(int x,int y,int w)
    {
        lst[x].push_back(make_pair(y,w));
    }

    bool BellmanFord()
    {
        d[0]=0;
        for(int k=0;k<nodes_count();k++)
        {
            for(int u=0;u<=nodes_count();u++)
            {
                for(auto e:lst[u])
                {
                    int v=e.first, w=e.second;
                    // relax(u,v,w);
                    if(d[v]>d[u]+w)
                    {
                        d[v]=d[u]+w;
                    }
                }
            }
        }

        for(int u=1;u<=nodes_count();u++)
        {
            for(auto e:lst[u])
            {
                int v=e.first, w=e.second;
                if(d[v]>d[u]+w)
                {
                    return false;
                }
            }
        }

        return true;
    }

    struct cmp
    {
        bool operator () (pair<int,int> x,pair<int,int> y)
        {
            return x.second > y.second;
        }
    };

    typedef priority_queue<pair<int,int>,vector<pair<int,int>>,cmp> p_queue;

    void Dijkstra(int s)
    {
        vector<int> all_dists(nodes_count()+1,INT_MAX/2);
        p_queue Q;

        Q.push(make_pair(s,0));
        all_dists[s] = 0;
        while(!Q.empty())
        {
            int u = Q.top().first;
            Q.pop();
            for (auto i : lst[u])
            {
                int v = i.first;
                int w = i.second;
                w=w+d[u]-d[v];
                //cout<<u<<' '<<v<<' '<<w<<'\n';
                if (all_dists[v] > all_dists[u] + w)
                {
                    all_dists[v] = all_dists[u] + w;
                    Q.push(make_pair(v,d[v]));
                }
            }
        }

        for(int i=1;i<=nodes_count();i++)
        {
            int dd=all_dists[i]-d[s]+d[i];
            if(dd>=INT_MAX/3) cout<<"  inf";
            else printf("%5i",dd);
        }
        cout<<'\n';
    }

    void Johnson()
    {
        if(!BellmanFord())
        {
            cout<<-1;
            return;
        }

        for(int u=1;u<=nodes_count();u++)
        {
            for(auto p:lst[u])
            {
                int v=p.first;
                int w=p.second+d[u]-d[v];
                cout<<u-1<<' '<<v-1<<' '<<w<<'\n';
            }
        }

        cout<<'\n';
        //return;
        for (int i = 1;i<=nodes_count();i++)
            Dijkstra(i);
    }

    int nodes_count() const { return lst.size()-1; }

    void print_graph()
    {

        for(int x=0;x<=nodes_count();x++)
        {
            for(auto p:lst[x])
            {
                int y=p.first;
                int w=p.second;
                cout<<x<<' '<<y<<' '<<w<<'\n';
            }
        }
    }

};

int main(int argc, char** argv)
{
    ifstream f(argv[1]);
    ofstream g(argv[2]);

    int V, E;
    f>>V>>E;

    Graph G(V);

    for(int x,y,w;E--;)
    {
        f>>x>>y>>w;
        G.add_edge(x+1,y+1,w);
    }

    G.Johnson();

    /*vector<int> d=G.BellmanFord(S);

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
        }*/

    f.close();
    g.close();
    return 0;
}
