%{					
	#include <stdio.h>    
	#include <string.h>    

	extern "C" int yylex();
	extern "C" int isatty(int);
	extern "C" int yyerror(const char*);	
	extern "C" int yywrap() { return 1; }
	extern "C" int yyerror(const char* msg)
	{
		printf("\nError: %s at %i\n", msg, yylineno);
		return 1;
	}
	
	#include "main.tab.cpp"
	
	#define __ldbg__(X) ([](){printf( #X "\n"); return X;})();

%}

%option yylineno

%%
[ \t\n]+                { /* Ignore all whitespace */ }

[a-z]                { printf("%c", yytext[0]); return yytext[0]; }
[A-Z]                { printf("%c", yytext[0]-'A'+'a'); return yytext[0]-'A'+'a'; }

	.                   { /* ignore */ }
%%


main(int argc, char** argv)
{	
	printf("I'm this main\n");	
	yyin = stdin;
	//yylex(); 
	yyparse();
}