using GaOperators;
using Xunit;

namespace GaOperators.Tests;

public class TournamentSelectionTests
{
    [Fact]
    public void Select_ReturnsCountIndices()
    {
        var pop = CreatePopulation(5, i => i);
        var result = new TournamentSelection(2).Select(pop, 10, new Random(1));
        Assert.Equal(10, result.Length);
    }

    [Fact]
    public void Select_AllIndicesInRange()
    {
        var pop = CreatePopulation(5, i => i);
        var result = new TournamentSelection(3).Select(pop, 50, new Random(42));
        Assert.All(result, i => Assert.InRange(i, 0, pop.Count - 1));
    }

    [Fact]
    public void Select_FullTournament_ReturnsGlobalMaximum()
    {
        var pop = CreatePopulation(5, i => i == 3 ? 100.0 : i);
        var result = new TournamentSelection(5).Select(pop, 20, new Random(7));
        Assert.All(result, i => Assert.Equal(3, i));
    }

    [Fact]
    public void Select_EachTournamentWinner_IsMaxAmongParticipants()
    {
        var pop = CreatePopulation(8, i => i * 1.5);
        const int count = 30;
        const int k = 3;

        var sel = new TournamentSelection(k);
        var r1 = sel.Select(pop, count, new Random(555));
        var r2 = sel.Select(pop, count, new Random(555));

        Assert.Equal(r1, r2);

        var verificationRng = new Random(555);
        for (int t = 0; t < count; t++)
        {
            var pool = Enumerable.Range(0, pop.Count).ToArray();
            int expectedBest = -1;
            double expectedFit = double.NegativeInfinity;

            for (int i = 0; i < k; i++)
            {
                int j = verificationRng.Next(i, pool.Length);
                (pool[i], pool[j]) = (pool[j], pool[i]);
                if (pop[pool[i]].Fitness > expectedFit)
                {
                    expectedFit = pop[pool[i]].Fitness;
                    expectedBest = pool[i];
                }
            }

            Assert.Equal(expectedBest, r1[t]);
        }
    }

    [Fact]
    public void Select_SameSeed_Reproducible()
    {
        var pop = CreatePopulation(6, i => i * 1.5);
        var sel = new TournamentSelection(3);

        var a = sel.Select(pop, 15, new Random(123));
        var b = sel.Select(pop, 15, new Random(123));

        Assert.Equal(a, b);
    }

    [Fact]
    public void Constructor_TournamentSizeLessThanTwo_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new TournamentSelection(1));
        Assert.Throws<ArgumentOutOfRangeException>(() => new TournamentSelection(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new TournamentSelection(-1));
    }

    [Fact]
    public void Select_TournamentSizeLargerThanPopulation_Throws()
    {
        var pop = CreatePopulation(3, i => i);
        var sel = new TournamentSelection(5);
        Assert.Throws<ArgumentException>(() => sel.Select(pop, 1, new Random(1)));
    }

    [Fact]
    public void Select_EmptyPopulation_Throws()
    {
        var sel = new TournamentSelection(2);
        Assert.Throws<ArgumentException>(
            () => sel.Select(Array.Empty<Chromosome>(), 1, new Random(1)));
    }

    [Fact]
    public void Select_NullArguments_Throw()
    {
        var sel = new TournamentSelection(2);
        var pop = CreatePopulation(3, i => i);

        Assert.Throws<ArgumentNullException>(() => sel.Select(null!, 1, new Random(1)));
        Assert.Throws<ArgumentNullException>(() => sel.Select(pop, 1, null!));
    }

    [Fact]
    public void Select_ZeroCount_ReturnsEmpty()
    {
        var pop = CreatePopulation(4, i => i);
        var result = new TournamentSelection(2).Select(pop, 0, new Random(1));
        Assert.Empty(result);
    }

    private static IReadOnlyList<Chromosome> CreatePopulation(int n, Func<int, double> fitness)
    {
        var list = new List<Chromosome>();
        for (int i = 0; i < n; i++)
        {
            list.Add(new Chromosome(new[] { i % 2 == 0, i % 3 == 0, i % 5 == 0 })
            {
                Fitness = fitness(i)
            });
        }
        return list;
    }
}