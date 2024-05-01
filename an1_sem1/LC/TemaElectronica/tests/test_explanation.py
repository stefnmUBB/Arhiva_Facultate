"""
Tema electronica LC - Neacsu-Miclea Liviu-Stefan 215
"""
import unittest

from domain.explanation import Explanation


class TestExplanation(unittest.TestCase):
    def test_init(self):
        e = Explanation()
        assert(e.lines==[""])

    def test_write(self):
        e = Explanation()
        e.write("line1")
        assert (e.lines == ["", "line1"])
        e.write("line2")
        assert (e.lines == ["", "line1", "line2"])

    def test_str(self):
        e = Explanation()
        e.write("line1")
        e.write("line2")
        assert(e.__str__()=="\nline1\nline2")

    def test_set_result(self):
        e = Explanation()
        e.result = 2
        assert(e.result == 2)