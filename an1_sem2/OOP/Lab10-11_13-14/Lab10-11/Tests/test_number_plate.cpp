#include "test_number_plate.h"

#include "../Domain/number_plate.h"
#include <cassert>

void test_number_plate_create()
{
	NumberPlate np;
	assert(np.get_value() == "");

	np = NumberPlate("BZ01PJN");
	assert(np.get_value() == "BZ01PJN");
}

void test_number_plate_equals()
{
	NumberPlate np1("BZ01PJN");
	NumberPlate np2("BZ01PJN");

	assert(np1 == np2);

	np2 = NumberPlate("BZ29YKA");

	assert(np1 != np2);
}

void test_number_plate_valid()
{
	NumberPlate np = NumberPlate("BZ29YKA");

	np.check_valid();

	try { np = NumberPlate("B"); np.check_valid(); assert(false); } catch (number_plate_exception) { assert(true); }

	try { np = NumberPlate("C123ABC"); np.check_valid(); assert(false); } catch (number_plate_exception) { assert(true); }

	try { np = NumberPlate("B231BC"); np.check_valid(); assert(false); } catch (number_plate_exception) { assert(true); }

	try { np = NumberPlate("C23ABC"); np.check_valid(); assert(false); } catch (number_plate_exception) { assert(true); }

	try { np = NumberPlate("BZ231BC"); np.check_valid(); assert(false); } catch (number_plate_exception) { assert(true); }

}