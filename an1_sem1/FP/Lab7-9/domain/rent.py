from dataclasses import dataclass
from datetime import datetime, date


@dataclass(init=False)
class Rent:
    id:int
    client_id:int
    movie_id:int
    rent_date:date
    return_date:date = None

    def __init__(self,id=-1,client_id=0,movie_id=0,rent_date="00-01-01",return_date=None):
        self._set_id(id)
        self.set_client_id(client_id)
        self.set_movie_id(movie_id)
        self.set_rent_date(rent_date)
        if return_date is not None:
            self.set_return_date(return_date)

    def get_id(self):
        """
        :return: rent id
        """
        return self.id

    def _set_id(self,id):
        """
        sets an id to rent
        :param id: id to be set
        :return:
        """
        self.id=int(id)

    def get_client_id(self):
        """
        :return: client id
        """
        return self.client_id

    def set_client_id(self,cid):
        """
        sets client it
        :param cid: client id
        """
        self.client_id=int(cid)

    def get_movie_id(self):
        """
        :return: movie id
        """
        return self.movie_id

    def set_movie_id(self,mid):
        """
        sets movie id
        :param mid: movie id
        """
        self.movie_id=int(mid)

    def get_rent_date_str(self):
        """
        :return: Default string representation of rent date
        """
        return self.get_rent_date().strftime("%y-%m-%d")

    def get_rent_date(self):
        """
        :return: date client rented video
        """
        return self.rent_date

    def set_rent_date(self, idate):
        """
        sets rent date
        :param idate: date client rented video
        :raises: ValueError if date cannot be parsed
        :raises: TypeError if argument is not a date(time) or a string
        """
        if isinstance(idate, str):
            try:
                self.rent_date=datetime.strptime(idate, '%y-%m-%d').date()
            except:
                self.rent_date = datetime.strptime(idate, '%Y-%m-%d').date()
        elif isinstance(idate, datetime):
            self.rent_date=idate.date()
        elif isinstance(idate,date):
            self.rent_date=idate
        else:
            raise TypeError("Invalid rent date")

    def get_return_date_str(self):
        """
        :return: Default string representation of rent date
        """
        return "" if self.get_return_date() is None else self.get_return_date().strftime("%y-%m-%d")

    def get_return_date(self):
        """
        :return: date client returned video (can be None is video not yet returned)
        """
        return self.return_date

    def set_return_date(self, idate):
        """
        sets date client returned video (can be None is video not yet returned)
        :param idate: returnal date
        :raises: ValueError if date cannot be parsed
        :raises: TypeError if argument is not a date(time) or a string
        """
        if idate is None:
            self.return_date = {0:1}.get(1==1) # assign None
            return
        if isinstance(idate, str):
            try:
                self.return_date=datetime.strptime(idate, '%y-%m-%d').date()
            except:
                self.return_date=datetime.strptime(idate, '%Y-%m-%d').date()
        elif isinstance(idate, datetime):
            self.return_date=idate.date()
        elif isinstance(idate,date):
            self.return_date=idate
        else:
            raise TypeError("Invalid return date")

    def is_returned(self):
        return self.get_return_date() is not None

    def clone(self):
        """
        Creates an annotation-level copy of the Rent object
        :return: new Rent instance with identical properties
        :rtype: Rent
        """
        rentdt = self.get_rent_date().strftime("%y-%m-%d")
        retdt = None if self.get_return_date() is None else self.get_return_date().strftime("%y-%m-%d")
        return Rent(self.get_id(), self.get_client_id(), self.get_movie_id(), rentdt,retdt)

    def assimilate_properties(self, other):
        """
        Copy attributes from another Rent instance, except for the id
        :param other: the source to copy from
        :type: Rent
        """
        self.set_client_id(other.get_client_id())
        self.set_movie_id(other.get_movie_id())
        self.set_rent_date(other.get_rent_date())
        self.set_return_date(other.get_return_date())