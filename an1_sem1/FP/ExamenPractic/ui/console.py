import os
from random import random

from service.song_service import SongService


class Console:
    def __init__(self):
        self.__service = SongService("songs.txt")
        self.step = "input"

    def get_service(self):
        return self.__service

    def do_change_song(self):
        title = input(" Title = ")
        artist = input(" Artist = ")
        genre = input(" Genre = ")
        date = input(" Date = ")
        print(self.get_service().change_song(title,artist,genre,date))

    def do_export(self):
        fname = input("Export filename = ")
        self.get_service().export_songs(fname)

    def do_add_random_songs(self):
        cnt = int(input("Number of songs = "))
        titles = [x.strip() for x in input("titles = ").split(",")]
        artists = [x.strip() for x in input("artists = ").split(",")]
        songs = self.get_service().add_random_songs(cnt,titles,artists)
        for song in songs:
            print(song)
        print(f"Added {len(songs)} songs")

    def do_view(self):
        songs = self.get_service().get_repo().get_songs()
        for song in songs:
            print(song)

    def process_cmd(self,cmd):
        if cmd=="0":
            exit()
        elif cmd=="1":
            self.do_change_song()
        elif cmd=="2":
            self.do_add_random_songs()
        elif cmd=="3":
            self.do_export()
        elif cmd=="4":
            self.do_view()
        else:
            return

    def run(self):
        if self.step == "input":
            os.system("cls")
            os.system("clear")
            print("0. exit")
            print("1. Change song")
            print("2. Generate random songs")
            print("3. Export sorted songs")
            cmd = input(" >>> ")
            try:
                self.process_cmd(cmd)
            except Exception as e:
                print("ERROR:")
                print(e)

            input("Press Enter to continue...")


