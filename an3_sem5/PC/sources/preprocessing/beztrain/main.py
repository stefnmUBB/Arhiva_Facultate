import tensorflow as tf
from keras import datasets, layers, models, activations
import matplotlib.pyplot as plt

from datasets import load_bezier_dataset, split_train_test

inputs, outputs = load_bezier_dataset()

train_i, train_o, test_i, test_o = split_train_test(inputs, outputs)

print(train_i.shape)
print(train_o.shape)

print(test_i.shape)
print(test_o.shape)

model = models.Sequential()
model.add(layers.Conv2D(32, (5, 5), activation=activations.gelu, input_shape=(64, 64, 1)))
model.add(layers.MaxPooling2D((2, 2)))
model.add(layers.Conv2D(96, (3, 3), activation=activations.exponential))
model.add(layers.MaxPooling2D((2, 2)))
model.add(layers.Conv2D(64, (3, 3), activation=activations.exponential))
model.add(layers.Flatten())
model.add(layers.Dense(600, activation=activations.leaky_relu))
model.add(layers.Dense(104))

model.summary()

print("Compiling")
model.compile(optimizer='adam',
              loss=tf.keras.losses.MeanAbsoluteError(),
              metrics=['accuracy'])

print("Training")

history = model.fit(train_i, train_o, epochs=2000, validation_data=(test_i, test_o), verbose=2)

plt.plot(history.history['accuracy'], label='accuracy')
plt.plot(history.history['val_accuracy'], label = 'val_accuracy')
plt.xlabel('Epoch')
plt.ylabel('Accuracy')
plt.ylim([0, 1])
plt.legend(loc='lower right')
plt.show()

test_loss, test_acc = model.evaluate(test_i, test_o, verbose=2)

print(test_acc)

model.save_weights("model2.h5",save_format='h5')