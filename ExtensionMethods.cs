namespace AOC24;

public static class ExtensionMethods
{
	public static bool InBounds<T>(this T[,] map, int x, int y) where T : IEquatable<T>
		=> x >= 0 && y >= 0 && x < map.GetLength(0) && y < map.GetLength(1);
}
