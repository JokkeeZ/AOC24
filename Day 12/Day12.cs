namespace AOC24;

class Day12 : IAdventDay
{
	public bool IsActive => true;

	private char[,] map;
	private bool[,] visited;
	private readonly List<GardenArea> areas = [];

	public void Solve(string[] input)
	{
		map = new char[input[0].Length, input.Length];
		visited = new bool[input[0].Length, input.Length];

		for (var y = 0; y < input.Length; ++y)
		{
			for (var x = 0; x < input[0].Length; ++x)
			{
				map[x, y] = input[y][x];
			}
		}

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

				MakeGardenArea(area, x, y);

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
	}

	private void MakeGardenArea(GardenArea area, int x, int y)
	{
		foreach (var (X, Y) in map.GetNeighbors(x, y).Where(p => map[p.X, p.Y] == area.Name && !visited[p.X, p.Y]))
		{
			area.Plants.Add((X, Y));
			visited[X, Y] = true;

			MakeGardenArea(area, X, Y);
		}
	}
}

class GardenArea
{
	public char Name { get; set; }
	public HashSet<(int X, int Y)> Plants { get; set; } = [];
	public int FenceCount { get; set; }
}
