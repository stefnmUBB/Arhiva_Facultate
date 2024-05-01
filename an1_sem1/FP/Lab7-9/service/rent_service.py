from datetime import datetime

from domain.rent import Rent
from domain.rent_validator import RentValidator
from repository.clients_list_memo import ClientsListMemo
from repository.list_validator import ListValidator
from repository.movies_list_memo import MoviesListMemo
from repository.rents_list_file import RentsListFile
from repository.rents_list_memo import RentsListMemo
from utils.event_handler import EventHandler


class RentService:
    def __init__(self):
        self.__list = RentsListMemo()
        self.on_added_rent = EventHandler()
        self.on_changed_rent = EventHandler()
        self.on_removed_rent = EventHandler()

    def set_rents_repo(self, rents_list):
        """
        sets rents list
        :param rents_list: RentsList
        """
        self.__list = rents_list

    def get_rents_repo(self):
        """
        :return: rents list storage attribute
        :rtype: RentsListMemo
        """
        return self.__list

    def get_rents_list(self):
        """
        :return: rents list
        :rtype: list
        """
        return self.get_rents_repo().get_rents()

    def get_clients_list(self):
        """
        :return: clients list
        :rtype:ClientsListMemo
        """
        return self.clients_list

    def set_clients_list(self, cl):
        """
        sets clients list
        :param cl: clients list
        """
        self.clients_list = cl

    def get_movies_list(self):
        """
        :return: movie list
        :rtype:MoviesListMemo
        """
        return self.movies_list

    def set_movies_list(self, ml):
        """
        sets movie service
        :param ml: movies list
        """
        self.movies_list = ml

    def __integrity_check(self):
        """
        Checks if rents list contain reliable data
        (eg. non-ghost clients/movies).
        Automatically corrects errors wherever possible.
        :raises: ValueError if check has failed
        """
        if not (isinstance(self.get_clients_list(), ClientsListMemo) and
                isinstance(self.get_movies_list(), MoviesListMemo)):
            raise ValueError("Inconsistent repos")
        remainedrents = []
        for rent in self.__list.get_rents():
            valid = True
            try:
                self.get_clients_list().get_client_by_id(rent.client_id)
                self.get_movies_list().get_movie_by_id(rent.movie_id)
            except(IndexError):
                valid = False
            if valid:
                RentValidator.validate_rent(rent)
                remainedrents.append(rent)
        self.__list.clear()
        for rent in remainedrents:
            self.__list.add_rent(rent)

    def add_rent(self, client_id, movie_id):
        """
        adds a rent with the given properties
        :param client_id: if of client who rents
        :param movie_id: if of rented movie
        :return: newly created rent
        """
        ListValidator.validate_existent_id(self.get_clients_list().get_clients(), client_id)
        ListValidator.validate_existent_id(self.get_movies_list().get_movies(), movie_id)
        rent_date = datetime.now()
        rent = Rent(-1, client_id, movie_id, rent_date)
        RentValidator.validate_rent(rent)
        rent = self.get_rents_repo().add_rent(rent)
        self.on_added_rent.invoke(self, rent)
        return rent

    def remove_rent_by_id(self, rent_id):
        """
        removes a rent from list by its id
        :param rent_id: if of target rent
        :return: removed rent
        """
        ListValidator.validate_existent_id(self.get_rents_list(), rent_id)
        rent = self.get_rents_repo().get_rent_by_id(rent_id)
        rent = self.get_rents_repo().remove_rent(rent)
        self.on_removed_rent.invoke(self, rent)
        return rent

    def close_rent(self, cid, mid):
        """
        marks a rent as returned movie
        :param cid: id of renter client
        :param mid: id of rented movie
        :return: affected rent
        :raises: IndexError if index does not exist in any of the lists
        :raises: ValueError if the rent coul not be found
        """
        ListValidator.validate_existent_id(self.get_clients_list().get_clients(), cid)
        ListValidator.validate_existent_id(self.get_movies_list().get_movies(), mid)
        try:
            rent = list(filter(lambda r: r.get_client_id() == cid and r.get_movie_id() == mid \
                               and not r.is_returned(), self.get_rents_list()))[0]
        except:
            raise ValueError("This client has not rented the movie")

        rent.set_return_date(datetime.now())
        self.on_changed_rent.invoke(self, rent)
        return rent

    def attach(self, clients_list, movies_list):
        """
        link rents list to its corresponding clients and movies lists
        :param clients_list: renters list
        :param movies_list: list of movies to rent
        """
        self.set_clients_list(clients_list)
        self.set_movies_list(movies_list)
        self.set_movies_list(movies_list)
        self.__integrity_check()

    @staticmethod
    def from_file(path):
        """
        loads rents list data form file
        :param path: file path
        :return: Rent Service instance
        """
        rs = RentService()
        rs.__list = RentsListFile()
        rs.__list.load_from_file(path)
        return rs

    def save_to_file(self, path):
        """
        saves rents list data to file
        :param path: file path
        :return:
        """
        if isinstance(self.__list,RentsListFile):
            self.__list.write_to_file(path)

    def get_rent_data(self, rent):
        """
        gets the renter client and rented movie instances
        featured in the given rent
        :return: a dictionary {client,movie}
        :rtype: dict
        """
        try:
            client = self.get_clients_list().get_client_by_id(rent.get_client_id())
        except IndexError:
            client = None
        try:
            movie = self.get_movies_list().get_movie_by_id(rent.get_movie_id())
        except IndexError:
            movie = None
        return {"client": client, "movie": movie}

    def get_movie_number_of_rents(self,mid):
        """
        gets the number of recorded times a movie has been rented
        :param mid: the movie to check id
        :return: number of rents for the movie
        """
        count = 0
        for rent in self.get_rents_list():
            mov = self.get_rent_data(rent)["movie"]
            if mov is not None and mov.get_id()==mid:
                count += 1
        movie = self.get_movies_list().get_movie_by_id(mid)
        movie.tag = count
        return count

    def get_client_number_of_rents(self,cid):
        """
        gets the number of recorded times a client has rented a movie
        :param cid: the client to check id
        :return: number of rents for the client
        """
        count = 0
        for rent in self.get_rents_list():
            cli = self.get_rent_data(rent)["client"]
            if cli is not None and cli.get_id()==cid:
                count += 1
        client = self.get_clients_list().get_client_by_id(cid)
        client.tag=count
        return count

    def add_random_rent(self):
        client=self.get_clients_list().choose_random_client()
        movie=self.get_movies_list().choose_random_movie()
        self.add_rent(client.get_id(),movie.get_id())

    def populate_random_rents(self,n):
        for i in range(n):
            self.add_random_rent()