from os import system
from time import sleep
from ui.option import Option

class UserInterface:

    def __init__(self):
        self.options = None
        self.step = 0  # 0 = wait option key, 1 = wait suboption key, 2 = execute func
        self.selected_option = None
        self.selected_key = "-"

    def cls(self):
        system('cls')
        system('clear')
        pass

    def printOptions(self):
        for option in self.options:
            if(len(option.key)==1):
                print(f"{option.key}. {option.caption}")

    def printSubOptions(self):
        print("0. back")
        for option in self.options:
            if len(option.key)==2 and option.key[0]==self.selected_key[0]:
                print(f"{option.key[1]}. {option.caption}")

    def requestOption(self):
        print("Enter your option:",end="")
        return input()

    def waitForKeyPressed(self):
        system("pause")

    def run_as_command_string(self,command):
        for option in self.options:
            if option.com_args is None:
                continue
            params = option.parse(command)
            if params is None:
                continue
            try:
                print("Identified operation : "+option.key)
                print("Arguments : "+str(params))
                if option.com_use is not None:
                    option.com_use(params)
            except Exception as e:
                print("Command not available.")
                print(e)
            finally:
                return
        print("No valid command found.")

    def run(self):
        self.cls()
        if self.step==0:
            self.selected_key="-"
            self.printOptions()
            key = self.requestOption()
            if len(key)>1:
                self.run_as_command_string(key)
                self.waitForKeyPressed()
                self.step = 0
                self.selected_key = "-"
                return
            if key in map(lambda o: o.key[0],self.options):
                self.selected_option = next((o for o in self.options if o.key[0] == key), None)
                if(self.selected_option.func==None):
                    self.step = 1
                    self.selected_key=key
                    return
                else:
                    self.step=2
                    return
        elif self.step==1:
            self.printSubOptions()
            bkp = self.selected_key
            self.selected_key += self.requestOption()
            if self.selected_key[-1]=="0":
                self.selected_key="-"
                self.step=0
                return
            if len(self.selected_key)!=2:
                self.selected_key = bkp
                return
            if self.selected_key in map(lambda o: o.key, self.options):
                self.selected_option = next((o for o in self.options if len(o.key)==2 and o.key == self.selected_key), None)
                self.step = 2
                return
            self.selected_key=bkp
        elif self.step==2:
            # print(self.selected_option.func)
            self.selected_option.func()
            self.waitForKeyPressed()
            self.step=0
            self.selected_key="-"
            return