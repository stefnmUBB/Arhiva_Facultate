from domain.movie import Movie
from ui.dataclass_printer import DataClassPrinter


class MoviesPrinter(DataClassPrinter):
    def __init__(self,data_source,print_tag=False):
        super().__init__(Movie)
        if print_tag:
            self.add_column("No. rents",self.get_property_function("tag"))
        self.set_data_source(data_source)