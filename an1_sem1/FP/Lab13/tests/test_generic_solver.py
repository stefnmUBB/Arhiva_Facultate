import unittest

from tests import expect_exception


class TestGenericSolver(unittest.TestCase):
    def solve_with(self, solver_type):
        solver = solver_type()

        solver.input("n",-1)
        expect_exception(ValueError, lambda:list(solver.solve()))

        solver.input("n", 0)
        expect_exception(ValueError, lambda:list(solver.solve()))

        solver.input("n", 3)
        sols = list(solver.solve_method())
        assert (len(sols) == 4)
        assert ([1, 2, 3] in sols)
        assert ([2, 3, 1] in sols)
        assert ([2, 1, 3] in sols)
        assert ([3, 2, 1] in sols)

        solver.input("n",4)
        assert(len(list(solver.solve_method())) == 8)

    def check_input_of(self,solver_type):
        solver = solver_type()

        expect_exception(KeyError,lambda: solver.get_input("n"))

        assert(isinstance(solver.validate_input(),KeyError)) # n does not exist

        solver.input("m",2)
        assert(isinstance(solver.validate_input(),KeyError)) # n does not exist

        solver.input("n",5)
        assert(solver.validate_input() == None) # Ok
        assert(solver.get_input("n") == 5)

        solver.input("n",-2)
        assert(isinstance(solver.validate_input(),ValueError))

        solver.input("n", 0)
        assert (isinstance(solver.validate_input(), ValueError))

        solver.input("n", "string")
        assert(isinstance(solver.validate_input(),ValueError))
