from domain.participant import Participant


class ParticipantsList:

    #participants = None
    #table_printer = None

    def __init__(self):
        self.participants = []

    def __getitem__(self, index):
        """
        shortcut: allow for partipants_list[index] instead of
                  participants_list.participants[i+1]
        :param index: the participant's index in raw list (NOT code! : code = index + 1)
        :type index: int
        :returns the participant at position index in list
        :type Participant
        """
        return self.participants[index]

    def add_participant_by_score(self, scores:tuple):
        """
        adds a new participant with a new code at the end of participants list
        :param scores: 10-uple meaning the scores for each problem
        """
        code = self.participants[-1].get_code()+1 if len(self.participants)!=0 else 1
        participant = Participant(code,scores)
        participant.set_index(len(self.participants)+1)
        self.participants.append(participant)
        return participant # for feedback purposes

    def insert_participant(self, code, scores):
        """
        inserts a new participant with given code and scores to the list
        :param code: participant's code
        :type code: int
        :param scores: participant's score
        :raises ValueError if participant code already exists in list
        """
        for part in self.participants:
            if part.get_code() == code:
                raise ValueError("Participant code already exists.")
        participant = Participant(code, scores)
        participant.set_index(len(self.participants) + 1)

        # naive lower bound to find where to insert the new participant
        index=self.count()
        for i in range(self.count()):
            if self.participants[i].get_code()>code:
                index=i
                break
        self.participants.insert(index,participant)

        # adjust indices for the rest of participants
        for i in range(index,self.count()):
            self.participants[i].set_index(i+1)

        return participant  # for feedback purposes


    def filter(self,condition = lambda x:True):
        """
        creates a new list of existing participants that satisfy the given condition
        :param condition: function : Particpant -> Bool
        :return:
        """
        self.participants = list(filter(condition,self.participants))
        for i,participant in enumerate(self.participants):
            participant.set_index(i+1)

    def print(self,table_printer, condition = lambda x:True, sortKey = lambda x:x.get_code(),sortReversed = False):
        """
        prints the list of participants following a certain condition and in a specified order
        :param table_printer: list printing handler
        :type table_printer: TablePrinter
        :param condition: property that all selected participants must satisfy
        :type condition: lambda : Participant -> Bool
        :param sortKey: rule to sort selected participants after
        :type sortKey: lambda : Participant -> <toCompareObject>
        :param sortReversed: False = Ascending, True = Descending
        :type sortReversed: Bool
        """
        list = sorted(filter(condition,self.participants),key = sortKey,reverse = sortReversed)
        table_printer.dataset = list
        table_printer.print_table()

    def _check_index(self,index):
        """
            :param code: vaue to check
            :raises TypeError or ValueError if a participant code is invalid
        """
        if type(index) != int:
            raise TypeError("Index must be int.")
        if (index<1) or (index>len(self.participants)):
            raise ValueError("Index does not exist.")

    def average(self, startIndex, endIndex):
        """
        gets the average score for the participants whose indices are
        in interval [startIndex, endIndex]
        :param startIndex: index of first participant in the interval
        :param endIndex: index of last participant in the interval
        :return: average overall score for participants in specified interval
        :raises ValueError is invalid interval
        """
        self._check_index(startIndex)
        self._check_index(endIndex)
        if (startIndex > endIndex):
            raise ValueError("Invalid interval.")

        startIndex-=1
        targetList = [p.overall_score() for p in self.participants[startIndex:endIndex]]
        return sum(targetList)/len(targetList)

    def min(self, startIndex, endIndex):
        """
            gets the lowest score for the participants whose indices are
            in interval [startIndex, endIndex]
            :param startIndex: index of first participant in the interval
            :param endIndex: index of last participant in the interval
            :return: lowest overall score for participants in specified interval
            :raises ValueError is invalid interval
        """
        self._check_index(startIndex)
        self._check_index(endIndex)
        if (startIndex > endIndex):
            raise ValueError("Invalid interval.")

        startIndex -= 1
        targetList = [p.overall_score() for p in self.participants[startIndex:endIndex]]
        return min(targetList)

    def remove(self,index):
        """
        removes participant by index
        :param index: index of participant to be removed
        """
        self._check_index(index)
        del self.participants[index-1]
        # adjust indices of remianed participants
        for i in range(index-1,len(self.participants)):
            self.participants[i].set_index(self.participants[i].get_index()-1)

    def remove_range(self,startIndex,endIndex):
        """
        removes participant by index in interval [startIndex,endIndex]
        :param startIndex: index of first participant to be removed
        :param endIndex: index of last participant to be removed
        """
        self._check_index(startIndex)
        self._check_index(endIndex)
        if(startIndex>endIndex):
            raise ValueError("Invalid interval.")
        del self.participants[startIndex-1:endIndex]
        numParts = endIndex-startIndex+1
        # adjust indices of remianed participants
        for i in range(startIndex-1,len(self.participants)):
            self.participants[i].set_index(self.participants[i].get_index()-numParts)

    def count(self):
        # shortcut to get participants list count
        return len(self.participants)

    def __delete__(self):
        del self.participants

    def clone(self):
        """
            Clones the Participants list into a new object
            :return: the Participants List clone
        """
        result = ParticipantsList()
        result.participants = [p.clone() for p in self.participants]
        return result

    def set_list(self,newlist):
        """
        sets the participants list to the indicated list
        :param newlist: the new participants list
        """
        for p in newlist:
            if not isinstance(p,Participant):
                raise TypeError("The indicated list contains non-Participant objects.")
        self.participants = newlist

    def get_participant_by_code(self,code):
        """
        Finds te participant with a certain code
        :param code: the code to be checked
        :return: participant instance with that code
        :rtype: Participant
        :raises: ValueError if code doesn't exist in the list
        """
        candidates = [p for p in self.participants if p.get_code() == code]
        if len(candidates)==0:
            raise ValueError(f"Participant with code {code} does not exist in participants list.")
        return candidates[0]
