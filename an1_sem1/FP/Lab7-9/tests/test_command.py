import unittest

from service.command import Command
from service.parse_error import ParseError
from tests import expect_exception


class TestCommand(unittest.TestCase):
    def test_parse(self):
        cmd = Command(lambda l: print(l), "Add movie %s from year %i", "")
        lst = cmd._parse('Add movie Paprika from year 2006')
        assert(lst==['Paprika',2006])

        lst = cmd._parse('Add movie "The girl who leapt through time" from year 2006')
        assert (lst == ['The girl who leapt through time', 2006])

        lst = cmd._parse('Add book whatever from year 2006')
        assert (lst == None)

        lst = cmd._parse('Totally not that command')
        assert (lst == None)

    def test_call(self):
        result = 0
        def add(a,b):
            nonlocal result
            result=a+b

        cmd = Command(add, "%i + %i =", "")
        cmd.call("2 + 3 =")
        assert(result==5)
        expect_exception(ParseError,lambda: cmd.call(" + 3 ="))

    def test_try_call(self):
        result = 0
        def add(a, b):
            nonlocal result
            result = a + b

        cmd = Command(add, "%i + %i =", "")
        assert (cmd.try_call("2 + 3 =")== True)
        assert (cmd.try_call("m + 3 =")== False)
        assert (cmd.try_call(" - 3 =")== False)
