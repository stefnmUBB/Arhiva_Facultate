#include <bits/stdc++.h>

using namespace std;

ostream& operator<<(ostream& o, const vector<int>& v)
{
    for(const auto& x:v) o<<x<<' ';
    return o;
}

class TreeNode
{
public:
    int value;
    TreeNode* parent = NULL;
    set<TreeNode*> children;
    bool is_leaf() {return children.size()==0;}
};

class Prufer
{
public:
    static vector<int> encode(vector<int> p)
    {
        vector<int> result;

        vector<TreeNode*> tree(p.size());


        for(int i=0;i<p.size();i++)
            tree[i] = new TreeNode();

        for(int i=0;i<p.size();i++)
        {
            tree[i]->value =i;
            tree[i]->parent = p[i]==-1 ? NULL : tree[p[i]];
            if(p[i]!=-1)
                tree[p[i]]->children.insert(tree[i]);
        }

        set<int> leaves;
        for(int i=0;i<p.size();i++)
        {
            if(tree[i]->is_leaf())
                leaves.insert(i);
        }

        while(!leaves.empty())
        {
            //for(auto x:leaves) cout<<x<<' ';  cout<<'\n';
            int leaf_id = *leaves.begin();
            leaves.erase(leaves.begin());

            TreeNode* leaf = tree[leaf_id];
            TreeNode* parent = leaf->parent;

            if(parent!=NULL)
            {
                result.push_back(parent->value);
                leaf->parent=NULL;
                parent->children.erase(leaf);
                if(parent->is_leaf())
                    leaves.insert(parent->value);
            }
        }
        return result;
    }

    static vector<int> decode(vector<int> pf)
    {
        vector<int> result(pf.size()+1,-1);
        vector<bool> visited(result.size(),true);
        vector<int> freq(result.size());

        set<int> leaves;
        for(int i=0;i<result.size();i++)
        {
            leaves.insert(i);
        }
        for(auto x:pf)
        {
            leaves.erase(x);
            visited[x]=false;
            freq[x]++;
        }

        int k=0;
        while(k<pf.size())
        {
            //for(auto x:leaves) cout<<x<<" "; cout<<'\n';
            int leaf = *leaves.begin();
            leaves.erase(leaves.begin());
            result[leaf]=pf[k];
            freq[pf[k]]--;
            if(freq[pf[k]]==0) // new leaf
            {
                leaves.insert(pf[k]);
            }
            k++;
        }


        return result;
    }
};

class BinNode
{
public:
    BinNode* left;
    BinNode* right;
    int fr;
    string value="";
    string code="";
    void show()
    {
        cout<<fr<<" v="<<value<<' '<<code<<'\n';
        if(left!=NULL) left->show();
        if(right!=NULL) right->show();
    }
    void make_codes()
    {
        if(left!=NULL)
        {
            left->code=code+"0";
            left->make_codes();
        }
        if(right!=NULL)
        {
            right->code=code+"1";
            right->make_codes();
        }
    }
};

bool cmp(const BinNode* b1, const BinNode* b2)
{
    /*if(b1->fr==b2->fr)
    {
        cout<<"EQ : "<<b1->value<<", "<<b2->value<<'\n';
    }*/
    return b1->fr<b2->fr || (b1->fr==b2->fr && b1->value<b2->value);
}

class Huffman
{
public:
    string message;
    map<char,int> freq;
    map<char,string> codes;
    string code;

    void build_codes()
    {
        vector<BinNode*> leaves;
        multiset<BinNode*, decltype(&cmp)> nodes(cmp);
        for(auto kv:freq)
        {
            BinNode* nd=new BinNode();
            string val="";
            val+=kv.first;
            nd->value=val;
            //cout<<kv.first<<"="<<""+kv.first<<'\n';
            nd->fr=kv.second;
            nd->left=nd->right=NULL;
            nodes.insert(nd);
            leaves.push_back(nd);
            //cout<<nodes.size()<<'\n';
        }

        for(int i=1;i<=freq.size()-1;i++)
        {
            //cout<<i<<' '<<freq.size()<<'\n';
            BinNode* z=new BinNode();
            z->left=*nodes.begin(); nodes.erase(nodes.begin());
            if(nodes.size()>0)
                z->right=*nodes.begin(); nodes.erase(nodes.begin());
            z->fr = z->left->fr;
            if(z->right!=NULL) z->fr+= z->right->fr;
            z->value = z->left->value;
            if(z->right!=NULL)
                if(z->right->value<z->left->value)
                    z->value=z->right->value;
            nodes.insert(z);
        }

        BinNode* root=*nodes.begin();
        root->make_codes();
        //root->show();
        for(auto l:leaves)
        {
            codes[l->value[0]]=l->code;
        }


    }

