namespace AOC24;

class Day15 : IAdventDay
{
	public bool IsActive => true;

	private char[,] map;
	private readonly Queue<char> moves = [];
	private readonly List<(int x, int y)> boxes = [];
	private (int x, int y) position;

	private readonly Dictionary<char, (int x, int y)> directions = new()
	{
		['^'] = (x: 0, y: -1),
		['v'] = (x: 0, y: 1),
		['<'] = (x: -1, y: 0),
		['>'] = (x: 1, y: 0)
	};

	public void Solve(string[] input)
	{
		ParseInput(input);

		Part1();
		Console.WriteLine($"Part 1: {boxes.Sum(b => 100 * b.y + b.x)}");
	}

	private void Part1()
	{
		while (moves.Count > 0)
		{
			var move = moves.Dequeue();
			var nextPosition = GetNextPosition(move);

			if (ContainsWall(nextPosition.x, nextPosition.y))
			{
				continue;
			}

			if (ContainsBox(nextPosition.x, nextPosition.y))
			{
				if (UpdateBoxes(move))
				{
					position = nextPosition;
				}
			}
			else
			{
				position = nextPosition;
			}
		}
	}

	private bool UpdateBoxes(char c)
	{
		if (c == '^')
		{
			return MoveNeighborBoxesUp(c);
		}
		else if (c == 'v')
		{
			return MoveNeighborBoxesDown(c);
		}
		else if (c == '>')
		{
			return MoveNeighborBoxesRight(c);
		}
		else if (c == '<')
		{
			return MoveNeighborBoxesLeft(c);
		}

		return false;
	}

	private (int x, int y) GetNextPosition(char c)
	{
		var nextX = position.x + directions[c].x;
		var nextY = position.y + directions[c].y;

		return (nextX, nextY);
	}

	private bool MoveNeighborBoxesLeft(char c)
	{
		var neighbors = new List<(int x, int y)>();

		for (var i = position.x - 1; i >= 0; --i)
		{
			if (ContainsBox(i, position.y))
			{
				neighbors.Add((i, position.y));
			}
			else
			{
				break;
			}
		}

		return CanMoveAllBoxes(neighbors, c);
	}

	private bool MoveNeighborBoxesRight(char c)
	{
		var neighbors = new List<(int x, int y)>();

		for (var i = position.x + 1; i < map.GetLength(0); ++i)
		{
			if (ContainsBox(i, position.y))
			{
				neighbors.Add((i, position.y));
			}
			else
			{
				break;
			}
		}

		return CanMoveAllBoxes(neighbors, c);
	}

	private bool MoveNeighborBoxesUp(char c)
	{
		var neighbors = new List<(int x, int y)>();

		for (var i = position.y - 1; i >= 0; --i)
		{
			if (ContainsBox(position.x, i))
			{
				neighbors.Add((position.x, i));
			}
			else
			{
				break;
			}
		}

		return CanMoveAllBoxes(neighbors, c);
	}

	private bool MoveNeighborBoxesDown(char c)
	{
		var neighbors = new List<(int x, int y)>();

		for (var i = position.y + 1; i < map.GetLength(1); ++i)
		{
			if (ContainsBox(position.x, i))
			{
				neighbors.Add((position.x, i));
			}
			else
			{
				break;
			}
		}

		return CanMoveAllBoxes(neighbors, c);
	}

	private bool CanMoveAllBoxes(List<(int x, int y)> neighbors, char c)
	{
		foreach (var (x, y) in neighbors)
		{
			if (!BoxCanMove(x, y, c))
			{
				return false;
			}
		}

		foreach (var neighbor in neighbors)
		{
			var idx = boxes.FindIndex(b => b == neighbor);
			boxes[idx] = (
					boxes[idx].x + directions[c].x,
					neighbor.y + directions[c].y
			);
		}

		return true;
	}

	private bool BoxCanMove(int x, int y, char c)
	{
		var nextX = x + directions[c].x;
		var nextY = y + directions[c].y;

		return !ContainsWall(nextX, nextY);
	}

	private bool ContainsBox(int x, int y)
	{
		return map.InBounds(x, y) && boxes.Contains((x, y));
	}

	private bool ContainsWall(int x, int y)
	{
		return map.InBounds(x, y) && map[x, y] == '#';
	}

	private void ParseInput(string[] input)
	{
		var strMap = input.TakeWhile(x => x.Length != 0).ToArray();
		map = new char[strMap[0].Length, strMap.Length];

		for (var y = 0; y < strMap.Length; y++)
		{
			for (var x = 0; x < strMap[0].Length; x++)
			{
				map[x, y] = strMap[y][x];

				if (map[x, y] == '@')
				{
					position = (x, y);
					map[x, y] = '.';
				}

				if (map[x, y] == 'O')
				{
					boxes.Add((x, y));
					map[x, y] = '.';
				}
			}
		}

		for (var i = strMap.Length + 1; i < input.Length; ++i)
		{
			foreach (var c in input[i].Trim().Where(directions.ContainsKey))
			{
				moves.Enqueue(c);
			}
		}
	}
}
