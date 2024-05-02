#pragma once

#include "ast.h"

#include <vector>
#include <string>
#include <map>
#include <stdexcept>
#include <sstream>
#include <regex>

#define EOL "\n"

enum class OpCode
{
	PushCt,   // PSHC @C  : push constant onto stack
	PushId,   // PSHI @O  : push value of id onto stack
	PushRef,  // PSHR @O  : push address of id onto stack
	Pop,      // POP  @O  : pop from stack to id offset
	
	Add,      // ADD pop 2 value from stack and push their sum
	Sub,      // SUB
	Mul,      // MUL
	Div,      // DIV
	Mod,      // MOD
	
	Read,     // RD      : calls built in read int function and puts the result at the address on top of the stack (pops it)
	Write,    // WR      : calls built in write int function
	WriteLn,  // WL      : calls built in write line function
};

struct AsmIntermediateInstr
{
	OpCode opcode;
	unsigned long long operand;


	std::string to_asm() const
	{
		switch(opcode)
		{
			case OpCode::PushCt   : return (std::stringstream()<<"push DWORD "<<operand).str();
			case OpCode::PushId   : return (std::stringstream()<<"push DWORD [ work_data + "<<operand<<" ]").str();
			case OpCode::PushRef  : return (std::stringstream()<<"push DWORD work_data + "<<operand).str();
			case OpCode::Pop      : return (std::stringstream()<<"pop  DWORD [ work_data + "<<operand<<" ]").str();
			case OpCode::Add      : return (std::stringstream()<<"pop  ebx\npop  eax\nadd  eax, ebx\npush eax").str();
			case OpCode::Sub      : return (std::stringstream()<<"pop  ebx\npop  eax\nsub  eax, ebx\npush eax").str();
			case OpCode::Mul      : return (std::stringstream()<<"pop  ebx\npop  eax\nimul ebx\npush eax").str();
			case OpCode::Div      : return (std::stringstream()<<"pop  ebx\npop  eax\ncdqe\nidiv ebx\npush eax").str();
			case OpCode::Mod      : return (std::stringstream()<<"pop  ebx\npop  eax\ncdqe\nidiv ebx\npush edx").str();
			case OpCode::Read     : return (std::stringstream()<<"call read").str();
			case OpCode::Write    : return (std::stringstream()<<"call write").str();
			case OpCode::WriteLn  : return (std::stringstream()<<"call writeln").str();
		}
		return "<asm>";		
	}		

	std::string to_string() const
	{
		switch(opcode)
		{
			case OpCode::PushCt: return std::string("PSHC ")+std::to_string((int)operand);
			case OpCode::PushId: return std::string("PSHI ")+std::to_string(operand);
			case OpCode::Pop: return std::string("POP ")+std::to_string(operand);			
			case OpCode::Add: return std::string("ADD");
			case OpCode::Sub: return std::string("SUB");
			case OpCode::Mul: return std::string("MUL");
			case OpCode::Div: return std::string("DIV");
			case OpCode::Mod: return std::string("MOD");
			case OpCode::Write: return std::string("WR");
			case OpCode::WriteLn: return std::string("WL");
			case OpCode::Read: return std::string("RD");
		}		
		return "<aii>";
	}
	
	
};

struct CompileContext
{
	std::map<std::string, int> data_offsets;

	int declare_variable(const char* name)
	{
		std::string vname = name;
		if(data_offsets.find(vname) != data_offsets.end())
			throw std::runtime_error((std::string("Duplicate identifier declaration: ") + vname).c_str());
		int offset = data_offsets.size();
		return data_offsets[vname] = 4*offset;
	}
	
	int get_variable_offset(const char* name)
	{
		std::string vname = name;
		if(data_offsets.find(vname) == data_offsets.end())
			throw std::runtime_error((std::string("Identifier not found: ") + vname).c_str());
		return data_offsets[vname];
	}
	
	int size() const { return 4*data_offsets.size()+4; }
	
};

class Compiler
{
private:
	CompileContext ctx;

	
	template<typename T>
	static const T* cast_node(const ASTNode* node)
	{
		const T* n = static_cast<const T*>(node);
		if(n==nullptr)
			throw std::runtime_error((std::string("Cast node returns null: ") + ASTNode::str(node)).c_str());
		return n;
	}
	
	static void append(std::vector<AsmIntermediateInstr>& dest, const std::vector<AsmIntermediateInstr>& src)
	{
		dest.insert(dest.end(), src.begin(), src.end());
	}
	
