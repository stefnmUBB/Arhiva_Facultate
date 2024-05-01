class EventHandler:
    def __init__(self):
        self.callback_list=[]

    def add_event(self,event):
        if(event not in self.callback_list):
            self.callback_list.append(event)

    def remove_event(self,event):
        if (event in self.callback_list):
            self.callback_list.remove(event)

    def __iadd__(self,event):
        self.add_event(event)
        return self

    def __isub__(self,event):
        self.remove_event(event)
        return self

    def invoke(self,*args):
        for event in self.callback_list:
            event(*args)
