import unittest
from unittest import TestCase

from domain.song import Song
from repo.song_repo import SongRepo
from tests import expect_exception


class TestSongRepo(TestCase):
    def test_get_songs(self):
        srepo = SongRepo()
        assert(len(srepo.get_songs())==0)

        srepo.add_song(Song("a","b","c","01.02.2022"))
        assert (len(srepo.get_songs()) == 1)

    def test_add_song(self):
        srepo = SongRepo()
        srepo.add_song(Song("a", "b", "c", "01.02.2022"))
        assert (len(srepo.get_songs()) == 1)
        assert(srepo.get_songs()[0].__str__()=="a;b;c;01.02.2022")

    def test_get_song(self):
        srepo = SongRepo()
        srepo.add_song(Song("a", "b", "c", "01.02.2022"))
        srepo.add_song(Song("d", "e", "f", "01.02.2022"))
        srepo.add_song(Song("g", "h", "i", "01.02.2022"))

        assert(srepo.get_song("d","e").get_genre() == "f")
        expect_exception(ValueError,lambda:srepo.get_song("j","k"))

    def test_change_song(self):
        srepo = SongRepo()
        srepo.add_song(Song("a", "b", "c", "01.02.2022"))
        srepo.add_song(Song("d", "e", "f", "01.02.2022"))
        srepo.add_song(Song("g", "h", "i", "01.02.2022"))
        srepo.change_song("a","b","y","05.05.2023")
        assert(srepo.get_songs()[0].__str__()=="a;b;y;05.05.2023")

    def test_save_to_file(self):
        srepo = SongRepo()
        srepo.populate_random(10,["A"],["B"])
        srepo.save_to_file("test.txt")
        linescnt=0
        with open("test.txt","r") as f:
            for line in f: linescnt+=1
        assert(linescnt==10)

    
