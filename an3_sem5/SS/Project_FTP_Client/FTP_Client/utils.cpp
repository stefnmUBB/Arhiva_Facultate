#include "utils.h"
#include "bout.h"

std::ostream& Utils::operator << (std::ostream& o, const Utils::Color& color)
{	
	if (o.rdbuf() == std::cout.rdbuf())				
		SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), color.code);
	return o;
}

// returns the address of the first occurence of c in buff, otherwise exception
const char* Utils::my_strnchr(const char* buff, int len, char c)
{
	for (int i = 0; i < len && *buff && *buff != c; i++, buff++);
	if (*buff == c) return buff;
	throw std::exception(bout() << "Failed to find character: '" << c << "'" << bfin);
}

int Utils::my_atoi(const char* input)
{
	constexpr int MAX_INPUT_LEN = 10;
	long long result = 0;
	int sgn = 1;

	for (int i = 0; i < MAX_INPUT_LEN && *input; i++, input++)
	{
		if (i == 0 && *input == '-')
		{
			sgn = -1;
			continue;
		}
		if ('0' <= *input && *input <= '9')
		{
			result = result * 10 + (*input - '0');
			continue;
		}
		throw std::exception(bout() << "Failed to parse integer: invalid character '" << *input << "'" << bfin);
	}
	if (*input)
		throw std::exception("Failed to parse integer: input length exceeded");

	result *= sgn;
	if (result >= INT_MAX || result <= INT_MIN)
		throw std::exception(bout() << "Argument out of range: " << result << bfin);
	return (int)result;
}

