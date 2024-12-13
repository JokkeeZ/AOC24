namespace AOC24;

class Day6 : IAdventDay
{
	private char[,] map;
	private enum Direction
	{
		Up,
		Down,
		Left,
		Right
	}

	public void Solve(string[] input)
	{
		map = new char[input[0].Length, input.Length];
		var position = (x: 0, y: 0);

		for (var y = 0; y < input.Length; ++y)
		{
			for (var x = 0; x < input[0].Length; ++x)
			{
				if (input[y][x] == '^')
				{
					position = (x, y);
					map[x, y] = 'X';
				}
				else
				{
					map[x, y] = input[y][x];
				}
			}
		}

		var direction = Direction.Up;

		while (true)
		{
			var (nextX, nextY) = direction switch
			{
				Direction.Up => (position.x, position.y - 1),
				Direction.Down => (position.x, position.y + 1),
				Direction.Left => (position.x - 1, position.y),
				Direction.Right => (position.x + 1, position.y),
				_ => throw new NotImplementedException(),
			};

			if (nextX < 0 || nextY < 0 || nextX >= map.GetLength(0) || nextY >= map.GetLength(1))
			{
				Console.WriteLine($"Part 1: {GetVisitedCellsCount()}");
				break;
			}

			if (map[nextX, nextY] is not '.' and not 'X')
			{
				direction = direction switch
				{
					Direction.Up => Direction.Right,
					Direction.Down => Direction.Left,
					Direction.Left => Direction.Up,
					Direction.Right => Direction.Down,
					_ => throw new NotImplementedException(),
				};

				continue;
			}

			position = (nextX, nextY);
			map[nextX, nextY] = 'X';
		}
	}

	private int GetVisitedCellsCount()
	{
		var count = 0;
		for (var y = 0; y < map.GetLength(1); ++y)
		{
			for (var x = 0; x < map.GetLength(0); ++x)
			{
				if (map[x, y] == 'X')
				{
					count++;
				}
			}
		}

		return count;
	}
}
