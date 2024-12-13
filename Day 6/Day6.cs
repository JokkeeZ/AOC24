namespace AOC24;

class Day6 : IAdventDay
{
	private readonly bool draw = false;

	private char[,] map;
	private (int x, int y) guard;
	private Direction direction;
	private (int x, int y) startCell;

	private readonly HashSet<(int x, int y)> visitedCells = [];

	private enum Direction
	{
		Up,
		Right,
		Down,
		Left
	}

	public void Solve(string[] input)
	{
		map = new char[input[0].Length, input.Length];

		for (var y = 0; y < input.Length; ++y)
		{
			for (var x = 0; x < input[0].Length; ++x)
			{
				if (input[y][x] == '^')
				{
					guard = (x, y);
					startCell = (x, y);
				}
				else
				{
					map[x, y] = input[y][x];
				}
			}
		}

		var visitedCells = new HashSet<(int x, int y)>
		{
			(guard.x, guard.y)
		};

		while (true)
		{
			if (draw)
			{
				DrawMap();
			}

			var (nextX, nextY) = GetNextCell((guard.x, guard.y), direction);
			if (!map.InBounds(nextX, nextY))
			{
				break;
			}

			if (map[nextX, nextY] is '#')
			{
				direction = GetNextRotation(direction);
				continue;
			}

			guard = (nextX, nextY);
			visitedCells.Add((guard.x, guard.y));
		}

		Console.WriteLine($"Part 1: {visitedCells.Count}");

		var count = 0;
		foreach (var cell in visitedCells)
		{
			if (IsLoop(cell, startCell))
			{
				count++;
			}
		}

		Console.WriteLine($"Part 2: {count}");
	}

	private bool IsLoop((int x, int y) cell, (int x, int y) current)
	{
		var currentDir = Direction.Up;

		var loopPath = new HashSet<((int x, int y), Direction)>()
		{
			(current, currentDir)
		};

		while (true)
		{
			var newCell = GetNextCell((current.x, current.y), currentDir);

			if (!map.InBounds(newCell.x, newCell.y))
			{
				return false;
			}

			if (map[newCell.x, newCell.y] == '#' || newCell == cell)
			{
				currentDir = GetNextRotation(currentDir);
				continue;
			}

			current = newCell;

			if (!loopPath.Add((current, currentDir)))
			{
				return true;
			}
		}
	}

	private static Direction GetNextRotation(Direction dir)
	{
		return dir switch
		{
			Direction.Up => Direction.Right,
			Direction.Down => Direction.Left,
			Direction.Left => Direction.Up,
			Direction.Right => Direction.Down,
			_ => throw new("HUH")
		};
	}

	private static (int x, int y) GetNextCell((int x, int y) pos, Direction dir)
	{
		return dir switch
		{
			Direction.Up => (pos.x, pos.y - 1),
			Direction.Right => (pos.x + 1, pos.y),
			Direction.Down => (pos.x, pos.y + 1),
			Direction.Left => (pos.x - 1, pos.y),
			_ => throw new("HUH"),
		};
	}

	private void DrawMap()
	{
		Console.Clear();

		for (var y = 0; y < map.GetLength(1); ++y)
		{
			for (var x = 0; x < map.GetLength(0); ++x)
			{
				var visitedCell = visitedCells.Contains((x, y));

				if (guard.x == x && guard.y == y && visitedCell)
				{
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.Write('@');
					Console.ResetColor();
				}
				else if (visitedCell)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write('X');
					Console.ResetColor();
				}
				else
				{
					Console.Write(map[x, y]);
				}
			}

			Console.WriteLine();
		}

		Thread.Sleep(TimeSpan.FromMilliseconds(50));
	}
}
