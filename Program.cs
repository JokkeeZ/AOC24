namespace AOC24;

interface IAdventDay
{
	void Solve(string[] input);
}

class Program
{
	static void Main(string[] args)
	{
		new Day2().Solve(Input(2));
	}

	static string[] Input(int day)
		=> File.ReadAllLines($@"..\..\..\Day {day}\input.txt");
}
