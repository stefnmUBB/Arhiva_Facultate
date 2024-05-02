#pragma once

#include<vector>
#include<functional>
#include<iostream>

enum class ParameterType
{
	NONE, 
	STRING,
	INTEGER,
	PATH
};

inline static constexpr const char* param_type_to_str(const ParameterType& type)
{
	switch (type)
	{
	case ParameterType::NONE: return "NONE";
	case ParameterType::STRING: return "STRING";
	case ParameterType::INTEGER: return "INTEGER";
	case ParameterType::PATH: return "PATH";
	default: return "ERROR";
	}
}

class Parameter
{
private:
	const char* name = nullptr;
	ParameterType type = ParameterType::NONE;	
	const char* value_str = nullptr;
	int value_int = 0;

	void validate_requested_type(ParameterType type) const;
public:
	Parameter() = default;
	Parameter(const char* name, const char* value_str);
	Parameter(const char* name, int value_int);

	const char* get_value_str() const;
	int get_value_int() const;


};

struct Param 
{	
	int id = 0;
	const char* name;
	ParameterType type = ParameterType::STRING;
	Param(int id, const char* name, ParameterType type = ParameterType::STRING);
};

class CommandInterpreter
{
private:
	struct _privates_;
	_privates_* privates;	

	struct Token
	{
		const char* literal = nullptr;
		int param_id;
		const char* param_name;
		ParameterType param_type;		
	};

	struct CommandBuilder
	{			
		std::vector<Token> tokens;	
		void add_token(const char* tk) { tokens.push_back(Token{ tk, 0, nullptr, ParameterType::NONE }); }
		void add_token(const Param& param) { tokens.push_back(Token{ nullptr, param.id, param.name, param.type }); }
	};

	template<typename... Tokens>
	void build_command(CommandBuilder* cb, const char* tk, Tokens... tokens)
	{
		cb->add_token(tk);
		build_command(cb, tokens...);
	}

	template<typename... Tokens>
	void build_command(CommandBuilder* cb, const Param& pm, Tokens... tokens)
	{
		cb->add_token(pm);
		build_command(cb, tokens...);
	}

	void build_command(CommandBuilder* cb, const char* tk) { cb->add_token(tk); }
	void build_command(CommandBuilder* cb, const Param& pm) { cb->add_token(pm); }


	struct Command
	{
		std::function<void(const Parameter*)> action;
		std::vector<Token> tokens;
	};

	void add_command(const Command& cmd);

public:
	CommandInterpreter();

	template<typename... Tokens>
	void register_command(std::function<void(const Parameter*)> action, Tokens... tokens)
	{
		CommandBuilder cb;
		build_command(&cb, tokens...);
		add_command(Command{ action, std::move(cb.tokens) });
	}

	void print_commands(std::ostream& o);

	void execute(const char* cmd);

	~CommandInterpreter();	
};