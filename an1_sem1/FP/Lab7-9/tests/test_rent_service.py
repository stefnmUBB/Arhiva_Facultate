import unittest
from datetime import date

from domain.rent import Rent
from repository.clients_list_memo import ClientsListMemo
from repository.rents_list_memo import RentsListMemo
from service.client_service import ClientService
from service.movie_service import MovieService
from service.rent_service import RentService
from tests import expect_exception


class TestRent(unittest.TestCase):
    def random_clients(self):
        cs = ClientService()
        cs.populate_random_clients(10)
        return cs.get_clients_repo()

    def random_movies(self):
        ms = MovieService()
        ms.populate_random_movies(10)
        return ms.get_movies_repo()

    def test_set_rents_repo(self):
        rl = RentsListMemo()
        rl.add_rent(Rent(0, 1, 1, "21-01-01"))
        rl.add_rent(Rent(1, 1, 2, "22-01-01"))
        rs = RentService()
        rs.set_rents_repo(rl)
        assert(len(rs.get_rents_list())==2)

    def test_attach(self):
        rs = RentService()
        rs.attach(self.random_clients(),self.random_movies())
        assert(rs.get_clients_list().count()==10)
        assert(rs.get_movies_list().count()==10)

    def test_get_clients_list(self):
        rs = RentService()
        rs.attach(self.random_clients(), self.random_movies())
        assert (rs.get_clients_list().count() == 10)

    def test_set_clients_list(self):
        rs = RentService()
        rs.set_clients_list(self.random_clients())
        assert (rs.get_clients_list().count() == 10)

    def test_get_movies_list(self):
        rs = RentService()
        rs.attach(self.random_clients(), self.random_movies())
        assert (rs.get_movies_list().count() == 10)

    def test_set_movies_list(self):
        rs = RentService()
        rs.set_movies_list(self.random_movies())
        assert (rs.get_movies_list().count() == 10)

    def test_add_rent(self):
        rs = RentService()
        rs.attach(self.random_clients(), self.random_movies())
        rs.add_rent(1,1)
        assert(len(rs.get_rents_list())==1)
        assert(isinstance(rs.get_rents_list()[0].get_rent_date(),date))
        expect_exception(IndexError,lambda:rs.add_rent(500,1))

    def test_remove_rent_by_id(self):
        rs = RentService()
        rs.attach(self.random_clients(), self.random_movies())
        rs.add_rent(1, 1)
        rs.add_rent(2, 5)
        rs.add_rent(4, 3)
        rs.remove_rent_by_id(1)
        assert(rs.get_rents_repo().count()==2)
        expect_exception(IndexError,lambda:rs.remove_rent_by_id(500))

    def test_close_rent(self):
        rs = RentService()
        rs.attach(self.random_clients(), self.random_movies())
        rs.add_rent(1, 1)
        rs.add_rent(2, 5)
        rs.add_rent(4, 3)
        rs.close_rent(2,5)
        assert(rs.get_rents_list()[1].return_date is not None)
        expect_exception(IndexError, lambda: rs.close_rent(10,5))
        expect_exception(ValueError, lambda: rs.close_rent(8,5))

    def test_get_rent_data(self):
        rs = RentService()
        rs.attach(self.random_clients(), self.random_movies())
        rs.add_rent(1, 1)
        rs.add_rent(2, 5)
        rs.add_rent(4, 3)
        data = rs.get_rent_data(rs.get_rents_list()[1])
        assert(data["client"].get_id()==rs.get_clients_list().get_clients()[2].get_id())
        assert(data["movie"].get_id()==rs.get_movies_list().get_movies()[5].get_id())

    def test_get_client_number_of_rents(self):
        rs = RentService()
        rs.attach(self.random_clients(), self.random_movies())
        rs.add_rent(1, 1)
        rs.add_rent(1, 8)
        rs.add_rent(2, 5)
        rs.add_rent(4, 3)
        rs.add_rent(1, 4)
        assert(rs.get_client_number_of_rents(1) == 3)
        assert(rs.get_client_number_of_rents(5) == 0)

    def test_get_movie_number_of_rents(self):
        rs = RentService()
        rs.attach(self.random_clients(), self.random_movies())
        rs.add_rent(1, 2)
        rs.add_rent(1, 8)
        rs.add_rent(2, 5)
        rs.add_rent(4, 3)
        rs.add_rent(1, 5)
        assert(rs.get_movie_number_of_rents(5) == 2)
        assert(rs.get_movie_number_of_rents(6) == 0)











