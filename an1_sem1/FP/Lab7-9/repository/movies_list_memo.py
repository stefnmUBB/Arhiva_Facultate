from os.path import isfile

from repository.list_validator import ListValidator
from utils.datafile import DataFile
from utils.randomizer import Randomizer


class MoviesListMemo:
    def __init__(self):
        self.movies = []

    def get_movies(self):
        """
        :return: list of all movies
        :rtype: list
        """
        return self.movies

    def is_empty(self):
        """
        checks if movies list is empty
        :return: True is empty list, False otherwise
        """
        return len(self.get_movies()) == 0

    def get_movie_by_id(self, mid):
        """
        Finds the movie in list having the specified id
        :param mid: the id to look for
        :type: int
        :return: the movie having the id
        :rtype: Movie
        """
        if self.is_empty(): return []
        ListValidator.validate_existent_id(self.movies, mid)
        if(len(self.get_movies())==0):
            raise IndexError("Movie id does not exist")
        return list(filter(lambda c: c.id == mid, self.get_movies()))[0]

    def get_new_id(self):
        """
        Generates an id to be assigned to a new movie
        :return: a new movie id
        :rtype: int
        """
        return 0 if len(self.movies) == 0 else self.movies[-1].id + 1

    def add_movie(self, movie):
        """
        Adds new movie object to list
        :param movie: movie to add
        :type: Movie
        :return: the added movie object
        """
        self.get_movies().append(movie)
        return movie

    def set_movie(self, mid, movie):
        """
        sets the attributes om movie with a certain id
        :param mid: the id of the target movie
        :param movie: movie data to set
        :return: affected movie instance
        :raises: IndexError if movie id does not exist
        """
        ListValidator.validate_existent_id(self.get_movies(), mid)
        src = self.get_movie_by_id(mid)
        src.assimilate_properties(movie)
        return src

    def remove_movie(self, movie):
        """
        removes movie from list
        :param movie: movie to be removed
        :return: removed movie
        :raises: ValueError if movie is not in list
        """
        self.get_movies().remove(movie)
        return movie

    def _set_movies_list(self, mlist):
        self.movies = mlist

    def choose_random_movie(self):
        """
        :return: random movie from the list
        """
        return self.get_movies()[Randomizer.number(0,len(self.get_movies())-1)]

    def count(self):
        """
        :return: number of movies in the list
        """
        return len(self.get_movies())