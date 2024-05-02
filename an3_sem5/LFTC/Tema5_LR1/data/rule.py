from __future__ import annotations

import re


class RuleComponent:
    def __init__(self, _type: str, value: str):
        self.type = _type
        self.value = value

    def __eq__(self, other):
        if not isinstance(other, RuleComponent):
            return False
        return self.type == other.type and self.value == other.value

    def __hash__(self) -> int:
        return hash(self.type+":"+self.value)


class RuleTerminal(RuleComponent):
    def __init__(self, value: str):
        super().__init__("terminal", value)

    def __str__(self):
        return "ε" if self.value=="" else str(self.value.encode("unicode_escape"))[1:]

    @staticmethod
    def epsilon() -> RuleTerminal:
        return RuleTerminal("")


class RuleNonTerminal(RuleComponent):
    def __init__(self, value: str):
        super().__init__("nonterminal", value)

    def __str__(self): return self.value


class Rule:
    def __init__(self, lval:RuleNonTerminal, rval: list[RuleComponent]):
        self.lval: RuleNonTerminal = lval
        self.rval: list[RuleComponent] = rval
        self.id: int = -1

    def __str__(self):
        return f"{str(self.lval)} -> {' '.join(map(str, self.rval))}"

    def derives_epsilon(self) -> bool:
        return len(self.rval)==1 and isinstance(self.rval[0], RuleTerminal) and self.rval[0].value == ""

    @staticmethod
    def parse(text: str) -> Rule:
        items = text.split()
        if len(items)<3: raise IOError(f"Invalid rule: '{text}'")
        if items[1] != "->": raise IOError(f"Expected '->', got {items[1]}")

        def item2comp(it:str) -> [RuleComponent]:
            if it == "ε":
                return [RuleTerminal.epsilon()]
            if it.startswith('"'):
                if not it.endswith('"'): raise IOError(f"Invalid token: '{it}'")
                it = it[1:-1]\
                    .replace("\\s", " ")\
                    .replace("\\n", "\n")\
                    .replace("\\\"", "\"")
                if len(it) == 0:
                    return [RuleTerminal(it)]
                return list(map(lambda x:RuleTerminal(x), it))
                #if len(it) > 1: raise IOError(f"Terminal too long: '{it}'")
                #return RuleTerminal(it)
            if not re.match(r'^[A-Za-z0-9_]+$', it): raise IOError(f"Invalid token: '{it}'")
            return [RuleNonTerminal(it)]

        lval = item2comp(items[0])[0]
        if not isinstance(lval, RuleNonTerminal):
            raise IOError("Nonterminal expected before '->'")

        rval = list(map(item2comp, items[2:]))
        return Rule(lval, sum(rval,[]))
