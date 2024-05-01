import unittest
from datetime import date, datetime

from domain.rent import Rent
from repository.rents_list_memo import RentsListMemo
from tests import expect_exception


class TestRentsList(unittest.TestCase):
    def test_get_rents(self):
        rl = RentsListMemo()
        rl.add_rent(Rent(0,1,1,"21-01-01"))
        rl.add_rent(Rent(1,1,2,"22-01-01"))
        assert(len(rl.get_rents())==2)

    def test_set_rents_list(self):
        rl = RentsListMemo()
        rents = [ Rent(0, 1, 1, "21-01-01"),  Rent(1,1,2,"22-01-01") ]
        rl._set_rents_list(rents)
        assert(len(rl.get_rents())==2)

    def test_is_empty(self):
        rl = RentsListMemo()
        assert(rl.is_empty())
        rl.add_rent(Rent(0, 1, 1, "21-01-01"))
        rl.add_rent(Rent(1, 1, 2, "22-01-01"))
        assert(not rl.is_empty())

    def test_add_rent(self):
        rl = RentsListMemo()
        rl.add_rent(Rent(0, 1, 2, "21-01-01"))
        rent=rl.get_rents()[0]
        assert(rent.get_id()==0)
        assert(rent.get_client_id()==1)
        assert(rent.get_movie_id()==2)
        assert(rent.get_rent_date().strftime("%y-%m-%d")=="21-01-01")

    def test_clear(self):
        rl = RentsListMemo()
        rl.add_rent(Rent(0, 1, 1, "21-01-01"))
        rl.add_rent(Rent(1, 1, 2, "22-01-01"))
        rl.clear()
        assert (len(rl.get_rents()) == 0)

    def test_get_new_id(self):
        rl = RentsListMemo()
        assert (rl.get_new_id() == 0)
        rl.add_rent(Rent(15, 1, 1, "21-01-01"))
        assert(rl.get_new_id()==1)
        rl.add_rent(Rent(15, 1, 1, "21-01-01"))
        rl.add_rent(Rent(15, 1, 1, "21-01-01"))
        assert (rl.get_new_id() == 3)

    def test_get_rent_by_id(self):
        rl = RentsListMemo()
        rl.add_rent(Rent(0, 1, 1, "21-01-01"))
        rl.add_rent(Rent(1, 1, 2, "22-01-01"))
        assert(rl.get_rent_by_id(0)==rl.get_rents()[0])
        expect_exception(IndexError,lambda:rl.get_rent_by_id(100))

    def test_set_rent(self):
        rl = RentsListMemo()
        rl.add_rent(Rent(0, 1, 1, "21-01-01"))
        rl.add_rent(Rent(1, 1, 2, "22-01-01"))
        rl.set_rent(1,Rent(-1,client_id=5))
        assert (rl.get_rents()[1].get_client_id() == 5)
        expect_exception(IndexError,lambda:rl.set_rent(15,Rent(-1)))

    def test_remove_rent(self):
        rl = RentsListMemo()
        rl.add_rent(Rent(0, 1, 1, "21-01-01"))
        rl.add_rent(Rent(1, 1, 2, "22-01-01"))
        rl.remove_rent(rl.get_rents()[0])
        assert (len(rl.get_rents())==1)
        expect_exception(ValueError, lambda: rl.remove_rent(Rent(10)))

    def test_count(self):
        rl = RentsListMemo()
        rl.add_rent(Rent(0, 1, 1, "21-01-01"))
        rl.add_rent(Rent(1, 1, 2, "22-01-01"))
        assert(rl.count()==2)