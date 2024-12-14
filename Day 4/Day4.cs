namespace AOC24;

class Day4 : IAdventDay
{
	public bool IsActive => false;

	private char[,] map;
	private readonly char[] XMAS = ['X', 'M', 'A', 'S'];

	public void Solve(string[] input)
	{
		map = new char[input[0].Length, input.Length];

		for (var x = 0; x < input[0].Length; ++x)
		{
			for (var y = 0; y < input.Length; ++y)
			{
				map[x, y] = input[y][x];
			}
		}

		var count = 0;
		var xMasCount = 0;
		for (var x = 0; x < map.GetLength(0); ++x)
		{
			for (var y = 0; y < map.GetLength(1); ++y)
			{
				if (map[x, y] == 'X')
				{
					if (CheckHorizontal(x, y))
					{
						count++;
					}

					if (CheckHorizontalReverse(x, y))
					{
						count++;
					}

					if (CheckVertical(x, y))
					{
						count++;
					}

					if (CheckReverseVertical(x, y))
					{
						count++;
					}

					if (CheckDiagonalToBottomRight(x, y))
					{
						count++;
					}

					if (CheckDiagonalToTopRight(x, y))
					{
						count++;
					}

					if (CheckDiagonalToBottomLeft(x, y))
					{
						count++;
					}

					if (CheckDiagonalToTopLeft(x, y))
					{
						count++;
					}
				}
				else if (map[x, y] == 'A')
				{
					if (CheckXMas(x, y))
					{
						xMasCount++;
					}
				}
			}
		}

		Console.WriteLine($"Part 1: {count}");
		Console.WriteLine($"Part 2: {xMasCount}");
	}

	private bool IsValidCell(int x, int y, char c)
		=> map.InBounds(x, y) && map[x, y] == c;

	private bool CheckHorizontal(int x, int y)
	{
		for (var i = 0; i < XMAS.Length; ++i)
		{
			if (!IsValidCell(x + i, y, XMAS[i]))
			{
				return false;
			}
		}

		return true;
	}

	private bool CheckHorizontalReverse(int x, int y)
	{
		for (var i = 0; i < XMAS.Length; ++i)
		{
			if (!IsValidCell(x - i, y, XMAS[i]))
			{
				return false;
			}
		}

		return true;
	}

	private bool CheckVertical(int x, int y)
	{
		for (var i = 0; i < XMAS.Length; ++i)
		{
			if (!IsValidCell(x, y + i, XMAS[i]))
			{
				return false;
			}
		}

		return true;
	}

	private bool CheckReverseVertical(int x, int y)
	{
		for (var i = 0; i < XMAS.Length; ++i)
		{
			if (!IsValidCell(x, y - i, XMAS[i]))
			{
				return false;
			}
		}

		return true;
	}

	private bool CheckDiagonalToBottomRight(int x, int y)
	{
		for (var i = 0; i < XMAS.Length; ++i)
		{
			if (!IsValidCell(x + i, y + i, XMAS[i]))
			{
				return false;
			}
		}

		return true;
	}

	private bool CheckDiagonalToTopRight(int x, int y)
	{
		for (var i = 0; i < XMAS.Length; ++i)
		{
			if (!IsValidCell(x + i, y - i, XMAS[i]))
			{
				return false;
			}
		}

		return true;
	}

	private bool CheckDiagonalToBottomLeft(int x, int y)
	{
		for (var i = 0; i < XMAS.Length; ++i)
		{
			if (!IsValidCell(x - i, y + i, XMAS[i]))
			{
				return false;
			}
		}

		return true;
	}

	private bool CheckDiagonalToTopLeft(int x, int y)
	{
		for (var i = 0; i < XMAS.Length; ++i)
		{
			if (!IsValidCell(x - i, y - i, XMAS[i]))
			{
				return false;
			}
		}

		return true;
	}

	private bool CheckXMas(int x, int y)
	{
		if (!map.InBounds(x - 1, y - 1)) return false;
		if (!map.InBounds(x + 1, y - 1)) return false;
		if (!map.InBounds(x - 1, y + 1)) return false;
		if (!map.InBounds(x + 1, y + 1)) return false;

		var topL = map[x - 1, y - 1];
		var topR = map[x + 1, y - 1];
		var botL = map[x - 1, y + 1];
		var botR = map[x + 1, y + 1];

		return (topL == 'M' && topR == 'S' && botL == 'M' && botR == 'S') ||
			(topL == 'S' && topR == 'M' && botL == 'S' && botR == 'M') ||
			(topL == 'M' && topR == 'M' && botL == 'S' && botR == 'S') ||
			(topL == 'S' && topR == 'S' && botL == 'M' && botR == 'M');
	}
}
