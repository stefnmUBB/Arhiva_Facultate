from data import Grammar, CanonicalCollection, RuleComponent, print_table, EndOfWord, RuleTerminal


class TableItem:
    def __init__(self, type:str, value:int):
        self.type:str = type
        self.value:int = value

    @staticmethod
    def Shift(value:int): return TableItem("s", value)

    @staticmethod
    def Reduce(value:int): return TableItem("r", value)

    @staticmethod
    def Accepted(): return TableItem("acc", -1)

    def is_shift(self): return self.type == "s"
    def is_reduce(self): return self.type == "r"
    def is_accepted(self): return self.type == "acc"

    def __eq__(self, other):
        if not isinstance(other, TableItem): return False
        return self.type==other.type and self.value==other.value

    def __str__(self): return self.type + (str(self.value) if self.value>=0 else "")

class AnalysisTable:
    def __init__(self, grammar:Grammar):
        self.grammar = grammar
        self.cc:CanonicalCollection = CanonicalCollection(grammar)
        self.table: dict[tuple[int, RuleComponent|EndOfWord], list[TableItem]] = {}

        # shift
        for state, sym in self.cc.transitions.keys():
            key = (state.id, sym)
            if key not in self.table: self.table[key] = []
            self.table[key].append(TableItem.Shift(self.cc.transitions[state, sym].id))

        # reduce/acc
        for state in self.cc.states:
            for elem in state.elems:
                if not elem.is_dot_at_end():
                    continue
                sym = elem.u_pred
                if isinstance(sym, EndOfWord) and elem.rule.lval == self.grammar.start_symbol:
                    key = (state.id, sym)
                    if key not in self.table: self.table[key] = []
                    self.table[key].append(TableItem.Accepted())
                elif isinstance(sym, RuleTerminal | EndOfWord):
                    key = (state.id, sym)
                    if key not in self.table: self.table[key] = []
                    r = TableItem.Reduce(elem.rule.id)
                    if r not in self.table[key]:
                        self.table[key].append(r)

    def __getitem__(self, item)->list[TableItem]:
        return self.table[item] if item in self.table else []

    def __str__(self):
        ids = list(set(map(lambda x:x[0], self.table.keys())))
        mx = max(ids+[1])
        lines = []

        non_terminals = self.grammar.non_terminals
        non_terminals.sort(key=lambda s:list(self.grammar.get_derivations_of(s))[0].id)

        symbols = non_terminals + self.grammar.terminals + [EndOfWord()]
        symbols.remove(self.grammar.start_symbol)

        for i in range(mx+1):
            l = [f"I{i}"]
            for _ in symbols: l.append("")
            lines.append(l)

        for key, item in self.table.items():
            row = key[0]
            col = 1+symbols.index(key[1])
            lines[row][col] += ", ".join(map(str,item))

        #lines = list(map(cvt_line, lines))

        s = print_table(lines, headers = [""]+list(map(str, symbols)), title="Analysis table")
        return s

