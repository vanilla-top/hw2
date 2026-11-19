namespace GaOperators;

public sealed class Chromosome
{
    private readonly bool[] _genes;

    public Chromosome(bool[] genes)
    {
        if (genes is null)
        {
            throw new ArgumentNullException(nameof(genes));
        }

        if (genes.Length < 1)
        {
            throw new ArgumentException(
                "Хромосома должна содержать хотя бы один ген.",
                nameof(genes));
        }

        _genes = new bool[genes.Length];
        Array.Copy(genes, _genes, genes.Length);
    }

    public int Length => _genes.Length;

    public double Fitness { get; set; }

    public bool this[int index] => _genes[index];

    public IReadOnlyList<bool> Genes => Array.AsReadOnly(_genes);

    public bool[] ToArray()
    {
        var copy = new bool[_genes.Length];
        Array.Copy(_genes, copy, _genes.Length);
        return copy;
    }

    public Chromosome Clone()
    {
        var clone = new Chromosome(_genes)
        {
            Fitness = Fitness
        };

        return clone;
    }
}