import unittest

from domain.client import Client
from repository.movies_list_memo import MoviesListMemo
from tests import expect_exception


class TestSearchableDataClass(unittest.TestCase):
    def test_search_in_object(self):
        client = Client(1,"Marius","Popescu",5738920563814)
        assert(client.search_in_object("Marius"))
        assert(client.search_in_object("892"))
        assert(not client.search_in_object("Valentin"))