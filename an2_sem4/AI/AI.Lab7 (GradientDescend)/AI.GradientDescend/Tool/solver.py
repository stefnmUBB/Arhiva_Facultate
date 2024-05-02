import csv
import math
import os
import random
import sys

import matplotlib.pyplot as plt
import numpy as np
from sklearn import linear_model
from sklearn.datasets import load_linnerud
from sklearn.linear_model import SGDRegressor
from sklearn.metrics import mean_squared_error


def loadData(fileName, inputVariabName1, inputVariabName2, outputVariabName):
    data = []
    dataNames = []
    with open(fileName) as csv_file:
        csv_reader = csv.reader(csv_file, delimiter=',')
        line_count = 0
        for row in csv_reader:
            if line_count == 0:
                dataNames = row
            else:
                data.append(row)
            line_count += 1
    selectedVariable1 = dataNames.index(inputVariabName1)
    selectedVariable2 = dataNames.index(inputVariabName2)
    inputs1 = [float(data[i][selectedVariable1] if data[i][selectedVariable1]!="" else "0") for i in range(len(data))]
    inputs2 = [float(data[i][selectedVariable2] if data[i][selectedVariable2]!="" else "0") for i in range(len(data))]
    selectedOutput = dataNames.index(outputVariabName)
    outputs = [float(data[i][selectedOutput]) for i in range(len(data))]

    return [[inputs1[i], inputs2[i]] for i in range(len(inputs1))], outputs

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

def make_hist(x,y, minx, maxx, miny, maxy):
    fig = plt.figure()
    ax = fig.add_subplot(projection='3d')
    hist, xedges, yedges = np.histogram2d(x, y, bins=5, range=[[minx, maxx], [miny, maxy]])

    # Construct arrays for the anchor positions of the 16 bars.
    xpos, ypos = np.meshgrid(xedges[:-1] + 0.2, yedges[:-1] + 0.2, indexing="ij")
    xpos = xpos.ravel()
    ypos = ypos.ravel()
    zpos = 0

    # Construct arrays with the dimensions for the 16 bars.
    dx = dy = 0.1 * np.ones_like(zpos)
    dz = hist.ravel()

    ax.bar3d(xpos, ypos, zpos, dx, dy, dz, zsort='average')

    ax.set_xlabel('GDP')
    ax.set_ylabel('Freedom')
    ax.set_zlabel('Count')

    plt.show()

def plotDataHistogram(x, variableName):
    n, bins, patches = plt.hist(x, 10)
    plt.title('Histogram of ' + variableName)
    plt.show()


def solveUni(data):
    train_x, train_y, test_x, test_y = data
    regressor = SGDRegressor(max_iter=200)
    #regressor.fit(train_x, train_y)
    regressor.partial_fit(train_x, train_y)
    coefs = regressor.coef_.tolist()

    w0, w1 = regressor.intercept_[0], coefs[0]
    print(f"w0={w0}")
    print(f"w1={w1}")

    x = [_[0] for _ in train_x]
    y = train_y

    print(len(x))

    plt.axis([min(x)-1, max(x)+1, min(y)-1, max(y)+1])
    plt.plot(x, y, 'g*', alpha=0.2)

    x = [_[0] for _ in test_x]
    y = test_y

    plt.plot(x, y, 'r*')

    cy = regressor.predict([[_] for _ in x])

    print(f"Uni error={mean_squared_error(y, cy)}")

    plt.plot(x, cy, 'b*')
    plt.show()

def solveMulti(data):
    train_x, train_y, test_x, test_y = data
    regressor = SGDRegressor(max_iter=200)
    #regressor.fit(train_x, train_y)
    regressor.partial_fit(train_x, train_y)
    coefs = regressor.coef_.tolist()

    w0, w1, w2 = regressor.intercept_[0], coefs[0], coefs[1]
    print(f"w0={w0}")
    print(f"w1={w1}")
    print(f"w2={w2}")

    t1 = [_[0] for _ in train_x]
    t2 = [_[1] for _ in train_x]

    # make_hist(t1,t2, min(t1), max(t1), min(t2), max(t2))

    fig = plt.figure()
    ax = fig.add_subplot(projection='3d')

    for i in range(len(train_y)):
        ax.scatter(train_x[i][0], train_x[i][1], train_y[i], marker='^', edgecolor='r', alpha=0.2)

    cy = []
    for i in range(len(test_y)):
        cy.append(w0 + w1 * test_x[i][0] + w2 * test_x[i][1])
        ax.scatter(test_x[i][0], test_x[i][1], w0 + w1 * test_x[i][0] + w2 * test_x[i][1], marker='o', edgecolor='g')
        ax.scatter(test_x[i][0], test_x[i][1], test_y[i], marker='o', edgecolor='b')

    X, Y = np.meshgrid(range(math.floor(min(t1) - 0.5), math.ceil(max(t1) + 0.5)),
                       range(math.floor(min(t2) - 0.5), math.ceil(max(t2) + 0.5)))
    Z = w0 + w1 * X + w2 * Y

    ax.plot_surface(X, Y, Z, edgecolor='gray', lw=0.5, rstride=8, cstride=8,
                    alpha=0.3)

    ax.set_xlabel('GDP')
    ax.set_ylabel('Freedom')
    ax.set_zlabel('Happiness')

    print(f"Multi error={mean_squared_error(test_y, cy)}")

    plt.show()

if __name__=='__main__':
    crtDir = os.getcwd()
    filePath = os.path.join(crtDir, "../Input", "2017.csv")

    x, y = loadData(filePath, "Economy..GDP.per.Capita.", "Freedom", "Happiness.Score")
    solveMulti(split_train_and_test(x,y))

    x, y = loadData(filePath, "Economy..GDP.per.Capita.", "Freedom", "Happiness.Score")
    x = [[_[0]] for _ in x]
    solveUni(split_train_and_test(x,y))

