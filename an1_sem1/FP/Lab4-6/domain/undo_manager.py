from dataclasses import dataclass



class UndoManager:

    def __init__(self,max_states_count = 10):
        self.states = []
        self.max_states_count = max_states_count

    def get_states_count(self):
        """
        :return: number of previous states
        """
        return len(self.states)

    def push_state(self,state):
        """
        adds a new state to the undo states list
        if the states count exceeds maximum, oldest states are discarded
        :param state: the state to add
        """
        if(self.get_states_count()<self.max_states_count):
            self.states.append(state)
        else:
            self.states.append(state)
            self.states.pop(0)

    def pop_state(self):
        """
        gets the last state and discards it
        :return: the last undo state
        """
        try:
            return self.states.pop()
        except(IndexError):
            raise IndexError("The undo states list is empty.")

    def last_state(self):
        """
        gets the last state without discarding it
        :return: the last undo state
        """
        try:
            return self.states[-1]
        except(IndexError):
            raise IndexError("The undo states list is empty.")