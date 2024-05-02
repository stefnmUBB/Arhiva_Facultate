import os

from sklearn.linear_model import LogisticRegression
from sklearn.metrics import accuracy_score
from sklearn.preprocessing import StandardScaler

from utils import loadData, plotDataHistogram, split_train_and_test
import matplotlib.pyplot as plt

def get1():
    filePath = os.path.join(os.getcwd(), "../Input", "Uni/wdbc.data")
    data = loadData(filePath)
    inputs = [[float(_[2]), float(_[3])] for _ in data]
    outputs = [_[1] for _ in data]
    return inputs, outputs

def solve1():
    inputs, outputs = get1()

    labels = set(outputs)
    noData = len(inputs)
    for crtLabel in labels:
        x = [inputs[i][0] for i in range(noData) if outputs[i] == crtLabel]
        y = [inputs[i][1] for i in range(noData) if outputs[i] == crtLabel]
        plt.scatter(x, y, label=crtLabel)
    plt.xlabel('Tx')
    plt.ylabel('Rd')
    plt.legend()
    plt.show()

    plotDataHistogram([inputs[i][0] for i in range(noData)], "", (0, 0, 1, 0.5))
    plotDataHistogram([inputs[i][1] for i in range(noData)], "Blue=Tx, Green = Rd", (0, 1, 0, 0.5))
    plotDataHistogram(outputs, "Blue=Tx, Green = Rd", (1, 0, 0, 0.5))
    plt.show()

    train_x,train_y, test_x, test_y = split_train_and_test(inputs, outputs)

    classifier = LogisticRegression()
    classifier.fit(train_x, train_y)
    w0, w1, w2 = classifier.intercept_[0], classifier.coef_[0][0], classifier.coef_[0][1]
    print(w0,w1,w2)

    computedOutputs = classifier.predict(test_x)

    labels = list(set(outputs))
    noData = len(test_x)
    for crtLabel in labels:
        x = [test_x[i][0] for i in range(noData) if test_y[i] == crtLabel and computedOutputs[i] == crtLabel]
        y = [test_x[i][1] for i in range(noData) if test_y[i] == crtLabel and computedOutputs[i] == crtLabel]
        plt.scatter(x, y, label=crtLabel + ' (correct)')
    for crtLabel in labels:
        x = [test_x[i][0] for i in range(noData) if test_y[i] == crtLabel and computedOutputs[i] != crtLabel]
        y = [test_x[i][1] for i in range(noData) if test_y[i] == crtLabel and computedOutputs[i] != crtLabel]
        plt.scatter(x, y, label=crtLabel + ' (incorrect)')
    plt.xlabel('tx')
    plt.ylabel('rd')
    plt.legend()
    plt.show()

    print(f"Acc={accuracy_score(test_y, computedOutputs)}")

def get2():
    filePath = os.path.join(os.getcwd(), "../Input", "Multi/iris.data")
    data = loadData(filePath)
    data = [_ for _ in data if len(_)==5]
    print(data)
    inputs = [[float(_[0]), float(_[1]), float(_[2]), float(_[3])] for _ in data]
    outputs = [_[4] for _ in data]
    return inputs, outputs


def normalisation(trainData, testData):
    scaler = StandardScaler()
    if not isinstance(trainData[0], list):
        # encode each sample into a list
        trainData = [[d] for d in trainData]
        testData = [[d] for d in testData]

        scaler.fit(trainData)  # fit only on training data
        normalisedTrainData = scaler.transform(trainData)  # apply same transformation to train data
        normalisedTestData = scaler.transform(testData)  # apply same transformation to test data

        # decode from list to raw values
        normalisedTrainData = [el[0] for el in normalisedTrainData]
        normalisedTestData = [el[0] for el in normalisedTestData]
    else:
        scaler.fit(trainData)  # fit only on training data
        normalisedTrainData = scaler.transform(trainData)  # apply same transformation to train data
        normalisedTestData = scaler.transform(testData)  # apply same transformation to test data
    return normalisedTrainData, normalisedTestData

def solve2():
    inputs, outputs = get2()

    labels = set(outputs)
    noData = len(inputs)
    for crtLabel in labels:
        x = [inputs[i][0] for i in range(noData) if outputs[i] == crtLabel]
        y = [inputs[i][2] for i in range(noData) if outputs[i] == crtLabel]
        plt.scatter(x, y, label=crtLabel)
    plt.xlabel('Sepal Len')
    plt.ylabel('Petal Len')
    plt.legend()
    plt.show()

    plotDataHistogram([inputs[i][0] for i in range(noData)], "", (0, 0, 1, 0.5))
    plotDataHistogram([inputs[i][2] for i in range(noData)], "", (0, 1, 0, 0.5))
    plotDataHistogram(outputs, "Blue=Sepal Len, Green = Petal len", (1, 0, 0, 0.5))
    plt.show()

    train_x,train_y, test_x, test_y = split_train_and_test(inputs, outputs)

    train_x, test_x = normalisation(train_x, test_x)

    classifier = LogisticRegression()
    classifier.fit(train_x, train_y)
    w0, w1, w2, w3, w4 = classifier.intercept_[0], classifier.coef_[0][0], classifier.coef_[0][1] \
        , classifier.coef_[0][2], classifier.coef_[0][3]
    print(w0,w1,w2,w3,w4)
    w0, w1, w2, w3, w4 = classifier.intercept_[1], classifier.coef_[1][0], classifier.coef_[1][1] \
        , classifier.coef_[1][2], classifier.coef_[1][3]
    print(w0, w1, w2, w3, w4)
    w0, w1, w2, w3, w4 = classifier.intercept_[2], classifier.coef_[2][0], classifier.coef_[2][1] \
        , classifier.coef_[2][2], classifier.coef_[2][3]
    print(w0, w1, w2, w3, w4)

    computedOutputs = classifier.predict(test_x)

    labels = list(set(outputs))
    noData = len(test_x)
    for crtLabel in labels:
        x = [test_x[i][0] for i in range(noData) if test_y[i] == crtLabel and computedOutputs[i] == crtLabel]
        y = [test_x[i][2] for i in range(noData) if test_y[i] == crtLabel and computedOutputs[i] == crtLabel]
        plt.scatter(x, y, label=crtLabel + ' (correct)')
    for crtLabel in labels:
        x = [test_x[i][0] for i in range(noData) if test_y[i] == crtLabel and computedOutputs[i] != crtLabel]
        y = [test_x[i][2] for i in range(noData) if test_y[i] == crtLabel and computedOutputs[i] != crtLabel]
        plt.scatter(x, y, label=crtLabel + ' (incorrect)')
    plt.xlabel('tx')
    plt.ylabel('rd')
    plt.legend()
    plt.show()

    print(f"Acc={accuracy_score(test_y, computedOutputs)}")


if __name__=="__main__":
    #solve1()
    solve2()