from os.path import isfile

from repository.rents_list_memo import RentsListMemo
from utils.datafile import DataFile


class RentsListFile(RentsListMemo):
    def load_from_file(self, path):
        """
        loads rents list from filesystem
        return: rents list loaded form file
         :raises: FileNotFoundError
        """
        if isfile(path):
            with open(path, "r") as file:
                self._set_rents_list(DataFile.read_elements(file))
        else:
            raise FileNotFoundError()

    def write_to_file(self, path):
        """
        writes rents to filesystem
        """
        file = open(path, "w")
        DataFile.write_elements(file, self.get_rents())
        file.close()
