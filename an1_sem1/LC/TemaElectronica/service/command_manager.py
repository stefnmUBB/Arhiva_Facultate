"""
Tema electronica LC - Neacsu-Miclea Liviu-Stefan 215
"""
class CommandManager:
    def __init__(self):
        self.commands = []

    def add_command(self,command):
        """
        adds a new command to the commands pool
        :param command: new command
        """
        self.get_commands().append(command)

    def execute(self,cmdstr):
        """
        Executes the command line according to the defined command rules
        :param cmdstr: line to be executed
        :return: command that was called
        :raises: Value error if command could not be executed
        """
        for command in self.get_commands():
            if(command.try_call(cmdstr)):
                return command
        raise ValueError("Could not execute command")

    def get_commands(self):
        """
        gets the commands list
        :return: list of commands
        """
        return self.commands
