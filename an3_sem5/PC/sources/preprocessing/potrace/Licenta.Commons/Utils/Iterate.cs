using System.Collections.Generic;
namespace Licenta.Commons.Utils
{
    public static class Iterate
    {
        public static IEnumerable<(int I, int J)> Rectangle(int i0, int j0, int i1, int j1)
        {
            for (int i = i0; i < i1; i++)
                for (int j = j0; j < j1; j++) 
                    yield return (i, j);
        }
    }
}
