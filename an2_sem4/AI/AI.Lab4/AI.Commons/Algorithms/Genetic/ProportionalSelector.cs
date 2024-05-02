using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Commons.Algorithms.Genetic
{
    internal class ProportionalSelector<P, R, F, C, POP> : IndividualsSelector<P, R, F, C, POP>
        where F : IComparable
        where C : AbstractChromosome<P, R, F>        
        where POP : AbstractPopulation<P, R, F, C>
    {
        public override void SelectionAction()
        {
            int size = Population.Count + Population.Gods.Count;
            if (size == 0) return;

            var pop = Population.Scope.Concat(Population.Gods);
            var parent1 = Utils.Random.ValueFromContainer(pop);
            var parent2 = Utils.Random.ValueFromContainer(pop);

            var chromos = parent1.Chromosome.Crossover(parent2.Chromosome);

            foreach (var chromo in chromos)
            {
                var child = (Individual<P, R, F, C>)Activator.CreateInstance(typeof(Individual<P, R, F, C>), new object[] { chromo });
                Children.Add(child);
            }
        }
    }
}
