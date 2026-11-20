namespace GaOperators;

/// <summary>
/// Турнирный отбор: из случайно выбранных участников побеждает хромосома
/// с наибольшим значением <see cref="Chromosome.Fitness"/>.
/// </summary>
public sealed class TournamentSelection : ISelection
{
    private readonly int _tournamentSize;

    /// <summary>
    /// Создаёт оператор турнирного отбора.
    /// </summary>
    /// <param name="tournamentSize">
    /// Число участников одного турнира; должно быть не меньше 2.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Если <paramref name="tournamentSize"/> меньше 2.
    /// </exception>
    public TournamentSelection(int tournamentSize)
    {
        if (tournamentSize < 2)
            throw new ArgumentOutOfRangeException(
                nameof(tournamentSize),
                tournamentSize,
                "Размер турнира должен быть не меньше 2.");

        _tournamentSize = tournamentSize;
    }

    /// <summary>
    /// Проводит ровно <paramref name="count"/> независимых турниров
    /// и возвращает индексы победителей в исходной популяции.
    /// </summary>
    /// <param name="population">Популяция с заданными значениями <see cref="Chromosome.Fitness"/>.</param>
    /// <param name="count">Количество турниров и, соответственно, индексов в результате.</param>
    /// <param name="rng">Генератор случайных чисел.</param>
    /// <returns>Массив из <paramref name="count"/> индексов победителей.</returns>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="population"/> или <paramref name="rng"/> равен <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Если популяция пуста или размер турнира превышает размер популяции.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Если <paramref name="count"/> меньше нуля.
    /// </exception>
    public int[] Select(IReadOnlyList<Chromosome> population, int count, Random rng)
    {
        if (population is null) throw new ArgumentNullException(nameof(population));
        if (rng is null) throw new ArgumentNullException(nameof(rng));
        if (population.Count == 0)
            throw new ArgumentException("Популяция не должна быть пустой.", nameof(population));
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), count, "Количество должно быть неотрицательным.");
        if (_tournamentSize > population.Count)
            throw new ArgumentException("Размер турнира превышает размер популяции.", nameof(population));

        var result = new int[count];
        var pool = new int[population.Count];

        for (int t = 0; t < count; t++)
        {
            for (int i = 0; i < pool.Length; i++) pool[i] = i;

            int bestIdx = -1;
            double bestFit = double.NegativeInfinity;

            for (int i = 0; i < _tournamentSize; i++)
            {
                int j = rng.Next(i, pool.Length);
                (pool[i], pool[j]) = (pool[j], pool[i]);

                int idx = pool[i];
                double fit = population[idx].Fitness;

                if (fit > bestFit)
                {
                    bestFit = fit;
                    bestIdx = idx;
                }
            }

            result[t] = bestIdx;
        }

        return result;
    }
}