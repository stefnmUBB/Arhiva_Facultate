"""
Tema electronica LC - Neacsu-Miclea Liviu-Stefan 215
"""
class Explanation:
    """
    Helper class to document a solution of a problem (conversion etc)
    while it is being executed
    """
    def __init__(self):
        self.lines=[""]
        self.result = None

    def write(self,line:str):
        """
        adds a new line to the explaination
        :param line: new line
        """
        self.lines.append(line)

    def append_explanation(self,explanation):
        """
        adds a full explanation to the current explanation
        :param explanation:
        :return:
        """
        self.lines+=explanation.lines
        self.write("")

    def set_result(self,result):
        """
        An explanation can carry a result of the exposed operation
        :param result: problem result
        """
        self.result=result

    def get_result(self):
        """
        :return: explanation result
        """
        return self.result

    def __str__(self):
        """
        :return: whole explanation as s string
        """
        return "\n".join(self.lines)