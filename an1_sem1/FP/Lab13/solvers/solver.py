class Solver:
    def __init__(self):
        self.__input = {}

    def input(self,key,value):
        """
        Adds a new input property to the solver
        :param key: property key
        :param value: property value
        """
        self.__input[key] = value

    def get_input(self, key):
        """
        :param key: property key
        :return: value of the property returned by the key
        :raises: KeyError if key does not exist in the input data
        """
        if not key in self.__input.keys():
            raise KeyError("Specified key not found in the input parameters")
        return self.__input[key]

    def solve_method(self):
        """
        yields solutions
        :return: generator of computed permutations
        """
        return

    def solve(self):
        """
        wrapper around solve_method, to be called in a for loop
        :return: generator of computed permutations
        """
        err = self.validate_input()
        if err is not None:
            raise err
        yield from self.solve_method()


    def validate_input(self):
        """
        checks if input data is correct
        :return: Exception if raised, None otherwise
        """
        try:
            n = int(self.get_input("n"))
            if n <= 0:
                raise ValueError("Input number must be non-zero positive")
        except KeyError as e:
            return e
        except ValueError as e:
            return e
        return None