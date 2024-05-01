import unittest

from domain.client import Client
from domain.searchable import Searchable


class TestClient(unittest.TestCase):

    def test_searchable(self):
        client = Client(1, "Mircea", "Popescu", 5220511108751)
        assert(issubclass(type(client),Searchable))

    def test_get_id(self):
        client = Client(1, "Mircea", "Popescu", 5220511108751)
        assert(client.get_id()==1)

    def test_set_id(self):
        client = Client(1, "Mircea", "Popescu", 5220511108751)
        client._set_id(2)
        assert(client.get_id()==2)
        client._set_id("3")
        assert (client.get_id() == 3)

    def test_get_name(self):
        client = Client(1, "Mircea", "Popescu", 5220511108751)
        assert (client.get_name() == "Mircea")

    def test_set_name(self):
        client = Client(1, "Mircea", "Popescu", 5220511108751)
        client.set_name("Vlad")
        assert (client.get_name() == "Vlad")

    def test_get_surname(self):
        client = Client(1, "Mircea", "Popescu", 5220511108751)
        assert (client.get_surname() == "Popescu")

    def test_set_surname(self):
        client = Client(1, "Mircea", "Popescu", 5220511108751)
        client.set_surname("Petrescu")
        assert (client.get_surname() == "Petrescu")

    def test_get_cnp(self):
        client = Client(1, "Mircea", "Popescu", 5220511108751)
        assert (client.get_cnp() == "5220511108751")

    def test_set_cnp(self):
        client = Client(1, "Mircea", "Popescu", 5220511108751)
        client.set_cnp("5220511108991")
        assert (client.get_cnp() == "5220511108991")

    def test_create(self):
        client = Client(1,"Mircea", "Popescu",5220511108751)
        assert(client.get_id() == 1)
        assert(client.get_name()=="Mircea")
        assert(client.get_surname()=="Popescu")
        assert(client.get_full_name()=="Popescu Mircea")
        assert(client.get_cnp()=="5220511108751")

    def test_clone(self):
        client = Client(1, "Mircea", "Popescu", 5220511108751)
        clone = client.clone()
        assert(client.get_id() == clone.get_id())
        assert(client.get_name() == clone.get_name())
        assert(client.get_surname() == clone.get_surname())
        assert(client.get_cnp() == clone.get_cnp())

    def test_assimilate_properties(self):
        client1 = Client(1, "Mircea", "Popescu", 5220511108751)
        client2 = Client(5)
        client2.assimilate_properties(client1)
        assert (client1.get_id() != client2.get_id())
        assert (client1.get_name() == client2.get_name())
        assert (client1.get_surname() == client2.get_surname())
        assert (client1.get_cnp() == client2.get_cnp())

    def test_eq(self):
        client = Client(1, "Mircea", "Popescu", 5220511108751)
        clone = client.clone()
        clone.set_name("Gabriel")
        assert (client == clone)