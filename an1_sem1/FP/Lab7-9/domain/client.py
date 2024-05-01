from dataclasses import dataclass

from domain.searchable_data_class import SearchableDataClass


@dataclass(init=False)
class Client(SearchableDataClass):
    id: int = -1
    name: str = ""
    surname: str = ""
    cnp: str = ""

    def __init__(self,id=-1,name="",surname="",cnp=""):
        self.id=id
        self.set_name(name)
        self.set_surname(surname)
        self.set_cnp(cnp)

    def get_id(self):
        """
        :return: client id
        :rtype: int
        """
        return self.id

    def get_name(self):
        """
        :return: client name
        :rtype: str
        """
        return self.name

    def get_surname(self):
        """
        :return: client surname
        :rtype: str
        """
        return self.surname

    def get_full_name(self):
        """
        :return: full name + surname
        """
        return f"{self.get_surname()} {self.get_name()}"

    def get_cnp(self):
        """
        :return: get client cnp
        :rtype: str
        """
        return self.cnp

    def set_name(self, name):
        """
        sets client name
        :param name: client name
        :type: str
        """
        self.name = str(name)

    def set_surname(self, surname):
        """
        sets client surname
        :param surname: client surname
        :return: str
        """
        self.surname = str(surname)

    def set_cnp(self, cnp):
        """
        sets client cnp
        :param cnp: client cnp
        :type: int, str
        """
        self.cnp = str(cnp)

    def clone(self):
        """
        Creates an annotation-level copy of the Client object
        :return: new Client instance with identical properties
        :rtype: Client
        """
        return Client(self.id, self.name, self.surname, self.cnp)

    def assimilate_properties(self, other):
        """
        Copy attributes from another Client instance, except for the id
        :param other: the source to copy from
        :type: Client
        """
        self.set_name(other.name)
        self.set_surname(other.surname)
        self.set_cnp(other.cnp)

    def __eq__(self, other):
        """
        Verifies if two client instances are equal
        :param other: Client
        :return: True if the current client is equal woth the other (have the same id)
        """
        # TO DO: more precise comparison
        return self.get_id() == other.get_id()

    def _set_id(self,id):
        """
        sets client id
        :param id: client id
        :type: int
        """
        self.id=int(id)
