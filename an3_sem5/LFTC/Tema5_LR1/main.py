# Proiect pe echipe: Neacsu Stefan, Condor Andrada

import os

from data import *

def print_list(l:list[any]):
    print(list(map(str, l)))

def run(path: str, texts: list[str], show_cc=False):
    g = Grammar.from_file(path)
    g=g.enrich("S'")
    print(g)
    print(g.first1())

    parser = ParserLR1(g)
    if show_cc:
        print(parser.table.cc)
    parser.print_analysis_table()
    parser.test_for_conflicts()

    for text in texts:
        print("---------------------------------------")
        print(text)
        print("---------------------------------------")
        parser.parse(text)



if __name__=='__main__':
    try:
        with open("./input/ok.cpp") as f: input_ok = f.read()
        with open("./input/neok.cpp") as f: input_neok=f.read()

        #run("input/g0.txt", ["aaxbb", "aaybb", "aaxb", "aax", "xb"], show_cc=True)
        run("input/g1.txt", [input_ok, input_neok])
    except ValueError as e:
        print(e)
        raise e
    except IOError as e:
        print(e)
