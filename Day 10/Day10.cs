namespace AOC24;

class Day10 : IAdventDay
{
	public bool IsActive => true;

	private TrailheadNode[,] map;

	public void Solve(string[] input)
	{
		map = new TrailheadNode[input[0].Length, input.Length];

		for (var y = 0; y < input.Length; ++y)
		{
			for (var x = 0; x < input[0].Length; ++x)
			{
				var value = int.TryParse(input[y][x].ToString(), out var i) ? i : -1;
				map[x, y] = new(x, y, value);
			}
		}

		SetupNeighbors();

		var trailheads = new List<int>();
		foreach (var start in GetNodesForNumber(0))
		{
			var count = 0;
			foreach (var end in GetNodesForNumber(9))
			{
				if (FindPath(start, end))
				{
					count++;
				}
			}

			trailheads.Add(count);
		}

		Console.WriteLine($"Part 1: {trailheads.Sum()}");
	}

	static bool FindPath(TrailheadNode startNode, TrailheadNode end)
	{
		var openList = new List<TrailheadNode>();
		var closedList = new List<TrailheadNode>();

		openList.Add(startNode);

		while (openList.Count > 0)
		{
			var current = openList.FirstOrDefault();

			openList.Remove(current);
			closedList.Add(current);

			if (current.X == end.X && current.Y == end.Y)
			{
				return true;
			}

			foreach (var neighbor in current.Neighbors)
			{
				if (closedList.Contains(neighbor))
				{
					continue;
				}

				if (current.Value + 1 == neighbor.Value && !openList.Contains(neighbor))
				{
					openList.Add(neighbor);
				}
			}
		}

		return false;
	}

	private void SetupNeighbors()
	{
		for (var y = 0; y < map.GetLength(1); ++y)
		{
			for (var x = 0; x < map.GetLength(0); ++x)
			{
				var neighbors = new List<TrailheadNode>
				{
					new(x, y - 1, 0),	// N
					new(x, y + 1, 0),	// S
					new(x + 1, y, 0),	// E
					new(x - 1, y, 0)	// W
				};

				foreach (var neighbor in neighbors)
				{
					if (map.InBounds(neighbor.X, neighbor.Y))
					{
						if (map[x, y].Value + 1 == map[neighbor.X, neighbor.Y].Value)
						{
							map[x, y].Neighbors.Add(map[neighbor.X, neighbor.Y]);
						}
					}
				}
			}
		}
	}

	private List<TrailheadNode> GetNodesForNumber(int num)
	{
		var positions = new List<TrailheadNode>();
		for (var y = 0; y < map.GetLength(1); ++y)
		{
			for (var x = 0; x < map.GetLength(0); ++x)
			{
				if (map[x, y].Value == num)
				{
					positions.Add(map[x, y]);
				}
			}
		}

		return positions;
	}
}

class TrailheadNode(int x, int y, int value)
{
	public int X { get; set; } = x;
	public int Y { get; set; } = y;
	public int Value { get; set; } = value;

	public List<TrailheadNode> Neighbors { get; set; } = [];
}
