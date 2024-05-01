import unittest

from utils.custom_sort import CustomSort


class TestCustomSort(unittest.TestCase):
    def test_by_insertion(self):
        input = [5,1,4,2,3]
        assert(CustomSort.by_insertion(input)==[1,2,3,4,5])
        assert(CustomSort.by_insertion(input,reverse=True)==[5,4,3,2,1])
        assert(CustomSort.by_insertion([])==[])
        assert(CustomSort.by_insertion([3,2,1])==[1,2,3])

        input = [(1,0), (2,5), (4,7), (5,3), (4,2), (3,3)]
        assert (CustomSort.by_insertion(input, key=lambda x: x[0]*x[0]+x[1]*x[1])
                == [(1, 0), (3, 3), (4, 2), (2, 5), (5, 3), (4, 7)])

        assert (CustomSort.by_insertion(input, cmp=lambda x, y: x[0] < y[0] or (x[0] == y[0] and x[1] < y[1]))
                == [(1, 0), (2, 5), (3, 3), (4, 2), (4, 7), (5, 3)])




    def test_by_combo(self):
        input = [5, 1, 4, 2, 3]
        assert (CustomSort.by_combo(input) == [1, 2, 3, 4, 5])
        assert (CustomSort.by_combo(input,reverse=True) == [5,4,3,2,1])
        assert (CustomSort.by_combo([]) == [])
        assert (CustomSort.by_combo([3,2,1]) == [1,2,3])

        input = [(1, 0), (2, 5), (4, 7), (5, 3), (4, 2), (3, 3)]
        assert(CustomSort.by_combo(input, cmp=lambda x, y: x[0] < y[0] or (x[0] == y[0] and x[1] < y[1]))
               == [(1, 0), (2, 5), (3, 3), (4, 2), (4, 7), (5, 3)])