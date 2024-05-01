"""
Tema electronica LC - Neacsu-Miclea Liviu-Stefan 215
"""
from domain.converter import Converter
from domain.explanation import Explanation
from domain.number import Number
from service.number_input_validator import NumberInputValidator


class NumberService:
    @staticmethod
    def convert_division(n,init_base,fin_base):
        """
        performs conversion by succesive division
        :param n: number to convert
        :type: str
        :param init_base: base of n
        :type: int
        :param fin_base: baste to convert to
        :type: int
        :return: Explanation of process (result = n in fin_base)
        """
        return Converter.by_successive_division(Number(n, init_base), fin_base)


    @staticmethod
    def convert_substitution(n,init_base,fin_base):
        """
        performs conversion by substitution
        :param n: number to convert
        :type: str
        :param init_base: base of n
        :type: int
        :param fin_base: baste to convert to
        :type: int
        :return: Explanation of process (result = n in fin_base)
        """
        return Converter.by_substitution(Number(n,init_base),fin_base)

    @staticmethod
    def convert_through_10(n,init_base,fin_base):
        """
       performs conversion through intermediate base 10
       :param n: number to convert
       :type: str
       :param init_base: base of n
       :type: int
       :param fin_base: baste to convert to
       :type: int
       :return: Explanation of process (result = n in fin_base)
       """
        return Converter.by_base_10(Number(n,init_base),fin_base)

    @staticmethod
    def add_base(n1,n2):
        """
        same as add, but enforces base of n1
        :param n1:
        :param n2:
        :return:
        """
        n1 = NumberInputValidator.validate_number(n1)
        n2 = NumberInputValidator.validate_number(n2)

        expl = Explanation()
        sum = n1 + n2

        expl.write("Solving operation:")
        expl.write(f"{n1} + {n2} = {sum}")

        return expl

    @staticmethod
    def sub_base(n1, n2):
        """
        same as sub, but enforces base of n1
        :param n1:
        :param n2:
        :return:
        """
        n1 = NumberInputValidator.validate_number(n1)
        n2 = NumberInputValidator.validate_number(n2)

        expl = Explanation()
        dif = n1 - n2

        expl.write("Solving operation:")
        expl.write(f"{n1} - {n2} = {sum}")

        return expl

    @staticmethod
    def mul_base(n, d):
        """
        same as mul, but enforces base of n
        :param n:
        :param d:
        :return:
        """
        n = NumberInputValidator.validate_number(n)
        d = NumberInputValidator.validate_digit(d, n.get_base())

        expl = Explanation()

        prod = n * d

        expl.write("Solving operation:")
        expl.write(f"{n} * {d} = {prod}")

        return expl

    @staticmethod
    def div_base(n, d):
        """
        same as div, but enforces base of n
        :param n:
        :param d:
        :return:
        """
        n = NumberInputValidator.validate_number(n)
        d = NumberInputValidator.validate_digit(d, n.get_base())

        expl = Explanation()

        div = Number.divmod(n,d)

        expl.write("Solving operation:")
        expl.write(f"{n} / {d} = {div}")

        return expl

    @staticmethod
    def add(n1,n2,base):
        """
        performs addition n1+n2 in a certain base
        :param n1: first number
        :type: str
        :param n2: second number
        :type: str
        :param base: result base
        :type: int
        :return: Explanation of process (result = n1+n2)
        """
        n1 = NumberInputValidator.validate_number(n1)
        n2 = NumberInputValidator.validate_number(n2)
        base = NumberInputValidator.validate_base(base)

        expl = Explanation()
        expl.write(f"{n1} + {n2} = ???? base {base}")

        e_uniform = NumberService.uniform_base(n1,n2,base)
        expl.append_explanation(e_uniform)

        n1 = e_uniform.get_result()[0]
        n2 = e_uniform.get_result()[1]

        sum = n1 + n2

        expl.write("Solving operation:")
        expl.write(f"{n1} + {n2} = {sum}")

        return expl

    @staticmethod
    def sub(n1, n2, base):
        """
        performs subtraction n1-n2 in a certain base
        :param n1: first number
        :type: str
        :param n2: second number
        :type: str
        :param base: result base
        :type: int
        :return: Explanation of process (result = n1-n2)
        """
        n1 = NumberInputValidator.validate_number(n1)
        n2 = NumberInputValidator.validate_number(n2)
        base = NumberInputValidator.validate_base(base)

        expl = Explanation()
        expl.write(f"{n1} - {n2} = ???? base {base}")

        e_uniform = NumberService.uniform_base(n1, n2, base)
        expl.append_explanation(e_uniform)

        n1 = e_uniform.get_result()[0]
        n2 = e_uniform.get_result()[1]

        sum = n1 - n2

        expl.write("Solving operation:")
        expl.write(f"{n1} - {n2} = {sum}")

        return expl

    @staticmethod
    def mul(n,d,base):
        n = NumberInputValidator.validate_number(n)
        d = NumberInputValidator.validate_digit(d,base)

        expl = Explanation()
        expl.write(f"{n} * {d} = ???? base {base}")

        e_digit = Converter.by_successive_division(d, base)
        expl.append_explanation(e_digit)

        d = e_digit.result.get_value()[0] # get its only digit as a str

        prod = n * d

        expl.write("Solving operation:")
        expl.write(f"{n} * {d} = {prod}")

        return expl

    @staticmethod
    def div(n, d, base):
        n = NumberInputValidator.validate_number(n)
        d = NumberInputValidator.validate_digit(d, base)

        expl = Explanation()
        expl.write(f"{n} / {d} = ???? base {base}")

        e_digit = Converter.by_successive_division(d, base)
        expl.append_explanation(e_digit)

        d = e_digit.result

        res = Number.divmod(n,d)

        expl.write("Solving operation:")
        expl.write(f"{n} / {d} = {res[0]} r {res[1]}")

        return expl

    @staticmethod
    def uniform_base(n1,n2,base):
        """
        brings n1 and n2 to the same base
        :param n1:
        :param n2:
        :param base: common result base
        :return: Explanation(result = list of converted numbers)
        :rtype Explanation
        """
        expl = Explanation()

        e1 = Converter.by_successive_division(n1, base)
        e2 = Converter.by_successive_division(n2, base)

        expl.append_explanation(e1)
        expl.append_explanation(e2)
        expl.set_result([e1.get_result(),e2.get_result()])

        return expl
