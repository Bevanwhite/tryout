using System;

namespace StudentApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			/*
			int stuId = 1000;
			string studentId = "1000";

			stuId = stuId + 1;

			studentId = studentId + 1;

			Console.WriteLine($"stuId = {stuId}, StudentId = {studentId}");

			Console.ReadKey();
			*/
			byte stuId = 0;

			Console.WriteLine("Please enter an Id for this student");
			stuId = Convert.ToByte(Console.ReadLine());
			Console.WriteLine($"stuId: {stuId}");
			Console.ReadKey();
		}
	}
}
