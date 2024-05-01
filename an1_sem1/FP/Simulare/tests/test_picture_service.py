import os.path
import unittest

from domain.picture import Picture
from repository.picture_repo import PictureRepo
from service.picture_service import PictureService, AuthorNotFoundError
from tests import expect_exception


class TestPictureService(unittest.TestCase):
    def test_load_from_file(self):
        pserv = PictureService()
        path = f"{os.path.dirname(__file__)}\\pictures.txt"
        pserv.load_from_file(path)
        assert(len(pserv.get_repo().get_list())==3)

    def test_search_in_name(self):
        pserv = PictureService()
        path = f"{os.path.dirname(__file__)}\\pictures.txt"
        pserv.load_from_file(path)
        pics_list = pserv.search_in_name("y")
        assert(len(pics_list)==2)
        assert(pics_list[0].get_name()=="The Starry Night")
        assert(pics_list[1].get_name()=="Lady with an Ermine")

    def test_get_authors_most_recent_pic(self):
        pserv = PictureService()
        path = f"{os.path.dirname(__file__)}\\pictures.txt"
        pserv.load_from_file(path)
        pic = pserv.get_authors_most_recent_pic("Leonardo da Vinci")
        assert(pic.get_year()==1519)
        expect_exception(AuthorNotFoundError,
                         lambda:pserv.get_authors_most_recent_pic("Leonardo di Caprio"))