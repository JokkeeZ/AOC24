namespace AOC24;

class Day11 : IAdventDay
{
	public bool IsActive => true;

	public void Solve(string[] input)
	{
		var stones = input[0].Split(' ').Select(long.Parse).ToList();

		for (var i = 0; i < 25; ++i)
		{
			stones = Blink(stones);
		}

		Console.WriteLine($"Part 1: {stones.Count}");
	}

	static List<long> Blink(List<long> stones)
	{
		var newStones = new List<long>();

		for (var i = 0; i < stones.Count; ++i)
		{
			if (stones[i] == 0)
			{
				newStones.Add(1);
			}
			else if (stones[i].NumberOfDigits() % 2 == 0)
			{
				var digits = stones[i].NumberOfDigits();
				var div = (long)Math.Pow(10, digits / 2);

				newStones.Add(stones[i] / div);
				newStones.Add(stones[i] % div);
			}
			else
			{
				newStones.Add(stones[i] * 2024);
			}
		}

		return newStones;
	}
}
