using System.Collections.ObjectModel;

namespace GaOperators;

/// <summary>
/// Бинарная хромосома фиксированной длины.
/// Значение <see langword="true"/> означает включённый ген, <see langword="false"/> — выключенный.
/// </summary>
public sealed class Chromosome
{
    private readonly bool[] _genes;
    private readonly ReadOnlyCollection<bool> _readOnlyGenes;

    /// <summary>
    /// Создаёт хромосому, копируя переданный массив генов.
    /// Последующие изменения исходного массива не влияют на хромосому.
    /// </summary>
    /// <param name="genes">Массив генов длиной не меньше 1.</param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="genes"/> равен <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Если длина массива меньше 1.
    /// </exception>
    public Chromosome(bool[] genes)
    {
        if (genes is null) throw new ArgumentNullException(nameof(genes));
        if (genes.Length < 1)
            throw new ArgumentException("Длина хромосомы должна быть не меньше 1.", nameof(genes));

        _genes = new bool[genes.Length];
        Array.Copy(genes, _genes, genes.Length);
        _readOnlyGenes = Array.AsReadOnly(_genes);
    }

    /// <summary>
    /// Длина хромосомы — количество генов.
    /// </summary>
    public int Length => _genes.Length;

    /// <summary>
    /// Приспособленность хромосомы. Значение задаёт вызывающий код;
    /// операторы не вычисляют приспособленность.
    /// </summary>
    public double Fitness { get; set; }

    /// <summary>
    /// Возвращает значение гена по указанному индексу.
    /// </summary>
    /// <param name="index">Индекс гена в диапазоне <c>[0, Length)</c>.</param>
    /// <returns>Значение гена: <see langword="true"/> или <see langword="false"/>.</returns>
    /// <exception cref="IndexOutOfRangeException">
    /// Если <paramref name="index"/> выходит за пределы хромосомы.
    /// </exception>
    public bool this[int index] => _genes[index];

    /// <summary>
    /// Доступный только для чтения список генов хромосомы.
    /// </summary>
    public IReadOnlyList<bool> Genes => _readOnlyGenes;

    /// <summary>
    /// Возвращает независимую копию массива генов.
    /// </summary>
    /// <returns>Новый массив с теми же значениями генов.</returns>
    public bool[] ToArray() => (bool[])_genes.Clone();

    /// <summary>
    /// Создаёт независимую копию хромосомы с тем же значением <see cref="Fitness"/>.
    /// </summary>
    /// <returns>Новый экземпляр <see cref="Chromosome"/> с копией генов.</returns>
    public Chromosome Clone() => new(_genes) { Fitness = Fitness };

    internal bool[] GenesInternal => _genes;

    /// <summary>
    /// Возвращает строковое представление генов, например <c>"1011"</c>.
    /// </summary>
    /// <returns>Строка из символов <c>'1'</c> и <c>'0'</c> длиной <see cref="Length"/>.</returns>
    public override string ToString()
    {
        var chars = new char[_genes.Length];
        for (int i = 0; i < _genes.Length; i++)
            chars[i] = _genes[i] ? '1' : '0';
        return new string(chars);
    }
}