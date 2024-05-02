namespace AI.Lab1.Solvers
{
    internal class Matrix
    {
        int[,] data;

        public Matrix(int rows, int cols)
        {
            data = new int[rows, cols];
        }

        public int RowsCount => data.GetLength(0);
        public int ColsCount => data.GetLength(1);
        public int this[int i, int j]
        {
            get => data[i, j];
            set => data[i, j] = value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Matrix matrix &&
                   RowsCount == matrix.RowsCount &&
                   ColsCount == matrix.ColsCount))
                return false;
            Matrix other = (Matrix)obj;
            for (int i = 0; i < RowsCount; i++)
                for (int j = 0; j < ColsCount; j++)
                    if (this[i, j] != other[i, j])
                        return false;
            return true;
        }       

        public override string ToString()
        {
            string result = "";

            for(int i=0;i<RowsCount;i++)
            {
                for(int j=0;j<ColsCount;j++)
                {
                    result += this[i, j].ToString() + " ";
                }
                result += "\n";
            }

            return result;
        }        
    }
}