    Huffman(string msg)
    {
        message = msg;
        for(auto ch : msg)
            freq[ch]++;

        build_codes();

        code="";
        for(auto ch:message)
        {
            code+=codes[ch];
        }
    }

    Huffman(map<char,int> cfreq)
    {
        freq=cfreq;
        build_codes();

    }

    void decode_message(string code)
    {
        this->code=code;
        message="";

        string chcd="";
        for(int i=0;i<code.size();i++)
        {
            chcd+=code[i];
            for(auto kv:codes)
            {
                if(kv.second==chcd)
                {
                    message+=kv.first;
                    chcd="";
                    break;
                }
            }
        }
    }
};

class Graph
{
private:
    int nodes_cnt;
    vector<vector<pair<int,int>>> adj;
public:

    Graph(int n)
    {
        nodes_cnt=n;
        adj=vector<vector<pair<int,int>>>(nodes_cnt);
    }

    void add_edge(int x,int y, int w)
    {
        adj[x].push_back({y, w});
        adj[y].push_back({x, w});
    }

    vector<pair<int,int>> Prim(int &cost)
    {
        vector<int> p(nodes_cnt,-1);
        vector<int> d(nodes_cnt,INT_MAX/2);
        vector<bool> visited(nodes_cnt, false);


        priority_queue< pair<int,int>, vector<pair<int,int>>, greater<pair<int,int>> > Q;

        int s=0;
        d[s]=0;

        Q.push({0,s});

        while(!Q.empty())
        {
            int u=Q.top().second; Q.pop();
            if(visited[u])
            {
                continue;
            }
            visited[u]=true;
            for(auto pr:adj[u])
            {
                int v=pr.first;
                int w=pr.second;

                if(!visited[v] && d[v]>w)
                {
                    d[v]=w;
                    Q.push({d[v],v});
                    p[v]=u;
                }
            }
        }
        vector<pair<int,int>> result;
        for(int i=1;i<nodes_cnt;i++)
        {
            result.push_back({p[i], i});
        }
        for(int i=1;i<nodes_cnt;i++)
            cost+=d[i];
        return result;
    }

};

void solve1(std::istream& in, std::ostream& out)
{
    int N;
    in>>N;
    vector<int> p(N);
    for(int i=0;i<N;i++)
        in>>p[i];
    vector<int> prf = Prufer::encode(p);
    out<<prf.size()<<'\n';
    out<<prf;
}

void solve2(std::istream& in, std::ostream& out)
{
    int N;
    in>>N;
    vector<int> pf(N);
    for(int i=0;i<N;i++)
        in>>pf[i];
    vector<int> p = Prufer::decode(pf);
    out<<p.size()<<'\n';
    out<<p;
}

void solve3(std::istream& in, std::ostream& out)
{
    string msg;
    getline(in,msg);

    Huffman huff(msg);

    out<<huff.freq.size()<<'\n';
    for(auto kv:huff.freq)
    {
        out<<kv.first<<' '<<kv.second<<'\n';
    }
    out<<huff.code;
}

void solve4(std::istream& in, std::ostream& out)
{
    int n; in>>n;
    map<char,int> frq;
    string line;
    getline(in,line);
    for(;n--;)
    {
        getline(in,line);
        istringstream ss(line.substr(2));
        int cnt; ss>>cnt;
        frq[line[0]]=cnt;
    }
    Huffman huff(frq);
    string code; in>>code;
    huff.decode_message(code);
    cout<<huff.message<<'\n';
    //cout<<code<<'\n';
    /*for(auto kv:huff.codes)
    {
        cout<<kv.first<<' '<<kv.second<<'\n';
    }*/
}

void solve5(istream& in, ostream& out)
{
    int n,e; in>>n>>e;
    Graph G(n);

    for(int x,y,w;e--;)
    {
        in>>x>>y>>w;
        G.add_edge(x,y,w);
    }
    int cost=0;
    vector<pair<int,int>> res = G.Prim(cost);
    cout<<cost<<'\n';
    cout<<res.size()<<'\n';
    for(auto &p:res)
    {
        if(p.first>p.second)
        {
            swap(p.first, p.second);
        }
    }
    sort(res.begin(),res.end());

    for(auto p:res)
    {
        out<<p.first<<' '<<p.second<<'\n';
    }

}

void (*solve[])(istream&, ostream&) = {NULL, solve1, solve2, solve3, solve4, solve5 };

// argv[1] - problem no.
// argv[2] - fin
// argv[3] - fout
int main(int argc,char **argv)
{
    /*string str="Loorrreem\n";
    istringstream ss(str);
    solve3(ss,cout);
    return 0;*/
    //cout<<Prufer::decode({1,2,1,0,5,0});
    //return 0;
    std::ifstream fin(argv[2]);
    std::ofstream fout(argv[3]);

    solve[argv[1][0]-'0'](fin,cout);

    fin.close();
    fout.close();
    return 0;
}
