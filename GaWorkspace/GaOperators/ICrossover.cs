namespace GaOperators;

/// <summary>
/// Оператор скрещивания: создаёт двух потомков из двух родителей.
/// </summary>
public interface ICrossover
{
    /// <summary>
    /// Скрещивает двух родителей и возвращает двух новых потомков.
    /// Родительские хромосомы не изменяются.
    /// </summary>
    /// <param name="parent1">Первый родитель.</param>
    /// <param name="parent2">Второй родитель; должен иметь ту же длину, что и <paramref name="parent1"/>.</param>
    /// <param name="rng">Генератор случайных чисел, переданный вызывающим кодом.</param>
    /// <returns>
    /// Пара новых хромосом — потомков. Если скрещивание не выполняется,
    /// возвращаются независимые клоны родителей.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="parent1"/>, <paramref name="parent2"/> или <paramref name="rng"/>
    /// равен <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Если длины родителей различаются.
    /// </exception>
    (Chromosome Child1, Chromosome Child2) Cross(
        Chromosome parent1,
        Chromosome parent2,
        Random rng);
}