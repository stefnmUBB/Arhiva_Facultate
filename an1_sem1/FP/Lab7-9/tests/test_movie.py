import unittest

from domain.movie import Movie


class TestMovie(unittest.TestCase):

    def test_get_id(self):
        movie = Movie(1, "Sahara", "see on imdb", "Action", 2006)
        assert(movie.get_id()==1)

    def test_set_id(self):
        movie = Movie(1, "Sahara", "see on imdb", "Action", 2006)
        movie._set_id(2)
        assert(movie.get_id()==2)

    def test_get_title(self):
        movie = Movie(1, "Sahara", "see on imdb", "Action", 2006)
        movie.set_title("new title")
        assert(movie.get_title()=="new title")

    def test_get_description(self):
        movie = Movie(1, "Sahara", "see on imdb", "Action", 2006)
        assert(movie.get_description()=="see on imdb")

    def test_set_description(self):
        movie = Movie(1, "Sahara", "see on imdb", "Action", 2006)
        movie.set_description("desc")
        assert(movie.get_description()=="desc")

    def test_get_genre(self):
        movie = Movie(1, "Sahara", "see on imdb", "Action", 2006)
        assert(movie.get_genre()=="Action")

    def test_set_genre(self):
        movie = Movie(1, "Sahara", "see on imdb", "Action", 2006)
        movie.set_genre("Adventure")
        assert(movie.get_genre()=="Adventure")

    def test_get_year(self):
        movie = Movie(1, "Sahara", "see on imdb", "Action", 2006)
        assert(movie.get_year()==2006)

    def test_set_year(self):
        movie = Movie(1, "Sahara", "see on imdb", "Action", 2006)
        movie.set_year(2005)
        assert(movie.get_year()==2005)

    def test_create(self):
        movie = Movie(1,"Sahara", "see on imdb", "Action", 2006)

        assert(movie.get_id() == 1)
        assert(movie.get_title()=="Sahara")
        assert(movie.get_description()=="see on imdb")
        assert(movie.get_genre()=="Action")
        assert(movie.get_year()==2006)

    def test_clone(self):
        movie = Movie(1,"Sahara", "see on imdb", "Action", 2006)
        clone = movie.clone()
        assert(movie.get_id() == clone.get_id())
        assert(movie.get_title() == clone.get_title())
        assert(movie.get_description() == clone.get_description())
        assert(movie.get_year() == clone.get_year())

    def test_assimilate_properties(self):
        movie1 = Movie(1,"Sahara", "see on imdb", "Action", 2006)
        movie2 = Movie(5)
        movie2.assimilate_properties(movie1)

        assert (movie1.get_id() != movie2.get_id())
        assert (movie1.get_title() == movie2.get_title())
        assert (movie1.get_description() == movie2.get_description())
        assert (movie1.get_year() == movie2.get_year())