import math
from random import random

from utils.date_validator import DateValidator, days_in_month


class Date:
    def __init__(self,d=1,m=2,y=2022):
        self.year=y
        self.month=m
        self.day=d
        DateValidator.validate_date(self)

    def __str__(self):
        """
        converts date to string
        :return: date as string
        """
        return f"{str(self.day).rjust(2,'0')}.{str(self.month).rjust(2,'0')}.{str(self.year).rjust(4)}"

    @staticmethod
    def from_str(strdate):
        """
        gets date form string
        :param strdate: date as string
        :type: str
        :return: Date object
        :raises: ValueError if date cannot be parsed
        """
        args = strdate.split('.')
        if len(args)!=3:
            raise ValueError(f"{strdate} is not a valid Date.")
        try:
            d = int(args[0])
            m = int(args[1])
            y = int(args[2])
        except:
            raise TypeError("Some of date components couldn't be parsed")

        return Date(d,m,y)

    @staticmethod
    def get_random_date():
        """
        :return: random Date instance
        """
        m = math.floor(random() * 11) + 1
        dim = days_in_month[m]
        d = math.floor(random()*(dim-1))+1
        y = math.floor(random()*50)+1970
        return Date(d,m,y)

    def get_sort_key(self):
        """
        :return: an unique number that reflects the order of the dates
        :rtype: int
        """
        return self.year*10000+self.month*100+self.day

