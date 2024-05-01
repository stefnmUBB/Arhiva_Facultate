from datetime import datetime, date

from domain.rent import Rent


class RentValidator:
    @staticmethod
    def validate_rent(rent):
        """
        Checks if a rent instance is correctly defined
        :param rent: rent to check
        :raises: TypeError if check has failed
        """
        if not isinstance(rent,Rent):
            raise TypeError("Rent type expected for validation")
        if not isinstance(rent.get_rent_date(),date):
            raise TypeError("Invalid rent date")
        if rent.get_return_date() is not None:
            if not isinstance(rent.get_return_date(),date):
                raise TypeError("Invalid return date")
