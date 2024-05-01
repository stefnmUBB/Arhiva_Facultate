"""
Tema electronica LC - Neacsu-Miclea Liviu-Stefan 215
"""
import os
from os import system

from service.command import Command
from service.command_manager import CommandManager

CMD_SCREEN = 0
EXE_SCREEN = 1
ERR_SCREEN = 2

class UserInterface:
    def __init__(self):
        self.cmd_mgr = CommandManager()
        self.current_screen = 0

        self.clrflag=True
        self.cmdstr=""
        self.get_cmd_manager().add_command(Command(exit,"exit","Quit execution"))
        self.get_cmd_manager().add_command(Command(self.print_commands,"help","Show help"))
        self.error=""

    def cls(self):
        system('cls')
        system('clear')

    def print_commands(self):
        for command in self.get_cmd_manager().get_commands():
            print(f"\"{command.pattern}\"")
            if command.description!="":
                print("   - ", command.description)
            print("")

    def get_cmd_manager(self):
        return self.cmd_mgr

    def wait_for_key_pressed(self):
        system("pause")

    def run(self):
        if self.clrflag:
            self.cls()
        else:
            self.clrflag=True
        if self.current_screen == CMD_SCREEN:
            print("")
            print("Tema facultativa electronica LC 2021")
            print("By Neacsu-Miclea Liviu-Stefan, Gr. 215\n")
            print("Type a command to execute.")
            print("Type \"help\" to show the list of available commands.")
            print(" >> ",end="")
            self.cmdstr=input()
            self.current_screen = EXE_SCREEN
            return
        if self.current_screen == EXE_SCREEN:
            try:
                if(self.cmdstr==""):
                    self.current_screen=CMD_SCREEN
                    return
                cmd = self.get_cmd_manager().execute(self.cmdstr)
                if cmd.pattern=="help":
                    self.clrflag=False
                else:
                    self.wait_for_key_pressed()
                self.current_screen = CMD_SCREEN
            except Exception as e:
                self.error = e
                self.current_screen = ERR_SCREEN
                # raise e
            return
        if self.current_screen == ERR_SCREEN:
            print()
            lines=str(self.error)
            lines=lines.split("\n")
            width = max([len(s) for s in lines])
            rows=[
                " \u2554" + "\u2550" * (width + 2) + "\u2557",
                " \u2551" + "Error".center(width + 2) + "\u2551",
                " \u2560" + "\u2550"*(width+2) + "\u2563"]
            for line in lines:
                rows.append(" \u2551 "+line.center(width)+" \u2551")
            rows.append(" \u255A" + "\u2550"*(width+2) + "\u255D")

            # get dimensions of the console
            try:
                h, w = os.popen('stty size', 'r').read().split()
            except:
                w = 0

            for row in rows:
                line=row.center(int(w)-1)
                print(line)
            self.wait_for_key_pressed()
            self.current_screen= CMD_SCREEN
            return