from domain.searchable import Searchable


class ListSearchEngineValidator:
    @staticmethod
    def validate_list(elist):
        """
        Checks if list contains only searchable elements
        :param list: list to be checked
        """
        if not isinstance(elist,list):
            raise TypeError("ListSearchEngine only works on lists of Searchable elements")
        for element in elist:
            if not issubclass(type(element),Searchable):
                raise TypeError("Search list contains non-Searchable elements")