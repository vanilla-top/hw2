namespace GaOperators;

public interface ISelection
{
    int[] Select(
        IReadOnlyList<Chromosome> population,
        int count,
        Random rng);
}

public interface ICrossover
{
    (Chromosome Child1, Chromosome Child2) Cross(
        Chromosome parent1,
        Chromosome parent2,
        Random rng);
}

public interface IMutation
{
    Chromosome Mutate(Chromosome source, Random rng);
}