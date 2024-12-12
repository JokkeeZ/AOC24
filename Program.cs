namespace AOC24;

interface IAdventDay
{
	void Solve(string[] input);
}

class Program
{
	static void Main(string[] args)
	{
		new Day3().Solve(Input(3));
	}

	static string[] Input(int day)
		=> File.ReadAllLines($@"..\..\..\Day {day}\input.txt");
}
