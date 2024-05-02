using AI.Commons.UI.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Commons.Algorithms.Genetic
{    
    public abstract class AbstractPopulation<P, R, F, C>
        where F:IComparable
        where C:AbstractChromosome<P,R,F>        
    {        

        class IndividualsComparer : IComparer<Individual<P, R, F, C>>
        { 
            public int Compare(Individual<P,R,F,C> i1, Individual<P, R, F, C> i2)
            {
                int c = i2.Chromosome.Fitness.CompareTo(i1.Chromosome.Fitness);
                if (c == 0)
                {
                    return i2.Chromosome.Equals(i1.Chromosome) ? 0 : 1;
                }
                return c;
            }
        }

        public P ProblemParam { get; set; }
        public SortedSet<Individual<P, R, F, C>> Scope { get; } = new SortedSet<Individual<P, R, F, C>>(new IndividualsComparer());

        public SortedSet<Individual<P, R, F, C>> Gods { get; } = new SortedSet<Individual<P, R, F, C>>(new IndividualsComparer());

        public IndividualsSelector<P, R, F, C, AbstractPopulation<P, R, F, C>> Selector { get; private set; }            

        public void SetSelector(IndividualsSelector<P, R, F, C, AbstractPopulation<P, R, F, C>> selector)
        {            
            selector.Population = this;
            Selector = selector;
        }

        Random rand = new Random();

        public int MaxLifespan { get; set; } = 100;

        public double MutationProbability { get; set; } = 0.1;

        bool FitnessStatisticsRunning = false;
        F WorstFitness;
        F BestFitness;

        public int SelectionFactor = 8;

        public void RunStep()
        {

            for (int i = 0; i < SelectionFactor * (Scope.Count + Gods.Count); i++) 
                Selector.PerformSelection();

            foreach (var individual in Scope)
            {
                individual.Age++;
            }

            Selector.Children.ForEach(c =>
            {
                if (rand.Next() < MutationProbability * 100)
                    c.Chromosome.Mutate();
                Scope.Add(c);
            });

            if (Scope.Count > 0)
            {
                if (!Gods.Contains(Scope.First())) 
                {
                    if (Gods.Count < 20) 
                        Gods.Add(Scope.First());
                    else
                    {
                        Gods.Add(Scope.First());
                        Gods.Remove(Gods.Last());
                    }
                    LocalBestFound?.Invoke(this, Scope.First().Chromosome);
                }
            }

            RemoveOldIndividuals();
            RemoveWeakIndividuals();            

            CollectStatistics();

            StepEnd?.Invoke(this);
        }

        public delegate void OnLocalBestFound(object sender, IChromosome<P, R, F> target);
        public event OnLocalBestFound LocalBestFound;


        void CollectStatistics()
        {
            if (Scope.Count == 0) return;
            if(!FitnessStatisticsRunning)
            {
                FitnessStatisticsRunning = true;
                BestFitness = Scope.First().Chromosome.Fitness;                    
                WorstFitness = Scope.Last().Chromosome.Fitness;
            }

            BestFitness = BestFitness.CompareTo(Scope.First().Chromosome.Fitness) < 0
                    ? Scope.First().Chromosome.Fitness : BestFitness;
            WorstFitness = WorstFitness.CompareTo(Scope.Last().Chromosome.Fitness) > 0
                ? Scope.Last().Chromosome.Fitness : WorstFitness;
        }

        public delegate void OnStepEnd(object sender);
        public event OnStepEnd StepEnd;

        double sel_f(double x)
        {
            return x * x * x * x * x * Math.Sin(Math.PI / 2 * x * x);
        }

        private void RemoveOldIndividuals() => Scope.RemoveWhere(i =>
        {            
            var selprob = Utils.Random.SelectionProbability(sel_f, i.Age, MaxLifespan);
            return Utils.Random.FromRange(0, 100) < selprob * 100;
        });            

        private void RemoveWeakIndividuals()
        {
            if (Scope.Count == 0)
                return;
            F avg = AverageFitness;

            F first = Scope.Select(i => i.Chromosome.Fitness).First();
            F last = Scope.Select(i => i.Chromosome.Fitness).Last();

            F ampl = (F)Math.Max((dynamic)last - avg, (dynamic)avg - last);

            if ((dynamic)ampl == 0) return;

            Scope.RemoveWhere(i =>
                (((dynamic)i.Chromosome.Fitness - avg) * 100 / (dynamic)ampl) < -50
                && rand.Next() % 100 < 3);
        }     

        private F TotalFitness => Scope.Select(i => i.Chromosome.Fitness).Aggregate((agg, next)
            => (F)((dynamic)agg + (dynamic)next));

        public F AverageFitness => Scope.Count == 0 ? default : (F)((dynamic)TotalFitness / Scope.Count);

        public int Count => Scope.Count;

        public AbstractPopulation(P problemParam)
        {
            ProblemParam = problemParam;            
            SetSelector(new ProportionalSelector<P, R, F, C, AbstractPopulation<P, R, F, C>>());
        }

        public void AddIndividual(C chromosome)
        {
            Scope.Add(new Individual<P, R, F, C>(chromosome));            
        }
    }
}
