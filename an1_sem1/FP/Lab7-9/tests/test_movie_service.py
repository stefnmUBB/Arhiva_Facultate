import unittest

from domain.movie import Movie
from repository.movies_list_memo import MoviesListMemo
from service.movie_service import MovieService
from tests import expect_exception


class TestMoviesService(unittest.TestCase):

    def test_get_movies_repo(self):
        ms = MovieService()
        ms.add_movie("Kimi no na wa", description="nice", genre="Anime", year=2016)
        ms.add_movie("Kono Sekai no Katasumi ni", description="nice", genre="Anime", year=2014)
        assert(isinstance(ms.get_movies_repo(), MoviesListMemo))
        assert(len(ms.get_movies_repo().get_movies())==2)

    def test_get_movies(self):
        ms = MovieService()
        ms.add_movie("Kimi no na wa", description="nice", genre="Anime", year=2016)
        ms.add_movie("Kono Sekai no Katasumi ni", description="nice", genre="Anime", year=2014)
        assert (len(ms.get_movies()) == 2)

    def test_add_movie(self):
        ms = MovieService()
        ms.add_movie("Kimi no na wa", description="nice", genre="Anime", year=2016)
        assert (ms.get_movies()[0].get_id() == 0)
        assert (len(ms.get_movies()) == 1)
        ms.add_movie("Kono Sekai no Katasumi ni", description="nice", genre="Anime", year=2016)
        assert (ms.get_movies()[1].get_id() == 1)
        assert (len(ms.get_movies()) == 2)

    def test_edit_movie(self):
        ms = MovieService()
        ms.add_movie("Kimi no na wa", description="nice", genre="Anime", year=2016)
        ms.add_movie("Kono Sekai no Katasumi ni", description="nice", genre="Anime", year=2014)

        ms.edit_movie(1, year=2016)
        assert (ms.get_movies()[1].get_year() == 2016)

        expect_exception(IndexError, lambda: ms.edit_movie(100))

    def test_remove_movie(self):
        mlist = MoviesListMemo()
        mlist.add_movie(Movie(0,"Kimi no na wa", "nice", "Anime", 2016))
        mlist.add_movie(Movie(1,"Kono Sekai no Katasumi ni", "nice", "Anime", 2014))

        mlist.remove_movie(mlist.get_movies()[0])
        assert (len(mlist.get_movies()) == 1)
        assert (mlist.get_movies()[0].get_id() == 1)

        expect_exception(ValueError, lambda: mlist.remove_movie(Movie(100,"_","_","_",2000)))

    def test_get_movie_by_id(self):
        ms = MovieService()
        ms.add_movie("Kimi no na wa", description="nice", genre="Anime", year=2016)
        ms.add_movie("Kono Sekai no Katasumi ni", description="nice", genre="Anime", year=2014)
        assert(ms.get_movie_by_id(0)==ms.get_movies()[0])