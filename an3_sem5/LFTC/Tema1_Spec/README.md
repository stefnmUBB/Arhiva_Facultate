# Lab1

## 1. Specificare MLP


### Atomi

- Cuvinte rezervate: int, float, for, while, if, cin, cout, endl, .....
- ...


### EBNF

```C++
letter           = "a" | ... | "z" | "A" | ... | "Z";
digit            = "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9";

identifier       = ("_" | letter), { "_"|letter|digit };
datatype         = "int" | "float" | "int_arr";

number_literal   = ["+"|"-"], (int_literal | real_literal);
int_literal      = digits;
real_literal     = digits, [".", digits ];
digits           = digit, {digit};

arithm_expr_term = number_literal |  identifier |  ( "(", arithm_expr, ")" );

muldiv_expr      = arithm_expr_term, { ("*"|"/"|"%"), arithm_expr_term };
addsub_expr      = muldiv_expr, { ("+"|"-"), muldiv_expr };
arithm_expr      = addsub_expr;

cond_expr        = arithm_expr, ("<"|"<="|">"|">="|"=="|"!="), arithm_expr;

program_header   = "#include", "<", "iostream", ">", "using", "namespace", "std", ";";
program_body     = "int", "main", "(", ")", "{", instr_list, "return", "0", ";", "}";

instr_list       =  [instr, {instr}];
scope            = "{", instr_list, "}";

scope_or_single_instr = scope | instr;

instr            =  (((i_cin | i_cout | i_attr | vdecl), ";") | i_cond | i_while | i_for), {";"}; // teoretic instr1;;;;; e valid

i_vdecl          = datatype, identifier, ["=", arithm_expr];
i_attr           = identifier, "=", arithm_expr;
i_cond           = "if", "(", cond_expr, ")", scope_or_single_instr, ["else", scope_or_single_instr];
i_while          = "while", "(", cond_expr, ")", scope_or_single_instr;
i_for            = "for", "(", i_vedcl, ";", cond_expr, ";", i_attr, ")", scope_or_single_instr;

i_cin            = "cin", { ">>", identifier };
i_cout           = "cout", { "<<", (identifier|"endl") };

program          = program_header, program_body;
```

## 2. Exemple surse

- calculeaza perimetrul si aria cercului de o raza data data

```C++

#include<iostream>
using namespace std;

int main()
{
    float pi = 3.14;
    float r;

    cin>>r;
    float perim = 2*pi*r;
    float aria  = pi*r*r;
    cout<<perim<<endl<<aria<<endl;
    return 0;
}
```

- determina cmmdc a 2 nr naturale

```C++

#include<iostream>
using namespace std;

int main()
{
    int a; int b;
    cin>>a>>b;
    while(b!=0)
    {
        int r = a%b;
        a=b;
        b=r;
    }    
    cout<<a;
    return 0;
}
```

- calculeaza suma a n numere citite de la tastatura

```C++

#include<iostream>
using namespace std;

int main()
{
    int n; int a;
    int s;
    for(int i=0;i<n;i=i+1)
    {
        cin>>a;
        s=s+a;
    }
    cout<<s;
    return 0;
}
```

## 3. Surse care contin erori conform MLP

- doua erori care sunt in acelasi timp erori in limbajul original

```C++
#include<iostream>
using namespace std;

int main()
{   
    while(int i=0;i<3;i++) // error: expected ‘)’ before ‘;’ token
        cout<<i<<endl;
    return ;  // error: return-statement with no value, in function returning ‘int’
}
```

- doua erori conform MLP, dar care nu sunt erori in limbajul original

```C++
#include<iostream>
using namespace std;

struct x{int a, b;}; // <-- struct not specified

int main()
{   
    for(int i=5;i--;) // for(vdecl; cond; i_attr)
        cout<<i<<endl;
    return 0;  
}
```