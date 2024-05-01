"""
Tema electronica LC - Neacsu-Miclea Liviu-Stefan 215
"""
import unittest

from domain.converter import Converter
from domain.number import Number
from tests import testException


class TestConverter(unittest.TestCase):
    def test_by_substitution(self):
        n = Number("1230231",4)
        e = Converter.by_substitution(n,8)
        assert(len(e.lines)==8)
        assert(e.result=="15455(8)")

        n = Number("1011001", 2)
        e = Converter.by_substitution(n,16)
        assert (e.result == "59(16)")

        n = Number("59", 16)
        e = Converter.by_substitution(n, 2)
        assert (e.result == "1011001(2)")

        testException(PermissionError,lambda:Converter.by_substitution(Number("123",5),2))
        testException(PermissionError,lambda:Converter.by_substitution(Number("123",4),7))
        testException(ValueError,lambda:Converter.by_substitution(Number("123",8),8))

    def test_by_base_10(self):
        n = Number("1230231", 4)
        e = Converter.by_base_10(n, 8)
        assert (e.result == "15455(8)")

        n = Number("1011001", 2)
        e = Converter.by_base_10(n, 16)
        assert (e.result == "59(16)")

        n = Number("59", 16)
        e = Converter.by_base_10(n, 2)
        assert (e.result == "1011001(2)")

        n = Number("255", 10)
        e = Converter.by_base_10(n, 16)
        assert (e.result == "FF(16)")

    def test_by_succesive_division(self):
        n = Number("1230231", 4)
        e = Converter.by_successive_division(n, 8)
        assert (e.result == "15455(8)")

        n = Number("1011001", 2)
        e = Converter.by_successive_division(n, 16)
        assert (e.result == "59(16)")

        n = Number("59", 16)
        e = Converter.by_successive_division(n, 2)
        assert (e.result == "1011001(2)")

        n = Number("255", 10)
        e = Converter.by_successive_division(n, 16)
        assert (e.result == "FF(16)")