	static void append(std::vector<AsmIntermediateInstr>& dest, const AsmIntermediateInstr& item) { dest.push_back(item); }	
	
public:
	std::vector<AsmIntermediateInstr> compile(const ASTNode* node)
	{
		std::vector<AsmIntermediateInstr> result;		
		if(node==nullptr) throw std::runtime_error("Node is null");
		
		switch(node->type)
		{
			case ASTNodeType::Const:
			{
				append(result, AsmIntermediateInstr{ OpCode::PushCt, cast_node<ConstNode>(node)->get_value() });
				break;
			}			
			case ASTNodeType::Id:
			{
				const char* id = cast_node<IdNode>(node)->get_name();	
				
				append(result, AsmIntermediateInstr{ OpCode::PushId, (unsigned long long)ctx.get_variable_offset(id) });				
				break;
			}			
			case ASTNodeType::Block:
			{
				const auto* nd = cast_node<BlockInstr>(node);				
				append(result, compile(nd->get_head()));
				append(result, compile(nd->get_tail()));		
				break;
			}
			case ASTNodeType::Decl:	
			{
				const auto* nd = cast_node<DeclNode>(node);							
				int offset = ctx.declare_variable(nd->get_id());								
				append(result, compile(nd->get_expr()));										
				append(result, AsmIntermediateInstr{ OpCode::Pop, (unsigned long long)offset });				
				break;
			}
			case ASTNodeType::Attr:	
			{
				const auto* nd = cast_node<AttrNode>(node);							
				int offset = ctx.get_variable_offset(nd->get_id());
				append(result, compile(nd->get_expr()));
				append(result, AsmIntermediateInstr{ OpCode::Pop, (unsigned long long)offset });				
				break;
			}			
			case ASTNodeType::BinaryExpr:	
			{				
				const auto* nd = cast_node<ExprBinaryNode>(node);							
				OpCode opc;		
				
				switch(nd->get_operation())
				{
					case ExprBinaryOp::Add: opc = OpCode::Add; break;
					case ExprBinaryOp::Sub: opc = OpCode::Sub; break;
					case ExprBinaryOp::Mul: opc = OpCode::Mul; break;
					case ExprBinaryOp::Div: opc = OpCode::Div; break;
					case ExprBinaryOp::Mod: opc = OpCode::Mod; break;
					default: throw std::runtime_error("Unknown operation");
				}				
				append(result, compile(nd->get_left_operand()));
				append(result, compile(nd->get_right_operand()));
				append(result, AsmIntermediateInstr{ opc });
				break;
			}
			case ASTNodeType::WriteLn:
			{
				append(result, AsmIntermediateInstr{ OpCode::WriteLn });
				break;
			}				
			case ASTNodeType::Write:
			{
				const auto* nd = cast_node<WriteNode>(node);							
				append(result, compile(nd->get_expr()));				
				append(result, AsmIntermediateInstr{ OpCode::Write });
				break;
			}				
			case ASTNodeType::Read:
			{
				const auto* nd = cast_node<ReadNode>(node);			
				int offset = ctx.get_variable_offset(nd->get_id());
				append(result, AsmIntermediateInstr{ OpCode::PushRef, (unsigned long long)offset });				
				append(result, AsmIntermediateInstr{ OpCode::Read });
				break;
			}																			
			
			default: break;
		}								
		
		return result;			
	}
	
	std::string build(const std::vector<AsmIntermediateInstr>& aiis)
	{
		std::string data_size = std::to_string(ctx.size());
		std::string code="";		
		for(auto aii : aiis) 
		{
			code+=aii.to_asm()+"\n";
		}
		code = "\n\t\t"+std::regex_replace(code, std::regex("\n"), "\n\t\t");
		
		
		std::string result = asm_template;
		result = std::regex_replace(result, std::regex("%%DATA_SIZE%%"), data_size);
		result = std::regex_replace(result, std::regex("%%CODE%%"), code);
		
		return result;
	}	
	
	
	std::string compile_asm(const ASTNode* node)
	{
		return build(compile(node));
	}
	
	std::string asm_template = 
		"bits 32" EOL
		"global start" EOL
		"extern exit" EOL
		"import exit msvcrt.dll" EOL
		"extern printf" EOL
		"import printf msvcrt.dll" EOL
		"extern scanf" EOL
		"import scanf msvcrt.dll" EOL
		"segment data use32 class=data" EOL
		"write_int_fmt DB \"%d \", 0" EOL
		"write_line_fmt DB 0xA, 0x0" EOL
		"request_int_fmt DB \"Va rog introduceti un numar: \", 0" EOL
		"read_int_fmt DB \"%i\", 0" EOL
		"work_data DD %%DATA_SIZE%%" EOL
		"segment code use32 class=code" EOL
		"	write:" EOL
		"		push DWORD [ESP+4]" EOL
		"		push write_int_fmt" EOL
		"		call [printf]" EOL
		"		add ESP, 4*2" EOL
		"		ret 4" EOL
		"	writeln:" EOL
		"		push write_line_fmt" EOL
		"		call [printf]" EOL
		"		add ESP, 4" EOL
		"		ret" EOL
		"	read:" EOL
		"		push request_int_fmt" EOL
		"		call [printf]" EOL
		"		add ESP, 4" EOL
		"		push DWORD [ESP+4]" EOL
		"		push read_int_fmt" EOL
		"		call [scanf]" EOL
		"		add ESP, 4*2" EOL
		"		ret 4" EOL
		"    start:" EOL
		"		%%CODE%%" EOL
		"		push    dword 0" EOL
		"		call    [exit]";
	
};