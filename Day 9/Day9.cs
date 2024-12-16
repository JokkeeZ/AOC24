namespace AOC24;

class Day9 : IAdventDay
{
	public bool IsActive => true;

	public void Solve(string[] input)
	{
		var nums = input[0].Select(x => int.Parse(x.ToString())).ToList();

		var counter = 0;
		var blocks = new List<int>();

		for (var i = 0; i < nums.Count; ++i)
		{
			blocks.AddRange(Enumerable.Repeat(i % 2 == 0 ? counter++ : -1, nums[i]));
		}

		Part1(new(blocks));
		Part2(new(blocks));
	}

	static void Part1(List<int> blocks)
	{
		while (true)
		{
			if (!Swap(blocks))
			{
				break;
			}
		}

		Console.WriteLine($"Part 1: {CalculateChecksum(blocks)}");
	}

	static void Part2(List<int> blocks)
	{
		for (var i = blocks.Max(); i >= 0; i--)
		{
			var blockStart = blocks.IndexOf(i);
			var blockEnd = blocks.LastIndexOf(i);
			var emptyBlockIndex = FindEmptyBlockIndex(blocks, (blockEnd - blockStart) + 1, blockStart);

			if (emptyBlockIndex > -1)
			{
				for (var j = blockStart; j <= blockEnd; ++j, emptyBlockIndex++)
				{
					blocks.SwapItems(emptyBlockIndex, j);
				}
			}
		}

		Console.WriteLine($"Part 2: {CalculateChecksum(blocks)}");
	}

	static int FindEmptyBlockIndex(List<int> blocks, int blockSize, int blockIndex)
	{
		var count = 0;
		for (var i = 0; i < blocks.Count; i++)
		{
			count = blocks[i] == -1 ? count + 1 : 0;

			if (count == blockSize)
			{
				var index = i - blockSize + 1;
				return index < blockIndex ? index : -1;
			}
		}

		return -1;
	}

	static long CalculateChecksum(List<int> blocks)
	{
		long checksum = 0;
		for (var i = 0; i < blocks.Count; ++i)
		{
			if (blocks[i] == -1)
			{
				continue;
			}

			checksum += blocks[i] * i;
		}

		return checksum;
	}

	static bool Swap(List<int> blocks)
	{
		var a = blocks.IndexOf(-1);
		var b = blocks.FindLastIndex(i => i != -1);

		if (a < b)
		{
			blocks.SwapItems(b, a);
			return true;
		}

		return false;
	}
}
