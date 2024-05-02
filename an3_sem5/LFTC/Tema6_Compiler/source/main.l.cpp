%{					
	#include <stdio.h>    
	#include <string.h>    
	#include <iostream>    
	#include <fstream>    
	#include <string>    
	
	#include "ast.h"	
	#include "compiler.h"	

	extern "C" int yylex();
	extern "C" int isatty(int);
	//extern "C" int yyerror(_C_ASTNode, const char*);	
	extern "C" int yywrap() { return 1; }
	int yyerror(_C_ASTNode*, const char* msg)
	{
		printf("Error: %s at %i\n", msg, yylineno);
		return 1;
	}
		
	
	#include "main.tab.cpp"
	
	//#define __ldbg__(X) ([](){printf( #X "\n"); return X;})();
	#define __ldbg__(X) ([](){ return X;})();

%}

%option yylineno

%%
[ \t\n]+                { /* Ignore all whitespace */ }

int                        { return __ldbg__(INT); }
in                        { return __ldbg__(CIN); }
out                       { return __ldbg__(COUT); }
">>"                       { return __ldbg__(OP_RSH); }
"<<"                       { return __ldbg__(OP_LSH); }
endl                       { return __ldbg__(ENDL); }

"="                        { return __ldbg__(OP_ATTR); }

"=="                       { return __ldbg__(OP_EQ); }
"!="                       { return __ldbg__(OP_NE); }
">="                       { return __ldbg__(OP_GE); }
"<="                       { return __ldbg__(OP_LE); }


"+"                        { return __ldbg__(OP_ADD); }
"-"                        { return __ldbg__(OP_SUB); }
"*"                        { return __ldbg__(OP_MUL); }
"/"                        { return __ldbg__(OP_DIV); }
"%"                        { return __ldbg__(OP_MOD); }

[a-zA-Z][a-zA-Z0-9_]*    { if(strlen(yytext)>200) { yyerror(nullptr, "id too long"); } return __ldbg__(ID); }

[1-9][0-9]*\.[0-9]*      { return __ldbg__(CONST); }
[1-9][0-9]*              { return __ldbg__(CONST); }
0                        { return __ldbg__(CONST); }

.                       { return yytext[0]; }
%%




main(int argc, char** argv)
{	
	printf("I'm this main\n");	
	yyin = stdin;
	
	_C_ASTNode cnode;	
	int result = yyparse(&cnode);	
	
	ASTNode* node = get_node<ASTNode>(cnode, ASTNodeType::Unknown);
	
	std::cout<<"Intermediate language:\n";
	std::cout<<ASTNode::str(node)<<"\n";
	std::cout<<"\n\n";
	
	
	std::cout<<"Compiling\n";
	Compiler compiler;		
	std::string compiled = compiler.compile_asm(node);	
	std::cout<<compiled<<"\n";
	
	if(argc>1)
	{
		std::ofstream f(argv[1]);
		f<<compiled;
		f.close();
	}	
	
	std::cout<<"Done.\n";
	
}