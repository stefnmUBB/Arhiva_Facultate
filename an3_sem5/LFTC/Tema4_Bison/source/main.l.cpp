%{					
	#include <stdio.h>    

	extern "C" int yylex();
	extern "C" int isatty(int);
	extern "C" int yyerror(const char*);	
	extern "C" int yywrap() { return 1; }
	extern "C" int yyerror(const char* msg)
	{
		printf("Error: %s at %i\n", msg, yylineno);
		return 1;
	}
	
	#include "main.tab.cpp"
	
	#define __ldbg__(X) ([](){printf( #X "\n"); return X;})();

%}

%option yylineno

%%
[ \t\n]+                { /* Ignore all whitespace */ }

^"#include"                { return __ldbg__(INCLUDE); }
"<iostream>"               { return __ldbg__(IOSTREAM); }

using                      { return __ldbg__(USING); }
namespace                  { return __ldbg__(NAMESPACE); }
std                        { return __ldbg__(STD); }
int                        { return __ldbg__(INT); }
main                       { return __ldbg__(MAIN); }
return                     { return __ldbg__(RETURN); }
if                         { return __ldbg__(IF); }
else                       { return __ldbg__(ELSE); }
while                      { return __ldbg__(WHILE); }
for                        { return __ldbg__(FOR); }
float                      { return __ldbg__(FLOAT); }
cin                        { return __ldbg__(CIN); }
cout                       { return __ldbg__(COUT); }
">>"                       { return __ldbg__(OP_RSH); }
"<<"                       { return __ldbg__(OP_LSH); }
endl                       { return __ldbg__(ENDL); }

"="                        { return __ldbg__(OP_ATTR); }

"=="                       { return __ldbg__(OP_EQ); }
"!="                       { return __ldbg__(OP_NE); }
">="                       { return __ldbg__(OP_GE); }
"<="                       { return __ldbg__(OP_LE); }

[a-zA-Z][a-zA-Z0-9_]*   { return __ldbg__(ID); }

[1-9][0-9]*\.[0-9]*      { return __ldbg__(CONST); }
[1-9][0-9]*              { return __ldbg__(CONST); }
0                        { return __ldbg__(CONST); }

.                       { return yytext[0]; }
%%


main(int argc, char** argv)
{	
	printf("I'm this main\n");	
	yyin = stdin;
	//yylex(); 
	yyparse();
}