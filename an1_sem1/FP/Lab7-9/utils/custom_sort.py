import math


class CustomSort:
    @staticmethod
    def by_insertion(items:list,key=lambda t: t, reverse=False, cmp = None):
        """
        sorts list by insertion
        if cmp is defined, it will be used as sort condition.
        Otherwise, key is the default comparison term
        :param items: items list
        :type: list
        :param key: function sort key f:element->key, e1<e2 iff f(e1)<f(e2)
        :param cmp: function sort f:elementxelement->bool, f(x,y)=true if x<y
        :param reverse: True if sort must be reversed
        :return: new sorted list
        :rtype: list
        """
        def compare(item1,item2): # compare detector
            if cmp is None:
                return key(item1)>key(item2)
            return not cmp(item1,item2)

        result = [item for item in items]
        for i in range(1,len(result)):
            j = i
            while j>0 and compare(result[j-1],result[j]): #key(result[j-1])>key(result[j]):
                result[j - 1], result[j] = result[j], result[j-1]
                j -= 1
            i += 1
        if reverse:
            result.reverse()
        return result

    @staticmethod
    def by_combo(items:list,key=lambda t: t, reverse=False, cmp = None):
        """
        sorts list by combo sort
        if cmp is defined, it will be used as sort condition.
        Otherwise, key is the default comparison term
        :param items: items list
        :type: list
        :param key: function sort key f:element->key, e1<e2 iff f(e1)<f(e2)
        :param reverse: True if sort must be reversed
        :return: new sorted list
        :rtype: list
        """

        def compare(item1,item2): # compare detector
            if cmp is None:
                return key(item1)>key(item2)
            return not cmp(item1,item2)

        result = [item for item in items]
        gap = len(result)
        shrink = 1.3
        sorted = False
        while not sorted:
            gap = math.floor(gap/shrink)
            if gap <= 1:
                gap = 1
                sorted = True
            i = 0
            while i+gap<len(result):
                if compare(result[i],result[i+gap]):#key(result[i])>key(result[i+gap]):
                    result[i], result[i+gap] = result[i+gap], result[i]
                    sorted = False
                i += 1
        if reverse:
            result.reverse()
        return result

    @staticmethod
    def perform(items:list, key=lambda t: t, reverse=False, cmp=None, method="combo"):
        """
       sorts list by a chosen method
       :param items: items list
       :type: list
       :param key: function sort key f:element->key, e1<e2 iff f(e1)<f(e2)
       :param reverse: True if sort must be reversed
       :type: bool
       :param method: sort method ("insertion" or "combo")
       :type: str
       :return: new sorted list
       :rtype: list
       """
        if method== "combo":
            return CustomSort.by_combo(items,key,reverse,cmp)
        if method== "insertion":
            return CustomSort.by_insertion(items, key, reverse,cmp)
        raise ValueError(f"No sort algorithm called {method} exists.")