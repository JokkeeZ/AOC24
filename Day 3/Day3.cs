using System.Text.RegularExpressions;

namespace AOC24;

class Day3 : IAdventDay
{
	public bool IsActive => false;

	public void Solve(string[] input)
	{
		var regex = new Regex("(mul\\(([0-9]+),([0-9]+)\\))");
		var sum = 0;

		foreach (var line in input)
		{
			var matches = regex.Matches(line);

			foreach (Match match in matches)
			{
				var group1 = int.Parse(match.Groups[2].Value);
				var group2 = int.Parse(match.Groups[3].Value);

				sum += group1 * group2;
			}
		}

		Console.WriteLine($"Part 1: {sum}");

		// Part 2
		var r2 = new Regex("(do\\(\\)|don't\\(\\)|mul\\(([0-9]+),([0-9]+)\\))");
		var mulEnabled = true;
		sum = 0;

		foreach (var line in input)
		{
			var matches = r2.Matches(line);

			foreach (Match match in matches)
			{
				if (match.Value == "don't()")
				{
					mulEnabled = false;
				}
				else if (match.Value == "do()")
				{
					mulEnabled = true;
				}
				else
				{
					if (mulEnabled)
					{
						var group1 = int.Parse(match.Groups[2].Value);
						var group2 = int.Parse(match.Groups[3].Value);

						sum += group1 * group2;
					}
				}
			}
		}

		Console.WriteLine($"Part 2: {sum}");
	}
}
