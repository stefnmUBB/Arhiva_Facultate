import csv
import os
from math import sqrt

from sklearn.metrics import mean_absolute_error, mean_squared_error, confusion_matrix, precision_score, accuracy_score, \
    recall_score


def loadData(fileName):
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
    return data

crtDir = os.getcwd()

print("\nSports\n")
filePath = os.path.join(crtDir, "../Input", 'sport.csv')
sports = loadData(filePath)
sports = [[int(_) for _ in item] for item in sports]

sports_real = [[_[0],_[1],_[2]] for _ in sports]
sports_pred = [[_[3],_[4],_[5]] for _ in sports]

print(f"Real = {sports_real}")
print(f"Pred = {sports_pred}")

print(f"Mean Absolute error = {mean_absolute_error(sports_real, sports_pred)}")
print(f"Mean Squared error = {mean_squared_error(sports_real, sports_pred)}")
print(f"Room Mean Squared error = {sqrt(mean_squared_error(sports_real, sports_pred))}")

print("\nFlowers\n")

filePath = os.path.join(crtDir, "../Input", 'flowers.csv')
flowers = loadData(filePath)

flowers_real = [_[0] for _ in flowers]
flowers_pred = [_[1] for _ in flowers]

print(f"Real = {flowers_real}")
print(f"Pred = {flowers_pred}")

conf_matrix = confusion_matrix(flowers_real, flowers_pred)
print(conf_matrix)

print(f"Accuracy = {accuracy_score(flowers_real, flowers_pred)}")
print(f"Daisy P+ = {precision_score(flowers_real, flowers_pred, labels=['Daisy'], average='weighted')}")
print(f"Daisy R+ = {recall_score(flowers_real, flowers_pred, labels=['Daisy'], average='weighted')}")

print(f"Tulip P+ = {precision_score(flowers_real, flowers_pred, labels=['Tulip'], average='weighted')}")
print(f"Tulip R+ = {recall_score(flowers_real, flowers_pred, labels=['Tulip'], average='weighted')}")

print(f"Rose P+ = {precision_score(flowers_real, flowers_pred, labels=['Rose'], average='weighted')}")
print(f"Rose R+ = {recall_score(flowers_real, flowers_pred, labels=['Rose'], average='weighted')}")

print(f"Precision+ = {precision_score(flowers_real, flowers_pred,labels=['Rose','Tulip','Daisy'], average='macro')}")
print(f"Recall+ = {recall_score(flowers_real, flowers_pred, average='macro')}")