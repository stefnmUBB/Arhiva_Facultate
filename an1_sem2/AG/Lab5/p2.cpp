#include <bits/stdc++.h>

using namespace std;

struct cf
{
    int c;
    int f;
    int c_f;
};

struct nd
{
    int h;
    int e;
};

class Graph
{
public:
    vector<nd> nodes;
    vector<map<int, cf>> lst;

    Graph(int n)
    {
        nodes = vector<nd>(n, {0,0});
        lst = vector<map<int, cf>>(n);
    }

    void add_edge(int u, int v, int c)
    {
        lst[u][v]={c, 0, 1};
        lst[v][u]={0, 0, 2};
    }

    int get_cf(int u,int v)
    {
        if(lst[u][v].c_f==1) // u,v in E
        {
            return lst[u][v].c-lst[u][v].f;
        }
        if(lst[u][v].c_f==2) // v,u in E
        {
            return lst[v][u].f;
        }
        return 0;
    }

    void init_preflux()
    {
        nodes[0].h = nodes.size();
        for(auto &ucf: lst[0])
        {
            int u=ucf.first;
            ucf.second.f = ucf.second.c;
            nodes[u].e = ucf.second.c;
            nodes[0].e -= ucf.second.c;
        }
    }

    bool cond_pomp(int &u,int &v)
    {
        for(u=1;u<nodes.size()-1;u++)
        {
            if(nodes[u].e<=0) continue;
            for(const auto& l:lst[u])
            {
                v=l.first;
                if(get_cf(u,v)<=0) continue;
                if(nodes[u].h == nodes[v].h+1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void pompare(int u,int v)
    {
        //cout<<"pompare "<<u<<' '<<v<<'\n';
        int df=min(nodes[u].e, get_cf(u,v));
        if(lst[u][v].c_f==1)
            lst[u][v].f += df;
        else
            lst[v][u].f -= df;

        nodes[u].e-=df;
        nodes[v].e+=df;
    }

    bool cond_inaltare(int &u)
    {
        int w=-1;
        //cout<<"Eligible nodes: ";
        for(u=1;u<nodes.size()-1;u++)
        {
            if(nodes[u].e<=0) continue;
            //cout<<u<<' ';

            bool ulowest=true;
            int k=0;
            for(const auto &vcf: lst[u])
            {
                int v=vcf.first;
                if(get_cf(u,v)>0)
                {
                    k++;
                    //cout<<"("<<v<<")";
                    if(nodes[u].h>nodes[v].h)
                    {
                        ulowest=false;
                        break;
                    }
                }
            }
            //if(k==0) ulowest=false;
            if(!ulowest) continue;
            //cout<<u<<'\n';
            if(w==-1)
            {
                w=u;
            }
            else
            {
                if(nodes[u].h<nodes[w].h)
                    w=u;
            }
        }
        //cout<<'\n';
        u=w;
        //cout<<"Chosen "<<w<<'\n';
        //cout<<u<<'\n';
        return u!=-1;
    }

    void inaltare(int u)
    {
        int hmin=INT_MAX;
        for(const auto &vcf: lst[u])
        {
            int v=vcf.first;
            if(get_cf(u,v)>0)
                hmin=min(hmin, nodes[v].h);
        }
        nodes[u].h=hmin+1;
        //cout<<"inaltare "<<u<<" la "<<nodes[u].h<<'\n';

    }

    void pomp_preflux()
    {
        int s=0;
        int t=nodes.size()-1;
        init_preflux();

        int u,v;
        while(true)
        {
            //cout<<"EH\n";
            /*for(int i=0;i<nodes.size();i++)
            {
                cout<<i<<": "<<nodes[i].e<<' '<<nodes[i].h<<'\n';
            }
            cout<<"CF_CF\n";*/
            /*for(int u=0;u<nodes.size();u++)
            {
                for(auto l:lst[u])
                {
                    int v=l.first;
                    int c=l.second.c;
                    int f=l.second.f;
                    int c_f=get_cf(u,v);
                    //cout<<"("<<u<<','<<v<<") : "<<c<<' '<<f<<' '<<c_f<<'\n';
                }
            }*/
            //cout<<"\n--------------------------------\n";
            if(cond_pomp(u,v))
            {
                pompare(u,v);
                continue;
            }
            if(cond_inaltare(u))
            {
                inaltare(u);
                continue;
            }
            break;
        }


        cout<<nodes.back().e<<'\n';
        //cout<<"HERE\n";
        //for(int i=0;i<=t;i++)
          //  cout<<nodes[i].e<<' '<<nodes[i].h<<'\n';
    }
};


int main(int argc, char** argv)
{
    ifstream f(argv[1]);

    int n, m;
    f>>n>>m;

    Graph G(n);

    for(int u,v,c; f>>u>>v>>c;)
    {
        G.add_edge(u,v,c);
    }

    G.pomp_preflux();




    f.close();
    return 0;
}
