import unittest

from service.report import Report
from tests import expect_exception


class TestRentsList(unittest.TestCase):
    def test_init(self):
        report=Report([1,2,3,4,5])
        assert(len(report.elem_list) == 5)

    def test_select(self):
        report = Report([4,1,5,3,2]) \
            .select(lambda x:x<=3,lambda x:x,True)
        assert (report.elem_list == [3, 2, 1])

    def test_first(self):
        report = Report([10,9,8,7,6,5,4,3,2,1]).first(4)
        assert (report.elem_list == [10, 9, 8, 7])

        report = Report([10, 9, 8, 7, 6, 5, 4, 3, 2, 1]).first(21)
        assert (report.elem_list == [10,9,8,7,6,5,4,3,2,1])

        expect_exception(ValueError,lambda: Report([1,2,3,4]).first(-1))

        report = Report([10, 9, 8, 7, 6, 5, 4, 3, 2, 1]).first(0)
        assert(report.elem_list==[])

    def test_first_blackbox(self):
        report = Report([10, 9, 8, 7, 6, 5, 4, 3, 2, 1]).first(4)
        assert (report.elem_list == [10, 9, 8, 7])

        expect_exception(ValueError, lambda: Report([1, 2, 3, 4]).first(-1))

    def test_first_rec(self):
        report = Report([10,9,8,7,6,5,4,3,2,1]).first_rec(4)
        assert (report.elem_list == [10, 9, 8, 7])

    def test_first_percent(self):
        report = Report([10,9,8,7,6,5,4,3,2,1]).first_percent(50)
        assert (report.elem_list == [10, 9, 8, 7, 6])
