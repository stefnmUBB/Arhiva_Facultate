#pragma once

#include <vector>
#include <set>
#include <map>
#include <string>
#include <algorithm>

struct Atom
{
	std::string text;
	std::string type;	
	
	int line, pos;
	
	Atom(const char* text, const char* type, int line, int pos) : text{text}, type{type}, line{line}, pos{pos} { }
	
	static Atom* create_identifier(const char* text, int line, int pos) { return new Atom(text, "ID", line, pos); }
	static Atom* create_keyword(const char* text,int line, int pos) { return new Atom(text, "KW", line, pos); }
	static Atom* create_const(const char* text,int line, int pos) { return new Atom(text, "CONST", line, pos); }
	static Atom* create_symbol(const char* text,int line, int pos) { return new Atom(text, "SYMBOL", line, pos); }
};

struct Symbol
{
	std::string text;
	int id;
	
	Symbol(std::string text, int id=-1) : text{text}, id{id} { }
};

#define cod first
#define ts_pos second

class SymbolsTable
{
private:
	static std::vector<std::string> ReservedAtomsList;	
public:
	std::vector<std::pair<int,int>> FIP;
	std::vector<std::pair<std::string, int>> TS;
	
	SymbolsTable(const std::vector<std::pair<int,int>>& FIP, const std::vector<std::pair<std::string, int>>& TS) : FIP(FIP), TS(TS) { }
	
	static SymbolsTable* build(std::vector<Atom*> atoms, std::string& err)
	{
		err="";
		std::map<std::string, Symbol*> symbols;
		std::vector<std::pair<int,int>> FIP(atoms.size());		
		std::vector<std::pair<int, Symbol*>> solveQ;
		
		for(int i=0;i<atoms.size();i++)
		{						
			if(i>0)
			{
				if (atoms[i - 1]->type == "CONST" && atoms[i]->type == "ID") 
				{					
					if (atoms[i - 1]->pos + atoms[i - 1]->text.size() == atoms[i]->pos) 
					{
						err = "Two identifiers next to each other: " + std::to_string(atoms[i]->pos) + " at line " + std::to_string(atoms[i]->line);
						break;
					}
				}
			}
			
			if(atoms[i]->type=="ID" && atoms[i]->text.size()>250)
			{
				err = "Identifier too long"+std::to_string(atoms[i]->pos) + " at line " + std::to_string(atoms[i]->line);
				break;
			}
			
			FIP[i].ts_pos = -1;
			
			int index = index_of(ReservedAtomsList, atoms[i]->text);
			
			if(index>=2)
			{
				FIP[i].cod = index;
			}
			else
			{
				if (index == 0 || index == 1) 
				{
					err = "ReservedAtomsList index 0 or 1";
					printf("erres\n");
					break;
				}
				
				if(!symbols.count(atoms[i]->text))								
					 symbols[atoms[i]->text] = new Symbol(atoms[i]->text);
				Symbol* s = symbols[atoms[i]->text];
				
				
				if(atoms[i]->type=="ID") FIP[i].cod = 0;					
				else if(atoms[i]->type=="CONST") FIP[i].cod = 1;
				else
				{
					err = "Invalid token: '" + atoms[i]->text +"'"+std::to_string(atoms[i]->pos) + " at line " + std::to_string(atoms[i]->line);;
					printf("errinvalid\n");
					break;
				}
				solveQ.push_back(std::make_pair(i,s));			
			}			
		}	

		int k=0;
		std::vector<std::pair<std::string, int>> ts;
		
		for(auto& kv : symbols) 		
		{			
			kv.second->id=k;
			ts.push_back(std::make_pair(kv.first, k++));						
		}
	
		for(auto& kv : solveQ)
		{
			FIP[kv.first].ts_pos = kv.second->id;
		}
		
		return new SymbolsTable(FIP, ts);									
		
	}
	
	template<typename T>
	static int index_of(const std::vector<T>& v, const T& x)
	{
		auto it = std::find(v.begin(), v.end(), x); 
		if (it != v.end())  	
			return it - v.begin(); 			
		return -1;
	}		
	

private:
	
};

std::vector<std::string> SymbolsTable::ReservedAtomsList = 
{
	"%ID%",
	"%CONST%",
	"#include",
	"iostream",
	"using",
	"namespace",
	"std",
	"int",
	"float",
	"main",
	"return",
	"while",
	"for",
	"if",
	"else",
	"cin",
	"cout",
	"endl",
	"+",
	"-",
	"*",
	"/",
	"%",
	"=",
	"<",
	">",
	"==",
	"!=",
	"<=",
	">=",
	"<<",
	">>",
	"(",
	")",
	"{",
	"}",
	";",
	",",
	"cattimp",
	"executa",
	"sfcattimp",
	"->"
};		