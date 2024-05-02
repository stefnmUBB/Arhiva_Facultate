using AI.Lab11.Tools;
using AI.Lab11.Utils;
using Keras;
using Keras.Callbacks;
using Keras.Layers;
using Keras.Models;
using Keras.PreProcessing.Image;
using Numpy;
using OpenCvSharp;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab11.Solvers
{
    public class FaceSolver
    {
        Sequential Model = new Sequential();

        string TrainFolder { get; }
        string TestFolder { get; }

        public FaceSolver(string trainFolder, string testFolder)
        {
            Model.Add(new Conv2D(32, Tuple.Create(3, 3), activation: "relu", input_shape: new Shape(48, 48, 1)));
            Model.Add(new Conv2D(/*64*/8, Tuple.Create(3, 3), activation: "relu"));
            Model.Add(new MaxPooling2D(Tuple.Create(2, 2)));
            Model.Add(new Dropout(0.25));

            Model.Add(new Conv2D(8 /*128*/, Tuple.Create(3, 3), activation: "relu"));
            Model.Add(new MaxPooling2D(Tuple.Create(2, 2)));
            Model.Add(new Conv2D(8 /*128*/, Tuple.Create(3, 3), activation: "relu"));
            Model.Add(new MaxPooling2D(Tuple.Create(2, 2)));
            Model.Add(new Dropout(0.25));

            Model.Add(new Flatten());
            Model.Add(new Dense(/*1024*/128, activation: "relu"));
            Model.Add(new Dropout(0.5));
            Model.Add(new Dense(7, activation: "softmax"));
            Model.Compile(optimizer: "Adam", loss: "categorical_crossentropy", metrics: new string[] { "accuracy" });

            TrainFolder = trainFolder;
            TestFolder = testFolder;
        }

        KerasIterator TrainGenerator;
        KerasIterator TestGenerator;

        int TrainCount;
        int TestCount;

        public string[] Labels { get; private set; }
        public int BatchSize { get; set; } = 64;

        public void Load()
        {
            TrainCount = Directory.GetFiles(TrainFolder, "*.png", SearchOption.AllDirectories).Length;
            TestCount = Directory.GetFiles(TestFolder, "*.png", SearchOption.AllDirectories).Length;

            var trainDataGen = new ImageDataGenerator(rescale: 1.0f / 255);
            TrainGenerator = trainDataGen.FlowFromDirectory(TrainFolder,
                target_size: Tuple.Create(48, 48),
                batch_size: BatchSize,
                color_mode: "grayscale",
                class_mode: "categorical");

            var classes = TrainGenerator.PyObject.GetAttr("class_indices");
            var dict_keys = classes.GetAttr("keys").Invoke();
            Labels = dict_keys.As<string[]>();            

            var testDataGen = new ImageDataGenerator(rescale: 1.0f / 255);
            TestGenerator = trainDataGen.FlowFromDirectory(TestFolder,
                target_size: Tuple.Create(48, 48),
                batch_size: BatchSize,
                color_mode: "grayscale",
                class_mode: "categorical");     
        }

        History History;

        public int EpochsCount { get; set; } = 1;

        public void Train()
        {            
            History = Model.FitGenerator(TrainGenerator,
                steps_per_epoch: TrainCount / BatchSize,
                epochs: EpochsCount,
                validation_data: TestGenerator,
                validation_steps: TestCount / BatchSize);                        
            Loss = History.HistoryLogs["loss"].ToArray();
            ValLoss = History.HistoryLogs["val_loss"].ToArray();
            Accuracy = History.HistoryLogs["accuracy"].ToArray();
            ValAccuracy = History.HistoryLogs["val_accuracy"].ToArray();                       
        }


        public IEnumerable<string> PredictClass(IEnumerable<Mat> faces)
        {
            return Predict(faces).Select(_ => Labels[_.ArgMax()]);
        }

        public IEnumerable<int> PredictClassIndex(IEnumerable<Mat> faces)
        {
            return Predict(faces).Select(_ => _.ArgMax());
        }

        public IEnumerable<double[]> Predict(IEnumerable<Mat> faces)
        {
            foreach (var face in faces) 
            {
                var mat = face.Resize(new Size(48, 48));
                mat.GetArray(out byte[] array);
                var m = new byte[48, 48, 1];
                for(int y=0;y<48;y++)
                {
                    for (int x = 0; x < 48; x++)
                        m[y, x, 0] = array[48 * y + x];
                }

                var img = np.expand_dims(np.expand_dims(np.array(m, dtype: np.uint8), -1), 0);                
                var result = Model.Predict(img);
                yield return result.GetData<double>();                
            }
        }

        public double[] Loss { get; private set; }
        public double[] ValLoss { get; private set; }
        public double[] Accuracy { get; private set; }
        public double[] ValAccuracy { get; private set; }
    }
}
