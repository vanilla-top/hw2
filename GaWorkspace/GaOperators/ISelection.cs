namespace GaOperators;

/// <summary>
/// Оператор отбора: выбирает родителей из готовой популяции.
/// </summary>
public interface ISelection
{
    /// <summary>
    /// Проводит отбор и возвращает индексы выбранных хромосом в исходной популяции.
    /// </summary>
    /// <param name="population">
    /// Популяция, из которой выбираются родители. Оператор только читает список и не изменяет его.
    /// </param>
    /// <param name="count">Количество индексов родителей, которое нужно вернуть.</param>
    /// <param name="rng">Генератор случайных чисел, переданный вызывающим кодом.</param>
    /// <returns>
    /// Массив индексов выбранных хромосом длиной <paramref name="count"/>;
    /// каждый индекс находится в диапазоне <c>[0, population.Count)</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="population"/> или <paramref name="rng"/> равен <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">Если популяция пуста.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Если <paramref name="count"/> меньше нуля.
    /// </exception>
    int[] Select(IReadOnlyList<Chromosome> population, int count, Random rng);
}