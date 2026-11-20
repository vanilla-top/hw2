namespace GaOperators;

/// <summary>
/// Инвертирует отдельные гены хромосомы с заданной вероятностью.
/// </summary>
public sealed class BitFlipMutation : IMutation
{
    private readonly double _probability;

    /// <summary>
    /// Создаёт оператор побитовой мутации.
    /// </summary>
    /// <param name="mutationProbability">
    /// Вероятность инвертировать каждый отдельный ген; должна быть в диапазоне <c>[0, 1]</c>.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Если <paramref name="mutationProbability"/> меньше 0, больше 1 или равен <see cref="double.NaN"/>.
    /// </exception>
    public BitFlipMutation(double mutationProbability)
    {
        if (!(mutationProbability >= 0 && mutationProbability <= 1))
            throw new ArgumentOutOfRangeException(nameof(mutationProbability));

        _probability = mutationProbability;
    }

    /// <summary>
    /// Возвращает мутированную копию хромосомы. Каждый ген независимо
    /// инвертируется с вероятностью, заданной в конструкторе.
    /// </summary>
    /// <param name="source">Исходная хромосома; не изменяется.</param>
    /// <param name="rng">Генератор случайных чисел.</param>
    /// <returns>Новая хромосома с копией значения <see cref="Chromosome.Fitness"/> исходной.</returns>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="source"/> или <paramref name="rng"/> равен <see langword="null"/>.
    /// </exception>
    public Chromosome Mutate(Chromosome source, Random rng)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (rng is null) throw new ArgumentNullException(nameof(rng));

        bool[] genes = source.ToArray();

        for (int i = 0; i < genes.Length; i++)
            if (rng.NextDouble() < _probability)
                genes[i] = !genes[i];

        return new Chromosome(genes) { Fitness = source.Fitness };
    }
}