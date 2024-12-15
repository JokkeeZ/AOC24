namespace AOC24;

class Day7 : IAdventDay
{
	public bool IsActive => false;

	public void Solve(string[] input)
	{
		var equations = new Dictionary<long, List<long>>();

		foreach (var line in input)
		{
			var split = line.Split(": ");

			equations.Add(long.Parse(split[0]),
				split[1].Split(' ')
				.Select(long.Parse)
				.ToList());
		}

		long calibrationResult = 0;
		foreach (var (sum, numbers) in equations)
		{
			if (IsValidEquation(numbers, sum))
			{
				calibrationResult += sum;
			}
		}

		Console.WriteLine($"Part 1: {calibrationResult}");

		calibrationResult = 0;
		foreach (var (sum, numbers) in equations)
		{
			if (IsValidEquationWithConcat(numbers, sum))
			{
				calibrationResult += sum;
			}
		}

		Console.WriteLine($"Part 2: {calibrationResult}");
	}

	static bool IsValidEquation(List<long> nums, long sum)
	{
		var stack = new Stack<(long current, int index)>();
		stack.Push((nums[0], 1));

		while (stack.Count > 0)
		{
			var (current, index) = stack.Pop();

			if (index == nums.Count - 1)
			{
				if (current + nums[index] == sum || current * nums[index] == sum)
				{
					return true;
				}
				continue;
			}

			stack.Push((current + nums[index], index + 1));
			stack.Push((current * nums[index], index + 1));
		}

		return false;
	}

	static bool IsValidEquationWithConcat(List<long> numbers, long sum)
	{
		var stack = new Stack<(long current, int index)>();
		stack.Push((numbers[0], 1));

		while (stack.Count > 0)
		{
			var (current, index) = stack.Pop();
			var concat = long.Parse($"{current}{numbers[index]}");

			if (index == numbers.Count - 1)
			{
				if (current + numbers[index] == sum ||
					current * numbers[index] == sum ||
					concat == sum)
				{
					return true;
				}

				continue;
			}

			stack.Push((current + numbers[index], index + 1));
			stack.Push((current * numbers[index], index + 1));
			stack.Push((concat, index + 1));
		}

		return false;
	}
}
