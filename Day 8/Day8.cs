namespace AOC24;

class Day8 : IAdventDay
{
	public bool IsActive => true;

	private char[,] map;
	private readonly HashSet<(int x, int y)> antinodes = [];

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
				{
					continue;
				}

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

		foreach (var (name, positions) in stations)
		{
			foreach (var current in positions)
			{
				foreach (var other in positions.Where(p => p.x != current.x || p.y != current.y))
				{
					var (x, y) = GetDelta(other, current);
					var antinode = (
						x: other.x + x,
						y: other.y + y
					);

					if (map.InBounds(antinode.x, antinode.y))
					{
						antinodes.Add(antinode);
					}
				}
			}
		}

		Console.WriteLine($"Part 1: {antinodes.Count}");

		foreach (var (_, positions) in stations)
		{
			foreach (var current in positions)
			{
				foreach (var other in positions.Where(p => p.x != current.x || p.y != current.y))
				{
					var delta = GetDelta(current, other);
					AddAntinode(current, delta, true);
					AddAntinode(current, delta, false);
				}
			}
		}

		Console.WriteLine($"Part 2: {antinodes.Count}");
	}

	private void AddAntinode((int x, int y) current, (int x, int y) delta, bool down)
	{
		var antinode = down
			? (x: current.x + delta.x, y: current.y + delta.y)
			: (x: current.x - delta.x, y: current.y - delta.y);

		while (map.InBounds(antinode.x, antinode.y))
		{
			antinodes.Add(antinode);
			antinode = down
				? (x: antinode.x + delta.x, y: antinode.y + delta.y)
				: (x: antinode.x - delta.x, y: antinode.y - delta.y);
		}
	}

	static (int x, int y) GetDelta((int x, int y) p1, (int x, int y) p2)
		=> (x: p1.x - p2.x, y: p1.y - p2.y);
}
