namespace AOC24;

class Day12 : IAdventDay
{
	public bool IsActive => false;

	private char[,] map;
	private readonly List<GardenArea> areas = [];

	public void Solve(string[] input)
	{
		map = new char[input[0].Length, input.Length];

		for (var y = 0; y < input.Length; ++y)
		{
			for (var x = 0; x < input[0].Length; ++x)
			{
				map[x, y] = input[y][x];
			}
		}

		var visited = new bool[input[0].Length, input.Length];
		for (var y = 0; y < map.GetLength(1); ++y)
		{
			for (var x = 0; x < map.GetLength(0); ++x)
			{
				if (visited[x, y])
				{
					continue;
				}

				var area = new GardenArea
				{
					Name = map[x, y]
				};

				area.Plants.Add((x, y));
				visited[x, y] = true;

				MakeGardenArea(area, x, y, visited);

				areas.Add(area);
			}
		}

		foreach (var area in areas)
		{
			foreach (var (X, Y) in area.Plants)
			{
				var neighbors = map.GetNeighbors(X, Y).Count(p => map[p.X, p.Y] == area.Name);
				area.FenceCount += Math.Abs(4 - neighbors);
			}
		}

		Console.WriteLine($"Part 1: {areas.Sum(a => a.Plants.Count * a.FenceCount)}");

		foreach (var area in areas)
		{
			CountAreaSides(area);
		}

		Console.WriteLine($"Part 2: {areas.Sum(a => a.Plants.Count * a.Sides)}");
	}

	static void CountAreaSides(GardenArea area)
	{
		foreach (var (x, y) in area.Plants)
		{
			var left = (x - 1, y);
			var top = (x, y - 1);
			var topleft = (x - 1, y - 1);
			var right = (x + 1, y);
			var topright = (x + 1, y - 1);
			var bottom = (x, y + 1);
			var bottomleft = (x - 1, y + 1);

			if (CheckForEdge(area, top, left, topleft))
			{
				area.Sides++;
			}

			if (CheckForEdge(area, top, right, topright))
			{
				area.Sides++;
			}

			if (CheckForEdge(area, left, top, topleft))
			{
				area.Sides++;
			}

			if (CheckForEdge(area, left, bottom, bottomleft))
			{
				area.Sides++;
			}
		}
	}

	private void MakeGardenArea(GardenArea area, int x, int y, bool[,] visited)
	{
		foreach (var (X, Y) in map.GetNeighbors(x, y).Where(p => map[p.X, p.Y] == area.Name && !visited[p.X, p.Y]))
		{
			area.Plants.Add((X, Y));
			visited[X, Y] = true;

			MakeGardenArea(area, X, Y, visited);
		}
	}

	static bool CheckForEdge(GardenArea area, (int, int) dir1, (int, int) dir2, (int, int) dir3)
	{
		return !area.Plants.Contains(dir2)
			&& (!area.Plants.Contains(dir1)
			|| area.Plants.Contains(dir3));
	}

}

class GardenArea
{
	public char Name { get; set; }
	public HashSet<(int X, int Y)> Plants { get; set; } = [];
	public int FenceCount { get; set; }
	public int Sides { get; set; }
}
