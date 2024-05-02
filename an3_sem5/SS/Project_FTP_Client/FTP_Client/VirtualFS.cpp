#include "VirtualFS.h"

#include <fstream>
#include <iostream>

namespace fs = std::filesystem;

namespace
{
	fs::path get_absolute_path(fs::path root, fs::path relative)
	{
		if (relative.string()._Starts_with("/"))
			relative = fs::path("." + relative.string());
		return root / relative;
	}
}

VirtualFS::VirtualFS(std::filesystem::path root) : root{ root }
{
	if (!fs::exists(root))
		fs::create_directory(root);
}

std::vector<char> VirtualFS::read(std::filesystem::path relative_path)
{
	fs::path path = get_absolute_path(root, relative_path);
	std::ifstream f(path, std::ios::binary);

	std::cout << "Reading path: " << path << "\n";
	if (f.fail())
	{
		throw std::exception((std::string("File not found: ") + path.string()).c_str());
	}

	f.seekg(0, std::ios::end);
	size_t length = f.tellg();
	f.seekg(0, std::ios::beg);
	std::cout << "File size : " << length << "\n";
	std::vector<char> buffer(length);
	f.read(buffer.data(), length);	

	if (f.fail())
		throw std::exception("File reading failed");

	f.close();	

	return buffer;
}

void VirtualFS::write(std::filesystem::path relative_path, std::vector<char> buffer)
{
	fs::path path = get_absolute_path(root, relative_path);
	std::cout << "Writing path: " << path << "\n";	
	std::ofstream f(path, std::ios::binary);

	if (f.fail())
	{
		throw std::exception((std::string("Unable to write file: ") + path.string()).c_str());
	}

	f.write(buffer.data(), buffer.size());

	if (f.fail())
		throw std::exception("File writing failed");

	f.close();	
}