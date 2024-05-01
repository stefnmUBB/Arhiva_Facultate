import unittest
from unittest import TestCase

from tests import expect_exception
from utils.date import Date


class TestDate(TestCase):
    def test_create(self):
        date = Date.from_str("01.02.2022")
        assert(date.year==2022)
        assert(date.month==2)
        assert(date.day==1)

    def test_str(self):
        date = Date(1,2,2022)
        assert(date.__str__()=="01.02.2022")

    def test_get_sort_key(self):
        date = Date(1, 2, 2022)
        assert(date.get_sort_key()==20220201)

    def test_date_validator(self):
        expect_exception(ValueError, lambda: Date(1,13,2000))
        expect_exception(ValueError, lambda: Date(32,12,2000))