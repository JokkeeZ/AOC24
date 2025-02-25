﻿using System.Diagnostics;
using System.Reflection;

namespace AOC24;

interface IAdventDay
{
	bool IsActive { get; }

	void Solve(string[] input);
}

class Program
{
	static void Main(string[] args)
	{
		foreach (var day in Assembly.GetExecutingAssembly().GetTypes()
			.Where(t => t.GetInterfaces().Contains(typeof(IAdventDay)))
			.Select(t => ((IAdventDay)Activator.CreateInstance(t)))
			.Where(d => d.IsActive))
		{
			var dayNumber = day.GetType().Name.Replace("Day", string.Empty);
			var inputPath = $@"..\..\..\Day {dayNumber}\input.txt";

			if (File.Exists(inputPath))
			{
				var sw = Stopwatch.StartNew();
				day.Solve(File.ReadAllLines(inputPath));
				Console.WriteLine($"[{dayNumber}] -> Elapsed: {sw.ElapsedMilliseconds}ms.");
			}
			else
			{
				Console.WriteLine($"No input file found for the day: {dayNumber}.");
			}
		}
	}
}
