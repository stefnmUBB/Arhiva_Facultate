namespace AI.Commons.Algorithms.Genetic
{
    public abstract class RealFitnessChromosome<P, R> : AbstractChromosome<P, R, double>
    {
        protected RealFitnessChromosome(P problemParam) : base(problemParam)
        {
        }
    }
}
