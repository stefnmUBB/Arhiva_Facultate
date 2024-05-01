class ListValidator:
    @staticmethod
    def validate_existent_id(list, id):
        """
        Checks if the speicified id exists among the list elements ids
        :param list: list to be checked
        :param id: the id to look for
        :raises: IndexError if the id does not exist in the list
        """
        if not id in [elem.id for elem in list]:
            raise IndexError(f"Specified id {id} does not exist")
