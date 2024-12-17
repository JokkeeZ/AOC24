﻿namespace AOC24;

public static class ExtensionMethods
{
	public static bool InBounds<T>(this T[,] map, int x, int y)
		=> x >= 0 && y >= 0 && x < map.GetLength(0) && y < map.GetLength(1);

	public static void SwapItems<T>(this IList<T> list, int index1, int index2)
	{
		if (index1 < 0 || index1 >= list.Count)
		{
			throw new IndexOutOfRangeException("First index is out of range.");
		}

		if (index2 < 0 || index2 >= list.Count)
		{
			throw new IndexOutOfRangeException("Second index is out of range.");
		}

		(list[index1], list[index2]) = (list[index2], list[index1]);
	}

	public static int NumberOfDigits(this long num)
	{
		if (num == 0)
		{
			return 1;
		}

		return (int)Math.Floor(Math.Log10(num) + 1);
	}
}
