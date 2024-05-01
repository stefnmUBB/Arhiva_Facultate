from collections import Callable
from dataclasses import dataclass

import regex as regex

from service.parse_error import ParseError


@dataclass
class Command:
    func: Callable
    pattern: str = ""
    description: str = ""

    def _parse(self, line):
        """
        scans string to identify the command arguments using the pattern
        :param line: string to parse
        :type: str
        :return: list of arguments
        :rtype: list
        """
        tokens = self.pattern.split()
        inputs = [p for p in regex.split("( |\\\".*?\\\"|'.*?')", line) if p.strip()]

        if len(tokens) != len(inputs):
            return None

        args = []
        for i in range(len(tokens)):
            token = tokens[i]
            inp = inputs[i]
            if token == '%s':
                if inp[0] != '"':
                    args.append(inp)
                else:
                    if inp[-1] != '"':
                        return None
                    args.append(inp[1:-1])
            elif token == '%i':
                try:
                    args.append(int(inp))
                except:
                    return None
            else:
                if token != inp:
                    return None
        return args

    def call(self, line):
        """
        Executes the given command line according to the pattern and function
        :param line: command text to be executed
        :raises: ValueError if cannot cannot be parsed
        """
        args = self._parse(line)
        if args is None:
            raise ParseError("Command cannot be executed")
        self.func(*args)

    def try_call(self, line):
        """
        Tries to execute a command line text.
        :param line: command text to be executed
        :return: True if execution completes, False otherwise
        """
        try:
            self.call(line)
            return True
        except ParseError:
            return False
