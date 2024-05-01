#pragma once

#include <map>
#include <string>
#include <utility>
#include <iostream>
#include <iomanip>

class ReportItem : public std::pair<std::string,int>
{
	friend std::ostream& operator << (std::ostream& o, const ReportItem& item);
public:
	ReportItem(std::string s="", int i=0);
};

class ReportMap : public std::map<std::string, ReportItem>
{
public:
	void add(std::string type);
};