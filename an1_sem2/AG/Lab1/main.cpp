#include <bits/stdc++.h>

using namespace std;

class Graph
{
private:
    int adj[100][100];
    int N;

    vector<set<int>> alist;
public:
    Graph(int n, vector<pair<int,int>> edges)
    {
        N=n;
        for(int i=1;i<=N;i++)
            for(int j=1;j<=N;j++)
            adj[i][j]=0;
        for(auto e:edges)
        {
            adj[e.first][e.second] = adj[e.second][e.first] = 1;
            //cout<<e.first<<' '<<e.second<<'\n';
        }

        alist = vector<set<int>>(N+1);
        for(int i=0;i<=N;i++)
        {
            alist[i]=set<int>();
        }
        for(auto e: edges)
        {
            int x=e.first, y=e.second;
            alist[x].insert(y);
            alist[y].insert(x);
        }
    }

    void printAdj()
    {
        cout<<"Matricea de adiacenta:\n";
        for(int i=1;i<=N;i++)
        {
            for(int j=1;j<=N;j++)
                cout<<adj[i][j]<<' ';
            cout<<'\n';
        }
    }

    void printList()
    {
        cout<<"Lista de adiacenta:\n";

        for(int i=1;i<=N;i++)
        {
            cout<<i<<" : ";
            for(int j=1;j<=N;j++)
            {
                if(adj[i][j])
                    cout<<j<<" ";
            }
            cout<<"\n";
        }
    }

    void printInc()
    {
        cout<<"Matricea de incidenta:\n";

        int matinc[100][200], k=0;

        for(int i=1;i<=N;i++)
            for(int j=0;j<200;j++)
                matinc[i][j]=0;
        for(int i=1;i<=N;i++)
        {
            for(int j=1;j<=i;j++)
                if(adj[i][j])
                {
                    matinc[i][k]=matinc[j][k]=1;
                    k++;
                }
        }

        for(int i=1;i<=N;i++)
        {
            cout<<i<<" : ";
            for(int j=0;j<k;j++)
                cout<<matinc[i][j]<<' ';
            cout<<"\n";
        }
    }

    vector<int> findIsolatedNodes()
    {
        vector<int> result;

        for(int i=1;i<=N;i++)
            if(alist[i].size()==0)
                result.emplace_back(i);
        return result;
    }

    bool isRegular()
    {
        if(N==0) return false;
        int drg = alist[1].size();
        for(int i=2;i<=N;i++)
        {
            if(alist[i].size() != drg)
                return false;
        }
        return true;
    }

    bool isConex()
    {
        vector<bool> visited(N+1,0);
        int nodesCnt=0;
        deque<int> Q;
        Q.push_back(1);
        while(!Q.empty())
        {
            int x = Q.front(); Q.pop_front();
            nodesCnt+=!visited[x];
            visited[x] = true;
            for(int j=1;j<=N;j++)
                if(adj[x][j] && !visited[j])
                {
                    Q.push_back(j);
                }
        }
        //cout<<nodesCnt<<' ';
        return nodesCnt==N;
    }

    vector<vector<int>> RoyFloyd()
    {
        vector<vector<int>> result(N+1);
        for(int i=0;i<=N;i++) result[i] = vector<int>(N+1,INT_MAX);

        // self
        for(int i=1;i<=N;i++)
            result[i][i]=0;

        // edges
        for(int i=1;i<=N;i++)
            for(int j=1;j<i;j++)
                if(adj[i][j])
                    result[i][j] = result[j][i] = 1;

        for(int k=1;k<=N;k++)
        {
            for(int i=1;i<=N;i++)
                for(int j=1;j<=N;j++)
                {
                    if(result[i][k]!=INT_MAX && result[k][j]!=INT_MAX)
                    {
                        int potential_dist = result[i][k]+result[k][j];
                        if(result[i][j]>potential_dist)
                        {
                            result[i][j]=potential_dist;
                        }
                    }
                }
        }
        return result;
    }
};

ostream& operator << (ostream&o, vector<int> v)
{
    for(auto x:v) o<<x<<' ';
    return o;
}

ostream& operator << (ostream&o, vector<vector<int>> m)
{
    for(int i=1;i<m.size();i++)
    {
        for(int j=1;j<m[i].size();j++)
            if(m[i][j]==INT_MAX)
                o<<right<<setw(5)<<"inf";
            else
                o<<right<<setw(5)<<m[i][j];
        o<<"\n";
    }
    return o;
}

int main()
{
    ifstream f("in.txt");
    int N;
    f>>N;
    vector<pair<int,int>> edges;
    for(int x,y;f>>x>>y;)
    {
        edges.emplace_back(make_pair(x,y));
    }

    Graph G(N,edges);
    // cerinta 1
    G.printAdj();
    G.printInc();
    G.printList();

    // cerinta 2
    cout<<"Isolated nodes : "<<G.findIsolatedNodes()<<"\n";
    cout<<"Is regular     : "<<(G.isRegular() ? "Yes":"No")<<'\n';
    cout<<"Is conex       : "<<(G.isConex() ? "Yes":"No")<<'\n';
    cout<<"Distance matrix:\n"<<G.RoyFloyd();

    return 0;
}
