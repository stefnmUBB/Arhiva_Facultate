#pragma once
#include "../Domain/list_item.h"
#include "../Repo/list.h"
#include "../Repo/list_storage.h"

class TestStorageListItem : public ListItem
{
private:
	int value;
public:	
	TestStorageListItem(int _id = -1) { set_id(_id); set_value(0); }
	inline void set_value(int _value) noexcept { value = _value; }
	inline int get_value() const noexcept { return value; }

	friend std::istream& operator >> (std::istream& i, TestStorageListItem& item)
	{
		int id, value;
		i >> id >> value;
		item.set_id(id);
		item.set_value(value);
		return i;
	}

	friend std::ostream& operator << (std::ostream& o, const TestStorageListItem& item)
	{
		o << item.get_id() << ' ' << item.get_value() << ' ';
		return o;
	}

};

//std::istream& operator >> (std::istream& i, TestStorageListItem& item)

//std::ostream& operator << (std::ostream& o, const TestStorageListItem& item)


using TestStorageList = ListStorage<TestStorageListItem>;

void test_list_storage_create();
