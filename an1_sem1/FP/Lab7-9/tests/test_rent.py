import unittest
from datetime import date, datetime

from domain.rent import Rent
from tests import expect_exception


class TestRent(unittest.TestCase):
    def test_get_id(self):
        rent=Rent(0,1,5,"21-05-11")
        assert(rent.get_id()==0)

    def test_get_client_id(self):
        rent=Rent(0,1,5,"21-05-11")
        assert(rent.get_client_id()==1)

    def test_get_movie_id(self):
        rent=Rent(0,1,5,"21-05-11")
        assert(rent.get_movie_id()==5)

    def test_get_rent_date(self):
        rent=Rent(0,1,5,"21-05-11")
        assert(rent.get_rent_date().strftime("%y-%m-%d")=="21-05-11")

    def test_get_return_date(self):
        rent = Rent(0, 1, 5, "21-05-11")
        assert (rent.get_return_date() is None)

    def test_set_id(self):
        rent = Rent(0, 1, 5, "21-05-11")
        rent._set_id(4)
        assert (rent.get_id() == 4)

    def test_set_client_id(self):
        rent = Rent(0, 1, 5, "21-05-11")
        rent.set_client_id(4)
        assert (rent.get_client_id() == 4)

    def test_set_movie_id(self):
        rent = Rent(0, 1, 5, "21-05-11")
        rent.set_movie_id(4)
        assert (rent.get_movie_id() == 4)

    def test_set_rent_date(self):
        rent = Rent(0, 1, 5, "21-05-11")
        rent.set_rent_date("21-12-12")
        assert (rent.get_rent_date().strftime("%y-%m-%d") == "21-12-12")

        rent.set_rent_date(date(2021,10,1))
        assert (rent.get_rent_date().strftime("%y-%m-%d") == "21-10-01")

        now = datetime.now()
        rent.set_rent_date(now)
        assert (rent.get_rent_date().strftime("%y-%m-%d") == now.strftime("%y-%m-%d"))

        expect_exception(TypeError,lambda: rent.set_rent_date(20128))

    def test_set_return_date(self):
        rent = Rent(0, 1, 5, "21-05-11")
        rent.set_return_date("21-12-12")
        assert (rent.get_return_date().strftime("%y-%m-%d") == "21-12-12")

        rent.set_return_date(date(2021, 10, 1))
        assert (rent.get_return_date().strftime("%y-%m-%d") == "21-10-01")

        now = datetime.now()
        rent.set_return_date(now)
        assert (rent.get_return_date().strftime("%y-%m-%d") == now.strftime("%y-%m-%d"))

        expect_exception(TypeError, lambda: rent.set_return_date(20128))

        expect_exception(ValueError,lambda: rent.set_return_date("21-60-59"))

    def test_is_returned(self):
        rent = Rent(0, 1, 5, "21-05-11")
        assert(not rent.is_returned())
        rent.set_return_date("21-12-12")
        assert (rent.is_returned())

    def test_clone(self):
        rent = Rent(0, 1, 5, "21-05-11")
        clone = rent.clone()
        assert(rent.get_id()==clone.get_id())
        assert(rent.get_client_id()==clone.get_client_id())
        assert(rent.get_movie_id()==clone.get_movie_id())
        assert(rent.get_rent_date().strftime("%y-%m-%d")==clone.get_rent_date().strftime("%y-%m-%d"))

    def test_assimilate_properties(self):
        rent1 = Rent(0, 1, 5, "21-05-11")
        rent2 = Rent(1)
        rent2.assimilate_properties(rent1)
        assert (rent1.get_id() != rent2.get_id())
        assert (rent1.get_client_id() == rent2.get_client_id())
        assert (rent1.get_movie_id() == rent2.get_movie_id())
        assert ((rent1.get_rent_date() - rent2.get_rent_date()).seconds==0)
