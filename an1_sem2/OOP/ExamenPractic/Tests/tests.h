#pragma once

#include "test_melody.h"
#include "test_repo.h"
#include "test_service.h"

inline void test_all()
{
	test_melody();
	test_repo();
	test_service();
}