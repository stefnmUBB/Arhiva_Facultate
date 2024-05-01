#include "test_car.h"
#include "../Domain/car.h"
#include <cassert>

void test_car_eq()
{
	Car c1(NumberPlate("BZ01PJN"), "Dacia", "1310", "-");
	Car c2(NumberPlate("BZ01PJN"), "Dacia", "1310", "-");

	assert(c1 == c2);
}

void test_get_number_plate()
{
	Car c(NumberPlate("BZ01PJN"), "Dacia", "1310", "-");
	assert(c.get_number_plate().get_value() == "BZ01PJN");
}

void test_get_producer()
{
	Car c(NumberPlate("BZ01PJN"), "Dacia", "1310", "-");
	assert(c.get_producer() == "Dacia");
}

void test_get_model()
{
	Car c(NumberPlate("BZ01PJN"), "Dacia", "1310", "-");
	assert(c.get_model() == "1310");
}

void test_get_type()
{
	Car c(NumberPlate("BZ01PJN"), "Dacia", "1310", "-");
	assert(c.get_type() == "-");
}