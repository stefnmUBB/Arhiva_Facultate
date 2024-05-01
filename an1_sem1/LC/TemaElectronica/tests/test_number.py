"""
Tema electronica LC - Neacsu-Miclea Liviu-Stefan 215
"""
import unittest
from domain.number import Number
from tests import testException


class TestNumber(unittest.TestCase):
    def test_create(self):
        n = Number()
        assert(n.get_base() == 10)
        assert(n.get_value() == [0])

        n = Number(base = 5)
        assert (n.get_base() == 5)
        assert (n.get_value() == [0])

        n = Number([1,0], 2)
        assert (n.get_base() == 2)
        assert(n.get_value() == [1])

    def test_set_value(self):

        # type(value) == int
        n = Number(base = 10)
        n.set_value(127)
        assert(n.get_value() == [7,2,1])

        n = Number(base = 5)
        n.set_value(12340)
        assert (n.get_value() == [0,4,3,2,1])

        n = Number(base = 2)
        testException(ValueError, lambda: n.set_value(2345))
        testException(ValueError, lambda: n.set_value(-10101))

        # type(value) == list
        n = Number(base = 10)
        val = [9, 8, 7]
        n.set_value(val)
        assert(n.get_value()==val)

        n.set_value([3,2,1,0,0,0])
        assert (n.get_value() == [3,2,1])

        n.set_value([])
        assert(n.get_value()==[0])

        testException(TypeError, lambda: n.set_value([1,"0",2]))
        testException(ValueError, lambda: n.set_value([10]))

        # type(value) == string
        n = Number(base = 7)
        n.set_value("001234560")
        assert(n.get_value()==[0,6,5,4,3,2,1])

        n = Number(base = 16)
        n.set_value("123FF")
        assert (n.get_value() == [15,15,3,2,1])

        testException(ValueError, lambda:n.set_value("1G"))

        # other type
        testException(TypeError, lambda:Number((1,2)))
        testException(TypeError, lambda:Number(None))

    def test_ge(self):
        a = Number("108", 16)
        b = Number("28", 16)
        assert(a>=b)
        assert(not(b>=a))

        a = Number("11", 14)
        b = Number("23", 14)
        assert (b>=a)

        a = Number("67", 10)
        b = Number("19", 10)
        assert (a>=b)

    def test_add(self):
        a = Number("108",16)
        b = Number("28",16)
        assert((a+b) == "130(16)")

    def test_sub(self):
        a = Number("108", 16)
        b = Number("28", 16)
        assert ((a - b) == "E0(16)")

        a = Number("67", 10)
        b = Number("19", 10)
        assert ((a - b) == "48(10)")

        a = Number(5,8)
        b = Number(10,8)
        testException(ValueError, lambda: a-b)

        b = Number(4, 7)
        testException(ValueError,lambda: a-b)

    def test_validate_mul(self):
        a = Number("108",16)
        assert(a.validate_mul(5)==5)
        assert(a.validate_mul("A")==10)
        testException(ValueError,lambda: a.validate_mul("J"))

    def test_mul(self):
        a = Number("10", 16)
        assert(a*'B'=="B0(16)")
        a = Number(19,10)
        assert(a*9=="171(10)")
        a = Number("10", 16)
        testException(ValueError,lambda: a*'W')

    def test_floordiv(self):
        a = Number(81, 16)
        assert(a // 2 == "40(16)")
        a = Number(148, 10)
        assert(a // 9 == "16(10)")
        testException(ValueError, lambda: a // 'A')
        a = Number(7, 12)
        assert (a // 8 == "0(12)")
        assert (Number("2C",16) // 2 == "16(16)")

    def test_mod(self):
        a = Number(81, 16)
        assert(a % 2 == "1(16)")
        a = Number(148, 10)
        assert(a % 9 == "4(10)")
        testException(ValueError, lambda: a % 'A')
        a = Number(7, 12)
        assert (a % 8 == "7(12)")

    def test_shl(self):
        a = Number(12,5)
        assert(a.shl()=="120(5)")
        a = Number(0, 5)
        assert (a.shl() == "0(5)")

    def test_divmod(self):
        a = Number(133,10)
        b = Number(11,10)
        assert(Number.divmod(a,b)==("12(10)","1(10)"))
        a=Number("2C", 16)
        b=Number("2", 16)
        assert(Number.divmod(a,b)==("16(16)","0(16)"))



