from os.path import isfile

from domain.rent import Rent
from repository.list_validator import ListValidator
from utils.datafile import DataFile


class RentsListMemo:
    def __init__(self):
        self.rents = []

    def get_rents(self):
        """
        :return: rents list
        """
        return self.rents

    def clear(self):
        """
        clears rents list
        """
        self.get_rents().clear()

    def get_new_id(self):
        """
        generates an id for newly added rents
        :return: new rent id
        """
        return 0 if len(self.get_rents()) == 0 else self.get_rents()[-1].id + 1

    def add_rent(self,rent):
        """
        adds new rent to the rents list
        :param rent: new rent
        :return: added rent
        """
        newrent = Rent(self.get_new_id())
        newrent.assimilate_properties(rent)
        self.get_rents().append(newrent)
        return newrent

    def is_empty(self):
        """
        checks if rents list is empty
        :return: True is empty list, False otherwise
        """
        return len(self.get_rents())==0

    def get_rent_by_id(self,rid):
        """
        Finds the rent in list having the specified id
        :param rid: the id to look for
        :type: int
        :return: the rent having the id
        :rtype: Rent
        """
        if self.is_empty(): return []
        ListValidator.validate_existent_id(self.rents, rid)
        return list(filter(lambda c: c.id == rid, self.get_rents()))[0]

    def set_rent(self, rid, rent):
        """
        sets the attributes of rent with a certain id
        :param rid: the id of the target rent
        :param rent: rent data to set
        :return: affected rent instance
        :raises: IndexError if rent id does not exist
        """
        ListValidator.validate_existent_id(self.get_rents(), rid)
        src = self.get_rent_by_id(rid)
        src.assimilate_properties(rent)
        return src

    def remove_rent(self,rent):
        """
        removes rent from
        :param rent: rent to remove
        :return: removed rent
        :raises: ValueError if target rent is not in list
        """
        self.get_rents().remove(rent)
        return rent

    def _set_rents_list(self, rlist):
        self.rents = rlist

    def count(self):
        """
        :return: number of rents in the list
        :rtype: int
        """
        return len(self.get_rents())