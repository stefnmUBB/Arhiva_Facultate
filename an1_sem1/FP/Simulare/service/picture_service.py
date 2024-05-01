from repository.picture_repo import PictureRepo


class PictureService:
    def __init__(self):
        self.set_repo(PictureRepo())

    def set_repo(self,repo):
        """
        sets picture repo
        :param repo: picture repo
        :type: PictureRepo
        """
        self.__repo = repo

    def load_from_file(self,path):
        """
        reads pictures from file and adds them to repo
        :param path: file to read from
        """
        self.get_repo().load_from_file(path)

    def get_repo(self):
        """
        :return: picture repo
        :rtype: PictureRepo
        """
        return self.__repo

    # func 1
    def search_in_name(self, key):
        """
        looks for key in each picture's name
        :param key: keyword to search for
        :type: str
        :return: list of pictures ordered descending by year
        :rtype: list
        """
        pics_list = [pic for pic in self.get_repo().get_list() if pic.contains(key)]
        return sorted(pics_list,key=lambda pic:pic.get_year(),reverse=True)

    # func 2
    def get_authors_most_recent_pic(self,author):
        """
        Gets the newest picture from a certain author
        :param author: author name
        :return: newest picture
        :rtype: Picture
        :raises: AuthorNotFoundError if author not exist in list
        """
        pics_list = [pic for pic in self.get_repo().get_list() if pic.get_author()==author]
        if len(pics_list)==0:
            raise AuthorNotFoundError()
        return sorted(pics_list, key=lambda pic: pic.get_year(), reverse=True)[0]

class AuthorNotFoundError(Exception):
    pass