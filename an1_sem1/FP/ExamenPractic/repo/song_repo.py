from domain.song import Song


class SongRepo:
    def __init__(self):
        self.__songs = []

    def get_songs(self):
        return self.__songs

    def load_from_file(self,path):
        """
        loads songs from file into the repo
        :param path:
        :param e_handler:
        :return:
        """
        errors = []
        line_counter = 1
        try:
            with open(path,"r") as f:
                for line in f:
                    try:
                        song = Song.from_str(line)
                        self.add_song(song)
                    except Exception as e:
                        errors.append(f"Error at line {line_counter}: {e}")
                    line_counter+=1
            if len(errors)>0:
                raise ValueError("\n".join(errors))
        except FileNotFoundError:
            self.populate_random(10,["T1","T2","T3","T4","T5"],["A1","A2","A3","A4","A5"])
            self.save_to_file(path)

    def save_to_file(self,path):
        with open(path,"w") as f:
            for song in self.get_songs():
                f.write(song.__str__()+"\n")

    def add_song(self,song):
        """
        adds a new song instance
        :param song: song to add
        """
        self.get_songs().append(song)

    def get_song(self,title,artist):
        """
        finds a song by a given title and artist
        :param title: song title
        :param artist: song artist
        :return: song with the title and artist
        :raises: ValueError if song could not be found
        """
        for song in self.get_songs():
            if song.get_title() == title and song.get_artist() == artist:
                return song
        raise ValueError(f"Song {title} by {artist} could not be found")

    def change_song(self,title,artist,genre,date):
        """
        changes genre and date of song given by title and artist
        :param title: song title
        :param artist: song artist
        :param genre: new song genre
        :param date: new song date
        :raises: ValueError if something went wrong
        """
        try:
            song = self.get_song(title,artist)
            song.set_genre(genre)
            song.set_date(date)
            return song
        except ValueError as e:
            raise ValueError(f"Failed to edit the specified song: \n{e}")

    def populate_random(self,cnt,titles,artists):
        """
        adds random song to the repo
        :param cnt: number of songs to add
        """
        newsongs = []
        for i in range(cnt):
            song = Song.generate_random_from_lists(titles,artists)
            self.add_song(song)
            newsongs.append(song)

        return newsongs






