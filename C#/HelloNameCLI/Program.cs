using System;

namespace HelloNameCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            	Console.WriteLine("Please enter your name");

		var name = Console.ReadLine();

		Console.WriteLine($"Hello {name}");

		Console.ReadKey();
        }
    }
}
