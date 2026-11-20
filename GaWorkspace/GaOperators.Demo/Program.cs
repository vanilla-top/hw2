using GaOperators;

var rng = new Random(123);

var population = new List<Chromosome>
{
    new(new[] { true, false, true, true }) { Fitness = 3.5 },
    new(new[] { false, true, true, false }) { Fitness = 5.1 },
    new(new[] { true, true, false, false }) { Fitness = 4.2 },
    new(new[] { false, false, true, true }) { Fitness = 2.8 },
};

Console.WriteLine("Исходная популяция:");
foreach (var c in population)
    Console.WriteLine($"  {c}  Fitness = {c.Fitness}");

ISelection selection = new TournamentSelection(tournamentSize: 2);
int[] parents = selection.Select(population, count: 2, rng);
Console.WriteLine($"\nВыбраны индексы родителей: [{parents[0]}, {parents[1]}]");

Chromosome p1 = population[parents[0]];
Chromosome p2 = population[parents[1]];
Console.WriteLine($"Родитель 1: {p1}  Fitness = {p1.Fitness}");
Console.WriteLine($"Родитель 2: {p2}  Fitness = {p2.Fitness}");

ICrossover crossover = new SinglePointCrossover(crossoverProbability: 0.9);
var (child1, child2) = crossover.Cross(p1, p2, rng);
Console.WriteLine($"\nSinglePointCrossover:");
Console.WriteLine($"  Потомок 1: {child1}");
Console.WriteLine($"  Потомок 2: {child2}");

IMutation mutation = new BitFlipMutation(mutationProbability: 0.25);
Chromosome mutated = mutation.Mutate(child1, rng);
Console.WriteLine($"\nМутация потомка 1: {child1} -> {mutated}");

ICrossover uniform = new UniformCrossover(crossoverProbability: 1.0);
var (u1, u2) = uniform.Cross(p1, p2, rng);
Console.WriteLine($"\nUniformCrossover:");
Console.WriteLine($"  Потомок 1: {u1}");
Console.WriteLine($"  Потомок 2: {u2}");

Console.WriteLine($"\nПосле всех операций родители не изменились:");
Console.WriteLine($"  Родитель 1: {p1}  Fitness = {p1.Fitness}");
Console.WriteLine($"  Родитель 2: {p2}  Fitness = {p2.Fitness}");