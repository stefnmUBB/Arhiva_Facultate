import unittest

def get_exception_type(expr):
    """
    :param expr: expression to be evaluated
    :return: expression type of None if no exception caught
    """
    e_type = None
    try:
        expr()
    except Exception as e:
        e_type=type(e)
    return e_type

def expect_exception(e_type, func):
    assert(get_exception_type(func) == e_type)

if __name__=='main':
    unittest.main()