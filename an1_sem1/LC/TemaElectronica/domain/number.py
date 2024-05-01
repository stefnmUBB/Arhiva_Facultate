"""
Tema electronica LC - Neacsu-Miclea Liviu-Stefan 215
"""
import string


class Number:
    """
    Number class described by a natural value and a base
    """

    # digits lexicon up to base 36
    digits = list(string.digits)+list(string.ascii_uppercase)

    def __init__(self, value = 0, base = 10):
        self._set_base(base)
        self.set_value(value)

    def get_value(self):
        """
        gets the raw value of the number as a copy list
        :return: value
        :type: list
        """
        return [digit for digit in self.value]

    def set_value(self,value):
        """
        sets the raw value of the number
        If type of value is int or string, that will be the number's value
        in the specified base
        If type of value is list, it gets set as it is. The first element in list
        is the least significant digit of the number.
        :param value: the new value of the number
        :type: list, string or int
        """
        base = self.get_base()
        computed_val = []

        if type(value)==list:
            if len(value)==0:
                value = [0]
            while len(value)>1 and value[-1]==0:
                value.pop()
            if not all(type(d) for d in value):
                raise TypeError(f"Digit must be a number from 0 to {base - 1}.")
            if not all(0<=d and d<base for d in value):
                raise ValueError(f"Digit must be a number from 0 to {base - 1}.")
            computed_val = value
        elif type(value)==int:
            if value==0:
                computed_val = [0]
            elif value<0:
                raise ValueError("The value cannot be negative.")
            else:
                while(value>0):
                    c = value % 10
                    value //= 10
                    if c>=base:
                        raise ValueError("The value is not a number in the specified base.")
                    computed_val.append(c)
        elif type(value) == str:
            value = value.lstrip("0")
            if value == "":
                computed_val = [0]
            else:
                if not all(str(d) in Number.digits[:base] for d in value):
                    raise ValueError(f"Digit must be a number from 0 to {base - 1}.")
                computed_val = [Number.digits.index(str(d)) for d in value]
                computed_val.reverse()
        else:
            raise TypeError("Incorrect value")
        self.value = computed_val

    def get_base(self):
        """
        gets the base of the number
        :return: base
        :type: int
        """
        return self.base

    def _set_base(self, base):
        """
        sets the base of the number without affecting its value
        :param base: the new base
        :raises TypeError if base is not an int
        :raises ValueError if base is not in 2..36
        """
        if not type(base)==int:
            raise TypeError("Base must be a positive integer up to 36.")
        if base<2 or base>36:
            raise ValueError("Base must be a number from 2 to 36.")
        self.base = base

    def _test_operation_valid(self, other):
        """
        Checks if self @ other is valid operation where
        @ = +, -, *, /, <(=), >(=)
        :param other: the number to check compatibility with
        :raises: TypeError if other is not Number
        :raises: VlaueError if the numbers are incompatible
        """
        if type(other)!=type(self):
            raise TypeError("Arithmetics must be done on two number instances.")
        if (self.get_base() != other.get_base()):
            raise ValueError("Cannot perform arithmetics on numbers of different bases")


    def __add__(self,other):
        """
        Performs addition self+other
        :param other: the number to add
        :type Number
        :return: result self+other
        :type: Number
        """
        self._test_operation_valid(other)
        base = self.get_base()
        a = self
        b = other
        if(a.digits_count()<b.digits_count()):
            a=other
            b=self
        res = []
        it = 0
        carry = 0
        while it<b.digits_count():
            r = a[it] + b[it] + carry
            if(r>=base):
                r-=base
                carry=1
            else: carry=0
            res.append(r)
            it+=1
        while it<a.digits_count():
            r = a[it] + carry
            if (r >= base):
                r -= base
                carry = 1
            else: carry = 0
            res.append(r)
            it+=1
        if carry == 1:
            res.append(carry)
        return Number(res,base)

    def __ge__(self,other):
        """
        Checks if self>=other
        :param other: the number to subtract
        :type Number
        :return: True if self>=other, False otherwise
        """
        self._test_operation_valid(other)
        if self.digits_count()<other.digits_count():
            return False
        if self.digits_count()>other.digits_count():
            return True
        for i in range(self.digits_count()-1,-1,-1):
            if self.get_value()[i]<other.get_value()[i]:
                return False
            if self.get_value()[i]>other.get_value()[i]:
                return True
        # self==other
        return True

    def __sub__(self,other):
        """
        Performs subtraction self-other
        :param other: the number to subtract
        :type Number
        :return: result self-other
        :type: Number
        """
        self._test_operation_valid(other)
        if not self>=other:
            raise ValueError("Cannot perform subtraction (result would be negative)")
        base = self.get_base()
        a = self
        b = other
        res = []
        it = 0
        carry = 0
        while it<b.digits_count():
            r = a[it] - b[it] - carry
            if (r<0):
                r+=base
                carry=1
            else: carry=0
            res.append(r)
            it+=1
        while it<a.digits_count():
            r = a[it] - carry
            if (r < 0):
                r += base
                carry = 1
            else: carry = 0
            res.append(r)
            it+=1
        return Number(res,base)

    def __mul__(self, digit):
        """
        performs multiplication self*digit
        :param digit: digit to multiply
        :type int, str
        :return: result self*digit
        :rtype: Number
        """
        digit = self.validate_mul(digit)
        if digit==0:
            return Number(0,self.base)

        res = []
        carry = 0
        for a in self.get_value():
            r = a * digit + carry
            carry = r // self.get_base()
            r = r % self.get_base()
            res.append(r)
        while carry>0:
            res.append(carry % self.get_base())
            carry//=self.get_base()
        return Number(res, self.get_base())

    def __floordiv__(self,digit):
        """
        performs division self//digit
        :param digit: digit to divide to
        :type int
        :return: result self//digit
        :rtype: Number
        """
        digit = self.validate_mul(digit)
        if digit==0:
            raise ZeroDivisionError("Attempting to divide by 0")
        res = []
        r = 0
        for a in self.get_value()[::-1]:
            r = r * self.get_base() + a
            res.append(r//digit)
            r %= digit
        res.reverse()
        return Number(res, self.get_base())

    def __mod__(self,digit):
        """
        performs division self%digit
        :param digit: digit to divide to
        :type int
        :return: result self%digit
        :rtype: Number
        """
        digit = self.validate_mul(digit)
        if digit==0:
            raise ZeroDivisionError("Attempting to divide by 0")
        res = []
        r = 0
        for a in self.get_value()[::-1]:
            r = r * self.get_base() + a
            res.append(r // digit)
            r %= digit
        res.reverse()
        return Number(r, self.get_base())

    def validate_mul(self,digit):
        """
        checks if self*digit can be performed
        requirements : digit is a 1-character number in the self's base
        :param digit: digit to check
        :raises: ValueError if requirements aren't met
        :return: index of digit in digits list
        """
        number = digit if isinstance(digit,Number) else Number(digit,self.get_base())

        if number.digits_count()!=1:
            raise ValueError("Multiplication must be done with a single digit")
        return number.get_value()[0]

    def __eq__(self,other):
        if type(other) == str:
            return other == self.__str__()
        return False

    def __getitem__(self, key):
        return self.get_value()[key]

    def digits_count(self):
        return len(self.get_value())

    def __repr__(self):
        return "Number()"

    def __str__(self,show_base:bool=True):
        """
        Gets string representation of the number
        :param show_base: True for base to appear at the end of the string {number}({base})
        :return: string representation of number
        """
        vals = self.get_value()
        vals.reverse()
        if show_base:
            return f'{"".join([Number.digits[d] for d in vals])}({self.get_base()})'
        return f'{"".join([Number.digits[d] for d in vals])}'

    def is0(self):
        """
        :return: True if self==0(base), False otherwise
        """
        return self.get_value()==[0]

    def shl(self):
        """
        adds a 0 as the least significant digit
        :return: self*10 (base)
        :rtype: Number
        """
        return Number([0]+self.get_value(),self.get_base())


    @staticmethod
    def divmod(a, b):
        """
        performs a//b, a%b
        :param a: first operand
        :type Number
        :param b: second operand
        :type Number
        :return: (a//b,a%b)
        :rtype tuple
        """
        if b.is0():
            raise ZeroDivisionError("Attempting to divide by 0")
        if a.get_base() != b.get_base():
            raise ValueError("Attempting to divide numbers in different bases")
        res = []
        r = Number(0, a.get_base())
        for x in a.get_value()[::-1]:
            r = r.shl() + Number([x], a.get_base())
            cnt = 0
            while r >= b:
                r = r - b
                cnt += 1
            res.append(cnt)
        res.reverse()
        return (Number(res,a.get_base()),r)