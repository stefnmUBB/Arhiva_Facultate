"""
Tema electronica LC - Neacsu-Miclea Liviu-Stefan 215
"""

from domain.converter import Converter
from domain.number import Number


class NumberInputValidator:
    @staticmethod
    def validate_number(n:str):
        """
        checks if given parameter is a valid number representation e.g <digits>(<base>)
        :param n: string to check
        :return: the Number instance of n
        :rtype: Number
        :raises: ValueError if n cannot be seen as a Number
        """
        try:
            m=n
            if n=="": raise Exception()
            if n[-1]!=")": raise Exception()
            n = n[:-1].split("(")
            if len(n)!=2: raise  Exception()
            n = Number(n[0],int(n[1]))
        except:
            raise ValueError(f"{m} does not denote a number")
        if n.digits_count() > 64:
            raise ValueError(f"No more than 64 digits are allowed for a single number")
        return n

    @staticmethod
    def validate_base(b):
        """
        checks if given parameter is a valid base (int, 2<=b<=36)
        :param b: base to check
        :return: number of base
        :rtype: int
        :raises: ValueError if n cannot be seen as a Number
        """
        try:
            b=int(b)
            if b<2 or b>36: raise Exception()
            return b
        except:
            return ValueError(f"{b} is not a valid base")

    @staticmethod
    def validate_digit(n,b):
        """
        checks if number n is a digit under base b
        :param n: number to check
        :type: str
        :param b: base
        :type: int
        :return: Number n in the original base
        :rtype: Number
        """
        n = NumberInputValidator.validate_number(n)
        if Converter.by_successive_division(n, b).result.digits_count()!=1:
            raise ValueError(f"{n} is not a digit under base {b}")
        return n
