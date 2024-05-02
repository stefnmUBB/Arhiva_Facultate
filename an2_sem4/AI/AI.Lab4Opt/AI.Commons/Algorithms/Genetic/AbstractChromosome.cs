using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Commons.Algorithms.Genetic
{
    public abstract class AbstractChromosome<P, R, F> : IChromosome<P, R, F> where F:IComparable
    {        
        public P ProblemParam { get; }

        private F _Fitness;
        public F Fitness => _Fitness;              

        protected R _Representation;

        public R Representation => _Representation;

        public List<IChromosome<P, R, F>> Crossover(IChromosome<P, R, F> other)
        {
            if (this.GetType() != other.GetType())
                throw new ArgumentException("Attempting to crossover different type chromosomes");
            return PerformCrossover(other);
        }

        public abstract List<IChromosome<P, R, F>> PerformCrossover(IChromosome<P, R, F> other);

        public abstract void Mutate();
        protected abstract F ComputeFitness();        

        public void SetRepresentation(R r)
        {
            _Representation = r;
            _Fitness = ComputeFitness();
        }

        public AbstractChromosome(P problemParam)
        {
            ProblemParam = problemParam;            
        }

        public override string ToString()
        {
            return $"(Chromo R=({Representation}) F=({Fitness}))";
        }

        public string ToString(Func<R, string> reprSerializer)
            => $"(Chromo R=({reprSerializer(Representation)}) F=({Fitness}))";
    }
}
