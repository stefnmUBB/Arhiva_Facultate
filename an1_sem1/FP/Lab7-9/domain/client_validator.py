class ClientValidator:
    @staticmethod
    def validate_client(client):
        """
        Checks if a client instance is coorectly defined
        :param client: client to check
        :raises: ValueError if check has failed
        """
        if client.get_name() == "":
            raise ValueError("Client name cannot be empty")

        if client.get_surname() == "":
            raise ValueError("Client surname cannot be empty")

        if len(client.get_cnp())!=13:
            raise ValueError("Client CNP must have 13 digits")

        try:
            int(client.get_cnp())
        except TypeError:
            raise ValueError("Client CNP is invalid")