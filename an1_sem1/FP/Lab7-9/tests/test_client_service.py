import unittest

from domain.client import Client
from repository.clients_list_memo import ClientsListMemo
from service.client_service import ClientService
from tests import expect_exception


class TestClientService(unittest.TestCase):
    def test_get_clients_repo(self):
        cs = ClientService()
        cs.add_client("Ion", "Popescu", 5220511108751)
        cs.add_client("Vlad", "Tepes", 1269511100005)
        assert(isinstance(cs.get_clients_repo(), ClientsListMemo))
        assert(len(cs.get_clients_repo().get_clients())==2)

    def test_get_clients(self):
        cs = ClientService()
        cs.add_client("Ion", "Popescu", 5220511108751)
        cs.add_client("Vlad", "Tepes", 1269511100005)
        assert (len(cs.get_clients()) == 2)

    def test_add_client(self):
        cs = ClientService()
        cs.add_client("Ion", "Popescu", 5220511108751)
        assert (cs.get_clients()[0].get_id() == 0)
        assert (len(cs.get_clients()) == 1)
        cs.add_client("Vlad", "Tepes", 1269511100005)
        assert (cs.get_clients()[1].get_id() == 1)
        assert (len(cs.get_clients()) == 2)

    def test_edit_client(self):
        cs = ClientService()
        cs.add_client("Ion", "Popescu", 5220511108751)
        cs.add_client("Vlad", "Tepes", 1269511100005)

        cs.edit_client(0, name="Vasile", cnp=5000000000000)
        assert (cs.get_clients()[0].get_full_name() == "Popescu Vasile")
        assert (cs.get_clients()[0].get_cnp() == "5000000000000")

        expect_exception(IndexError, lambda: cs.edit_client(100, name="Vlad"))

    def test_remove_client_by_id(self):
        cs = ClientService()
        cs.add_client("Ion", "Popescu", 5220511108751)
        cs.add_client("Vlad", "Tepes", 1269511100005)
        cs.remove_client_by_id(0)
        assert (len(cs.get_clients()) == 1)
        assert (cs.get_clients()[0].get_id() == 1)

        expect_exception(IndexError, lambda: cs.remove_client_by_id(100))

    def test_get_client_by_id(self):
        cs = ClientService()
        cs.add_client("Ion", "Popescu", 5220511108751)
        cs.add_client("Vlad", "Tepes", 1269511100005)
        cs.add_client("Tudor", "Vladimirescu", 3846519859036)

        assert(cs.get_client_by_id(1).get_name()=="Vlad")
        cs.remove_client_by_id(1)
        assert(cs.get_client_by_id(2).get_name()=="Tudor")