import unittest

from domain.participant import Participant
from domain.undo_manager import UndoManager
from test_utils import *

class TestParticipant(unittest.TestCase):

    def test_push(self):
        um = UndoManager(2)
        um.push_state(5)
        assert(um.last_state()==5)

        p = Participant(0,(1,1,1,1,1,1,1,1,1,1))
        um.push_state(p.clone())
        assert(um.last_state().equals(p))

        um.push_state("oveflow")
        assert(um.get_states_count()==um.max_states_count)

    def test_pop(self):
        um = UndoManager(3)
        um.push_state(3)
        um.push_state(2)
        um.push_state(1)
        assert(um.pop_state()==1)
        assert(um.pop_state()==2)
        assert(um.pop_state()==3)
        assert (getExceptionType(lambda: um.pop_state()) == IndexError)