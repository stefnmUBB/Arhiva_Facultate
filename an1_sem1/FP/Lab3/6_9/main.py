import os

base_list = []  # the (global) input

def findDistinct():
    """
    function to find the longest sequence of distinct numbers in base_list
    :return: a sublist of all distinct elements
    """
    freq0=[]
    crtpos=crtlen=0
    respos=reslen=0

    for i in range(len(base_list)):
        if not (base_list[i] in dict):
            freq0.append((base_list[i]))
            crtlen+=1
        else:
            if crtlen>reslen:
                respos=crtpos
                reslen=crtlen
            freq0.clear()
            freq0.append(base_list[i])
            crtpos=i
            crtlen=1
    if crtlen > reslen:
        respos = crtpos
        reslen = crtlen
    return base_list[respos:respos + reslen]


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

def solveQ2():
    """
       Operation 2 function
       :return:
       """
    print("Result:\n")
    print(findDistinct())
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


"""
Dictionary of the form id->operation
user input is compared to the id to execute the corresponding operation
"""
menuOptions = {
    1: Operation("Read a numbers list", opReadList),
    2: Operation("Find the longest subsequence of distinct numbers", solveQ2, checkInput),
    4: Operation("Exit", exit)
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
            x = 0/0 # raise exception
    except:
        print("Not a valid id.")
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
