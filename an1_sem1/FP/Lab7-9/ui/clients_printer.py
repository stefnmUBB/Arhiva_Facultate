from domain.client import Client
from ui.dataclass_printer import DataClassPrinter


class ClientsPrinter(DataClassPrinter):
    def __init__(self,data_source,print_tag=False):
        super().__init__(Client)
        if print_tag:
            self.add_column("No. rented movies",self.get_property_function("tag"))
        self.set_data_source(data_source)