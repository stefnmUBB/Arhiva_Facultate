import tensorflow as tf

def split_input_target(sequence):
    input_text = sequence[:-1]
    target_text = sequence[1:]
    return input_text, target_text

class TextParams:

    def __init__(self, text):
        self.text=text
        self.vocab = sorted(set(text))
        self.chars = tf.strings.unicode_split(text, input_encoding='UTF-8')
        self.ids_from_chars = tf.keras.layers.StringLookup(vocabulary=list(self.vocab), mask_token=None)
        self.chars_from_ids = tf.keras.layers.StringLookup(vocabulary=self.ids_from_chars.get_vocabulary(), invert=True, mask_token=None)
        self.all_ids = self.ids_from_chars(tf.strings.unicode_split(self.text, 'UTF-8'))
        self.ids_dataset = tf.data.Dataset.from_tensor_slices(self.all_ids)

    def text_from_ids(self, ids):
        return tf.strings.reduce_join(self.chars_from_ids(ids), axis=-1)

    def get_sequences(self, seq_length=100):
        return self.ids_dataset.batch(seq_length+1, drop_remainder=True)

    def get_dataset(self):
        dataset = self.get_sequences().map(split_input_target)
        BATCH_SIZE = 64
        BUFFER_SIZE = 10000
        dataset = (dataset
            .shuffle(BUFFER_SIZE)
            .batch(BATCH_SIZE, drop_remainder=True)
            .prefetch(tf.data.experimental.AUTOTUNE))
        return dataset