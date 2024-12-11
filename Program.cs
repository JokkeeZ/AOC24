namespace AOC24;

interface IAdventDay
{
	void Solve(string[] input);
}

class Program
{
	static void Main(string[] args)
	{
		new Day1().Solve(File.ReadAllLines(@"..\..\..\day1.txt"));
	}
}
