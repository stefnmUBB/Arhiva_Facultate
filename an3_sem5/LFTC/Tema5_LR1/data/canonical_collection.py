from __future__ import annotations
from data import AnalysisElement, Grammar, RuleNonTerminal, EndOfWord, RuleTerminal, RuleComponent


class State:
    def __init__(self, grammar:Grammar, id: int, elems: list[AnalysisElement]):
        self.grammar = grammar
        self.id = id
        self.elems: list[AnalysisElement] = elems
        self.closure: list[AnalysisElement] = list(elems)

    def build_closure(self):
        tmp_elems: list[AnalysisElement] = list(self.elems)
        new_elems: list[AnalysisElement] = []

        while True:
            for elem in tmp_elems:
                next = elem.get_after_dot()
                if not isinstance(next, RuleNonTerminal): continue

                beta = self.grammar.first1_seq(elem.rule.rval[elem.dot + 1:])
                if len(beta) == 0:
                    beta:list[any] = [elem.u_pred]
                #print(list(map(str, beta)))

                for rule in self.grammar.get_derivations_of(next):
                    for b in beta:
                        a_elem = AnalysisElement(rule, 0, b)

                        exists = False
                        for existing_elem in tmp_elems:
                            if existing_elem == a_elem:
                                exists=True
                        if not exists:
                            new_elems.append(a_elem)
            tmp_elems += new_elems
            tmp_elems = list(set(tmp_elems))

            if len(new_elems) == 0:
                break
            new_elems = []

        self.closure = tmp_elems


    def goto(self, t:RuleNonTerminal | RuleTerminal):
        elems = list(filter(lambda a:a.get_after_dot()==t, self.closure))
        return State(self.grammar, -1, list(map(lambda e:e.advance_dot(), elems)))

    def get_transitions(self):
        # filter(None, x) -> elementele din x care nu sunt None
        # filter(lambda t:t is not None,x)
        # S->[A B C . , $]
        return list(set(filter(None, map(lambda x:x.get_after_dot(), self.closure))))


    def __str__(self):
        s = f"State I{self.id}"
        for e in self.closure:
            s += "\n  "+str(e)
        return s


    def is_equivalent(self, other: State) -> bool:
        for x in self.elems:
            if x not in other.elems:
                return False
        for x in other.elems:
            if x not in self.elems:
                return False
        return True

class CanonicalCollection: # graful
    def __init__(self, grammar:Grammar):
        self.grammar:Grammar  = grammar

        i0 = State(grammar, 0, [AnalysisElement(grammar.rules[0])])
        i0.build_closure()

        self.transitions: dict[tuple[State, RuleComponent], State] = {}

        self.states:list[State] = [i0]
        self.__state_id = 1
        self.discover()

        #i1 = State(grammar, 0, [AnalysisElement(grammar.rules[1])])
        #print(self.get_equivalent_state(i1))


    def get_equivalent_state(self, new_state: State):
        for s in self.states:
            if s.is_equivalent(new_state):
                return s
        return None


    def discover(self): #bfs
        new_states = []
        while True:
            for state in self.states:
                for sym in state.get_transitions():
                    #print(f"From I{state.id} with {sym}")
                    # tranzitia ++
                    new_state = state.goto(sym)
                    eq_state = self.get_equivalent_state(new_state)
                    if eq_state is None:
                        new_state.id = self.__state_id
                        self.__state_id += 1
                        new_states.append(new_state)
                        new_state.build_closure()
                    else:
                        new_state = eq_state
                    self.transitions[state, sym] = new_state
            if len(new_states) == 0:
                break
            self.states += new_states
            new_states = []

    def __str__(self):
        s = ""
        for state in self.states:
            s+=str(state)+"\n"
        for k, v in self.transitions.items():
            s += f"  (I{k[0].id},{k[1]}) --> I{v.id}\n"
        return s