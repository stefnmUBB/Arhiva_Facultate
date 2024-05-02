namespace AI.ML.Data
{
    public class PredictedData<T>
    {
        public T Real;
        public T Predicted;

        public PredictedData(T real, T predicted)
        {
            Real = real;
            Predicted = predicted;
        }

        public override string ToString()
        {
            return $"Real=({Real}), Predicted=({Predicted})";
        }


    }
}
