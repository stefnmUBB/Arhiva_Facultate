"""
Tema electronica LC - Neacsu-Miclea Liviu-Stefan 215
"""
from service.number_service import NumberService
from service.command import Command
from ui.user_interface import UserInterface


class ProgramUI(UserInterface):
    def __init__(self):
        UserInterface.__init__(self)
        self.init_commands()

    @staticmethod
    def wrap_explanation(func,*args):
        def result(*args):
            print(func(*args))
        return result

    def init_commands(self):
        self.get_cmd_manager().add_command(
            Command(self.wrap_explanation(NumberService.add), "%s + %s = ? base %i",
                    "%s = [first number] <digits>(<base>) e.g. 1FF(16) \n" +
                    "      %s = [second number] <digits>(<base>)\n"+
                    "      %i = [final base of addition result]\n" +
                    "      E.g. : 12(7) + 5(8) = ? base 15")
        )

        self.get_cmd_manager().add_command(
            Command(self.wrap_explanation(NumberService.sub), "%s - %s = ? base %i",
                    "%s = [first number] <digits>(<base>) e.g. 1FF(16) \n" +
                    "      %s = [second number] <digits>(<base>)\n" +
                    "      %i = [final base of subtraction result]\n" +
                    "      E.g. : 12(7) - 5(8) = ? base 15")
        )

        self.get_cmd_manager().add_command(
            Command(self.wrap_explanation(NumberService.mul), "%s * %s = ? base %i",
                    "%s = [first number] <digits>(<base>) e.g. 1FF(16) \n" +
                    "      %s = [second number] <digits>(<base>)\n" +
                    "      %i = [final base of multiplication result]\n" +
                    "    ! Second number must be a digit in the result base\n"
                    "      E.g. : 12(7) * 5(7) = ? base 7")
        )

        self.get_cmd_manager().add_command(
            Command(self.wrap_explanation(NumberService.div), "%s / %s = ? base %i",
                    "%s = [first number] <digits>(<base>) e.g. 1FF(16) \n" +
                    "      %s = [second number] <digits>(<base>)\n" +
                    "      %i = [final base of division result]\n" +
                    "    ! Second number must be a digit in the result base\n"
                    "      E.g. : 12(7) / 5(7) = ? base 7")
        )

        self.get_cmd_manager().add_command(
            Command(self.wrap_explanation(NumberService.convert_substitution),
                    "Convert %s from base %i to base %i by substitution",
                    "%s = [number to convert] <digits> e.g. 1FF \n" +
                    "      %i = [base of number] e.g. 16\n" +
                    "      %i = [base to convert to]\n" +
                    "    ! The start and destination bases must be either 2,4,8 or 16\n"
                    "      E.g. : Convert FF from base 16 to base 4 by substitution")
        )

        self.get_cmd_manager().add_command(
            Command(self.wrap_explanation(NumberService.convert_division),
                    "Convert %s from base %i to base %i by successive division",
                    "%s = [number to convert] <digits> e.g. 1FF \n" +
                    "      %i = [base of number] e.g. 16\n" +
                    "      %i = [base to convert to]\n" +
                    "      E.g. : Convert 123456 from base 7 to base 4 by successive division")
        )

        self.get_cmd_manager().add_command(
            Command(self.wrap_explanation(NumberService.convert_through_10),
                    "Convert %s from base %i to base %i through base 10",
                    "%s = [number to convert] <digits> e.g. 1FF \n" +
                    "      %i = [base of number] e.g. 16\n" +
                    "      %i = [base to convert to]\n" +
                    "      E.g. : Convert 123456 from base 7 to base 4 through base 10")
        )


        self.get_cmd_manager().add_command(
            Command(self.wrap_explanation(NumberService.add_base), "%s + %s = ?",
                    "%s = [first number] <digits>(<base>) e.g. 1FF(16) \n" +
                    "      %s = [second number] <digits>(<base>)\n" +
                    "      SAME BASE\n" +
                    "      E.g. : 12(7) + 5(8) = ? base 15")
        )

        self.get_cmd_manager().add_command(
            Command(self.wrap_explanation(NumberService.sub_base), "%s - %s = ?",
                    "%s = [first number] <digits>(<base>) e.g. 1FF(16) \n" +
                    "      %s = [second number] <digits>(<base>)\n" +
                    "      SAME BASE\n" +
                    "      E.g. : 12(7) - 5(8) = ? base 15")
        )

        self.get_cmd_manager().add_command(
            Command(self.wrap_explanation(NumberService.mul_base), "%s * %s = ?",
                    "%s = [first number] <digits>(<base>) e.g. 1FF(16) \n" +
                    "      %s = [second number] <digits>(<base>)\n" +
                    "      %i = [final base of multiplication result]\n" +
                    "    ! Second number must be a digit in base of the first number"
                    "      SAME BASE"
                    "      E.g. : 12(7) * 5(7) = ? base 7")
        )

        self.get_cmd_manager().add_command(
            Command(self.wrap_explanation(NumberService.div_base), "%s / %s = ?",
                    "%s = [first number] <digits>(<base>) e.g. 1FF(16) \n" +
                    "      %s = [second number] <digits>(<base>)\n" +
                    "      %i = [final base of division result]\n" +
                    "    ! Second number must be a digit in base of the first number"
                    "      SAME BASE"
                    "      E.g. : 12(7) / 5(7) = ? base 7")
        )


