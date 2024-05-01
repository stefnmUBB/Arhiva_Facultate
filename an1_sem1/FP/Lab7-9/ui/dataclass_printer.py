from ui.table_printer import TablePrinter


class DataClassPrinter(TablePrinter):
    @staticmethod
    def get_property_function(field):
        property = field
        return lambda o: getattr(o,property)

    def __init__(self, classtype):
        super().__init__()
        for field in classtype.__annotations__:
            self.add_column(field,self.get_property_function(field))