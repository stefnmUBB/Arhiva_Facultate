import unittest

from repository.clients_list_memo import ClientsListMemo
from service.client_service import ClientService
from service.list_search_engine import ListSearchEngine


class TestClientsList(unittest.TestCase):
    def test_look_for(self):
        clientserv=ClientService()
        clientserv.add_client("Vladimir", "Putin", 3422715101199)
        clientserv.add_client("Paul", "Madalin", 3422715101199)
        clientserv.add_client("Nicolae", "Popa", 5027711155183)
        clientserv.add_client("Andrei", "Anreescu", 5027719055183)
        clientserv.add_client("Andrei", "Popescu", 5027893255183)

        found = ListSearchEngine.look_for("P", clientserv.get_clients())
        assert(len(found)==4)
        assert(found[0].get_id()==0)
        assert(found[1].get_id()==1)
        assert(found[2].get_id()==2)
        assert(found[3].get_id()==4)