from os.path import isfile

from repository.movies_list_memo import MoviesListMemo
from utils.datafile import DataFile


class MoviesListFile(MoviesListMemo):
    def load_from_file(self,path):
        """
        loads movies list from filesystem
        return: movies list loaded form file
        :raises: FileNotFoundError
        """
        if isfile(path):
            with open(path, "r") as file:
                self._set_movies_list(DataFile.read_elements(file))
        else:
            raise FileNotFoundError()

    def write_to_file(self, path):
        """
        writes movies to filesystem
        """
        file = open(path, "w")
        DataFile.write_elements(file, self.get_movies())
        file.close()
