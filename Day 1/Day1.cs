namespace AOC24;

class Day1 : IAdventDay
{
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

		var total = 0;

		for (var i = 0; i < left.Count; i++)
		{
			var distance = left[i] - right[i];

			total += Math.Abs(distance);
		}

		Console.WriteLine(total);
	}
}
