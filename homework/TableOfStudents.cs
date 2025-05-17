using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace projectc
{
	internal class Program
	{
		static void Main(string[] args)
		{
			TableOfStudents table = new TableOfStudents();

			string[] names = table["name"];

			foreach (var name in names)
			{
				Console.WriteLine(name);
			}

			Console.WriteLine("Count of student with the lastname - Nechay: " + table.CountOfNechay);
		}
	}

}
