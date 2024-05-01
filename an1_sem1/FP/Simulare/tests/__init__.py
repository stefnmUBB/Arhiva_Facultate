import unittest

def get_exception_type(func):
    """
    :param func: function to evaluate
    :return: exception raised or None if no exception
    """
    exc_type = None
    try:
        func()
    except Exception as e:
        exc_type = type(e)
    return exc_type

def expect_exception(exc_type,func):
    """
    checks if func() raises exception of specified type
    :param exc_type: excpected exception
    :param func: function to evaluate
    """
    assert(get_exception_type(func)==exc_type)

if __name__=="__main__":
    unittest.main()

