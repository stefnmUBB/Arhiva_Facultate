from solvers.solver import Solver


class  RecursiveSolver(Solver):

    def __consistent(self,lst):
        """
        Checks if the last element may lead to a correct solution
        :param lst: list to be checked
        :return: true if solution can be found from this configuration, false otherwise
        """
        return True
        #if len(lst) <= 1:
        #    return True
        #return 1 in [abs(x-lst[-1]) for x in lst[:-1]]

    def __is_solution(self,lst):
        """
        Checks if list is a permutation
        :param lst:
        :return:
        """
        return len(lst) == self.get_input("n")

    def __rec_solve(self,stack):
        if self.__is_solution(stack):
            yield stack.copy()
        n = int(self.get_input("n"))
        # assure permutation:
        if stack == []:
            values=range(1, n+1)
        else:
            # generate possible candidates (abs(last-anyone)==1)
            values = [x+1 for x in stack if x < n] + [x-1 for x in stack if x > 1]
            # remove duplicates
            values = list(dict.fromkeys(values))
            # assure it's a permutation
            values = [x for x in values if x not in stack]
            values = sorted(values)

        for i in values:
            stack.append(i)
            if self.__consistent(stack):
                yield from self.__rec_solve(stack)
            stack.pop()

    def solve_method(self):
        """
        iteratively generates permutations with the wanted property
        :return: generator of computed permutations
        """
        yield from self.__rec_solve([])
