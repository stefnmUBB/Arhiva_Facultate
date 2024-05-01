"""
Tema electronica LC - Neacsu-Miclea Liviu-Stefan 215
"""
from domain.number import Number


class ConvertValidator:
    @staticmethod
    def validate_substitution(n:Number,base:int):
        if (not n.get_base() in [2,4,8,16]) or (not base in [2,4,8,16]):
            raise PermissionError("Conversion by substitution only works between bases 2,4,8 and 16.")
        if base == n.get_base():
            raise ValueError("Attempted to convert to the same base")