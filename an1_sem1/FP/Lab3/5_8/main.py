import os

base_list = []  # the (global) input

def findLongestSubsequence(hasProperty):
    """
    find longest subsequence of base_list based on the result of a bool function
    :param hasProperty: a function of 3 parameters (pos, len, elem), where
                        pos = first element of the current found subsequence
                        len = the current length of this subsequence
                        elem = the element to check
                        Note: if the function name ends with "countOnFalseReturn",
                        the routine's behavior changes. Use "countOnFalseReturn"
                        if the result of hasProperty depends on the on more than
                        element of the subsequence (if function uses arguments pos & len)
    :return: a copy of the desired subsequence
    """

    resStart = resLength = 0
    crtStart = crtLength = 0
    for i in range(0, len(base_list)):
        if hasProperty(crtStart, crtLength, base_list[i]):
            crtLength += 1
        else:
            if crtLength > resLength:
                resStart = crtStart
                resLength = crtLength
            if hasProperty.__name__.endswith("countOnFalseReturn"):
                crtLength = 1
                crtStart = i
            else:
                crtLength = 0
                crtStart = i+1
    if crtLength > resLength:
        resStart = crtStart
        resLength = crtLength
    return base_list[resStart:resStart + resLength]


class Operation:
    """
    UI component described by a label (text) the user will see on screen
    and an action to be executed when that option is selected.
    """
    text = ""
    func = lambda: print("")
    init = lambda: None # use this in case precomputation is required

    def __init__(self, _text, _func, _init=lambda: True):
        self.text = _text
        self.func = _func
        self.init = _init
        return


def checkInput():
    """
    serves as init function for the operation 1 (read a numbers list).
    Prints messages depending on the result
    :return: True is numbers list is not empty, false otherwise
    """
    if len(base_list) == 0:
        print("Numbers list empty. Please input a list of numbers.")
        return False
    print(f"Executing operation on list:\n  {base_list}")
    return True


def opReadList():
    """
    operation function 1 - read numbers separated by space from input
    :return: list of numbers
    """
    global base_list
    base_list = [int(x) for x in input("Enter the numbers on one line, separated by spaces:\n").split()]
    return

def lmbAllEqual__countOnFalseReturn(pos,len,elem):
    """
    lambda function to be used by findLongestSubsequence
    used to find the longest subsequence with equal terms
    :param pos: start of subsq
    :param len: length of subsq
    :param elem: the element to check
    :return: True if the current element equals th first element of the subsequence used as a sample
    """
    return base_list[pos]==elem

def lmbBetween0_10(pos,len,elem):
    """
    lambda function to be used by findLongestSubsequence
    used to find the longest subsequence of terms between 0 and 10
    :param pos: not used
    :param len: not used
    :param elem: the element to check
    :return: True if elem in 0..10, false otherwise
    """
    return 0<=elem<=10


def opSolveQ2():
    """
    Operation 2 function
    :return:
    """
    print("Result:\n")
    print(findLongestSubsequence(lmbAllEqual__countOnFalseReturn))
    return


def opSolveQ3():
    """
    Operation 3 function
    :return:
    """
    print("Result:\n")
    print(findLongestSubsequence(lmbBetween0_10))
    return


"""
Dictionary of the form id->operation
user input is compared to the id to execute the corresponding operation
"""
menuOptions = {
    1: Operation("Read a numbers list", opReadList),
    2: Operation("Find the longest subsequence of equal numbers", opSolveQ2, checkInput),
    3: Operation("Find the longest subsequence of numbers between 0 and 10", opSolveQ3, checkInput),
    4: Operation("Exit", exit),
}


def runMenu():
    """
    Driver function to read input and execute operations
    :return:
    """
    for key, op in menuOptions.items():
        print(f"{key}. {op.text}")
    try:
        user_input = int(input("Enter your operation id : "))
        if not (user_input in menuOptions.keys()):
            raise  ValueError("Not a valid id.") # raise exception
    except ValueError as e:
        print(e)
        return

    if menuOptions[user_input].init():
        menuOptions[user_input].func()


if __name__ == '__main__':
    while True:
        os.system("clear")
        os.system("cls") # windows, linux ?
        runMenu()
        print("\nPress Enter to continue...")
        input()
