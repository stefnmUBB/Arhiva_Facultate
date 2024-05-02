using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Commons.Algorithms.Genetic
{
    public interface IChromosome<P,R,F> where F:IComparable
    {
        P ProblemParam { get; }
        F Fitness { get; }
        R Representation { get; }
        List<IChromosome<P, R, F>> Crossover(IChromosome<P, R, F> other);
        void Mutate();
    }
}
