import unittest
from datetime import date, datetime

from domain.rent import Rent
from domain.rent_validator import RentValidator
from tests import expect_exception


class TestRent(unittest.TestCase):
    def test_validate_rent(self):
        rent = Rent(0, 1, 5, "21-05-11")
        RentValidator.validate_rent(rent)
        rent.rent_date="clearly not a date"
        expect_exception(TypeError,lambda: RentValidator.validate_rent(rent))
        rent = Rent(0, 1, 5, "21-05-11")
        rent.return_date="11-11-11" # also not a date type
        expect_exception(TypeError, lambda: RentValidator.validate_rent(rent))
