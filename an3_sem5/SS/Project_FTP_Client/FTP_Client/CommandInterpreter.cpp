#include "CommandInterpreter.h"

#include <exception>
#include "bufferf.h"
#include "bout.h"
#include "utils.h"

static constexpr int CMD_MAX_LENGTH = 256;

Parameter::Parameter(const char* name, const char* value_str) : name{name}, type{ParameterType::STRING}, value_str{value_str}, value_int{0}{}
Parameter::Parameter(const char* name, int value_int) : name{ name }, type{ ParameterType::STRING }, value_str{ nullptr }, value_int{ value_int }{}

void Parameter::validate_requested_type(ParameterType type) const
{
	if (this->type != type)
		throw std::exception(bout() << "Invalid parameter type for '" << this->name << "'" << bfin);
}

const char* Parameter::get_value_str() const
{
	validate_requested_type(ParameterType::STRING);
	return value_str;
}

int Parameter::get_value_int() const
{
	validate_requested_type(ParameterType::INTEGER);
	return value_int;
}

Param::Param(int id, const char* name, ParameterType type) : id{ id }, name{ name }, type { type } { }

struct CommandInterpreter::_privates_
{
	std::vector<Command> commands;

	static void validate_path(const char* word)
	{
		int dirname_len = 0;
		int folders_count = 0;

		const char* w = word;
		for (int i = 0; *w && i < CMD_MAX_LENGTH; i++, w++)
		{
			if (*w == '/')
			{
				if (dirname_len == 0 && folders_count != 0)
					throw std::exception("Invalid path name: duplicate / separators aren't allowed");
				folders_count++;
				dirname_len = 0;
			}
			else dirname_len++;
		}
		if (*w)
			throw std::exception("Path too long");
	}

	static bool try_match_token(const Token& tk, const char* word, /* ref */ Parameter*& param)
	{		

		if (tk.literal != nullptr)
		{
			//std::cout << "Matching " << word << " with " << tk.literal << "\n";
			return strncmp(tk.literal, word, CMD_MAX_LENGTH) == 0;
		}

		if (tk.param_type == ParameterType::STRING)
		{			
			*(param++) = Parameter{ tk.param_name, word };
			return true;
		}

		if (tk.param_type == ParameterType::INTEGER)
		{
			*(param++) = Parameter{ tk.param_name, Utils::my_atoi(word) };
			return true;
		}

		if (tk.param_type == ParameterType::PATH)
		{
			validate_path(word);
			*(param++) = Parameter{ tk.param_name, word };
			return true;
		}
		
		return false;
	}

	bool try_parse_command(const Command& cmd, const std::vector<char*>& words, /* out */ Parameter* pms)
	{
		int i = 0;
		Parameter* iter_pms = pms;
		for (const auto& tk : cmd.tokens)
		{
			if (i == words.size())
				return false;
			if (!try_match_token(tk, words[i++], iter_pms))
				return false;
		}
		if (i < words.size())
			return false;

		return true;
	}

	bool try_execute(const std::vector<char*>& words)
	{		
		Parameter pms[10];
		for (const auto& cmd : commands)
		{
			if (try_parse_command(cmd, words, pms))
			{
				cmd.action(pms);
				return true;
			}
		}
		return false;
	}

};

CommandInterpreter::CommandInterpreter()
{
	privates = new _privates_();


}

void CommandInterpreter::add_command(const Command& cmd) { privates->commands.push_back(cmd); }

void CommandInterpreter::print_commands(std::ostream& o)
{
	for (const auto& cmd : privates->commands)
	{
		for (const auto& tk : cmd.tokens)
		{
			if (tk.literal != nullptr)
				o << tk.literal << " ";
			else
				o << "<" << tk.param_id << "=" << tk.param_name << ":" << param_type_to_str(tk.param_type) << "> ";
		}
		o << "\n";
	}
	o << "\n";
}

namespace
{
	bool is_valid_character(char c)
	{
		if ('a' <= c && c <= 'z') return true;
		if ('A' <= c && c <= 'Z') return true;
		if ('0' <= c && c <= '9') return true;
		if (c == '_') return true;
		if (c == ' ') return true;
		if (c == '/') return true;		
		if (c == '.') return true;		
		return false;
	}
}

void CommandInterpreter::execute(const char* cmd)
{
	char word[CMD_MAX_LENGTH] = { 0 };
	char* iw = word;

	std::vector<char*> words;

	for (int i = 0; i < CMD_MAX_LENGTH && *cmd; cmd++, i++)
	{
		if (!is_valid_character(*cmd))
			throw std::exception(bout() << "Invalid character: '" << *cmd << "'" << bfin);

		if (*cmd == ' ')
		{
			if (iw == word) continue;
			int wlen = (int)(iw - word);
			char* found_word = new char[wlen + 1];
			memcpy(found_word, word, wlen);
			found_word[wlen] = '\0';
			words.push_back(found_word);
			iw = word;
		}
		else
		{
			*(iw++) = *cmd;
		}
	}

	if (iw != word)
	{
		int wlen = (int)(iw - word);
		char* found_word = new char[wlen + 1];
		memcpy(found_word, word, wlen);
		found_word[wlen] = '\0';
		words.push_back(found_word);
	}

	if (*cmd)
	{
		throw std::exception("Failed to parse command: input too long");
	}

	//for (auto w : words) std::cout << w << " "; std::cout << "\n";
	if (words.size() == 0) return;

	if (!privates->try_execute(words))
	{
		throw std::exception("Wrong command");
	}
}


CommandInterpreter::~CommandInterpreter()
{
	delete privates;
}