%glr-parser

%%

seq                 : 'a' seq 'a' 
                    | 'b' seq 'b' 
                    | 'c' seq 'c' 
                    | 'd' seq 'd' 
                    | 'e' seq 'e' 
                    | 'f' seq 'f' 
                    | 'g' seq 'g' 
                    | 'h' seq 'h' 
                    | 'i' seq 'i' 
                    | 'j' seq 'j' 
                    | 'k' seq 'k' 
                    | 'l' seq 'l' 
                    | 'm' seq 'm' 
                    | 'n' seq 'n' 
                    | 'n' seq 'n' 
                    | 'o' seq 'o' 
                    | 'p' seq 'p' 
                    | 'q' seq 'q' 
                    | 'r' seq 'r' 
                    | 's' seq 's' 
                    | 't' seq 't' 
                    | 'u' seq 'u' 
                    | 'v' seq 'v' 
                    | 'w' seq 'w' 
                    | 'x' seq 'x' 
                    | 'y' seq 'y' 
                    | 'z' seq 'z' 								
					| 'a' | 'b' | 'c' | 'd' | 'e' | 'f' | 'g' | 'h' | 'i' | 'j' | 'k' | 'l' | 'm' | 'n' | 'o' | 'p' | 'q' | 'r' | 's' | 't' | 'u' | 'v' | 'w' | 'x' | 'y' | 'z' |;


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