from keras import models, layers, activations
import tensorflow as tf
import numpy as np

model = models.Sequential()
model.add(layers.Conv2D(32, (5, 5), activation=activations.gelu, input_shape=(64, 64, 1)))
model.add(layers.MaxPooling2D((2, 2)))
model.add(layers.Conv2D(96, (3, 3), activation=activations.exponential))
model.add(layers.MaxPooling2D((2, 2)))
model.add(layers.Conv2D(64, (3, 3), activation=activations.exponential))
model.add(layers.Flatten())
model.add(layers.Dense(600, activation=activations.leaky_relu))
model.add(layers.Dense(104))

model.load_weights('C:\\Users\\Stefan\\Pictures\\beztrain\\1\\model2.h5')
model.compile(optimizer='adam',
              loss=tf.keras.losses.MeanAbsoluteError(),
              metrics=['accuracy'])

import sys
input = np.array(list(map(float, sys.argv[1].split(" "))))
input = np.array([input[64*x:64*(x+1)] for x in range(64)])
#print(input.shape)
#print(input)

res = model.predict(np.array([input]), verbose=0)[0]

print(' '.join([str(i*64) for i in res]))