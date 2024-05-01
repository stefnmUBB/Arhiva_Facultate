import unittest

from domain.picture import Picture, InvalidPictureDataError
from tests import expect_exception


class TestPicture(unittest.TestCase):
    def test_init(self):
        pic = Picture(23, "The Starry Night","Vincent van Gogh", 1889)
        assert(pic.get_id()==23)
        assert(pic.get_name()=="The Starry Night")
        assert(pic.get_author()=="Vincent van Gogh")
        assert(pic.get_year()==1889)

    def test_get_id(self):
        pic = Picture(23, "The Starry Night", "Vincent van Gogh", 1889)
        assert (pic.get_id() == 23)

    def test_set_id(self):
        pic = Picture(0, "The Starry Night", "Vincent van Gogh", 1889)
        pic.set_id(23)
        assert (pic.get_id() == 23)

    def test_get_name(self):
        pic = Picture(23, "The Starry Night", "Vincent van Gogh", 1889)
        assert (pic.get_name() == "The Starry Night")

    def test_set_name(self):
        pic = Picture(23, "noname", "Vincent van Gogh", 1889)
        pic.set_name("The Starry Night")
        assert (pic.get_name() == "The Starry Night")

    def test_get_author(self):
        pic = Picture(23, "The Starry Night", "Vincent van Gogh", 1889)
        assert (pic.get_author() == "Vincent van Gogh")

    def test_set_author(self):
        pic = Picture(23, "The Starry Night", "noauthor", 1889)
        pic.set_author("Vincent van Gogh")
        assert (pic.get_author() == "Vincent van Gogh")

    def test_get_year(self):
        pic = Picture(23, "The Starry Night", "Vincent van Gogh", 1889)
        assert (pic.get_year() == 1889)

    def test_set_year(self):
        pic = Picture(23, "The Starry Night", "Vincent van Gogh", 1000)
        pic.set_year(1889)
        assert (pic.get_year() == 1889)

    def test_deserialize(self):
        pic = Picture.deserialize("23,The Starry Night,Vincent Van Gogh,1889")
        assert (pic.get_id() == 23)
        assert (pic.get_name() == "The Starry Night")
        assert (pic.get_author() == "Vincent Van Gogh")
        assert (pic.get_year() == 1889)
        expect_exception(InvalidPictureDataError,
                         lambda: Picture.deserialize("23,The Starry Night,Vincent Van Gogh"))
        expect_exception(ValueError, lambda: Picture.deserialize("abc,The Starry Night,Vincent Van Gogh,1889"))

    def test_str(self):
        pic = Picture(23, "The Starry Night", "Vincent Van Gogh", 1889)
        assert(pic.__str__() == "23,The Starry Night,Vincent Van Gogh,1889")

    def test_contains(self):
        pic = Picture.deserialize("23,The Starry Night,Vincent Van Gogh,1889")
        assert(pic.contains("Starry"))
        assert(not pic.contains("Day"))