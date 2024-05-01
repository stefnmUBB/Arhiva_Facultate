import string

from domain.movie import Movie
from domain.movie_validator import MovieValidator
from repository.list_validator import ListValidator
from repository.movies_list_file import MoviesListFile
from repository.movies_list_memo import MoviesListMemo
from utils.event_handler import EventHandler
from utils.randomizer import Randomizer, GenresList


class MovieService:
    def __init__(self):
        self.__list=MoviesListMemo()
        self.on_added_movie = EventHandler()
        self.on_changed_movie = EventHandler()
        self.on_removed_movie = EventHandler()

    @staticmethod
    def from_file(path):
        """
        loads movies list data form file
        :param path: file path
        :return: Movie Service instance
        """
        ms = MovieService()
        ms.__list = MoviesListFile()
        ms.__list.load_from_file(path)
        return ms

    def save_to_file(self, path):
        """
        saves movies list data to file
        :param path: file path
        :return:
        """
        if isinstance(self.__list, MoviesListFile):
            self.__list.write_to_file(path)

    def get_movies_repo(self):
        return self.__list

    def get_movies(self):
        return self.get_movies_repo().get_movies()

    def get_movie_by_id(self, mid):
        """
        gets the movie instance having the given id
        :param mid: id of the movie
        :return: movie having the certain if
        :raises: IndexError if id does not exist
        """
        return self.__list.get_movie_by_id(mid)

    def add_movie(self, title, year, genre, description):
        """
        Adds new client to the clients list
        :param title: movie title
        :type: str
        :param year: movie year
        :type: int
        :param genre: movie genre
        :type: str
        :param description: movie description
        :type:str
        :return: added movie instance
        :raises: IndexError if id is invalid
        """
        movie = Movie(self.__list.get_new_id(), title, description, genre, year)
        MovieValidator.validate_movie(movie)
        movie = self.__list.add_movie(movie)
        self.on_added_movie.invoke(self, movie)
        return movie

    def edit_movie(self, mid, title=None, description=None, genre=None, year=None):
        """
        Changes attributes of movie identified by id
        :param mid: movie if
        :param title: movie title
        :param description: movie description
        :param genre: movie genre
        :param year: movie year
        :return: affected movie instance
        :raises: IndexError if movie id does not exist
        """
        clone = self.get_movie_by_id(mid).clone()
        if title is not None:
            clone.set_title(title)
        if description is not None:
            clone.set_description(description)
        if genre is not None:
            clone.set_genre(genre)
        if year is not None:
            clone.set_year(year)
        MovieValidator.validate_movie(clone)
        movie = self.__list.set_movie(mid, clone)
        self.on_changed_movie.invoke(self, movie)
        return movie

    def remove_movie_by_id(self, mid):
        """
        Removes a movie with a certain id
        :param mid: the if of the movie to remove from the list
        :return: the id of the removed movie
        :raises: IndexError if id is invalid
        """
        ListValidator.validate_existent_id(self.get_movies(), mid)
        movie = self.__list.get_movie_by_id(mid)
        self.__list.remove_movie(movie)
        self.on_removed_movie.invoke(self, movie)
        return movie

    def add_random_movie(self):
        self.add_movie(Randomizer.generate_random_name(20),Randomizer.number(1960,2021),
                        Randomizer.choose(GenresList),Randomizer.generate_chars(16,string.ascii_lowercase))

    def populate_random_movies(self,count):
        if count>0:
            for i in range(count):
                self.add_random_movie()
