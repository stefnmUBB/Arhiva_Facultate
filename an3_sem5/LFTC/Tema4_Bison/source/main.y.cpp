%token INCLUDE IOSTREAM USING NAMESPACE STD INT MAIN RETURN IF ELSE WHILE FOR FLOAT CIN COUT ENDL ID CONST
%token OP_RSH OP_LSH OP_EQ OP_NE OP_GE OP_LE OP_ATTR
%%

program          : program_header program_body ;

program_header   : INCLUDE IOSTREAM USING NAMESPACE STD ';' ;
program_body     : INT MAIN '(' ')' '{' instr_list RETURN CONST ';' '}' ; 

instr_list       : instr_list instr | instr ;

scope            : '{' instr_list '}' ;
scope_or_single_instr : scope | instr ;

instr             : instr ';' | _instr ;
_instr            : i_cond | i_while | i_for | i_attr ';' | i_vdecl ';' | i_cin ';' | i_cout ';';

i_cond           : IF '(' cond_expr ')' scope_or_single_instr ELSE scope_or_single_instr | IF '(' cond_expr ')' scope_or_single_instr ;
i_while          : WHILE '(' cond_expr ')' scope_or_single_instr ;
i_for            : FOR '(' i_vdecl ';' cond_expr ';' i_attr ')' scope_or_single_instr ;
i_attr           : ID OP_ATTR arithm_expr;
i_vdecl          : datatype ID OP_ATTR arithm_expr | datatype ID ;

i_cin            : i_cin OP_RSH ID | CIN ;
i_cout           : i_cout OP_LSH ENDL | i_cout OP_LSH ID | COUT ;


datatype         : INT | FLOAT ;

cond_expr        : arithm_expr OP_EQ arithm_expr | arithm_expr OP_NE arithm_expr | arithm_expr OP_GE arithm_expr | arithm_expr OP_LE arithm_expr 
                 | arithm_expr '<' arithm_expr  | arithm_expr '>' arithm_expr ;

arithm_expr      : addsub_expr ;
addsub_expr      : muldiv_expr '+' addsub_expr | muldiv_expr '-' addsub_expr | muldiv_expr ;
muldiv_expr      : arithm_expr_term '*' muldiv_expr | arithm_expr_term '/' muldiv_expr | arithm_expr_term '%' muldiv_expr | arithm_expr_term ;
arithm_expr_term : ID | CONST |  '(' arithm_expr ')';


%%

/*
i_cond         : IF '(' cond_expr ')' scope_or_single_instr ELSE scope_or_single_instr | IF '(' cond_expr ')' scope_or_single_instr ;
i_while        : WHILE '(' cond_expr ')' scope_or_single_instr ;

cond_expr      : '1'

*/

// i_attr         : ID '=' ID ;

/*program_body : 

program : VAR typedecls ;
typedecls : typedecl | typedecls typedecl ;
typedecl : varlist ':' var_type ';' ;
varlist : VAR_NAME | varlist ',' VAR_NAME ;
var_type : REAL | BOOLEAN | INTEGER | CHAR ;*/