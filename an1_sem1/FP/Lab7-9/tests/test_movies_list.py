import unittest

from domain.movie import Movie
from repository.movies_list_memo import MoviesListMemo
from tests import expect_exception


class TestMoviesList(unittest.TestCase):

    def test_get_movies(self):
        mlist = MoviesListMemo()
        mlist.add_movie(Movie(0, "Kimi no na wa", "nice", "Anime", 2016))
        mlist.add_movie(Movie(1, "Kono Sekai no Katasumi ni", "nice", "Anime", 2016))
        assert (len(mlist.get_movies()) == 2)

    def test_set_movies_list(self):
        mlist = MoviesListMemo()
        movies = [
            Movie(0, "Kimi no na wa", "nice", "Anime", 2016),
            Movie(1, "Kono Sekai no Katasumi ni", "nice", "Anime", 2016)
        ]
        mlist._set_movies_list(movies)
        assert (len(mlist.get_movies()) == 2)

    def test_is_empty(self):
        mlist = MoviesListMemo()
        assert(mlist.is_empty())
        mlist.add_movie(Movie(0, "Kimi no na wa", "nice", "Anime", 2016))
        assert (not mlist.is_empty())

    def test_get_new_id(self):
        mlist = MoviesListMemo()
        assert(mlist.get_new_id()==0)
        mlist.add_movie(Movie(15, "Kimi no na wa", "nice", "Anime", 2016))
        assert (mlist.get_new_id() == 16)

    def test_add_movie(self):
        mlist = MoviesListMemo()
        mlist.add_movie(Movie(0,"Kimi no na wa", "nice", "Anime", 2016))
        assert (mlist.get_movies()[0].get_id() == 0)
        assert (len(mlist.get_movies()) == 1)
        mlist.add_movie(Movie(1,"Kono Sekai no Katasumi ni", "nice", "Anime", 2016))
        assert (mlist.get_movies()[1].get_id() == 1)
        assert (len(mlist.get_movies()) == 2)

    def test_set_movie(self):
        mlist = MoviesListMemo()
        mlist.add_movie(Movie(0,"Kimi no na wa", "nice", "Anime", 2016))
        mlist.add_movie(Movie(1,"Kono Sekai no Katasumi ni", "nice", "Anime", 2014))

        mlist.set_movie(1,Movie(-1, year=2016))
        assert (mlist.get_movies()[1].get_year() == 2016)

        expect_exception(IndexError, lambda: mlist.set_movie(100,Movie()))

    def test_remove_movie(self):
        mlist = MoviesListMemo()
        mlist.add_movie(Movie(0,"Kimi no na wa", "nice", "Anime", 2016))
        mlist.add_movie(Movie(1,"Kono Sekai no Katasumi ni", "nice", "Anime", 2014))

        mlist.remove_movie(mlist.get_movies()[0])
        assert (len(mlist.get_movies()) == 1)
        assert (mlist.get_movies()[0].get_id() == 1)

        expect_exception(ValueError, lambda: mlist.remove_movie(Movie(100,"_","_","_",2000)))
        # expect_exception(IndexError, lambda: mlist.remove_movie(100))

    def test_get_movie_by_id(self):
        mlist = MoviesListMemo()
        mlist.add_movie(Movie(0, "Kimi no na wa", "nice", "Anime", 2016))
        mlist.add_movie(Movie(1, "Kono Sekai no Katasumi ni", "nice", "Anime", 2014))
        assert(mlist.get_movie_by_id(0)==mlist.get_movies()[0])

    def test_choose_random_movie(self):
        mlist = MoviesListMemo()
        mlist.add_movie(Movie(0, "Kimi no na wa", "nice", "Anime", 2016))
        mlist.add_movie(Movie(1, "Kono Sekai no Katasumi ni", "nice", "Anime", 2014))
        assert (mlist.choose_random_movie() in mlist.get_movies())

    def test_count(self):
        mlist = MoviesListMemo()
        mlist.add_movie(Movie(0, "Kimi no na wa", "nice", "Anime", 2016))
        mlist.add_movie(Movie(1, "Kono Sekai no Katasumi ni", "nice", "Anime", 2014))
        assert (mlist.count()==2)