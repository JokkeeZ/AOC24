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
		foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
			.Where(type => type.GetInterfaces().Contains(typeof(IAdventDay))))
		{
			var instance = (IAdventDay)Activator.CreateInstance(type);

			if (instance.IsActive)
			{
				var day = type.Name.Replace("Day", string.Empty);
				var inputPath = $@"..\..\..\Day {day}\input.txt";

				if (File.Exists(inputPath))
				{
					instance.Solve(File.ReadAllLines(inputPath));
				}
			}
		}
	}
}
