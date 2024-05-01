"""
    pretty-print table helper
"""

class TableColumn:
    def __init__(self, _name="", _data=lambda x:""):
        self.name = _name
        self.data = _data


class TablePrinter:
    def __init__(self):
        self.columnsData=[]
        self.dataset=[]

    def add_column(self,name,data):
        self.columnsData.append(TableColumn(name,data))

    def set_data_source(self,data_source):
        self.dataset=data_source

    def print_table(self):
        columns = [ [col.name] for col in self.columnsData ]

        # fetch data
        for element in self.dataset:
            for i, colData in enumerate(self.columnsData):
                columns[i].append(str(colData.data(element)))
        # print(columns)
        # align data

        for i in range(len(columns)):
            colLength = len(max(columns[i], key=len))
            columns[i] = list(map(lambda cell : cell.ljust(colLength), columns[i]))

        for row in range(len(self.dataset)+1):
            for col in range(len(columns)):
                print(" \u2551 ",columns[col][row],end="",sep="")
            print(" \u2551")
            print(" ",end="")
            for col in range(len(columns)):
                cLen=len(columns[col][row])
                if col==0:
                    if row < len(self.dataset):
                        print("\u2560\u2550\u2550","\u2550"*cLen,end="",sep="")
                    else:
                        print("\u255A\u2550\u2550", "\u2550" * cLen, end="", sep="")
                else:
                    if row < len(self.dataset):
                        print("\u256C\u2550\u2550", "\u2550" * cLen, end="", sep="")
                    else:
                        print("\u2569\u2550\u2550", "\u2550" * cLen, end="", sep="")
            print("\u2563" if row < len(self.dataset) else "\u255D")




