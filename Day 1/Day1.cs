namespace AOC24;

class Day1 : IAdventDay
{
	public bool IsActive => false;

	public void Solve(string[] input)
	{
		var left = new List<int>();
		var right = new List<int>();

		foreach (var line in input)
		{
			var split = line.Split("   ");

			left.Add(int.Parse(split[0]));
			right.Add(int.Parse(split[1]));
		}

		left.Sort();
		right.Sort();

		var totalDistance = 0;
		for (var i = 0; i < left.Count; i++)
		{
			totalDistance += Math.Abs(left[i] - right[i]);
		}

		Console.WriteLine($"Part 1: {totalDistance}");

		// Part 2
		var nums = new List<int>();
		for (var i = 0; i < left.Count; ++i)
		{
			var c = right.Count(x => x == left[i]);
			nums.Add(left[i] * c);
		}

		Console.WriteLine($"Part 2: {nums.Sum()}");
	}
}
