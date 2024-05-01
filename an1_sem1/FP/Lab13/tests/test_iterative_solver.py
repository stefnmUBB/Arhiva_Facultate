import unittest

from solvers.iterative_solver import IterativeSolver
from tests.test_generic_solver import TestGenericSolver


class TestIterativeSolver(TestGenericSolver):

    def test_solve(self):
        self.solve_with(IterativeSolver)

    def test_validate_input(self):
        self.check_input_of(IterativeSolver)