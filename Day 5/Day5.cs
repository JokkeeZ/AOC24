using System.Data;

namespace AOC24;

class Day5 : IAdventDay
{
	public bool IsActive => false;

	public void Solve(string[] input)
	{
		var rules = new List<(int, int)>();
		var updates = new List<PrintQueueUpdate>();

		foreach (var line in input)
		{
			if (line.Contains('|'))
			{
				var split = line.Split('|');
				rules.Add((int.Parse(split[0]), int.Parse(split[1])));
			}
			else if (line.Contains(','))
			{
				updates.Add(new(line.Split(',').Select(x => int.Parse(x)).ToList(), false));
			}
		}

		var part1MiddleSum = 0;

		foreach (var update in updates)
		{
			if (IsValidUpdate(update.Pages, rules))
			{
				update.CorrectOrder = true;
				part1MiddleSum += update.Pages[update.Pages.Count / 2];
			}
		}

		Console.WriteLine($"Part 1: {part1MiddleSum}");

		while (true)
		{
			var swapped = false;

			foreach (var update in updates)
			{
				if (update.CorrectOrder)
				{
					continue;
				}

				foreach (var (b, a) in rules)
				{
					var after = update.Pages.IndexOf(a);
					var before = update.Pages.IndexOf(b);

					if (after != -1 && before != -1 && before > after)
					{
						update.Pages[after] = b;
						update.Pages[before] = a;

						swapped = true;
					}
				}
			}

			if (!swapped)
			{
				break;
			}
		}

		var part2MiddleSum = 0;
		foreach (var update in updates)
		{
			if (update.CorrectOrder)
			{
				continue;
			}

			part2MiddleSum += update.Pages[update.Pages.Count / 2];
		}

		Console.WriteLine($"Part 2: {part2MiddleSum}");
	}

	static bool IsValidUpdate(List<int> update, List<(int before, int after)> rules)
	{
		var numbersSeenBefore = new List<int>();

		foreach (var number in update)
		{
			if (!rules.Any(x => x.after == number || x.before == number))
			{
				continue;
			}

			if (rules.Any(x => x.before == number && numbersSeenBefore.Contains(x.after)))
			{
				return false;
			}

			numbersSeenBefore.Add(number);
		}

		return true;
	}
}

class PrintQueueUpdate(List<int> pages, bool inCorredOrder)
{
	public List<int> Pages = pages;
	public bool CorrectOrder = inCorredOrder;
}
