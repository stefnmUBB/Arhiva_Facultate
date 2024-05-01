import unittest
from unittest import TestCase

from domain.song import Song
from service.song_validator import SongValidator
from tests import expect_exception
from utils.date import Date


class TestSong(TestCase):
    def test_create(self):
        song = Song("song1","artist1","genre1","01.02.2021")
        assert(song.get_title()=="song1")
        assert(song.get_artist()=="artist1")
        assert(song.get_genre()=="genre1")
        assert(song.get_date().__str__()=="01.02.2021")

    def test_get_title(self):
        song = Song("song1", "artist1", "genre1", "01.02.2021")
        assert (song.get_title() == "song1")

    def test_set_title(self):
        song = Song("song1", "artist1", "genre1", "01.02.2021")
        song.set_title("song2")
        assert (song.get_title() == "song2")

    def test_get_artist(self):
        song = Song("song1", "artist1", "genre1", "01.02.2021")
        assert (song.get_artist() == "artist1")

    def test_set_artist(self):
        song = Song("song1", "artist1", "genre1", "01.02.2021")
        song.set_artist("artist2")
        assert (song.get_artist() == "artist2")

    def test_get_genre(self):
        song = Song("song1", "artist1", "genre1", "01.02.2021")
        assert (song.get_genre() == "genre1")

    def test_set_genre(self):
        song = Song("song1", "artist1", "genre1", "01.02.2021")
        song.set_genre("genre2")
        assert (song.get_genre() == "genre2")

    def test_get_date(self):
        song = Song("song1", "artist1", "genre1", "01.02.2021")
        assert (song.get_date().__str__() == "01.02.2021")

    def test_set_date(self):
        song = Song("song1", "artist1", "genre1", "01.02.2021")
        song.set_date("02.03.2022")
        assert (song.get_date().__str__() == "02.03.2022")
        song.set_date(Date(4,5,2023))
        assert (song.get_date().__str__() == "04.05.2023")
        expect_exception(TypeError,lambda: song.set_date(12))

    def test_from_str(self):
        song = Song.from_str("title1; artist1; genre1; 01.02.2021")
        assert(song.get_title()=="title1")
        assert(song.get_artist()=="artist1")
        assert(song.get_genre()=="genre1")
        assert(song.get_date().__str__()=="01.02.2021")

    def test_str(self):
        song = Song.from_str("title1; artist1; genre1; 01.02.2021")
        assert(song.__str__() == "title1;artist1;genre1;01.02.2021")

    def test_song_validator(self):
        expect_exception(ValueError,lambda:SongValidator.validate_song(None,"a","g","01.01.2000"))
        expect_exception(ValueError,lambda:SongValidator.validate_song("","a","g","01.01.2000"))
        expect_exception(ValueError,lambda:SongValidator.validate_song("t",None,"g","01.01.2000"))
        expect_exception(ValueError,lambda:SongValidator.validate_song("t","","g","01.01.2000"))
        expect_exception(None,lambda:SongValidator.validate_song("t","g","Rock","01.01.2000"))