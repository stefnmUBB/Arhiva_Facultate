import csv
import random

import numpy as np
from matplotlib import pyplot as plt


def loadData(fileName):
    data = []
    with open(fileName) as csv_file:
        csv_reader = csv.reader(csv_file, delimiter=',')
        line_count = 0
        for row in csv_reader:
            data.append(row)
            line_count += 1
    return data

def split_train_and_test(x,y):
    train_x = []
    train_y = []
    test_x = []
    test_y = []

    for i in range(len(y)):
        if random.random()<0.8:
            train_x.append(x[i])
            train_y.append(y[i])
        else:
            test_x.append(x[i])
            test_y.append(y[i])

    return [train_x,train_y, test_x, test_y]

def plotDataHistogram(x, variableName, fc=(0, 0, 1, 0.5)):
    n, bins, patches = plt.hist(x, 10, fc=fc)
    plt.title('Histogram of ' + variableName)