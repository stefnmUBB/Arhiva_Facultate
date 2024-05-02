from data import Grammar, AnalysisTable, RuleComponent, TableItem, RuleTerminal, RuleNonTerminal, EndOfWord


class StateId:
    def __init__(self, _id:int):
        self.id:int = _id
    def __str__(self): return f"${self.id}"

class Config:
    def __init__(self, table:AnalysisTable, stack:list[RuleComponent|StateId], inputs:str, outputs:list[int]):
        self.table:AnalysisTable = table
        self.stack:list[RuleComponent|StateId] = stack
        self.inputs:str = inputs
        self.outputs: list[int] = outputs

    def get_next_action(self, s:RuleComponent) -> TableItem | None :
        last_id = self.stack[-1]
        next: list[TableItem] = self.table[last_id.id, s]
        if len(next) == 0: return None
        return next[0]

    def push(self, s:RuleComponent) -> TableItem | None :
        next = self.get_next_action(s)
        if next is None: return None
        if not next.is_shift(): return None
        self.stack += [s, StateId(next.value)]
        return next

    def pop(self, N: int) -> bool | None:
        if len(self.stack)<2*N+1: return None
        self.stack = self.stack[:-2*N]
        return True


    def do_shift(self) -> TableItem | None: # modifica config, return "s_i" sau None daca nu se poate
        c = RuleTerminal(self.inputs[0]) if len(self.inputs)>0 else EndOfWord()
        push_result = self.push(c)
        if push_result is None: return None
        self.inputs = self.inputs[1:]
        return push_result

    def do_reduce(self) -> TableItem | None: # modifica config, return "r_i" sau None daca nu se poate
        c = RuleTerminal(self.inputs[0]) if len(self.inputs)>0 else EndOfWord()
        next = self.get_next_action(c)
        if next is None: return None
        if not next.is_reduce(): return None
        rule = self.table.grammar.rules[next.value]
        pop_result = self.pop(len(rule.rval))
        if pop_result is None: return None
        push_result = self.push(rule.lval)
        if push_result is None:
            for s in rule.rval:
                self.push(s)
            return None
        self.outputs = [rule.id]+self.outputs
        return next

    def do_accept(self) -> TableItem | None:
        c = RuleTerminal(self.inputs[0]) if len(self.inputs) > 0 else EndOfWord()
        next = self.get_next_action(c)
        if next is None: return None
        if not next.is_accepted(): return None
        return next

    def next(self):
        r = self.do_shift()
        if r is not None: return str(r)
        r = self.do_reduce()
        if r is not None: return str(r)
        r = self.do_accept()
        if r is not None: return str(r)
        return "err"

    def __str__(self):
        return f"({''.join(map(str, self.stack))}, {self.inputs}$, {' '.join(map(str, self.outputs))})"


class ParserLR1:
    def __init__(self, grammar: Grammar):
        self.grammar: Grammar = grammar
        self.table: AnalysisTable = AnalysisTable(grammar)

    def print_analysis_table(self): print(self.table)

    def test_for_conflicts(self):
        for v in self.table.table.values():
            if len(v) > 1:
                raise ValueError(f"Grammar has conflicts: {list(map(str, v))}")


    def parse(self, text: str):
        s0 = Config(self.table, [StateId(0)], text, []) # (R0, text...$, {})

        while True:
            print(str(s0).replace("\n","\\n").ljust(50), end=" ⊢ ")
            r = s0.next()
            print(r)
            if r=="acc" or r=="err":
                break
