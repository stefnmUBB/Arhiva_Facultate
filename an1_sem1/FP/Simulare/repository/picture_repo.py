import os

from domain.picture import Picture, InvalidPictureDataError


class PictureRepo:
    def __init__(self):
        self.set_list([])

    def get_list(self):
        """
        gets pictures list
        :return: pics list
        :rtype: list
        """
        return self.pics_list

    def set_list(self,pics_list):
        """
        sets pics list
        :param pics_list: pictures list
        :type: list
        """
        self.pics_list = pics_list

    def add_picture(self,picture):
        """
        adds new picture to the list
        :param picture: picture to add
        :type: Picture
        """
        self.get_list().append(picture)

    def load_from_file(self,path):
        """
        loads picture from file
        :param path: file to load form
        :return: PictureRepo instance
        :raises: PictureFileLoadError if picture data incorrect
        """
        err = []
        with open(path,'r') as file:
            lines = file.readlines()
            cnt = 1
            for line in lines:
                # print(line)
                try:
                    self.add_picture(Picture.deserialize(line))
                except(InvalidPictureDataError):
                    err.append(f"line {cnt} : Invalid picture data")
                except(ValueError):
                    err.append(f"line {cnt} : Invalid picture arguments")
        if(len(err)>0):
            raise PictureFileLoadError("\n".join(err))


class PictureFileLoadError(Exception):
    pass


