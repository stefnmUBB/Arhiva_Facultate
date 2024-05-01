class MovieValidator:
    @staticmethod
    def validate_movie(movie):
        """
        Checks if a movie instance is coorectly defined
        :param movie: movie to check
        :raises: ValueError if check has failed
        """
        if movie.get_title() == "":
            raise ValueError("Movie title cannot be empty")

        if len(movie.get_description()) > 255:
            raise ValueError("Movie description is too long")

        if movie.get_genre()=="":
            raise ValueError("Movie genre cannot be empty")

        if movie.get_year()<1888:
            raise ValueError("Movie year incorrect")