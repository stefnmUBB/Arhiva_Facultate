from os.path import isfile

from repository.clients_list_memo import ClientsListMemo
from utils.datafile import DataFile


class ClientsListFile(ClientsListMemo):
    def load_from_file(self,path):
        """
        loads clients list from filesystem
        return: clients list loaded form file
        :raises: FileNotFoundError
        """
        if isfile(path):
            with open(path, "r") as file:
                self._set_clients_list(DataFile.read_elements(file))
        else:
            raise FileNotFoundError()

    def write_to_file(self, path):
        """
        writes clients to filesystem
        """
        file = open(path, "w")
        DataFile.write_elements(file, self.get_clients())
        file.close()
