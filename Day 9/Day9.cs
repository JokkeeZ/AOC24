namespace AOC24;

class Day9 : IAdventDay
{
	public bool IsActive => true;
	private readonly bool draw = false;

	private readonly string example = "2333133121414131402";

	public void Solve(string[] input)
	{
		//input[0] = example;
		var blocks = new List<int>();
		var counter = 0;

		var nums = input[0].Select(x => int.Parse(x.ToString())).ToList();
		for (var i = 0; i < nums.Count; ++i)
		{
			blocks.AddRange(Enumerable.Repeat(i % 2 == 0 ? counter++ : -1, nums[i]));
		}

		if (draw)
		{
			DrawBlocks(blocks);
		}

		while (true)
		{
			if (!Swap(blocks))
			{
				break;
			}

			if (draw)
			{
				DrawBlocks(blocks);
			}
		}

		long checksum = 0;
		for (var i = 0; i < blocks.Count; ++i)
		{
			if (blocks[i] == -1)
			{
				break;
			}

			checksum += (blocks[i] * i);
		}

		Console.WriteLine($"Part 1: {checksum}");
	}

	static bool Swap(List<int> blocks)
	{
		var a = blocks.IndexOf(-1);
		var b = blocks.FindLastIndex(i => i != -1);

		if (a < b)
		{
			(blocks[b], blocks[a]) = (blocks[a], blocks[b]);
			return true;
		}

		return false;
	}

	static void DrawBlocks(List<int> blocks)
	{
		foreach (var b in blocks)
		{
			Console.Write(b == -1 ? '.' : b.ToString());
		}

		Console.WriteLine();
		Thread.Sleep(TimeSpan.FromMilliseconds(25));
	}
}
