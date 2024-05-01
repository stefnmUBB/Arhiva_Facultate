#include "report_item.h"

std::ostream& operator << (std::ostream& o, const ReportItem& item)
{
	o << std::setw(15) << item.first << " " << std::setw(10) << item.second;
	return o;
}

ReportItem::ReportItem(std::string s, int i) : std::pair<std::string, int>(s, i) { }

void ReportMap::add(std::string type)
{
	if (find(type) == end())
	{
		(*this)[type].first = type;
		(*this)[type].second = 0;
	}
	(*this)[type].second++;	
}
