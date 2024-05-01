"""
Tema electronica LC - Neacsu-Miclea Liviu-Stefan 215
"""
import math

from domain.convert_validator import ConvertValidator
from domain.explanation import Explanation
from domain.number import Number


class Converter:
    sub_dict = {
         4 : ["00","01","10","11"],
         8 : ["000", "001", "010", "011", "100", "101", "110", "111"],
        16 : ["0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111",
              "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111"]
    }

    @staticmethod
    def by_substitution(n:Number,base:int):
        """
        Converts number from a base 2^x to another base 2^y
        :param n: initial number (must be of base 2,4,8,16)
        :param base: base to convert to (2,4,8,16)
        :return: step-by-step ready-to-print explanation containing the converted number as the result
        :rtype: Explanation
        :raises: PermissionError if bases are not powers of 2
        :raises: ValueError if attempting to convert to the same base
        """
        ConvertValidator.validate_substitution(n,base)
        explanation = Explanation()
        explanation.write(f"{n} =  ????({base})")

        digits = n.get_value()
        conv_b2 = []

        if n.get_base() == 2:
            conv_b2 = digits
        else:
            explanation.write("Converting to base 2:")
            for d in digits:
                conv_b2 += [int(b) for b in Converter.sub_dict[n.get_base()][d][::-1]]
            rev2 = [str(x) for x in list(conv_b2[::-1])]
            explanation.write(f'{n} = {"".join(rev2)}(2)')
        nbits = int(math.log2(base))
        explanation.write("Adding leading zeros & group bits:")
        while len(conv_b2) % nbits != 0:
            conv_b2.append(0)
        rev2 = [str(x) for x in list(conv_b2[::-1])]
        grouped = "_".join(["".join([str(i) for i in t]) for t in list(zip(*(iter(rev2),) * nbits))])
        explanation.write(f"{n} = {grouped}(2)")

        ndigits = len(conv_b2) // nbits
        converted = []
        for i in range(ndigits):
            digit = 0
            for j in range(nbits-1,-1,-1):
                digit= digit*2 + conv_b2[nbits*i+j]
            converted.append(digit)

        explanation.write(f"Converting to base {base}:")
        explanation.result = Number(converted,base)
        explanation.write(f"{n} = {explanation.result}")
        return explanation

    @staticmethod
    def by_successive_division(n: Number, base: int):
        """
        Converts number from a base to another base by succesive division
        :param n: initial number
        :param base: base to convert to
        :return: step-by-step ready-to-print explanation containing the converted number as the result
        :rtype: Explanation
        :raises: ValueError if attempting to convert to the same base
        """
        explanation = Explanation()
        explanation.write(f"{n} =  ????({base})")
        explanation.write(f"Convert from base {n.get_base()} to base {base}:")
        b10 = base # copy of base parameter
        base = Converter.by_base_10(Number(base,10),n.get_base()).result
        explanation.write(f"{b10}(10) = {base}")
        converted = []
        nlen = len(n.__str__())
        m = Number(n.__str__(show_base=False),n.get_base())
        while not n.is0():
            divres = Number.divmod(n,base)
            q = divres[0] # n/base
            r =Converter.by_base_10(divres[1],b10).result # n%base
            converted.append(r.get_value()[0])
            explanation.write(f"{n.__str__().rjust(nlen)} / {base} = {q.__str__().rjust(nlen)} r {r}")
            n = q
        explanation.result = Number(converted, b10)
        explanation.write(f"{m} = {explanation.result}")
        return explanation


    def by_base_10(n:Number,base:int):
        """
        Converts number from a base to another base through base 10
        :param n: initial number
        :param base: base to convert to
        :return: step-by-step ready-to-print explanation containing the converted number as the result
        :rtype: Explanation
        :raises: ValueError if attempting to convert to the same base
        """
        explanation = Explanation()
        explanation.write(f"{n} =  ????({base})")

        explanation.write(f"Convert from base {n.get_base()} to base 10:")
        line = f"{n} = "
        terms = []
        n10 = 0
        f = 1
        for i in range(n.digits_count()):
            terms.append(f"{n.get_value()[i]}*{n.get_base()}^{i}")
            n10+=n.get_value()[i]*f
            f*=n.get_base()
        line += " + ".join(terms)
        explanation.write(line)
        explanation.write(f"{n} = {n10}(10)")

        explanation.write(f"Convert from base 10 to base {base}:")

        n10len = 1 if n10==0 else math.floor(math.log10(n10)+1)
        converted = []
        while n10>0:
            q = n10 // base
            r = n10 % base
            explanation.write(f"{str(n10).rjust(n10len)} / {base} = {str(q).rjust(n10len)} r {r}:")
            n10 = q
            converted.append(r)

        explanation.result = Number(converted,base)
        explanation.write(f"{n} = {explanation.result}")
        return explanation

