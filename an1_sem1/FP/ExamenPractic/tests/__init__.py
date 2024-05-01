import unittest

def expect_exception(etype,f):
    err = None
    try:
        f()
    except Exception as e:
        err = type(e)
    assert(etype==err)

if __name__=="__main__":
    unittest.main()