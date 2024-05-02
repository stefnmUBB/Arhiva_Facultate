#pragma once

#include<stdlib.h>
#include<string>
#include<string.h>

enum class ASTNodeType
{
	Unknown,
	Id,
	Const,	
	BinaryExpr,
	Decl,
	Attr,	
	Read,
	Write,	
	WriteLn,
	Block,
	Nop
};

const char* node_type_str(ASTNodeType t)
{
	switch(t)
	{
		case ASTNodeType::Unknown: return "Unknown";
		case ASTNodeType::Id: return "Id";
		case ASTNodeType::Const: return "Const";
		case ASTNodeType::BinaryExpr: return "BinaryExpr";
		default: return "(invalid)";
	}
}

extern "C" enum ExprBinaryOp
{
	Add, Sub, Mul, Div, Mod
};

const char* binary_op_str(ExprBinaryOp t)
{
	switch(t)
	{
		case Add: return "+";
		case Sub: return "-";
		case Mul: return "*";
		case Div: return "/";
		case Mod: return "%";
		default: return "?!";
	}
}

class ASTNode
{
public:
	ASTNodeType type;	
		
	virtual std::string to_string() const { return std::string("<ASTNode:") + node_type_str(type) + ">"; }
		
	virtual ~ASTNode() = default;
	
	static std::string str(const ASTNode* nd) { return nd ? nd->to_string() : "<ASTNode:null>"; }	
	
protected:
	ASTNode(ASTNodeType type) : type(type) { }
};

class ExprNode : public ASTNode 
{ 
protected:
	ExprNode(ASTNodeType type) : ASTNode(type) { }
};

class ExprTerminalNode : public ExprNode 
{ 
protected:
	ExprTerminalNode(ASTNodeType type) : ExprNode(type) { }
};

char* alc_copy_str(const char* str) 
{
	char* r = new char[strlen(str)+1];
	strcpy(r, str);
	return r;
}

class IdNode : public ExprTerminalNode
{
private:
	char* name;
public:
	IdNode(const char* name) : ExprTerminalNode(ASTNodeType::Id), name{alc_copy_str(name)} { }	
	
	const char* get_name() const { return name; } 
	
	std::string to_string() const override { return std::string("id:")+name; }
	
	~IdNode() { delete[] name; }
};

class ConstNode : public ExprTerminalNode
{
private:
	unsigned value;
public:
	ConstNode(int value) : ExprTerminalNode(ASTNodeType::Const), value(value)  {}		
	unsigned get_value() const { return value; }		
	
	std::string to_string() const override
	{		
		return std::string("int:")+std::to_string(value);
	}
	
};

class ExprBinaryNode : public ExprNode
{
private:
	ExprBinaryOp operation;
	ExprNode* left;
	ExprNode* right;	
public:
	ExprBinaryNode(ExprBinaryOp operation, ExprNode* left, ExprNode* right) : ExprNode(ASTNodeType::BinaryExpr),
		operation(operation), left{left}, right{right} {}	
		
	ExprBinaryOp get_operation() const { return operation; }
	const ExprNode* get_left_operand() const { return left; }
	const ExprNode* get_right_operand() const { return right; }
		
	std::string to_string() const override
	{		
		return "("+ str(left)+binary_op_str(operation)+str(right)+")";
	}
};

class InstrNode : public ASTNode
{
protected:
	InstrNode(ASTNodeType type) : ASTNode(type) { }
};

class BlockInstr : public InstrNode
{
private:
	const InstrNode* head;
	const InstrNode* tail;
public:
	BlockInstr(const InstrNode* head, const InstrNode* tail) : InstrNode(ASTNodeType::Block), head{head}, tail{tail} { }			
	
	const InstrNode* get_head() const { return head; }
	const InstrNode* get_tail() const { return tail; }		
	
	std::string to_string() const override
	{		
		return str(head)+"\n"+str(tail);
	}
};

class DeclNode : public InstrNode
{
private:
	char* id;
	ExprNode* expr;
public:
	DeclNode(const char* id, ExprNode* expr) : InstrNode(ASTNodeType::Decl), id{alc_copy_str(id)}, expr{expr} { }			
	
	const char* get_id() const { return id; }
	const ExprNode* get_expr() const { return expr; }
	
	std::string to_string() const override
	{		
		return "int "+std::string(id)+" = " + str(expr);
	}
};

class AttrNode : public InstrNode
{
private:
	char* id;
	ExprNode* expr;
public:
	AttrNode(const char* id, ExprNode* expr) : InstrNode(ASTNodeType::Attr), id{alc_copy_str(id)}, expr{expr} { }		

	const char* get_id() const { return id; }
	const ExprNode* get_expr() const { return expr; }	
	
	std::string to_string() const override
	{		
		return std::string(id)+" = " + str(expr);
	}
};

