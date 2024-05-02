%code requires{
	#include "ast.h"
}

%union{
	unsigned number;
	char* string;
	_C_ASTNode node;
	ExprBinaryOp oper;
}

%parse-param {
	struct _C_ASTNode* res_node
}

%token INT CIN COUT ENDL

%token OP_RSH OP_LSH OP_EQ OP_NE OP_GE OP_LE OP_ATTR
%token<oper> OP_ADD OP_SUB OP_MUL OP_DIV OP_MOD

%token<string> ID
%token<number> CONST

%type <number> wrap_const
%type <string> wrap_id

%type <node> program atom instr_list instr _instr i_attr i_vdecl i_cin i_cout arithm_expr addsub_expr muldiv_expr arithm_expr_term
%type <oper> add_or_sub mul_or_div_or_mod



%%

program           : instr_list                                                                { *res_node = $$ = $1; };

instr_list        : instr instr_list                                                          { $$ = create_block($1, $2); }
                  | instr                                                                     { $$ = $1; };

instr             : _instr ';' _any_sc                                                        { $$ = $1; };       

_any_sc           : ';' _any_sc | ; // inifnite ;;;;

_instr            : i_attr                                                                    { $$ = $1; };
                  | i_vdecl                                                                   { $$ = $1; };
				  | i_cin                                                                     { $$ = $1; };
				  | i_cout                                                                    { $$ = $1; };


i_attr            : wrap_id OP_ATTR arithm_expr                                               { $$ = creade_attr($1, $3);  }
                  ;
i_vdecl           : INT wrap_id OP_ATTR arithm_expr                                           { $$ = creade_decl($2, $4);  }
                  | INT wrap_id                                                               { $$ = creade_decl($2, create_const(0));  }
				  ;
i_cin             : i_cin OP_RSH wrap_id                                                      { $$ = create_block($1, create_read($3));  }
                  | CIN                                                                       { $$ = create_nop();  }
				  ;				  
i_cout            : i_cout OP_LSH ENDL                                                        { $$ = create_block($1, create_writeline());  }
                  | i_cout OP_LSH arithm_expr                                                 { $$ = create_block($1, create_write($3));  }
                  | COUT                                                                      { $$ = create_nop();  }
				  ;


add_or_sub        : OP_ADD                                                                    { $$ = Add; };
                  | OP_SUB                                                                    { $$ = Sub; };
mul_or_div_or_mod : OP_MUL                                                                    { $$ = Mul };
                  | OP_DIV                                                                    { $$ = Div; };
                  | OP_MOD                                                                    { $$ = Mod; };

arithm_expr       : addsub_expr                                                               { $$ = $1; };
addsub_expr       : addsub_expr add_or_sub muldiv_expr                                        { $$ = create_binary_expression($2, $1, $3); }
                  | muldiv_expr                                                               { $$ = $1; };
muldiv_expr       : arithm_expr_term mul_or_div_or_mod muldiv_expr                            { $$ = create_binary_expression($2, $1, $3); }
                  | arithm_expr_term                                                          { $$ = $1; }
				  ;
arithm_expr_term  : atom                                                                      { $$ = $1; }
                  | '(' arithm_expr ')'                                                       { $$ = $2; }
				  ;

atom              : wrap_id                                                                   { $$ = create_id($1); }
                  | wrap_const                                                                { $$ = create_const($1); }
				  ;

wrap_id           : ID                                                                        { $$ = alc_copy_str(yytext); }
                  ;
wrap_const        : CONST                                                                     { $$ = atoi(yytext); }
                  ;


%%