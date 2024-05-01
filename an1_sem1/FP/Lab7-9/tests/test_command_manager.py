import unittest

from service.command import Command
from service.command_manager import CommandManager
from tests import expect_exception


class TestCommandManager(unittest.TestCase):
    result = 0

    @staticmethod
    def add(a,b): TestCommandManager.result = a + b

    @staticmethod
    def sub(a, b): TestCommandManager.result = a - b

    @staticmethod
    def mul(a, b): TestCommandManager.result = a * b

    @staticmethod
    def div(a, b): TestCommandManager.result = a // b

    def test_add_command(self):

        cmdmgr = CommandManager()
        cmdmgr.add_command(Command(TestCommandManager.add,"%i + %i = ","addition"))
        cmdmgr.add_command(Command(TestCommandManager.sub,"%i - %i = ","subtraction"))
        cmdmgr.add_command(Command(TestCommandManager.mul,"%i * %i = ","multiplication"))
        cmdmgr.add_command(Command(TestCommandManager.mul,"%i - %i = ","division"))

        assert(len(cmdmgr.get_commands())==4)

    def test_execute(self):
        cmdmgr = CommandManager()
        cmdmgr.add_command(Command(TestCommandManager.add, "%i + %i = ", "addition"))
        cmdmgr.add_command(Command(TestCommandManager.sub, "%i - %i = ", "subtraction"))
        cmdmgr.add_command(Command(TestCommandManager.mul, "%i * %i = ", "multiplication"))
        cmdmgr.add_command(Command(TestCommandManager.div, "%i / %i = ", "division"))

        cmdmgr.execute("10 + 12 =")
        assert(TestCommandManager.result==22)

        cmdmgr.execute("6 - 2 =")
        assert (TestCommandManager.result == 4)

        cmdmgr.execute("7 * 8 =")
        assert (TestCommandManager.result == 56)

        cmdmgr.execute("100 / 5 =")
        assert (TestCommandManager.result == 20)

        expect_exception(ValueError,lambda:cmdmgr.execute("sqrt(100) ="))