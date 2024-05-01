from domain.client import Client
from domain.client_validator import ClientValidator
from repository.clients_list_file import ClientsListFile
from repository.clients_list_memo import ClientsListMemo
from repository.list_validator import ListValidator
from utils.event_handler import EventHandler
from utils.randomizer import Randomizer, NamesList, SurnamesList


class ClientService:
    def __init__(self):
        self.__list =  ClientsListMemo()
        self.on_added_client = EventHandler()
        self.on_changed_client = EventHandler()
        self.on_removed_client = EventHandler()

    @staticmethod
    def from_file(path):
        """
        loads client list data form file
        :param path: file path
        :return: Client Service instance
        """
        cs=ClientService()
        cs.__list = ClientsListFile()
        cs.__list.load_from_file(path)
        return cs

    def save_to_file(self,path):
        """
        saves client list data to file
        :param path: file path
        :return:
        """
        if isinstance(self.__list,ClientsListFile):
            self.__list.write_to_file(path)

    def get_clients_repo(self):
        """
        returns clients storage attribute
        :return: clients
        :rtype: ClientsListMemo
        """
        return self.__list

    def get_clients(self):
        """
        :return: list of all clients
        :rtype: list
        """
        return self.get_clients_repo().get_clients()

    def get_client_by_id(self, cid):
        """
        gets the movie instance having the given id
        :param cid: id of the movie
        :return: movie having the certain if
        :raises: IndexError if id does not exist
        """
        return self.__list.get_client_by_id(cid)

    def add_client(self, name, surname, cnp):
        """
        Adds new client to the clients list
        :param name: client's name
        :type: str
        :param surname: client's surname
        :type: str
        :param cnp: client's cnp
        :type: str
        :raises: IndexError if id is invalid
        """
        client = Client(self.__list.get_new_id(), name, surname, cnp)
        ClientValidator.validate_client(client)
        client = self.__list.add_client(client)
        self.on_added_client.invoke(self,client)
        return client

    def edit_client(self, cid, name=None, surname=None, cnp=None):
        """
        Edits the client having the speicified id. If a certain argument is None,
        then that property remains untouched.
        :param cid: client id
        :param name: new client's name
        :param surname: new client's surname
        :param cnp: new clinet's cnp
        :return: the modified client instance
        :raises: IndexError if id is invalid
        """
        clone=self.get_client_by_id(cid).clone()
        if name is not None:
            clone.set_name(name)
        if surname is not None:
            clone.set_surname(surname)
        if cnp is not None:
            clone.set_cnp(cnp)
        ClientValidator.validate_client(clone)

        client=self.__list.set_client(cid,clone)

        self.on_changed_client.invoke(self, client)
        return client

    def remove_client_by_id(self,id):
        """
        Removes a client with a certain id
        :param id: the if of the client to remove from the list
        :return: the id of the removed client
        :raises: IndexError if id is invalid
        """
        ListValidator.validate_existent_id(self.get_clients(), id)
        client = self.__list.get_client_by_id(id)
        self.__list.remove_client(client)

        self.on_removed_client.invoke(self, client)

        return client

    def add_random_client(self):
        self.add_client(Randomizer.choose(NamesList),Randomizer.choose(SurnamesList),
                        Randomizer.generate_chars(13))

    def populate_random_clients(self,count):
        if count>0:
            for i in range(count):
                self.add_random_client()