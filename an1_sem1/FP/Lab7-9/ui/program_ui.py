from service.client_service import ClientService
from service.command import Command
from service.list_search_engine import ListSearchEngine
from service.movie_service import MovieService
from service.rent_service import RentService
from service.report import Report
from ui.clients_printer import ClientsPrinter
from ui.movies_printer import MoviesPrinter
from ui.rents_printer import RentsPrinter
from ui.user_interface import UserInterface


class ProgramUI(UserInterface):
    def __init__(self):
        UserInterface.__init__(self)
        try:
            self.cserv = ClientService.from_file("clients.ini")
        except:
            self.cserv = ClientService()

        try:
            self.mserv = MovieService.from_file("movies.ini")
        except:
            self.mserv = MovieService()

        try:
            self.rserv = RentService.from_file("rents.ini")
        except:
            self.rserv = RentService()

        self.rserv.attach(self.cserv.get_clients_repo(), self.mserv.get_movies_repo())
        self.init_commands()
        self.init_events()

    def clients_save(self,sender,client): self.cserv.save_to_file("clients.ini")

    @staticmethod
    def get_client_feedback_notifier(msg):
        """
        Creates a function that prints a message and the client information
        :param msg: message to be printed
        :return: client event function
        """
        message = msg
        def fun(sender,client):
            print(message)
            print(f"   Id : {client.get_id()}")
            print(f" Name : {client.get_full_name()}")
            print(f"  CNP : {client.get_cnp()}")
        return fun

    @staticmethod
    def get_movie_feedback_notifier(msg):
        """
        Creates a function that prints a message and the movie information
        :param msg: message to be printed
        :return: movie event function
        """
        message = msg
        def fun(sender, movie):
            print(message)
            print(f"         Id  : {movie.get_id()}")
            print(f"       Title : {movie.get_title()}")
            print(f"       Genre : {movie.get_genre()}")
            print(f" Description : {movie.get_description()}")
            print(f"        Year : {movie.get_year()}")
        return fun

    @staticmethod
    def get_rent_feedback_notifier(msg):
        """
        Creates a function that prints a message and the rent information
        :param msg: message to be printed
        :return: rent event function
        """
        message = msg

        def fun(sender, rent):
            """
            :param sender: service which raised event
            :type:RentService
            :param rent:target rent
            :type: Rent
            """
            print(message)
            print(f"         Id  : {rent.get_id()}")
            print(f"   Client Id : {rent.get_client_id()}")
            print(f" Client Name : {sender.get_rent_data(rent)['client'].get_full_name()}")
            print(f"    Movie Id : {rent.get_client_id()}")
            print(f"  Movie Name : {sender.get_rent_data(rent)['movie'].get_title()}")
            print(f"   Rent Date : {rent.get_rent_date_str()}")
            print(f"    Returned : {'Yes' if rent.is_returned() else 'No'}")
            print(f"          On : {rent.get_return_date_str()}")
        return fun

    def client_change_name(self, id, name): self.cserv.edit_client(id, name=name)

    def client_change_surname(self, id, surname): self.cserv.edit_client(id, surname=surname)

    def client_change_cnp(self, id, cnp): self.cserv.edit_client(id, cnp=cnp)


    def clients_print_all(self): ClientsPrinter(self.cserv.get_clients()).print_table()

    def clients_print_most_rents(self):
        def client_comparator(cl1,cl2):
            rents1 = self.rserv.get_client_number_of_rents(cl1.get_id())
            rents2 = self.rserv.get_client_number_of_rents(cl2.get_id())
            return rents1>rents2 or \
                (rents1==rents2 and cl1.get_full_name()<cl2.get_full_name())


        report = Report(self.cserv.get_clients()) \
            .select(
                condition=lambda client: self.rserv.get_client_number_of_rents(client.get_id()) > 0,
                sortReversed=False,
                cmp=client_comparator
            )
            #.select(lambda client: True, lambda client: client.get_full_name())\
            #.select(lambda client: self.rserv.get_client_number_of_rents(client.get_id()) > 0,
            #        lambda client: client.tag, True)
        ClientsPrinter(report.elem_list, True).print_table()

    def clients_print_percent_most_rents(self,p):
        report = Report(self.cserv.get_clients()) \
            .select(lambda client: True, lambda client: client.get_full_name()) \
            .select(lambda client: self.rserv.get_client_number_of_rents(client.get_id()) > 0,
                    lambda client: client.tag, True) \
            .first_percent(p[:-1]) # remove "%" character from p
        ClientsPrinter(report.elem_list, True).print_table()

    def movie_change_year(self,id,year): self.mserv.edit_movie(id,year=year)

    def movie_change_title(self,id,title): self.mserv.edit_movie(id,title=title)

    def movie_change_description(self,id,dsc): self.mserv.edit_movie(id,description=dsc)

    def movie_change_genre(self,id,genre): self.mserv.edit_movie(id,genre=genre)

    def movies_save(self,sender,movie): self.mserv.save_to_file("movies.ini")

    def movies_print_all(self): MoviesPrinter(self.mserv.get_movies()).print_table()

    def movies_print_most_rented(self):
        report = Report(self.mserv.get_movies())\
            .select(lambda movie:self.rserv.get_movie_number_of_rents(movie.get_id())>0,
                    lambda movie:movie.tag,True)
        MoviesPrinter(report.elem_list, True).print_table()

    def movies_print_top_most_rented(self,n):
        report = Report(self.mserv.get_movies()) \
            .select(lambda movie: self.rserv.get_movie_number_of_rents(movie.get_id()) > 0,
                    lambda movie: movie.tag, True) \
            .first(n)
        MoviesPrinter(report.elem_list, True).print_table()

    def rents_save(self, sender, rent): self.rserv.save_to_file("rents.ini")

    def rent_close(self,cid,mid): self.rserv.close_rent(cid,mid)

    def rents_print_all(self): RentsPrinter(self.rserv.get_rents_list()).print_table()

    def search_key(self,key,entity_type):
        if entity_type=="clients":
            found = ListSearchEngine.look_for(key, self.cserv.get_clients())
            ClientsPrinter(found).print_table()
        elif entity_type=="movies":
            found = ListSearchEngine.look_for(key, self.mserv.get_movies())
            MoviesPrinter(found).print_table()
        else:
            raise ValueError(f"Cannot look for {key} in {entity_type}")

    def init_commands(self):
        self.get_cmd_manager().add_command(
            Command(self.cserv.add_client,
                "Add client %s %s with cnp %s",
                "%s = [Name], %s = [Surname], %s = [CNP]"
            )
        )

        self.get_cmd_manager().add_command(
            Command(self.cserv.remove_client_by_id,"Remove client %i", "%i = [id]"))

        self.get_cmd_manager().add_command(
            Command(self.client_change_name, "Change client %i name to %s",
                    "%i = [id], %s = [name]"))

        self.get_cmd_manager().add_command(
            Command(self.client_change_surname, "Change client %i surname to %s",
                    "%i = [id], %s = [surname]"))

        self.get_cmd_manager().add_command(
            Command(self.client_change_cnp, "Change client %i cnp to %s",
                    "%i = [id], %s = [cnp]"))

        self.get_cmd_manager().add_command(
            Command(self.clients_print_all,
                "Display clients",
                "Show a list of all clients"
            )
        )

        self.get_cmd_manager().add_command(
            Command(self.mserv.add_movie,
                "Add movie %s from year %i genre %s and description %s",
                "%s = [Title], %i = [year], %s = [genre], %s = [description]"
                )
        )

        self.get_cmd_manager().add_command(
            Command(self.mserv.remove_movie_by_id, "Remove movie %i", "%i = [id]"))

        self.get_cmd_manager().add_command(
            Command(self.movie_change_title, "Change movie %i title to %s",
                "%i = [id], %s = [title]"))

        self.get_cmd_manager().add_command(
            Command(self.movie_change_year, "Change movie %i year to %i",
                    "%i = [id], %i = [year]"))

        self.get_cmd_manager().add_command(
            Command(self.movie_change_description, "Change movie %i description to %s",
                    "%i = [id], %s = [description]"))

        self.get_cmd_manager().add_command(
            Command(self.movie_change_genre, "Change movie %i genre to %s",
                    "%i = [id], %s = [genre]"))

        self.get_cmd_manager().add_command(
            Command(self.movies_print_all,
                "Display movies",
                "Show a list of all movies"
                )
        )

        self.get_cmd_manager().add_command(
            Command(self.search_key, "Search %s in %s",
                    "%s = [Searck key], %s = [clients|movies]"))


        self.get_cmd_manager().add_command(
            Command(self.cserv.populate_random_clients,"Populate with %i random clients",
                    "%i = [Clients count]")
        )

        self.get_cmd_manager().add_command(
            Command(self.mserv.populate_random_movies, "Populate with %i random movies",
                    "%i = [Movies count]")
        )

        self.get_cmd_manager().add_command(
            Command(self.rserv.add_rent,"Client %i rents movie %i",
                    "%i = [Client id], %i = [Movie id]")
        )

        self.get_cmd_manager().add_command(
            Command(self.rserv.remove_rent_by_id, "Cancel rent %i",
                    "%i = [Rent id]")
        )

        self.get_cmd_manager().add_command(
            Command(self.rents_print_all, "Display rents",
                    "Prints all rents")
        )

        self.get_cmd_manager().add_command(
            Command(self.rent_close, "Client %i returns movie %i",
                    "%i = [Client id], %i = [Movie id]")
        )

        self.get_cmd_manager().add_command(
            Command(self.rserv.populate_random_rents, "Add %i random rents",
                    "%i = [Number of rents]")
        )

        self.get_cmd_manager().add_command(
            Command(self.movies_print_most_rented, "Display most rented movies",
                    "Prints a list of rented movies ordered by number of rents")
        )

        self.get_cmd_manager().add_command(
            Command(self.clients_print_most_rents, "Display clients with most rents",
                    "Prints a list of clients ordered by the number of rented movies")
        )

        self.get_cmd_manager().add_command(
            Command(self.clients_print_percent_most_rents, "Display first %s clients with most rents",
                    "[%s] = percentage (e.g. \"30%\")")
        )

        self.get_cmd_manager().add_command(
            Command(self.movies_print_top_most_rented, "Display top %i most rented movies",
                    "[%i] - first movies to display")
        )


    def init_events(self):
        self.cserv.on_added_client += self.get_client_feedback_notifier("Successfully added client:")
        self.cserv.on_changed_client += self.get_client_feedback_notifier("Successfully changed client:")
        self.cserv.on_removed_client += self.get_client_feedback_notifier("Successfully removed client:")

        self.cserv.on_added_client += self.clients_save
        self.cserv.on_changed_client += self.clients_save
        self.cserv.on_removed_client += self.clients_save

        self.mserv.on_added_movie += self.get_movie_feedback_notifier("Successfully added movie:")
        self.mserv.on_changed_movie += self.get_movie_feedback_notifier("Successfully changed movie:")
        self.mserv.on_removed_movie += self.get_movie_feedback_notifier("Successfully removed movie:")

        self.mserv.on_added_movie += self.movies_save
        self.mserv.on_changed_movie += self.movies_save
        self.mserv.on_removed_movie += self.movies_save

        self.rserv.on_added_rent += self.get_rent_feedback_notifier("Successfully created rent:")
        self.rserv.on_changed_rent += self.get_rent_feedback_notifier("Successfully changed rent:")
        self.rserv.on_removed_rent += self.get_rent_feedback_notifier("Successfully removed rent:")

        self.rserv.on_added_rent += self.rents_save
        self.rserv.on_changed_rent += self.rents_save
        self.rserv.on_removed_rent += self.rents_save
