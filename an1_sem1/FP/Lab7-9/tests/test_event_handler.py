import unittest

from utils.event_handler import EventHandler


class TestEventHandler(unittest.TestCase):
    result = 0

    @staticmethod
    def add1():
        TestEventHandler.result += 1

    @staticmethod
    def add2():
        TestEventHandler.result += 2

    def test_add_event(self):
        e = EventHandler()
        e.add_event(TestEventHandler.add1)
        assert(len(e.callback_list)==1)
        e+=TestEventHandler.add2
        assert (len(e.callback_list) == 2)

    def test_remove_event(self):
        e = EventHandler()
        e+=TestEventHandler.add1
        e += TestEventHandler.add2

        e-=TestEventHandler.add1
        assert (len(e.callback_list) == 1)

        e -= TestEventHandler.add2
        assert (len(e.callback_list) == 0)

    def test_invoke(self):
        TestEventHandler.result = 0
        e = EventHandler()
        e.invoke()
        e += TestEventHandler.add1
        e += TestEventHandler.add2
        e.invoke()
        assert(TestEventHandler.result==3)