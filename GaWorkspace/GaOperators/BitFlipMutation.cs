namespace GaOperators;

public sealed class BitFlipMutation : IMutation
{
    private readonly double _mutationProbability;

    public BitFlipMutation(double mutationProbability)
    {
        if (double.IsNaN(mutationProbability) ||
            mutationProbability < 0 ||
            mutationProbability > 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(mutationProbability),
                "Вероятность мутации должна быть в диапазоне [0, 1].");
        }

        _mutationProbability = mutationProbability;
    }

    public Chromosome Mutate(Chromosome source, Random rng)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (rng is null)
        {
            throw new ArgumentNullException(nameof(rng));
        }

        bool[] genes = source.ToArray();

        for (int i = 0; i < genes.Length; i++)
        {
            if (rng.NextDouble() < _mutationProbability)
            {
                genes[i] = !genes[i];
            }
        }

        return new Chromosome(genes)
        {
            Fitness = source.Fitness
        };
    }
}