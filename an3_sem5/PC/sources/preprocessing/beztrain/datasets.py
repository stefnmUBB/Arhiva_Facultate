from random import randint, choice
import numpy as np

def load_bezier_dataset():
    path ="D:\\Users\\Stefan\\fuck_it_licenta\\proiect_licenta\\app\\Licenta\\Helpers.CurveDetectorDataSetGenerator\\bin\\Debug\\bezier.csv"
    with open(path, 'r') as f:
        lines = [_.split(",") for _ in f.readlines()]
        inputs = np.array([np.array([float(_) for _ in line[:64*64]]) for line in lines])
        inputs = [ np.array([i[64*x:64*(x+1)] for x in range(64)]) for i in inputs ]
        outputs = np.array([np.array([float(_) for _ in line[64*64:]]) for line in lines])
        return inputs, outputs

def split_train_test(inputs, outputs):
    l = len(inputs)
    r = [0 if randint(0,10)<8 else 1 for _ in range(l)]

    train_i = np.array([inputs[i] for i in range(l) if r[i]==0], np.float)
    train_o = np.array([outputs[i] for i in range(l) if r[i] == 0], np.float)

    test_i = np.array([inputs[i] for i in range(l) if r[i] == 1], np.float)
    test_o = np.array([outputs[i] for i in range(l) if r[i] == 1], np.float)

    return train_i, train_o, test_i, test_o


