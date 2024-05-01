import math
from datetime import datetime
from random import random

from domain.genres import GENRES
from utils.date import Date


class Song:
    def __init__(self,title,artist,genre,date):
        """
        create a new song instance
        :param title:
        :param artist:
        :param genre:
        :param date:
        """
        self.set_title(title)
        self.set_artist(artist)
        self.set_genre(genre)
        self.set_date(date)

    def get_title(self):
        """
        :return: song title
        :rtype: str
        """
        return self.title

    def get_artist(self):
        """
        :return: song artist name
        :rtype: str
        """
        return self.artist

    def get_genre(self):
        """
        :return: song genre
        :rtype: str
        """
        return self.genre

    def get_date(self):
        """
        :return: song date
        :rtype: Date
        """
        return self.date

    def set_title(self,title):
        """
        sets song title
        :param title: song title
        """
        self.title=str(title)

    def set_artist(self,artist):
        """
        sets song artist
        :param artist: song artist
        """
        self.artist = str(artist)

    def set_genre(self,genre):
        """
        sets song genre
        :param genre: song genre
        """
        self.genre = str(genre)

    def set_date(self,date):
        """
        set song release date
        :param date: song date
        :raises: TypeError is attempting to set a non-str or non-Date type object
        """
        if isinstance(date,str):
            self.date = Date.from_str(date)
        elif isinstance(date,Date):
            self.date=date
        else: raise TypeError("Incorrect date type")

    @staticmethod
    def from_str(songstr):
        """
        Creates song instance from string formatted "{title};{artist};{genre};{date}"
        :param songstr: song as string
        :type: str
        :return: Song instance from string
        :rtype: Song
        """
        args = songstr.split(";")
        if len(args) != 4:
            raise ValueError("Incorrect song string format")

        title = args[0].strip()
        artist = args[1].strip()
        genre = args[2].strip()
        date = args[3].strip()

        return Song(title,artist,genre,date)

    def __str__(self):
        return f"{self.get_title()};{self.get_artist()};{self.get_genre()};{self.get_date()}"

    @staticmethod
    def generate_random():
        def get_random_str():
            alphabet = "abcdefghijklmnopqrstuvwxyz"
            length = math.floor(random()*18)+2
            str = alphabet[math.floor(random()*25)].upper()
            for i in range(length):
                str+= alphabet[math.floor(random()*26)]
            return str

        title = get_random_str()
        artist = get_random_str()
        genre = GENRES[math.floor(random()*(len(GENRES)-1))]
        date = Date.get_random_date()
        return Song(title,artist,genre,date)

    @staticmethod
    def generate_random_from_lists(titles,artists):
        title = titles[math.floor(random()*(len(titles)-1))]
        artist = artists[math.floor(random()*(len(artists)-1))]
        genre = GENRES[math.floor(random()*(len(GENRES)-1))]
        date = Date.get_random_date()
        return Song(title,artist,genre,date)


