from dataclasses import dataclass

from domain.searchable import Searchable


class SearchableDataClass(Searchable):
    def search_in_object(self,key):
        """
        Search for key in object's annontations
        :param key:
        :return:
        """
        for field in self.__annotations__:
            if key in str(getattr(self,field)):
                return True
        return False