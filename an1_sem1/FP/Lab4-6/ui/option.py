class Option:
    def __init__(self,key,caption,func=None,com_string=None,com_use=None):
        self.key=key
        self.caption=caption
        self.func=func
        self.com_args = None if com_string is None else com_string.split()
        self.com_use = com_use

    def parse(self,str):
        args = str.split()
        if len(args)!=len(self.com_args):
            return None
        params = []
        for i in range(len(args)):
            if self.com_args[i]=="%i":
                try:
                    params.append(int(args[i]))
                except:
                    return None
            else:
                if args[i]!=self.com_args[i]:
                    return None
        return params