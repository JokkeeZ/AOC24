namespace AOC24;

class Day2 : IAdventDay
{
	public void Solve(string[] input)
	{
		var reports = new List<List<int>>();

		foreach (var line in input)
		{
			reports.Add(line.Split(' ').Select(x => int.Parse(x)).ToList());
		}

		var safeReports = 0;

		foreach (var report in reports)
		{
			var valuesIncreasing = report[1] > report[0];

			for (var i = 0; i < report.Count; ++i)
			{
				if ((i + 1) > report.Count - 1)
				{
					safeReports++;
					break;
				}

				var current = report[i];
				var next = report[i + 1];

				var isValidLevelPair = valuesIncreasing
					? next >= current && next <= current + 3
					: next <= current && next >= current - 3;

				if (!isValidLevelPair || current == next)
				{
					break;
				}
			}
		}

		Console.WriteLine($"Part 1: {safeReports}");
	}
}
