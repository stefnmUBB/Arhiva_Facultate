import unittest

from solvers.recursive_solver import RecursiveSolver
from tests.test_generic_solver import TestGenericSolver


class RecursiveSolverTest(TestGenericSolver):

    def test_solve(self):
        self.solve_with(RecursiveSolver)

    def test_validate_input(self):
        self.check_input_of(RecursiveSolver)