%{
#include <iostream>
#include <stdio.h>

extern "C" int yylex();
extern "C" int isatty(int);

#include "symtable.hpp"

std::vector<Atom*> atoms;

int pos = 0;

%}

%option yylineno

alpha       [A-Za-z]
alphanum    [A-Za-z0-9]
ID          {alpha}{alphanum}*
KW          "if"|"else"|"cin"|"cout"|"int"|"main"|"float"|"using"|"namespace"|"return"|"#include"|"cattimp"|"executa"|"sfcattimp"
CONST       [1-9][0-9]*
CONST_F     {CONST}"."[0-9]*
SYMBOL      "+"|"-"|"*"|"/"|"%"|"="|"<"|">"|"=="|"!="|"<="|">="|"<<"|">>"|"("|")"|"{"|"}"|";"|","|"->"

%%
[ \t\r\n]           /* ignore white spaces */
{KW}                atoms.push_back(Atom::create_keyword(yytext, yylineno , pos )); pos += yyleng;
{ID}                atoms.push_back(Atom::create_identifier(yytext, yylineno , pos )); pos += yyleng;
{CONST}             atoms.push_back(Atom::create_const(yytext, yylineno , pos  )); pos += yyleng;
{CONST_F}           atoms.push_back(Atom::create_const(yytext, yylineno , pos )); pos += yyleng;
{SYMBOL}            atoms.push_back(Atom::create_symbol(yytext, yylineno , pos  )); pos += yyleng;
.                   /* ignore */
%%

int main()
{
	yylex();
	
	for(int i=0;i<atoms.size();i++)
	{
		printf("%10s %10s\n", atoms[i]->text.c_str(), atoms[i]->type.c_str());
	}	
	
	printf("-----------------------------------------------------------------------\n");
	
	std::string err;
	
	auto* sym_table = SymbolsTable::build(atoms, err);
	
	if(err!="")
	{
		printf("ERROR: %s\n", err.c_str());
	}
	
	printf("_FIP___________________\n");
	printf("%10s|%10s\n", "cod", "ts_pos");
	
	for(auto& p : sym_table->FIP)
	{
		printf("%10i|%10i\n", p.first, p.second);
	}
	
	printf("\n\n");
	
	
	printf("_TS___________________\n");
	printf("%10s|%10s\n", "atom", "id");
	
	
	for(auto& p : sym_table->TS)
	{
		printf("%10s|%10i\n", p.first.c_str(), p.second);
	}
	
	
	delete sym_table;
	return 0;	
}