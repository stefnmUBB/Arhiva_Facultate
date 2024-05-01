#pragma once

typedef enum
{
	OP_QUANTITY_UPDATED = 1 << 1,
	OP_OK = 1,
	OP_INCONSISTENT_PRICE = 1 << 2,
	OP_PRODUCT_NOT_FOUND = 1 << 3,
	OP_ID_PRODUCT_ALREADY_EXISTS = 1 << 4
} op_result;

/*const char* error_msg[] =
{
	"Ok",
	"Same product cannot have different prices",
	"The specified product doesn't exist"
};*/