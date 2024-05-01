import unittest
from unittest import TestCase

from domain.song import Song
from repo.song_repo import SongRepo
from service.song_service import SongService


class TestSongService(TestCase):
    def test_change_song(self):
        with open("x.txt", "w"): pass  # clear
        serv = SongService("x.txt")
        serv.get_repo().add_song(Song("A","B","Rock","12.07.1995"))
        sng = serv.change_song("A","B","Pop","13.07.1995")
        assert(sng.__str__()=="A;B;Pop;13.07.1995")

    def test_get_songs_path(self):
        with open("x.txt", "w"): pass  # clear
        serv = SongService("x.txt")
        assert(serv.get_songs_path()=="x.txt")

    def test_get_repo(self):
        with open("x.txt", "w"): pass  # clear
        serv = SongService("x.txt")
        assert (isinstance(serv.get_repo(),SongRepo))

    def test_add_random_songs(self):
        with open("x.txt", "w"): pass  # clear
        serv = SongService("x.txt")
        serv.add_random_songs(13,["A"],["B"])
        assert(len(serv.get_repo().get_songs())==13)

    def test_get_sorted_songs_by_date(self):
        with open("x.txt", "w"): pass  # clear
        serv = SongService("x.txt")
        serv.add_random_songs(13,["A"],["B"])
        songs = serv.get_sorted_songs_by_date()
        keys = [song.get_date().get_sort_key() for song in songs]
        for i in range(len(songs)-1):
            assert(keys[i] >= keys[i+1])