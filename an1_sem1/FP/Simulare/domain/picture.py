class Picture:
    def __init__(self,pid,name,author,year):
        self.set_id(pid)
        self.set_name(name)
        self.set_year(year)
        self.set_author(author)

    def get_id(self):
        """
        gets picture id
        :return: picture id
        :rtype: int
        """
        return self.__pid

    def set_id(self,pid):
        """
        sets picture id
        :param pid: pic id
        :type: int
        """
        self.__pid = int(pid)

    def get_name(self):
        """
        gets picture name
        :return: picture name
        :rtype: str
        """
        return self.__name

    def set_name(self,name):
        """
        sets picture name
        :param name:  picture name
        :type: str
        """
        self.__name = str(name)

    def get_author(self):
        """
        gets author name
        :return: author name
        :rtype: str
        """
        return self.__author

    def set_author(self,author):
        """
        sets author name
        :param author: author name
        :type: str
        """
        self.__author = str(author)

    def get_year(self):
        """
        gets year
        :return: year
        :rtype: int
        """
        return self.__year

    def set_year(self, year):
        """
        sets year
        :param year: picture year
        :type: int
        """
        self.__year = int(year)

    @staticmethod
    def deserialize(text):
        """
        Gets picture data from a patterned text
        :param text: formatted text  "<id>,<name>,<author>,<year>"
        :type: str
        :return: picture instance
        :rtype: Picture
        """
        args = [arg.strip() for arg in text.split(',')]
        if(len(args)!=4):
            raise InvalidPictureDataError("Wrong number of arguments for Picture data.")
        return Picture(*args)

    def __str__(self):
        """
        Converts picture data to string
        :return: string picture data
        :rtype: str
        """
        return f"{self.get_id()},{self.get_name()},{self.get_author()},{self.get_year()}"

    def contains(self,key):
        """
        detects if key is contained in picture's name
        :param key: sequence to search for
        :type: str
        :return: True if name contains key, False otherwise
        """
        return key.upper() in self.get_name().upper()


class InvalidPictureDataError(Exception):
    pass






