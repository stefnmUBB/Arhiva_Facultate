import unittest

from domain.movie import Movie
from domain.movie_validator import MovieValidator
from tests import expect_exception


class TestMovieValidator(unittest.TestCase):
    def test_validate(self):
        def validate():
            MovieValidator.validate_movie(movie)

        movie = Movie(1, "Perfect Blue", "...", "Anime", 1997)
        expect_exception(None, validate)

        movie = Movie(1, "", "...", "Anime", 1997)
        expect_exception(ValueError, validate)

        movie = Movie(1, "Perfect Blue", "...", "", 1997)
        expect_exception(ValueError, validate)

        movie = Movie(1, "Perfect Blue", "...", "Anime", 1600)
        expect_exception(ValueError, validate)
