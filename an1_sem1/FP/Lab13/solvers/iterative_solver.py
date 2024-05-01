from solvers.solver import Solver


class IterativeSolver(Solver):

    def __consistent(self,lst):
        """
        Checks if the last element may lead to a correct solution
        :param lst: list to be checked
        :return: true if solution can be found from this configuration, false otherwise
        """
        if len(lst) <= 1:
            return True
        if lst[-1] in lst[:-1]:
            return False
        return 1 in [abs(x-lst[-1]) for x in lst[:-1]]

    def __is_solution(self,lst):
        """
        Checks if list is a permutation
        :param lst:
        :return:
        """
        return len(lst) == self.get_input("n")

    def solve_method(self):
        """
        iteratively generates permutations with the wanted property
        :return: generator of computed permutations
        """
        n = int(self.get_input("n"))

        stack = [0]
        while len(stack) > 0:
            chosen = False
            while not chosen and stack[-1] < n:
                stack[-1] += 1
                chosen = self.__consistent(stack)
            if chosen:
                if self.__is_solution(stack):
                    yield stack.copy()
                stack.append(0)
            else:
                if stack[-1] == n:
                    stack.pop()