namespace AOC24;

interface IAdventDay
{
	void Solve(string[] input);
}

class Program
{
	static void Main(string[] args)
	{
		new Day6().Solve(Input(6));
	}

	static string[] Input(int day)
		=> File.ReadAllLines($@"..\..\..\Day {day}\input.txt");
}
