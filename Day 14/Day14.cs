using System.Drawing;
using System.Drawing.Imaging;

namespace AOC24;

class Day14 : IAdventDay
{
	public bool IsActive => false;

	const int MapWidth = 101;
	const int MapHeight = 103;

	private readonly List<Robot> robots = [];

	private readonly List<(int x, int y)> neighbors =
	[
		(x: 0, y: -1),
		(x: -1, y: 0),
		(x: 1, y: 0),
		(x: 0, y: 1),
		(x: -1, y: -1),
		(x: 1, y: -1),
		(x: -1, y: 1),
		(x: 1, y: 1),
	];

	public void Solve(string[] input)
	{
		ParseRobotsFromInput(input);

		UpdateRobotPositions(100);
		Console.WriteLine($"Part 1: {CalculateSafetyFactor()}");

		var seconds = FindChristmasTree(100, 10000);
		Console.WriteLine($"Part 2: {seconds}");
	}

	private void ParseRobotsFromInput(string[] input)
	{
		foreach (var line in input)
		{
			var split = line.Split(' ');

			robots.Add(new()
			{
				Position = ParseXY(split[0]),
				Velocity = ParseXY(split[1]),
			});
		}
	}

	private bool QuarterOfRobotsHaveNeighbors()
	{
		// HashSet for faster lookup
		var robotPositions = new HashSet<(int x, int y)>(robots.Select(r => r.Position));
		var robotsWith4Neighbors = robots.Count(r =>
			neighbors.Count(n =>
				robotPositions.Contains((r.Position.x + n.x, r.Position.y + n.y))) > 4);

		return robotsWith4Neighbors > robots.Count / 4;
	}

	public int FindChristmasTree(int start, int end)
	{
		for (var i = start; i <= end; i++)
		{
			var current = i + 1;
			UpdateRobotPositions(1);

			if (QuarterOfRobotsHaveNeighbors())
			{
				MakeImage(current);
				return current;
			}
		}

		return 0;
	}

	private void UpdateRobotPositions(int seconds)
	{
		for (var i = 0; i < robots.Count; ++i)
		{
			var nextX = robots[i].Position.x + robots[i].Velocity.x * seconds;
			var nextY = robots[i].Position.y + robots[i].Velocity.y * seconds;
			robots[i].Position = (nextX.Mod(MapWidth), nextY.Mod(MapHeight));
		}
	}

	private int RobotsInChunk(int x, int y, int w, int h)
	{
		return robots.Count(r =>
			r.Position.x >= x && r.Position.y >= y &&
			r.Position.x < w && r.Position.y < h);
	}

	private int CalculateSafetyFactor()
	{
		var (w, h) = (MapWidth / 2, MapHeight / 2);

		// Top-left chunk
		var chunk1 = RobotsInChunk(0, 0, w, h);
		// Bottom-left chunk
		var chunk3 = RobotsInChunk(0, h + 1, w, MapHeight);

		// Top-right chunk
		var chunk2 = RobotsInChunk(w + 1, 0, MapWidth, h);
		// Bottom-right chunk
		var chunk4 = RobotsInChunk(w + 1, h + 1, MapWidth, MapHeight);

		return chunk1 * chunk2 * chunk3 * chunk4;
	}

	private void MakeImage(int seconds)
	{
		using var bmp = new Bitmap(MapWidth, MapHeight);

		for (var y = 0; y < MapHeight; ++y)
		{
			for (var x = 0; x < MapWidth; ++x)
			{
				bmp.SetPixel(x, y, robots.Any(r => r.Position == (x, y))
					? Color.LightGreen
					: Color.Black);
			}
		}

		bmp.Save($@"..\..\..\Day 14\{seconds}.png", ImageFormat.Png);
	}

	static (int x, int y) ParseXY(string str)
	{
		var split = str.Split('=')[1].Split(',');
		return (int.Parse(split[0]), int.Parse(split[1]));
	}
}

class Robot
{
	public (int x, int y) Position { get; set; }
	public (int x, int y) Velocity { get; set; }
}
