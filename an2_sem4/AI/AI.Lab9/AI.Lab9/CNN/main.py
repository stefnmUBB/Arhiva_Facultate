import math

import tensorflow as tf
from tensorflow import keras
from keras import datasets, layers, models

import matplotlib.pyplot as plt
import random


def to_px_list(row):
    res=[]
    for i in range(32):
        res.append(row[3*i:3*i+3])
    return res

def make_img(lst):
    res=[]
    for i in range(32):
        res.append(to_px_list(lst[96*i:96*i+96]))
    return res

def read_input():
    with open("C:\\Users\\Stefan\\Desktop\\sepiaData\\c\\sepia.dat", "r") as file:
        data = file.readlines()
        data = [[int(x) for x in _.split(' ')] for _ in data]
        data = [{"input":make_img([x/255.0 for x in _[:-1]]), "output":[_[-1]]} for _ in data]
        return data

def split_train_test(x):
    l = math.floor(len(x)*0.8)
    return [x[:l], x[l:]]

data=read_input()

plt.figure(figsize=(10,10))
for i in range(25):
    plt.subplot(5,5,i+1)
    plt.xticks([])
    plt.yticks([])
    plt.grid(False)
    plt.imshow(data[i]["input"])
plt.show()

random.shuffle(data)
train, test = split_train_test(data)

model = models.Sequential()
model.add(layers.Conv2D(32, (3, 3), activation='relu', input_shape=(32, 32, 3)))
model.add(layers.MaxPooling2D((2, 2)))
model.add(layers.Conv2D(64, (3, 3), activation='relu'))
model.add(layers.MaxPooling2D((2, 2)))
model.add(layers.Conv2D(64, (3, 3), activation='relu'))

model.summary()

model.add(layers.Flatten())
model.add(layers.Dense(64, activation='relu'))
model.add(layers.Dense(2))

model.compile(optimizer='adam',
              loss=tf.keras.losses.SparseCategoricalCrossentropy(from_logits=True),
              metrics=['accuracy'])

train_images = [_["input"] for _ in train]
train_labels = [_["output"] for _ in train]
test_images = [_["input"] for _ in test]
test_labels = [_["output"] for _ in test]

print(test_labels)

history = model.fit(train_images, train_labels, epochs=10,
                    validation_data=(test_images, test_labels))

plt.clf()
plt.plot(history.history['accuracy'], label='accuracy')
plt.plot(history.history['val_accuracy'], label = 'val_accuracy')
plt.xlabel('Epoch')
plt.ylabel('Accuracy')
plt.ylim([0.5, 1])
plt.legend(loc='lower right')
plt.show()

test_loss, test_acc = model.evaluate(test_images,  test_labels, verbose=2)

print("Loss = ",test_loss)
print("Accuracy = ",test_acc)