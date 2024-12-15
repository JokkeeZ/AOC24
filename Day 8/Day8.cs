namespace AOC24;

class Day8 : IAdventDay
{
	public bool IsActive => true;

	private char[,] map;
	private readonly HashSet<(int x, int y)> antinodeLocations = [];

	public void Solve(string[] input)
	{
		map = new char[input[0].Length, input.Length];

		var stations = new Dictionary<char, List<(int x, int y)>>();

		for (var y = 0; y < input.Length; ++y)
		{
			for (var x = 0; x < input[0].Length; ++x)
			{
				map[x, y] = input[y][x];

				if (input[y][x] == '.')
					continue;

				if (stations.TryGetValue(map[x, y], out var value))
				{
					value.Add((x, y));
				}
				else
				{
					stations.Add(map[x, y], [(x, y)]);
				}
			}
		}

		foreach (var (_, positions) in stations)
		{
			foreach (var position in positions)
			{
				foreach (var otherPosition in positions.Where(p => p != position))
				{
					CreateAntinode(position, otherPosition);
				}
			}
		}

		PrintMap();
		Console.WriteLine($"Part 1: {antinodeLocations.Count}");
	}

	private void CreateAntinode((int x, int y) station1, (int x, int y) station2)
	{
		var antinode = (
			x: (station2.x - station1.x + station2.x),
			y: (station2.y - station1.y + station2.y)
		);

		if (map.InBounds(antinode.x, antinode.y))
		{
			//map[antinode.x, antinode.y] = '#';
			antinodeLocations.Add(antinode);
		}
	}

	private void PrintMap()
	{
		for (var y = 0; y < map.GetLength(1); ++y)
		{
			for (var x = 0; x < map.GetLength(0); ++x)
			{
				Console.Write(map[x, y]);
			}

			Console.WriteLine();
		}
	}
}
