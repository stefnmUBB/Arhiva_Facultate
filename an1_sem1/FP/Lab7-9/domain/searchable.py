class Searchable:
    """
    Abstract base class for objects you can look for data
    """
    def search_in_object(self,key):
        """
        method to decide whether object contains key or not
        :param key: key to search
        :return: True if the object contains key
        """
        return False
