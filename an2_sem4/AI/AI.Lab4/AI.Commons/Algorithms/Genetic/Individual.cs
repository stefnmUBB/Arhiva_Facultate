using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Commons.Algorithms.Genetic
{
    public class Individual<P,R,F,C> 
        where F:IComparable 
        where C : AbstractChromosome<P, R, F>
    {
        public C Chromosome { get; }
        public int Age { get; internal set; } = 0;

        public Individual(C chromosome)
        {
            Chromosome = chromosome;            
        }

        public Individual(P problemParam)
        {
            Chromosome = (C)Activator.CreateInstance(typeof(C), new object[] { problemParam });            
        }

        public override string ToString()
        {
            return $"(I Age={Age}, Chromo={Chromosome})";
        }
    }
}
