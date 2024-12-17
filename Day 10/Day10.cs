namespace AOC24;

class Day10 : IAdventDay
{
	public bool IsActive => true;

	private TrailNode[,] map;

	public void Solve(string[] input)
	{
		map = new TrailNode[input[0].Length, input.Length];

		for (var y = 0; y < input.Length; ++y)
		{
			for (var x = 0; x < input[0].Length; ++x)
			{
				map[x, y] = new(x, y, int.Parse(input[y][x].ToString()));
			}
		}

		SetupNeighbors();

		var trailheadScores = new List<int>();
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

			trailheadScores.Add(count);
		}

		Console.WriteLine($"Part 1: {trailheadScores.Sum()}");

		var trailheadRatings = new List<int>();
		foreach (var start in GetNodesForNumber(0))
		{
			var allPaths = new List<List<TrailNode>>();
			var currentPath = new List<TrailNode>();

			foreach (var end in GetNodesForNumber(9))
			{
				DepthFirstSearch(start, end, currentPath, allPaths);
			}

			trailheadRatings.Add(allPaths.Count);
		}

		Console.WriteLine($"Part 2: {trailheadRatings.Sum()}");
	}

	// https://en.wikipedia.org/wiki/Depth-first_search
	private void DepthFirstSearch(TrailNode current, TrailNode end, List<TrailNode> path, List<List<TrailNode>> allPaths)
	{
		if (!map.InBounds(current.X, current.Y))
		{
			return;
		}

		if (map[current.X, current.Y].Visited)
		{
			return;
		}

		path.Add(current);
		map[current.X, current.Y].Visited = true;

		if (current.Position == end.Position)
		{
			allPaths.Add(new List<TrailNode>(path));
		}
		else
		{
			foreach (var neighbor in current.Neighbors)
			{
				DepthFirstSearch(neighbor, end, path, allPaths);
			}
		}

		path.RemoveAt(path.Count - 1);
		map[current.X, current.Y].Visited = false;
	}

	static bool FindPath(TrailNode start, TrailNode end)
	{
		var openList = new HashSet<TrailNode>();
		var closedList = new List<TrailNode>();

		openList.Add(start);

		while (openList.Count > 0)
		{
			var current = openList.FirstOrDefault();

			openList.Remove(current);
			closedList.Add(current);

			if (current.Position == end.Position)
			{
				return true;
			}

			foreach (var neighbor in current.Neighbors)
			{
				if (closedList.Contains(neighbor))
				{
					continue;
				}

				openList.Add(neighbor);
			}
		}

		return false;
	}

	private void SetupNeighbors()
	{
		foreach (var node in map)
		{
			var neighbors = new List<TrailNode>
			{
				new(node.X, node.Y - 1, 0),	// N
				new(node.X, node.Y + 1, 0),	// S
				new(node.X + 1, node.Y, 0),	// E
				new(node.X - 1, node.Y, 0)	// W
			};

			foreach (var neighbor in neighbors)
			{
				if (map.InBounds(neighbor.X, neighbor.Y))
				{
					if (node.Value + 1 == map[neighbor.X, neighbor.Y].Value)
					{
						node.Neighbors.Add(map[neighbor.X, neighbor.Y]);
					}
				}
			}
		}
	}

	private List<TrailNode> GetNodesForNumber(int num)
	{
		var positions = new List<TrailNode>();

		foreach (var item in map)
		{
			if (item.Value == num)
			{
				positions.Add(item);
			}
		}

		return positions;
	}
}

class TrailNode(int x, int y, int value)
{
	public int X { get; } = x;
	public int Y { get; } = y;
	public int Value { get; } = value;
	public bool Visited { get; set; }
	public List<TrailNode> Neighbors { get; } = [];
	public (int X, int Y) Position => (X, Y);
}
