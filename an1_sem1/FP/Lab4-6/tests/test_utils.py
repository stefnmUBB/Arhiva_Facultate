# The following function are used only for testing purposes.

def cpReal(a, b):
    """
    utility function to compare 2 real values
    :param a: first number to compare
    :param b: second number to compare
    :return: True if a==b
    """
    return abs(a - b) < 0.00001

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
