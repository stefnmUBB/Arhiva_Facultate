import unittest

def getExceptionType(expr):
    """
    :param expr: expression to be evaluated
    :return: expression type of None if no exception caught
    """
    eType = None
    try:
        expr()
    except Exception as e:
        eType=type(e)
    return eType

def testException(exctype,expr):
    """
    tests if expression raises a certain exception
    :param exctype: the exception type
    :param expr: the expression to evaluate
    :return:
    """
    assert(getExceptionType(expr) == exctype)

if __name__ == '__main__':
    unittest.main()
