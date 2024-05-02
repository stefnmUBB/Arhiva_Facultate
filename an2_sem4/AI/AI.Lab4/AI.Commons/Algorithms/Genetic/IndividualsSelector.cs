using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Commons.Algorithms.Genetic
{
    public abstract class IndividualsSelector<P, R, F, C, POP>
        where F : IComparable
        where C : AbstractChromosome<P, R, F>        
        where POP : AbstractPopulation<P, R, F, C>    
    {
        public POP Population { get; set; }

        public List<Individual<P, R, F, C>> Children = new List<Individual<P, R, F, C>>();

        public virtual void SelectionInit() { }
        public abstract void SelectionAction();

        public virtual void PerformSelection()
        {
            Children.Clear();
            SelectionInit();
            SelectionAction();
        }
    }
}
