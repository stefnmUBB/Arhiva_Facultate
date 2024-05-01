import random
import string

NamesList = ["Gabriel", "Mircea", "Mihai", "Alexandru", "Vlad"]
SurnamesList = ["Popescu", "Ionescu","Ionascu","Vladimirescu","Tanasescu"]
GenresList = ["Action","Adventure","Comedy","Slice of Life", "Psychological","History","Military","Magic",
              "Supernatural","Romance","Drama"]

class Randomizer:
    @staticmethod
    def generate_chars(len,charset="0123456789"):
        return "".join(random.choice(charset) for i in range(len))

    @staticmethod
    def generate_random_name(max_len:int):
        return Randomizer.generate_chars(max_len,string.ascii_uppercase).capitalize()

    @staticmethod
    def choose(list):
        return str(random.choice(list))

    @staticmethod
    def number(left,right):
        return random.randint(left,right)