from math import floor

from utils.custom_sort import CustomSort


class Report:
    """
    Wrapper class for list manipulation
    """
    def __init__(self, el_list):
        self.elem_list=el_list

    def select(self,condition=lambda x:True,sortKey=lambda:1,sortReversed=False,
               cmp=None):
        """
        Selects elements from a list based on a condition & sorts them
        :param condition: condition selected elements meet
        :param sortKey: a comparable value used to sort elements
        :param sortReversed: True if sortkeys needs to be ordered from bigger to smaller
        :param cmp: comparator function
        :return: a new Report containing the desired list
        :rtype: Report
        """
        #rlist = sorted(filter(condition, self.elem_list), key=sortKey, reverse=sortReversed)
        rlist = CustomSort.perform(
            list(filter(condition, self.elem_list)),
            key=sortKey,
            reverse=sortReversed,
            cmp=cmp,
            method="insertion")
        return Report(rlist)


    def first(self,n):
        """
        Selects first n elements from the list
        :param n:
        :type: int
        :return: a new Report containing the desired list
        :rtype: Report
        :raises: ValueError if n<0
        """
        return self.first_rec(n)
        n=int(n)
        if(n<0):
            raise ValueError(f"Number of elements to select must be non-zero positive")
        if n>=len(self.elem_list):
            return Report(list)
        return Report(self.elem_list[:n])

    def first_rec(self,n):
        """
        recursive implementation of first
        :param: __ret_list: internal use only
        """
        n = int(n)
        if n < 0:
            raise ValueError(f"Number of elements to select must be positive")
        if n > len(self.elem_list):
            return self.first_rec(len(self.elem_list))
        if(n==0):
            return Report([])
        return Report(self.first_rec(n-1).elem_list+[self.elem_list[n-1]])


    def first_percent(self,p):
        """
        Selects first p% elements from the list
        :param p:
        :type: float
        :return: a new Report containing the desired list
        :rtype: Report
        """
        return self.first(floor(float(p) * len(self.elem_list) * 0.01))



