namespace AOC24;

class Day11 : IAdventDay
{
	public bool IsActive => true;

	public void Solve(string[] input)
	{
		var stones = input[0].Split(' ').Select(long.Parse).ToDictionary(s => s, s => 1L);
		for (var i = 0; i < 25; ++i)
		{
			stones = Blink(stones);
		}

		Console.WriteLine($"Part 1: {stones.Values.Sum()}");

		stones = input[0].Split(' ').Select(long.Parse).ToDictionary(s => s, s => 1L);
		for (var i = 0; i < 75; ++i)
		{
			stones = Blink(stones);
		}

		Console.WriteLine($"Part 2: {stones.Values.Sum()}");
	}

	static Dictionary<long, long> Blink(Dictionary<long, long> stones)
	{
		var newStones = new Dictionary<long, long>(stones);

		foreach (var (stone, count) in stones.Where(s => s.Value != 0))
		{
			newStones[stone] -= count;

			if (stone == 0L)
			{
				AddStone(newStones, 1L, count);
			}
			else if (stone.NumberOfDigits() % 2 == 0)
			{
				var digits = stone.NumberOfDigits();
				var div = (long)Math.Pow(10, digits / 2);

				AddStone(newStones, stone / div, count);
				AddStone(newStones, stone % div, count);
			}
			else
			{
				AddStone(newStones, stone * 2024, count);
			}
		}

		return newStones;
	}

	static void AddStone(Dictionary<long, long> stones, long stone, long count)
	{
		if (!stones.TryAdd(stone, count))
		{
			stones[stone] += count;
		}
	}
}
