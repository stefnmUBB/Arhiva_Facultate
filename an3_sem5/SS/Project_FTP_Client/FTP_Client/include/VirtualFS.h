#pragma once

#include <string>
#include <filesystem>
#include <vector>

class VirtualFS
{
private:
	std::filesystem::path root;
public:
	VirtualFS(std::filesystem::path root);
	std::vector<char> read(std::filesystem::path relative_path);
	void write(std::filesystem::path relative_path, std::vector<char> buffer);	
};