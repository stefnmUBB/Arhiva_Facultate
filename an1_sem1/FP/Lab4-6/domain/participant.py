"""
    module : participant
"""
#from dataclasses import dataclass

#@dataclass
class Participant:
    #scores:list = []
    #code:int
    #index:int

    def __init__(self, _code:int, _scores:tuple):
        """
        creates a new participant with its code and score attributes
        :param _code: participant's ID number
        :type _code: int
        :param _scores: participant's scores
        :type _scores: tuple
        :returns instance of Participant
        :type Participant
        """
        self.set_scores(_scores)
        self.set_code(_code)
        self.set_index(0)

    def get_index(self): return self.index2

    def get_scores(self): return self.scores2

    def get_code(self): return self.code2

    def set_scores(self, scores):
        """
        Change all of the participant's scores
        :param scores: 10-uple representing the new scores
        :raises TypeError if scores if not a tuple
        :raises ValueError if scores is not a 10-length tuple with 1..10 values
        """
        if not (type(scores) is tuple):
            raise TypeError("Scores property must be a tuple")
        if len(scores) != 10:
            raise ValueError("There must be 10 scores for a participant")
        if not all(i in range(1, 11) for i in scores):
            raise ValueError("Every score's value must be a number from 0 to 10.")
        self.scores2 = scores

    def set_index(self,index):
        """
        sets participant index
        :param index: the new value
        """
        self.index2 = index

    def set_code(self,code):
        """
        sets participant code
        :param code: the new value
        """
        self.code2 = code

    def overall_score(self):
        """
        :return: sum of all scores (1..100)
        :type: int
        """
        return sum(self.get_scores())

    def change_score(self,problem,score):
        """
        Change problem's score
        :param problem: problem affected
        :type int
        :param score: new score
        :type int
        :raises TypeError if there is arguments type mismatch
        :raises ValueError if either problem index or scores is not correct
        """
        if (type(problem) is not int) or (type(score) is not int):
            raise TypeError("Problem index and score must be numbers.")
        if problem<1 or problem>10:
            raise ValueError("Problem number must be between 1 and 10.")
        if score<1 or score>10:
            raise ValueError("Score must be between 1 and 10.")
        scList = list(self.get_scores())
        scList[problem-1] = score
        self.set_scores(tuple(scList))

    def clone(self):
        """
        Clones the properties of Participant into a new object
        :return: the Participant's clone
        """
        result = Participant(self.get_code(),self.get_scores())
        result.set_index(self.get_index())
        return result

    def equals(self, part):
        """
        compares this participant's values with another participant instance
        :param part: the participant to compare to
        :return: true if all the properties are identical
        """
        return (self.get_code(), self.get_index(), self.get_scores())== \
               (part.get_code(), part.get_index(), part.get_scores())
