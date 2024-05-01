from domain.genres import GENRES
from utils.date import Date


class SongValidator:
    @staticmethod
    def validate_song(title,artist,genre,date):
        """
        checks if could create a song from given arguments
        :raises: ValueError if song is not valid
        """
        errors = []
        if not isinstance(title,str):
            errors.append("Song title is not a string")
        else:
            if title=="":
                errors.append("Song title cannot be empty")

        if not isinstance(artist,str):
            errors.append("Song artist is not a string")
        else:
            if artist=="":
                errors.append("Song artist cannot be empty")

        if genre not in GENRES:
            errors.append("Unknown song genre")

        try:
            Date.from_str(date)
        except Exception as e:
            errors.append(f"{e}")

        if len(errors)>0:
            raise ValueError("\n".join(errors))
