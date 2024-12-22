namespace AOC24;

class Day13 : IAdventDay
{
	public bool IsActive => false;

	public void Solve(string[] input)
	{
		var tokensUsed1 = 0L;
		var tokensUsed2 = 0L;
		var machines = CreateMachinesFromInput(input);

		const long p2 = 10000000000000L;
		foreach (var machine in machines)
		{
			tokensUsed1 += CalculateTokensUsed(machine);

			machine.Price = (machine.Price.x + p2, machine.Price.y + p2);
			tokensUsed2 += CalculateTokensUsed(machine);
		}

		Console.WriteLine($"Part 1: {tokensUsed1}");
		Console.WriteLine($"Part 2: {tokensUsed2}");
	}

	static long CalculateTokensUsed(ArcadeMachine mac)
	{
		var determinant = (mac.A.x * mac.B.y) - (mac.A.y * mac.B.x);

		// Determinants for A and B matrices using Cramer's rule.
		var determinantA = (mac.Price.x * mac.B.y) - (mac.Price.y * mac.B.x);
		var determinantB = (mac.A.x * mac.Price.y) - (mac.A.y * mac.Price.x);

		// Make sure that both A and B are integers (no fractions).
		if (determinantA % determinant != 0 || determinantB % determinant != 0)
		{
			return 0;
		}

		return ((determinantA / determinant) * 3) + (determinantB / determinant);
	}

	static List<ArcadeMachine> CreateMachinesFromInput(string[] input)
	{
		var machines = new List<ArcadeMachine>();

		for (var i = 0; i < input.Length; i += 2)
		{
			var machine = new ArcadeMachine();

			var split = input[i].Split(':');
			machine.A = ParseLineIntoXY(split[1], '+');

			split = input[++i].Split(':');
			machine.B = ParseLineIntoXY(split[1], '+');

			split = input[++i].Split(':');
			machine.Price = ParseLineIntoXY(split[1], '=');

			machines.Add(machine);
		}

		return machines;
	}

	static (long x, long y) ParseLineIntoXY(string row, char c)
	{
		var split = row.Split(',');
		var x = long.Parse(split[0].Split(c)[1]);
		var y = long.Parse(split[1].Split(c)[1]);

		return (x, y);
	}
}

class ArcadeMachine
{
	public (long x, long y) Price { get; set; }
	public (long x, long y) A { get; set; }
	public (long x, long y) B { get; set; }
}
