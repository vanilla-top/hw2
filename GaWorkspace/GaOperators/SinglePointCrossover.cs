namespace GaOperators;

/// <summary>
/// Одноточечное скрещивание: родители обмениваются частями после случайной точки разрыва.
/// </summary>
public sealed class SinglePointCrossover : ICrossover
{
    private readonly double _probability;

    /// <summary>
    /// Создаёт оператор одноточечного скрещивания.
    /// </summary>
    /// <param name="crossoverProbability">
    /// Вероятность выполнить скрещивание; должна быть в диапазоне <c>[0, 1]</c>.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Если <paramref name="crossoverProbability"/> меньше 0, больше 1 или равен <see cref="double.NaN"/>.
    /// </exception>
    public SinglePointCrossover(double crossoverProbability)
    {
        if (!(crossoverProbability >= 0 && crossoverProbability <= 1))
            throw new ArgumentOutOfRangeException(nameof(crossoverProbability));

        _probability = crossoverProbability;
    }

    /// <summary>
    /// Скрещивает родителей в случайной точке разрыва.
    /// Если скрещивание не выполняется или длина хромосомы равна 1,
    /// возвращаются независимые клоны родителей.
    /// </summary>
    /// <param name="parent1">Первый родитель.</param>
    /// <param name="parent2">Второй родитель той же длины.</param>
    /// <param name="rng">Генератор случайных чисел.</param>
    /// <returns>Пара новых хромосом — потомков.</returns>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="parent1"/>, <paramref name="parent2"/> или <paramref name="rng"/>
    /// равен <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Если длины родителей различаются.
    /// </exception>
    public (Chromosome Child1, Chromosome Child2) Cross(
        Chromosome parent1, Chromosome parent2, Random rng)
    {
        if (parent1 is null) throw new ArgumentNullException(nameof(parent1));
        if (parent2 is null) throw new ArgumentNullException(nameof(parent2));
        if (rng is null) throw new ArgumentNullException(nameof(rng));
        if (parent1.Length != parent2.Length)
            throw new ArgumentException("Родители должны иметь одинаковую длину.", nameof(parent2));

        int n = parent1.Length;

        if (n < 2 || rng.NextDouble() >= _probability)
            return (parent1.Clone(), parent2.Clone());

        int point = rng.Next(1, n);

        bool[] g1 = parent1.ToArray();
        bool[] g2 = parent2.ToArray();

        var c1 = new bool[n];
        var c2 = new bool[n];

        for (int i = 0; i < n; i++)
        {
            bool fromFirst = i < point;
            c1[i] = fromFirst ? g1[i] : g2[i];
            c2[i] = fromFirst ? g2[i] : g1[i];
        }

        return (new Chromosome(c1), new Chromosome(c2));
    }
}