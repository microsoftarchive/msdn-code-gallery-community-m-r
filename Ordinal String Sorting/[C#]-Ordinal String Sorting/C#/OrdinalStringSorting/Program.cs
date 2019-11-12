using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tempesta.Extensions;

namespace NaturalStringSorting
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> files = new List<string>()
			{
				"File 1.txt",
				"File 2.jpg",
				"File 3.doc",
				"File 10.txt",
				"File 11.csv",
				"File 20.xls",
				"File 21.ppt"
			};

			Console.WriteLine("Normal OrderBy");
			foreach (var file in files.OrderBy(λ => λ))
			{
				Console.WriteLine(file);
			}

			Console.WriteLine();

			Console.WriteLine("Ascending Ordinal OrderBy with explicit OrdinalStringComparer");
			foreach (var file in files.OrderBy(λ => λ, new OrdinalStringComparer()))
			{
				Console.WriteLine(file);
			}

			Console.WriteLine();

			Console.WriteLine("Ascending Ordinal OrderBy with implicit OrdinalStringComparer");
			foreach (var file in files.OrderByOrdinal(λ => λ))
			{
				Console.WriteLine(file);
			}
			
			Console.WriteLine();

			Console.WriteLine("Descending Ordinal OrderBy with implicit OrdinalStringComparer");
			foreach (var file in files.OrderByOrdinalDescending(λ => λ))
			{
				Console.WriteLine(file);
			}
		}
	}
}
