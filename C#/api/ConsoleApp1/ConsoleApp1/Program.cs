class Program
{
	static void Main()
	{
		List<int> numbers = new List<int> { 1, 1, 2, 4, 4, 3, 5, 7, 9 };

		int? previousValue = null;

		foreach (int currentValue in numbers)
		{
			// Check if it's not the first iteration
			if (previousValue.HasValue)
			{
				// Compare current value with the previous one
				if (currentValue > previousValue.Value)
				{
					Console.WriteLine($"{currentValue} is greater than {previousValue}");
				}
				else if (currentValue < previousValue.Value)
				{
					Console.WriteLine($"{currentValue} is less than {previousValue}");
				}
				else
				{
					Console.WriteLine($"{currentValue} is equal to {previousValue}");
				}
			}

			// Update the previous value for the next iteration
			previousValue = currentValue;
		}
	}
}
