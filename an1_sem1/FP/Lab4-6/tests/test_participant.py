####################################################################
#                           Participant
####################################################################

import random
import unittest
from domain.participant import Participant
from test_utils import *

class TestParticipant(unittest.TestCase):

    def test_create(self):
        # expected creation
        code = 5
        scores = (1,2,3,4,5,6,7,8,9,10)
        part = Participant(code, scores)
        assert(part.get_code() == code and part.get_scores() == scores)

        # ill-formed creation attempts

        scores = (1,2,3)
        eType=getExceptionType(lambda: Participant(code,scores))
        assert(eType==ValueError)

        scores = (1, 2, 3, 4, 500, 6, 7, 8, 9, 10)
        eType = getExceptionType(lambda: Participant(code, scores))
        assert(eType==ValueError)

    def test_getters(self):
        #p=self.random_participant()
        scores=(1,2,2,2,2,5,6,9,10,10)
        p=Participant(5,scores)
        p.set_index(3)
        assert(p.get_scores()==scores)
        assert(p.get_code()==5)
        assert(p.get_index()==3)

    def test_setters(self):
        p=self.random_participant()
        p.set_code(5)
        assert(p.get_code()==5)
        p.set_index(0)
        assert(p.get_index()==0)
        scores=(1,2,3,4,5,6,7,8,9,10)
        p.set_scores(scores)
        assert(p.get_scores()==scores)

        scores=(11,0,0,0,0,0,0,0,0)
        assert(getExceptionType(lambda:p.set_scores(scores))==ValueError)
        scores = (9,10)
        assert (getExceptionType(lambda: p.set_scores(scores)) == ValueError)
        scores = "definitely not a tuple"
        assert (getExceptionType(lambda: p.set_scores(scores)) == TypeError)

    def test_overall_score(self):
        scores = (1, 2, 3, 4, 5, 6, 7, 8, 9, 10)
        part = Participant(1, scores)
        assert (part.overall_score() == 10 * 11 / 2)

        scores = (10, 10, 10, 10, 10, 10, 10, 10, 10, 10)
        part = Participant(1, scores)
        assert (part.overall_score() == 100)

    def test_change_score(self):
        scores = (1, 2, 3, 4, 5, 6, 7, 8, 9, 10)
        part = Participant(1, scores)
        part.change_score(1,7)
        assert (part.get_scores() == (7, 2, 3, 4, 5, 6, 7, 8, 9, 10))

    def random_participant(self):
        code = random.randrange(1,100)
        index = random.randrange(1,100)
        scores = tuple(random.randrange(1, 10) for t in range(10))
        p = Participant(code,scores)
        p.set_index(index)
        return p

    def test_clone(self):
        for i in range(10):
            p=self.random_participant()
            c=p.clone()
            assert(c.equals(p))

    def test_equals(self):
        p = self.random_participant()
        q = Participant(p.get_code(),p.get_scores())
        q.set_index(p.get_index())
        assert(p.equals(q))
        q.set_index("sth different")
        assert(not p.equals(q))


if __name__ == '__main__':
    unittest.main()