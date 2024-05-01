####################################################################
#                        Participants List
####################################################################
import random
import unittest
from domain.participantslist import ParticipantsList
from test_utils import *
from tests.test_participant import TestParticipant


class TestParticipantsList(unittest.TestCase):

    def dummyList(self):
        """
        :returns sample list for testing purposes
        """
        list=ParticipantsList()
        list.add_participant_by_score((1, 1, 2, 1, 1, 3, 1, 9, 1, 1))
        list.add_participant_by_score((1, 1, 1, 6, 1, 5, 1, 4, 1, 2))
        list.add_participant_by_score((1, 1, 10, 1, 7, 10, 8, 1, 5, 2))
        list.add_participant_by_score((10, 10, 10, 10, 10, 10, 10, 10, 10, 10))
        return list

    def test_create(self):
        plist = self.dummyList()

        assert (plist.count() == 4)
        # use overall score as a checksum to see if every element generated correctly
        assert(plist[0].overall_score() == 21)
        assert(plist[1].overall_score() == 23)
        assert(plist[2].overall_score() == 46)
        assert(plist[3].overall_score() ==100)

        del plist

    def test_average(self):
        plist = self.dummyList()

        # expected execution
        assert(cpReal(plist.average(1, 4), 47.5))
        assert(cpReal(plist.average(2, 3), 34.5))
        assert(cpReal(plist.average(1, 3), 30))
        assert(cpReal(plist.average(2, 4), 56.3333333))
        assert(cpReal(plist.average(1, 1), 21))

        plist.remove(3)
        assert (cpReal(plist.average(1, 3), 48))

        # ill-formed input attempts
        eType = getExceptionType(lambda: plist.average(1.2,3))
        assert (eType == TypeError)

        eType = getExceptionType(lambda: plist.average(-1, 15))
        assert (eType == ValueError)

        del plist

    def test_min(self):
        plist = self.dummyList()

        # expected execution
        assert (cpReal(plist.min(1, 4), 21))
        assert (cpReal(plist.min(2, 3), 23))
        assert (cpReal(plist.min(1, 3), 21))
        assert (cpReal(plist.min(3, 4), 46))
        assert (cpReal(plist.min(1, 1), 21))

        # ill-formed input attempts
        eType = getExceptionType(lambda: plist.min(1.2, 3))
        assert (eType == TypeError)

        eType = getExceptionType(lambda: plist.min(-1, 15))
        assert (eType == ValueError)

        del plist

    def test_remove(self):
        plist = self.dummyList()

        plist.remove(2)
        assert(plist.count()==3)
        assert(plist[0].get_index()==1 and plist[0].get_code()==1)
        assert(plist[1].get_index()==2 and plist[1].get_code()==3)
        assert(plist[2].get_index()==3 and plist[2].get_code()==4)

        plist.remove(1)
        assert(plist.count()==2)
        assert (plist[0].get_index() == 1 and plist[0].get_code() == 3)
        assert (plist[1].get_index() == 2 and plist[1].get_code() == 4)

        plist.remove(2)
        assert (plist.count() == 1)
        assert (plist[0].get_index() == 1 and plist[0].get_code() == 3)

        eType = getExceptionType(lambda: plist.remove(2.5))
        assert (eType == TypeError)

        eType = getExceptionType(lambda: plist.remove(2))
        assert (eType == ValueError)

        del plist

    def test_remove_range(self):
        plist = self.dummyList()
        plist.remove_range(2,3)
        assert(plist.count()==2)
        assert(plist[0].get_code()==1 and plist[1].get_code()==4)

    def test_insert(self):
        plist = self.dummyList()
        scores=(1,1,1,1,1,1,1,1,1,1)
        plist.insert_participant(10,scores)
        assert(plist.count()==5)
        assert(getExceptionType(lambda:plist.insert_participant(10,scores))==ValueError)

    def get_random_list(self,elcount=100):
        list = ParticipantsList()
        for i in range(0,elcount):
            scores = tuple(random.randrange(1, 10) for t in range(10))
            list.add_participant_by_score(scores)
        return list

    def test_filter_multiple(self):
        plist = self.dummyList()
        plist.filter(lambda p:p.overall_score()%2==0)
        assert(plist.count()==2)

        plist = self.dummyList()
        plist.filter(lambda p: p.overall_score() % 5 == 0)
        assert (plist.count() == 1)

        for it in range(30):
            size = random.randrange(10,300)
            val = random.randrange(1,100)
            plist = self.get_random_list(size)
            plist.filter(lambda p:p.overall_score()%val==0)
            for p in plist:
                assert(p.overall_score()%val==0)


    def test_filter_less_than(self):
        for it in range(30):
            size = random.randrange(10, 300)
            val = random.randrange(1, 100)
            plist = self.get_random_list(size)
            plist.filter(lambda p: p.overall_score() <= val)
            for p in plist:
                assert (p.overall_score() <= val)

    def test_clone(self):
        plist = self.get_random_list()
        clone = plist.clone()
        assert(plist.count()==clone.count())
        for i in range(plist.count()):
            p0 = plist.participants[i]
            p1 = clone.participants[i]
            assert(p0.equals(p1))
        plist.set_list([])
        assert(plist.count()== 0)
        assert(clone.count()!=0)


    def test_set_list(self):
        ptest = TestParticipant()
        list = [ptest.random_participant() for i in range(5)]
        plist = ParticipantsList()
        plist.set_list(list)
        assert(plist.count()==5)
        list = []
        plist.set_list(list)
        assert(plist.count()==0)
        list= [5]
        assert(getExceptionType(lambda:plist.set_list(list))==TypeError)

    def test_get_participant_by_code(self):
        plist = self.dummyList()
        part = plist.get_participant_by_code(3)
        assert(part.get_index()==3)
        plist.remove(2)
        part = plist.get_participant_by_code(4)
        assert(part.get_index() == 3)
        assert (getExceptionType(lambda: plist.get_participant_by_code(10)) == ValueError)

if __name__ == '__main__':
    unittest.main()