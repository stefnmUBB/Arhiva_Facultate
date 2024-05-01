import unittest

from domain.client import Client
from domain.client_validator import ClientValidator
from tests import expect_exception


class TestClientValidator(unittest.TestCase):
    def test_validate(self):
        def validate():
            ClientValidator.validate_client(client)

        client = Client(1, "Mircea", "Popescu", 5220511108751)
        expect_exception(None, validate)

        client = Client(1, "", "Popescu", 5220511108751)
        expect_exception(ValueError, validate)

        client = Client(1, "Ion", "", 5220511108751)
        expect_exception(ValueError, validate)

        client = Client(1, "Ion", "Popescu", "")
        expect_exception(ValueError, validate)
