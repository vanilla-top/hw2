namespace GaOperators;

/// <summary>
/// Оператор мутации: создаёт изменённую копию хромосомы.
/// </summary>
public interface IMutation
{
    /// <summary>
    /// Мутирует копию исходной хромосомы. Исходная хромосома не изменяется.
    /// </summary>
    /// <param name="source">Хромосома, копию которой нужно мутировать.</param>
    /// <param name="rng">Генератор случайных чисел, переданный вызывающим кодом.</param>
    /// <returns>Новая хромосома — результат мутации.</returns>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="source"/> или <paramref name="rng"/> равен <see langword="null"/>.
    /// </exception>
    Chromosome Mutate(Chromosome source, Random rng);
}