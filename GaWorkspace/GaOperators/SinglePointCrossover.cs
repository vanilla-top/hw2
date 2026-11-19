namespace GaOperators;

public sealed class SinglePointCrossover : ICrossover
{
    private readonly double _crossoverProbability;

    public SinglePointCrossover(double crossoverProbability)
    {
        if (double.IsNaN(crossoverProbability) ||
            crossoverProbability < 0 ||
            crossoverProbability > 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(crossoverProbability),
                "Вероятность скрещивания должна быть в диапазоне [0, 1].");
        }

        _crossoverProbability = crossoverProbability;
    }

    public (Chromosome Child1, Chromosome Child2) Cross(
        Chromosome parent1,
        Chromosome parent2,
        Random rng)
    {
        if (parent1 is null)
        {
            throw new ArgumentNullException(nameof(parent1));
        }

        if (parent2 is null)
        {
            throw new ArgumentNullException(nameof(parent2));
        }

        if (rng is null)
        {
            throw new ArgumentNullException(nameof(rng));
        }

        if (parent1.Length != parent2.Length)
        {
            throw new ArgumentException(
                "Родители должны иметь одинаковую длину.");
        }

        // Для длины 1 нельзя выбрать точку разрыва с двумя непустыми частями.
        if (parent1.Length == 1 || rng.NextDouble() >= _crossoverProbability)
        {
            return (parent1.Clone(), parent2.Clone());
        }

        int breakPoint = rng.Next(1, parent1.Length);

        var child1Genes = new bool[parent1.Length];
        var child2Genes = new bool[parent1.Length];

        for (int i = 0; i < parent1.Length; i++)
        {
            if (i < breakPoint)
            {
                child1Genes[i] = parent1[i];
                child2Genes[i] = parent2[i];
            }
            else
            {
                child1Genes[i] = parent2[i];
                child2Genes[i] = parent1[i];
            }
        }

        return (
            new Chromosome(child1Genes),
            new Chromosome(child2Genes));
    }
}