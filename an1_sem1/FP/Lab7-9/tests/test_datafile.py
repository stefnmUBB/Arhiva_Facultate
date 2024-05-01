import unittest

from domain.client import Client
from domain.movie import Movie
from utils.datafile import DataFile


class TestDataFile(unittest.TestCase):
    def test_read(self):
        file = open("test_datafile_write_expected.txt", "r")
        list = DataFile.read_elements(file)
        file.close()
        assert(len(list)==3)
        assert(isinstance(list[0], Client))
        assert(list[0].get_id()==12)
        assert(list[0].get_name()=="Name1")
        assert(list[0].get_surname()=="Surname1")
        assert(list[0].get_cnp()=="1111111111111")
        assert(isinstance(list[1], Client))
        assert (list[1].get_id() == 24)
        assert (list[1].get_name() == "Name2")
        assert (list[1].get_surname() == "Surname2 Surname3")
        assert (list[1].get_cnp() == "2111111111111")
        assert(isinstance(list[2], Movie))
        assert (list[2].get_id() == 1)
        assert (list[2].get_title() == "Movie1")
        assert (list[2].get_description() == "desc")
        assert (list[2].get_genre() == "genre")
        assert (list[2].get_year() == 2015)

    def test_write(self):
        file = open("test_datafile_write.txt","w")
        list = []
        list.append(Client(12,"Name1","Surname1",1111111111111))
        list.append(Client(24,"Name2","Surname2 Surname3",2111111111111))
        list.append(Movie(1,"Movie1","desc","genre",2015))
        DataFile.write_elements(file,list)
        file.close()
        file=open("test_datafile_write.txt","r")
        contentf = file.read()
        file.close()
        file=open("test_datafile_write_expected.txt","r")
        contente=file.read()
        file.close()
        assert(contentf==contente)