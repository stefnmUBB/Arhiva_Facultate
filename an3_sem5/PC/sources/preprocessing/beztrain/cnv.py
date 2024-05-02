import pandas as pd

with pd.HDFStore('model2.h5', 'r') as d:
    df = d.get('TheData')
    df.to_csv('myfile.csv')
