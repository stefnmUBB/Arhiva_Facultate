import unittest

from domain.picture import Picture
from repository.picture_repo import PictureRepo


class TestPictureRepo(unittest.TestCase):
    def test_set_list(self):
        pics_list = [
            Picture.deserialize("23,The Starry Night,Vincent Van Gogh,1889"),
            Picture.deserialize("9,Mona Lisa, Leonardo da Vinci,1519"),
            Picture.deserialize("84,Lady with an Ermine, Leonardo da Vinci, 1491"),
        ]

        repo = PictureRepo()
        repo.set_list(pics_list)
        assert(len(repo.get_list())==3)

    def test_get_list(self):
        pics_list = [
            Picture.deserialize("23,The Starry Night,Vincent Van Gogh,1889"),
            Picture.deserialize("9,Mona Lisa, Leonardo da Vinci,1519"),
            Picture.deserialize("84,Lady with an Ermine, Leonardo da Vinci, 1491"),
        ]

        repo = PictureRepo()
        repo.set_list(pics_list)
        assert(repo.get_list()==pics_list)

    def test_add_picture(self):
        repo = PictureRepo()
        repo.add_picture(Picture.deserialize("23,The Starry Night,Vincent Van Gogh,1889"))
        assert(len(repo.get_list())==1)
        repo.add_picture(Picture.deserialize("23,The Starry Night,Vincent Van Gogh,1889"))
        assert (len(repo.get_list()) == 2)
