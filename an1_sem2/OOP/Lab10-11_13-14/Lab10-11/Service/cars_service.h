#pragma once

#include "../Domain/car.h"
#include "../Repo/list_storage.h"
#include "undo.h"

#include<string>
#include<functional>
#include<utility>
#include <map>
#include <stack>
#include "wash_list.h"

typedef ParseItemFunc<const Car&> ParseCarFunc;

/// <summary>
/// Car filter function type
/// </summary>
typedef function<bool(const Car&)> CarFilterFunc;

/// <summary>
/// Car comparator function type
/// </summary>
typedef function<bool(const Car&, const Car&)> CarCompFunc;

class CarsService
{
private:
	ListStorage<Car> cars_list;

	stack<unique_ptr<UndoAction>> undo_list;	
public:
	void debug_populate_random(size_t cnt);	

	ReportMap report_types();

	CarsService(string fname = "cars.txt");


	/// <summary>
	/// adds a new car to the list, giving it a new id
	/// </summary>
    /// <param name="plate">number plate</param>
	/// <param name="producer">producer</param>
	/// <param name="model">model</param>
	/// <param name="type">type</param>
	void add_car(string plate, string producer, string model, string type);

	/// <summary>
	/// removes a car from the list
	/// </summary>
	/// <param name="id">car id</param>
	/// <returns>true if the operation succeeded, false otherwise</returns>
	bool remove_car(int id);

	/// <summary>
	/// edits a car in list
	/// </summary>
	/// <param name="id">car id</param>
	/// <param name="plate">number plate</param>
	/// <param name="producer">producer</param>
	/// <param name="model">model</param>
	/// <param name="type">type</param>
	/// <throws>id_not_found_exception if id is not found</throws> 	
	void edit_car(int id, string plate, string producer, string model, string type);
	
	/// <summary>
	/// parses the list in order of ids and executes certain action for each car
	/// </summary>
	/// <param name="do_with_car">action to execute for each car</param>
	void for_each(ParseCarFunc do_with_car);
	
	/// <summary>
	/// returns number of cars in the list
	/// </summary>
	size_t get_cars_count() const noexcept;
	
	/// <summary>
	/// returns car having a certain id
	/// </summary>
	/// <throws>id_not_found_exception if id is not found</throws> 	
	const Car& get_car_by_id(int id) const;

	/// <summary>
	/// filters cars based on a certain property.
	/// For each filtered car an action is executed
	/// </summary>
	void filter(const CarFilterFunc& filter_func, const ParseCarFunc& do_with_car);

	/// <summary>
	/// sorts cars based on a certain property, then for each car in order 
	/// an action is executed
	/// </summary>
	void sort(const CarCompFunc& comp_func, const ParseCarFunc& do_with_car);

	/// <summary>
	/// Finds a car with given properties. Write "*" in  field that is excluded from search	
	/// </summary>
	const Car& find_car(string number_plate, string producer, string model, string type);

	/// <summary>
	/// built-in cars compare method by number plate
	/// </summary>
	static bool compare_by_number_plate(const Car& c1, const Car& c2);

	/// <summary>
	/// built-in cars compare method by type
	/// </summary>
	static bool compare_by_type(const Car& c1, const Car& c2) noexcept;

	/// <summary>
	/// built-in cars compare method by prod + model
	/// </summary>
	static bool compare_by_prod_model(const Car& c1, const Car& c2) noexcept;

	void validate(const Car& c, const string& scope) const;

	void undo();	

	vector<Car> get_wash_list();

	~CarsService();

	inline List<Car>& get_repo() { return cars_list; }
};

class id_not_found_exception :public exception 
{
public:
	id_not_found_exception(const char* msg) noexcept : exception(msg) {}
};

class car_not_found_exception :public exception
{
public:
	car_not_found_exception(const char* msg) noexcept : exception(msg)  {}
};

class UndoAdd : public UndoAction
{
private:
	List<Car>& cars_list;
	Car target_car;
public:
	UndoAdd(List<Car>& cars_list, const Car& target_car);
	void do_undo() override;
};

class UndoEdit : public UndoAction
{
private:
	List<Car>& cars_list;
	Car target_car;
public:
	UndoEdit(List<Car>& cars_list, const Car& target_car);
	void do_undo() override;
};

class UndoRemove : public UndoAction
{
private:
	List<Car>& cars_list;
	Car target_car;
public:
	UndoRemove(List<Car>& cars_list, const Car& target_car);
	void do_undo() override;
};