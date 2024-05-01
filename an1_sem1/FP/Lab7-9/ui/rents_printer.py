from domain.rent import Rent
from ui.dataclass_printer import DataClassPrinter


class RentsPrinter(DataClassPrinter):
    def __init__(self,data_source):
        super().__init__(Rent)
        self.set_data_source(data_source)