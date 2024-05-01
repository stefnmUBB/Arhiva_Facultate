import unittest

from domain.client import Client
from repository.clients_list_memo import ClientsListMemo
from tests import expect_exception


class TestClientsList(unittest.TestCase):
    def test_get_clients(self):
        clist = ClientsListMemo()
        clist.add_client(Client(0, "Ion", "Popescu", 5220511108751))
        clist.add_client(Client(1, "Vlad", "Tepes", 1269511100005))
        assert (len(clist.get_clients()) == 2)

    def test_is_empty(self):
        clist = ClientsListMemo()
        assert (clist.is_empty())
        clist.add_client(Client(0, "Ion", "Popescu", 5220511108751))
        clist.add_client(Client(1, "Vlad", "Tepes", 1269511100005))
        assert (not clist.is_empty())


    def test_add_client(self):
        clist = ClientsListMemo()
        clist.add_client(Client(0,"Ion", "Popescu", 5220511108751))
        assert (clist.get_clients()[0].get_id() == 0)
        assert (len(clist.get_clients()) == 1)
        clist.add_client(Client(1,"Vlad", "Tepes", 1269511100005))
        assert (clist.get_clients()[1].get_id() == 1)
        assert (len(clist.get_clients()) == 2)

    def test_set_client(self):
        clist = ClientsListMemo()
        clist.add_client(Client(0,"Ion", "Popescu", 5220511108751))
        clist.add_client(Client(1,"Vlad", "Tepes", 1269511100005))

        clist.set_client(0, Client(-1,name="Vasile",surname="Popescu", cnp=5000000000000))
        assert (clist.get_clients()[0].get_full_name() == "Popescu Vasile")
        assert (clist.get_clients()[0].get_cnp() == "5000000000000")

        expect_exception(IndexError, lambda: clist.set_client(100, Client(name="Vlad")))

    def test_remove_client(self):
        clist = ClientsListMemo()
        clist.add_client(Client(0,"Ion", "Popescu", 5220511108751))
        clist.add_client(Client(1,"Vlad", "Tepes", 1269511100005))

        clist.remove_client(clist.get_clients()[0])
        assert (len(clist.get_clients()) == 1)
        assert (clist.get_clients()[0].get_id() == 1)

        expect_exception(ValueError, lambda: clist.remove_client(Client(100,"N","S","1111111111111")))

    def test_get_new_id(self):
        clist = ClientsListMemo()
        assert(clist.get_new_id()==0)
        clist.add_client(Client(15, "Ion", "Popescu", 5220511108751))
        assert (clist.get_new_id() == 16)

    def test_get_client_by_id(self):
        clist = ClientsListMemo()
        clist.add_client(Client(0,"Ion", "Popescu", 5220511108751))
        clist.add_client(Client(1,"Vlad", "Tepes", 1269511100005))
        clist.add_client(Client(2,"Tudor", "Vladimirescu", 3846519859036))

        expect_exception(IndexError,lambda: clist.get_client_by_id(15))

        assert(clist.get_client_by_id(1).get_name()=="Vlad")
        clist.remove_client(clist.get_clients()[1])
        assert(clist.get_client_by_id(2).get_name()=="Tudor")

    def test_get_client_by_id_rec(self):
        clist = ClientsListMemo()
        clist.add_client(Client(0,"Ion", "Popescu", 5220511108751))
        clist.add_client(Client(1,"Vlad", "Tepes", 1269511100005))
        clist.add_client(Client(2,"Tudor", "Vladimirescu", 3846519859036))

        expect_exception(IndexError,lambda: clist.get_client_by_id_rec(15))

        assert(clist.get_client_by_id_rec(1).get_name()=="Vlad")
        clist.remove_client(clist.get_clients()[1])
        assert(clist.get_client_by_id_rec(2).get_name()=="Tudor")

    def test_set_clients_list(self):
        clist = ClientsListMemo()
        clients = [
            Client(0, "Ion", "Popescu", 5220511108751),
            Client(1, "Vlad", "Tepes", 1269511100005),
            Client(2, "Tudor", "Vladimirescu", 3846519859036)
        ]
        clist._set_clients_list(clients)
        assert(len(clist.get_clients())==3)

    def test_choose_random_client(self):
        clist = ClientsListMemo()
        clist.add_client(Client(0, "Ion", "Popescu", 5220511108751))
        clist.add_client(Client(1, "Vlad", "Tepes", 1269511100005))
        clist.add_client(Client(2, "Tudor", "Vladimirescu", 3846519859036))

        assert(clist.choose_random_client() in clist.get_clients())

    def test_count(self):
        clist = ClientsListMemo()
        clist.add_client(Client(0, "Ion", "Popescu", 5220511108751))
        clist.add_client(Client(1, "Vlad", "Tepes", 1269511100005))
        clist.add_client(Client(2, "Tudor", "Vladimirescu", 3846519859036))

        assert(clist.count()==3)