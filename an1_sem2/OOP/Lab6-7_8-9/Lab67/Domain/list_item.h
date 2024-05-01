#pragma once

#include "../Repo/list.h"

/// <summary>
/// Generic list item class
/// </summary>
class ListItem
{	
private:
	int id = -1;
public:	
	/// <returns>Gets item unique id</returns>
	int get_id() const;

	/// <summary>
	/// Sets item id
	/// </summary>
	/// <param name="_id">New item id</param>
	void set_id(int _id);
};

