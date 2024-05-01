from domain.client import Client
from repository.list_validator import ListValidator
from utils.randomizer import Randomizer


class ClientsListMemo:
    def __init__(self):
        self.clients = []

    def get_clients(self):
        """
        :return: list of clients
        :rtype: list
        """
        return self.clients

    def is_empty(self):
        """
        Check if list contains or not clients
        :return: True is list is empty, False otherwise
        """
        return len(self.get_clients()) == 0

    def get_client_by_id(self, cid):
        """
        Finds the client in list having the specified id
        :param cid: the id to look for
        :type: int
        :return: the client having the id
        :rtype: Client
        :raises: IndexError if id is not in list
        """
        ''' 
        Complexity
        Time: Overall O(n)
        BC = θ(1) : clients[0].id == cid
        WC = θ(n) : clients[-1].id == cid
        AC = sum([1/n * i for i in 1..n]) = n*(n+1)/2/n = (n+1)/2 => θ(n)                  
        Memory: θ(n) for clients list
        '''
        return self.get_client_by_id_rec(cid)
        if self.is_empty(): return None
        ListValidator.validate_existent_id(self.clients, cid)
        return list(filter(lambda c: c.id == cid, self.get_clients()))[0]

    def get_client_by_id_rec(self, cid,__index=0):
        """
        recursive implementation of get_clients_by_id
        Finds the client in list having the specified id
        :param cid: the id to look for
        :type: int
        :param index: internal, do not use
        :return: the client having the id
        :rtype: Client
        """
        if __index>=len(self.get_clients()): #return None
            raise IndexError(f"Client with id {cid} was not found.")
        client = self.get_clients()[__index]
        if client.get_id() == cid:
            return client
        return self.get_client_by_id_rec(cid,__index+1)


    def get_new_id(self):
        """
        Generates an id to be assigned to a new client
        :return: a new client id
        :rtype: int
        """
        return 0 if len(self.clients) == 0 else self.clients[-1].id + 1

    def add_client(self, client):
        """
        Adds new client object to list
        :param client: client to add
        :type: Client
        :return: the added client object
        """
        self.get_clients().append(client)
        return client

    def set_client(self, cid, client):
        """
        sets the attributes om movie with a certain id
        :param cid: the id of the target movie
        :param client: movie data to set
        :return: affected movie instance
        :raises: IndexError if movie id does not exist
        """
        ListValidator.validate_existent_id(self.get_clients(), cid)
        src = self.get_client_by_id(cid)
        src.assimilate_properties(client)
        return src

    def remove_client(self, client):
        """
        removes client from list
        :param client: client to be removed
        :return: removed client
        :raises: ValueError if client is not in list
        """
        self.get_clients().remove(client)
        return client

    def _set_clients_list(self, clist):
        self.clients = clist

    def choose_random_client(self):
        """
        :return: random client from the list
        """
        return self.get_clients()[Randomizer.number(0,len(self.get_clients())-1)]

    def count(self):
        """
        :return: number of clients
        """
        return len(self.get_clients())