#include <bits/stdc++.h>

using namespace std;

ostream& operator << (ostream&o, vector<int> v)
{
    if(v.size()==0)
    {
        o<<"(None)";
    }
    else
    {
        for(auto x:v) o<<x<<' ';
    }
    return o;
}

ifstream f("graph.txt");

class Graph
{
private:
    vector<vector<int>> lst;
public:
    Graph(int n)
    {
        lst = vector<vector<int>>(n+1);
        for(int i=0;i<n;i++)
            lst[i]=vector<int>();
    }

    void add_edge(int x,int y)
    {
        lst[x].push_back(y);
    }

    void print()
    {
        for(int i=1;i<lst.size();i++)
        {
            cout<<i<<": ";
            for(auto x:lst[i])
                cout<<x<<" ";
            cout<<"\n";
        }
        cout<<"\n";
    }

    vector<vector<int>> Warshall()
    {
        int N=lst.size()-1;
        vector<vector<int>> result(N+1);
        for(int i=0;i<=N;i++) result[i] = vector<int>(N+1,0);
        for(int i=1;i<=N;i++)
            for(int j=1;j<=N;j++)
                if(find(lst[i].begin(),lst[i].end(),j)!=lst[i].end())
                    result[i][j] = 1;

        for(int k=1;k<=N;k++)
        {
            for(int i=1;i<=N;i++)
                for(int j=1;j<=N;j++)
                    if(!result[i][j] && result[i][k] && result[k][j])
                    {
                        result[i][j]=1;
                    }
        }
        return result;
    }

    vector<int> Moore(int u,int w)
    {
        int N=lst.size()-1;
        vector<int> l(N+1);
        vector<int> p(N+1);
        for(int v=1;v<=N;v++)
        {
            l[v]=INT_MAX;
        }
        l[u]=0;

        deque<int> Q;
        Q.push_back(u);
        while(!Q.empty())
        {
            int x = Q.front(); Q.pop_front();
            for(auto y:lst[x])
            {
                if(l[y]==INT_MAX)
                {
                    p[y]=x;
                    l[y]=l[x]+1;
                    Q.push_back(y);
                }
            }
        }

        vector<int> result;
        int last=w;
        result.push_back(last);
        if(l[w]==INT_MAX)
        {
            return vector<int>();
        }
        for(int k=l[w];k!=0;)
        {
            result.push_back(p[last]);
            last=p[last];
            k--;
        }
        reverse(result.begin(),result.end());
        return result;
    }

    void print_DFS(int x,map<int,int>* visited=new map<int,int>(),int len=0)
    {
        cout<<string(len,' ');
        cout<<x<<" - "<<len<<"\n";
        (*visited)[x]=1;
        for(auto y:lst[x])
        {
            if((*visited)[y]==0)
            {
                print_DFS(y,visited,len+1);
            }
        }
    }

    void print_BFS(int x)
    {
        deque<pair<int,int>> Q;
        Q.push_back(make_pair(x,0));

        vector<int> visited(lst.size()+1,0);
        while(!Q.empty())
        {
            int x = Q.front().first;
            int l = Q.front().second;
            Q.pop_front();
            if(visited[x]!=0) continue;
            visited[x]=1;

            cout<<string(l,' ')<<x<<" - "<<l<<"\n";

            for(auto y:lst[x])
            {
                if(visited[y]==0)
                {
                    Q.push_back(make_pair(y,l+1));
                }
            }
        }
    }
};

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

void solve_labirint(string lab_name)
{
    ifstream in(lab_name);
    vector<string> mat;
    for(string s;getline(in,s);mat.push_back(s));

    size_t max_len=0;
    for(auto s:mat) max_len=max(max_len,s.size());
    for(auto &s:mat)
    {
        while(s.size()<max_len) s+=" ";
    }

    int N=mat.size();
    int M=max_len;
    pair<int,int> S,F;
    for(int i=0;i<N;i++)
    {
        for(int j=0;j<M;j++)
        {
            if(mat[i][j]=='S')
            {
                S=make_pair(i,j);
                mat[i][j]=' ';
            }
            if(mat[i][j]=='F')
            {
                F=make_pair(i,j);
                mat[i][j]=' ';
            }
        }
    }

    vector<vector<int>> L(N);
    for(int i=0;i<N;i++)
        L[i]=vector<int>(M,0);
    for(int i=0;i<N;i++)
        for(int j=0;j<M;j++)
            if(mat[i][j]=='1') L[i][j]=-1;

    deque<pair<int,int>> Q;
    Q.push_back(S);
    L[S.first][S.second]=1;

    int dr[] = {-1,  0, 1, 0};
    int dc[] = { 0, -1, 0, 1};

    while(!Q.empty())
    {
        pair<int,int> p=Q.front(); Q.pop_front();
        int r=p.first;
        int c=p.second;

        for(int i=0;i<4;i++)
        {
            int ri=r+dr[i], ci=c+dc[i];
            if(0<=ri && ri<N && 0<=ci && ci<M)
            {
                if(L[ri][ci]==0)
                {
                    L[ri][ci]=L[r][c]+1;
                    Q.push_back(make_pair(ri,ci));
                }
            }
        }
    }

    pair<int,int> pt = F;
    for(int k=L[pt.first][pt.second];k>1;)
    {
        int r=pt.first, c=pt.second;
        mat[r][c]='*';
        for(int i=0;i<4;i++)
        {
            int ri=r+dr[i], ci=c+dc[i];
            if(0<=ri && ri<N && 0<=ci && ci<M)
            {
                if(L[ri][ci]==k-1)
                {
                    pt=make_pair(ri,ci);
                    k--;
                    break;
                }
            }
        }
    }

    mat[S.first][S.second]='S';
    mat[F.first][F.second]='F';

    ofstream g("lab_res.txt");
    for(auto s:mat) g<<s<<"\n";
}

int main()
{
    int n; f>>n;
    Graph g(n);

    for(int x,y; f>>x>>y;)
    {
        g.add_edge(x,y);
    }

    int src;
    cout<<"DFS source node: "; cin>>src;
    g.print_DFS(src);

    cout<<"BFS source node: "; cin>>src;
    g.print_BFS(src);

    /*cout<<g.Warshall()<<"\n";
    cout<<"\n";
    int u,w;
    cout<<"from : "; cin>>u;
    cout<<"to   : "; cin>>w;
    cout<<g.Moore(u,w);
    */
    solve_labirint("labirint_1.txt");

    return 0;
}
