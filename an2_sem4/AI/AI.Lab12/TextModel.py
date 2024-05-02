import tensorflow as tf
import os

from keras import Model

from TextUtils import TextParams

path_to_file = "C:\\Users\\Stefan\\Desktop\\chaste_dataset.txt"

text = open(path_to_file, 'rb').read().decode(encoding='utf-8').lower()

pms = TextParams(text)

dataset = pms.get_dataset()
print(dataset)

vocab_size = len(pms.ids_from_chars.get_vocabulary())
embedding_dim = 256
rnn_units = 1024


class TextGenModel(Model):
    def __init__(self, vocab_size, embedding_dim, rnn_units):
        super().__init__(self)
        self.embedding = tf.keras.layers.Embedding(vocab_size, embedding_dim)
        self.gru = tf.keras.layers.GRU(rnn_units, return_sequences=True, return_state=True)
        self.dense = tf.keras.layers.Dense(vocab_size)

    def call(self, inputs, states=None, return_state=False, training=False):
        x = self.embedding(inputs, training=training)
        if states is None:
            states = self.gru.get_initial_state(x)
        x, states = self.gru(x, initial_state=states, training=training)
        x = self.dense(x, training=training)

        if return_state:
            return x, states
        else:
            return x

def train_model(epochs=100):
    def train(epuchs_count=100):
        checkpoint_dir = './training_checkpoints'
        checkpoint_prefix = os.path.join(checkpoint_dir, "ckpt_{epoch}")
        checkpoint_callback = tf.keras.callbacks.ModelCheckpoint(filepath=checkpoint_prefix, save_weights_only=True)
        model.fit(dataset, epochs=epuchs_count, callbacks=[checkpoint_callback])

    model = TextGenModel(vocab_size=vocab_size, embedding_dim=embedding_dim, rnn_units=rnn_units)

    loss = tf.losses.SparseCategoricalCrossentropy(from_logits=True)
    model.compile(optimizer='adam', loss=loss)

    train(epochs)



class OneStep(Model):
    def __init__(self, model, chars_from_ids, ids_from_chars):
        super().__init__()
        self.model = model
        self.chars_from_ids = chars_from_ids
        self.ids_from_chars = ids_from_chars

    @tf.function
    def generate_one_step(self, inputs, states=None):
        input_chars = tf.strings.unicode_split(inputs, 'UTF-8')
        input_ids = self.ids_from_chars(input_chars).to_tensor()

        predicted_logits, states = self.model(inputs=input_ids, states=states,
                                              return_state=True)
        predicted_logits = predicted_logits[:, -1, :]

        predicted_ids = tf.random.categorical(predicted_logits, num_samples=1)
        predicted_ids = tf.squeeze(predicted_ids, axis=-1)

        predicted_chars = self.chars_from_ids(predicted_ids)

        return predicted_chars, states

model = TextGenModel(vocab_size=vocab_size, embedding_dim=embedding_dim, rnn_units=rnn_units)
model.load_weights(os.path.join('./weights', "ckpt_100"))
one_step_model = OneStep(model, pms.chars_from_ids, pms.ids_from_chars)

def generateNiceMessage(beginMsg):
    states = None
    next_char = tf.constant([beginMsg])
    result = [next_char]

    for n in range(256):
      next_char, states = one_step_model.generate_one_step(next_char, states=states)
      result.append(next_char)

    result = tf.strings.join(result)
    print(result[0].numpy().decode('utf-8'), '\n\n' + '_'*80)
