import unittest

from repository.list_validator import ListValidator
from tests import expect_exception


class TestClientsList(unittest.TestCase):
    class dummy:
        def __init__(self,id):
            self.id=id

    def test_validate_existent_id(self):
        list = [self.dummy(0), self.dummy(1), self.dummy(6)]
        ListValidator.validate_existent_id(list,0)

        expect_exception(IndexError,lambda:ListValidator.validate_existent_id(list,4))
