import os

from service.picture_service import PictureService, AuthorNotFoundError


class UI:
    def __init__(self,path):
        self.__pserv = PictureService()
        self.__pserv.load_from_file(path)

    def run(self):
        while True:
            try:
                os.system("clear")
                os.system("cls")
                print("1. Search word in picture's name")
                print("2. Get each author's most recent picture")
                option = input(">> ")
                if(option == "1"):
                    word = input("Keyword  = ")
                    pics_list = self.__pserv.search_in_name(word)
                    if(len(pics_list)==0):
                        print("No entries available")
                    else:
                        print("\n".join([pic.__str__() for pic in pics_list]))
                elif option == "2":
                    authors = []
                    for pic in self.__pserv.get_repo().get_list():
                        author = pic.get_author()
                        if not author in authors:
                            authors.append(author)
                    for author in authors:
                        pic = self.__pserv.get_authors_most_recent_pic(author)
                        print(f" - {pic.get_author()}: {pic.get_year()}")
                else:
                    print("Wrong command")
            except Exception as exc:
                print("Error : ", exc)
            os.system("pause")