class ReadNode : public InstrNode
{
private:
	const char* id;
public:
	ReadNode(const char* id) : InstrNode(ASTNodeType::Read), id{id} { }	

	const char* get_id() const { return id; }

	std::string to_string() const override
	{		
		return std::string("read ")+ id;
	}
};

class WriteNode : public InstrNode
{
private:
	ExprNode* expr;
public:
	WriteNode(ExprNode* expr) : InstrNode(ASTNodeType::Write), expr{expr} { }		

	const ExprNode* get_expr() const { return expr; }

	std::string to_string() const override
	{		
		return std::string("write ")+ str(expr);
	}
};

class WriteLineNode : public InstrNode
{
public:
	WriteLineNode() : InstrNode(ASTNodeType::WriteLn) { }		
	std::string to_string() const override { return "writeline"; }	
};

class NopNode : public InstrNode
{
public:
	NopNode() : InstrNode(ASTNodeType::Nop) { }		
	std::string to_string() const override { return "nop"; }	
};

extern "C" struct _C_ASTNode { void* node; };


template<typename T> T* get_node(_C_ASTNode cnode, ASTNodeType type, bool suppressError=false)
{
	ASTNode* node = static_cast<ASTNode*>(cnode.node);
	
	if(node==nullptr) return nullptr;
	
	if(type==ASTNodeType::Unknown)
		return (T*)node;
	
	if(node->type != type && !suppressError) {
		printf("Invalid node type: requested %s, got %s\n", node_type_str(type), node_type_str(node->type));
		return nullptr;
	}
	
	return (T*)node;	
}


ExprNode* get_expr_node(_C_ASTNode cnode)
{
	ExprNode* node = nullptr;	
	if(node = get_node<ExprNode>(cnode, ASTNodeType::BinaryExpr, true)) return node;	
	if(node = get_node<ExprNode>(cnode, ASTNodeType::Const, true)) return node;	
	if(node = get_node<ExprNode>(cnode, ASTNodeType::Id, true)) return node;
		
	return get_node<ExprNode>(cnode, ASTNodeType::BinaryExpr); // fallthrough to show error
}

InstrNode* get_instr_node(_C_ASTNode cnode)
{
	InstrNode* node = nullptr;	
	if(node = get_node<InstrNode>(cnode, ASTNodeType::Decl, true)) return node;	
	if(node = get_node<InstrNode>(cnode, ASTNodeType::Attr, true)) return node;	
	if(node = get_node<InstrNode>(cnode, ASTNodeType::Read, true)) return node;
	if(node = get_node<InstrNode>(cnode, ASTNodeType::Write, true)) return node;
	if(node = get_node<InstrNode>(cnode, ASTNodeType::Block, true)) return node;
		
	return get_node<InstrNode>(cnode, ASTNodeType::Decl);		
}


extern "C"
{	
	_C_ASTNode no_ast() { return _C_ASTNode{nullptr}; }  
	_C_ASTNode create_nop() { return _C_ASTNode{new NopNode()}; }  
	
	_C_ASTNode create_id(const char* name) { return _C_ASTNode{ new IdNode(name) }; }
	_C_ASTNode create_const(unsigned value) { return _C_ASTNode{ new ConstNode(value) }; }
	
	_C_ASTNode create_binary_expression(ExprBinaryOp op, _C_ASTNode left, _C_ASTNode right) 
	{ 
		ExprNode* lnode = get_expr_node(left);
		ExprNode* rnode = get_expr_node(right);				
		return _C_ASTNode{ new ExprBinaryNode(op, lnode, rnode) }; 
	}	
	
	_C_ASTNode creade_decl(const char* id, _C_ASTNode expr)
	{
		ExprNode* enode = get_expr_node(expr);
		return _C_ASTNode{ new DeclNode(id, enode) };
	}
	
	_C_ASTNode creade_attr(const char* id, _C_ASTNode expr)
	{
		ExprNode* enode = get_expr_node(expr);
		return _C_ASTNode{ new AttrNode(id, enode) };
	}

	_C_ASTNode create_block(_C_ASTNode instr, _C_ASTNode list)
	{
		InstrNode* einstr = get_instr_node(instr);
		InstrNode* elist = get_instr_node(list);
		return _C_ASTNode{ new BlockInstr(einstr, elist) };
	}
	
	const char* to_string(_C_ASTNode node)
	{
		ASTNode* enode = get_node<ASTNode>(node, ASTNodeType::Unknown);				
		return alc_copy_str(ASTNode::str(enode).c_str());
	}
	
	_C_ASTNode create_write(_C_ASTNode expr)
	{
		ExprNode* enode = get_expr_node(expr);
		return _C_ASTNode{ new WriteNode(enode) };
	}
	
	_C_ASTNode create_read(const char* id)
	{		
		return _C_ASTNode{ new ReadNode(id) };
	}
	
	
	
	
	_C_ASTNode create_writeline() { return _C_ASTNode{new WriteLineNode()}; }  
	
}


