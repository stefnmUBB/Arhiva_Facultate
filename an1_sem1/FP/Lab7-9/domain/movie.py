from dataclasses import dataclass

from domain.searchable_data_class import SearchableDataClass


@dataclass
class Movie(SearchableDataClass):
    id: int = -1
    title: str = ""
    description: str = ""
    genre: str = ""
    year: int = 0

    def get_id(self):
        """
        :return: movie id
        :rtype: int
        """
        return self.id

    def get_title(self):
        """
        :return: movie title
        :rtype: str
        """
        return self.title

    def get_description(self):
        """
        :return: movie description
        :rtype: str
        """
        return self.description

    def get_genre(self):
        """
        :return: movie genre
        :rtype: str
        """
        return self.genre

    def get_year(self):
        """
        :return: movie release year
        :rtype: int
        """
        return self.year

    def set_title(self, title):
        """
        sets movie title
        :param title: movie title
        :type: str
        """
        self.title = str(title)

    def set_description(self, description):
        """
        sets movie description
        :param description: movie description
        :type: str
        """
        self.description = str(description)

    def set_genre(self, genre):
        """
        sets movie genre
        :param genre: movie genre
        :type: str
        """
        self.genre = str(genre)

    def set_year(self, year):
        """
        sets movie year
        :param year: movie year
        :return: str
        """
        self.year = int(year)

    def clone(self):
        """
        Creates an annotation-level copy of the Movie object
        :return: new Movie instance with identical properties
        :rtype: Movie
        """
        return Movie(self.id, self.title, self.description, self.genre, self.year)

    def assimilate_properties(self, other):
        """
        Copy attributes from another Movie instance, except for the id
        :param other: the source to copy from
        :type: Movie
        """
        self.set_title(other.title)
        self.set_description(other.description)
        self.set_genre(other.genre)
        self.set_year(other.year)

    def _set_id(self,id):
        """
        sets movie if
        :param id: movie id
        :type: int
        """
        self.id=int(id)
