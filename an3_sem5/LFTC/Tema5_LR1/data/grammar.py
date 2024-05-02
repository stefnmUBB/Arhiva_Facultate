from __future__ import annotations

from data.rule import Rule, RuleNonTerminal, RuleTerminal, RuleComponent
from data.table import *

class Prediction1Table:
    def __init__(self, values: dict[RuleNonTerminal, list[RuleTerminal]]):
        self.values: dict[RuleNonTerminal, list[RuleTerminal]] = values

    def __getitem__(self, key): return self.values[key]

    def items(self): return self.values.items()

    def __iter__(self): return iter(self.values)

    def __str__(self):
        lines = list(map(lambda x:[x[0], ", ".join(map(str, x[1]))], self.values.items()))
        return print_table(lines, headers=["A", "FIRST1(A)"], title="FIRST1 table")


class Grammar:
    def __init__(self, rules: list[Rule]):
        self.rules = rules

        for i in range(len(self.rules)):
            self.rules[i].id = i

        if len(self.rules) == 0: raise ValueError('Grammar with no rules are not allowed')
        self.start_symbol: RuleNonTerminal = self.rules[0].lval
        self.non_terminals: list[RuleNonTerminal] = list(set(map(lambda r: r.lval, self.rules)))
        self.terminals: list[RuleTerminal] \
            = list(filter(lambda x:isinstance(x, RuleTerminal), set(sum(map(lambda r: r.rval, self.rules),[]))))
        self.memo_first1 = None

        rhs_terms = set(sum(map(lambda r: r.rval, self.rules),[]))
        rhs_terms = filter(lambda x: isinstance(x, RuleNonTerminal), rhs_terms)

        # check if there is a right hand side nonterminal that was not defined
        rhs_terms = list(filter(lambda x: not (x in self.non_terminals), rhs_terms))
        if len(rhs_terms)>0:
            raise ValueError(f"Nonterminals not defined: {', '.join(set(map(str,rhs_terms)))}")


    @staticmethod
    def parse(lines: list[str]) -> Grammar:
        lines = list(filter(lambda x: not x.startswith("#"), lines))
        rules = list(map(Rule.parse, lines))
        return Grammar(rules)

    @staticmethod
    def from_file(path:str) -> Grammar:
        with open(path, 'r', encoding='utf-8') as f:
            lines = (line.rstrip() for line in f)
            lines = [line for line in lines if line]
            return Grammar.parse(lines)

    def enrich(self, start_symbol_name: str) -> Grammar:
        new_rule = Rule(RuleNonTerminal(start_symbol_name), [self.start_symbol])
        return Grammar([new_rule] + self.rules)

    def get_derivations_of(self, n: RuleNonTerminal):
        return filter(lambda r: r.lval==n, self.rules)

    def first1(self) -> Prediction1Table:
        starts_with_terminal = lambda r: isinstance(r.rval[0], RuleTerminal)
        f0 = {}
        for A in self.non_terminals:
            derivations = set(filter(starts_with_terminal, self.get_derivations_of(A)))
            f0[A] = list(map(lambda r: r.rval[0], derivations))

        while True:
            f1 = {}
            for A in self.non_terminals:
                first1_A: list[RuleTerminal] = []
                for d in self.get_derivations_of(A):
                    for s in d.rval:
                        if isinstance(s, RuleTerminal):
                            if len(s.value) > 0:
                                first1_A.append(RuleTerminal(s.value[0]))
                                break
                            else: # s is epsilon ""
                                first1_A.append(s)
                        elif isinstance(s, RuleNonTerminal):
                            first1_A += f0[s]
                            if not RuleTerminal.epsilon() in f0[s]:
                                break
                        else:
                            raise AssertionError("Should not be here!")
                f1[A] = list(set(first1_A))

            f_equal = True
            for A in self.non_terminals:
                l0 = f0[A]
                l1 = f1[A]

                for x in l0:
                    if not x in l1:
                        f_equal=False
                        break
                for x in l1:
                    if not x in l0:
                        f_equal=False
                        break
                if not f_equal: break

            del f0
            f0 = f1
            if f_equal: break

        self.memo_first1 = Prediction1Table(f0)
        return self.memo_first1

    def first1_seq(self, seq: list[RuleComponent]):
        if self.memo_first1 is None:
            self.first1()

        first1_S=[]
        for s in seq:
            if isinstance(s, RuleTerminal):
                if len(s.value) > 0:
                    first1_S.append(RuleTerminal(s.value[0]))
                    break
                else:  # s is epsilon ""
                    first1_S.append(s)
            elif isinstance(s, RuleNonTerminal):
                first1_S += self.memo_first1[s]
                if not RuleTerminal.epsilon() in self.memo_first1[s]:
                    break
            else:
                raise AssertionError("Should not be here!")
        return list(set(first1_S))

    def __str__(self):
        def mstr(r: Rule) -> str: return f"{str(r).ljust(50)} ({r.id})"
        return "\n".join(map(mstr, self.rules))
