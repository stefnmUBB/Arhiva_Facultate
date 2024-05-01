from repo.song_repo import SongRepo
from service.song_validator import SongValidator


class SongService:
    def __init__(self,songs_path):
        self.__songs_path = songs_path
        self.__repo = SongRepo()
        self.get_repo().load_from_file(self.get_songs_path())

    def change_song(self,title,artist,genre,date):
        """
        Chenges song given by title and artist
        :param title: song title
        :param artist: song artist
        :param genre: new song genre
        :param date: new song date
        :return: the edited song
        """
        SongValidator.validate_song(title,artist,genre,date)
        song = self.get_repo().change_song(title,artist,genre,date)
        self.save_songs()
        return song

    def get_songs_path(self):
        """
        retrieves given songs path
        :return: songs path
        :rtype: str
        """
        return self.__songs_path

    def get_repo(self):
        """
        :return: the songs repo
        """
        return self.__repo

    def save_songs(self):
        """
        Saves songs to the file given by songs path
        """
        self.get_repo().save_to_file(self.get_songs_path())

    def add_random_songs(self,cnt, titles, artists):
        """
        adds cnt random songs by choosing from titles and artists
        :param cnt: number of songs to add
        :param titles: list of available titles
        :param artists: list of available artists
        """
        songs = self.get_repo().populate_random(cnt,titles,artists)
        self.save_songs()
        return songs

    def get_sorted_songs_by_date(self):
        return sorted(self.get_repo().get_songs(), key=lambda sng: -sng.get_date().get_sort_key())

    def export_songs(self,fname):
        """
        exports sorted songs to fname
        :param fname: filename
        """
        songs = self.get_sorted_songs_by_date()
        if fname==self.get_songs_path():
            raise ValueError("Export file cannot be the same as app storage file")
        with open(fname,"w") as f:
            for song in songs:
                f.write(song.__str__()+"\n")

