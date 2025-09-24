using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using withEnteties;
using withFiles;


namespace ConsoleApp
{

    internal class ConsoleMenu
    {
        static void display(Student[] students)
        {
            foreach (Student s in students)
            {
                Console.WriteLine(s.FirstName + " " + s.LastName);
                Console.WriteLine("ID: " + s.StudentID.FullID);
                Console.WriteLine("Birth data: " + s.BirthDate.ToShortDateString());
                Console.WriteLine("Course: " + s.Course);
            }
        }
        static void Main(string[] args)
        {
            Student[] students = new Student[]
            {
                new Student("Margaryta", "Smal", new StudentID("KB", 123456), new DateTime(2007, 11, 14), 2),
                new Student("Olena", "Smal", new StudentID("KB", 789034), new DateTime(2005, 04, 26), 4),
                new Student("Tetiana", "Smal", new StudentID("KB", 112233), new DateTime(2002, 11, 29), 4),
                new Student("Ruvim", "Smal", new StudentID("KB", 789634), new DateTime(2004, 05, 01), 4),
            };

            DataStream dataStream = new DataStream(students);
            dataStream.CreateFile("file.txt");
            Console.WriteLine("Yeah!");
            string path = Path.GetFullPath("file.txt");
            Console.WriteLine("File path: " + path);
            Console.WriteLine("Selected students: " + dataStream.ReadStudents("file.txt", null));
            display(dataStream.ReturnSpring4Students("file.txt"));

            Baker newBaker1 = new Baker("Maryna", "Solom");
            Baker newBaker2 = new Baker("Sofia", "Marv");

            Entrepreneur newEnt = new Entrepreneur("Evgenii", "Smolyn");
            newEnt.AddWorkers(newBaker1);
            newEnt.AddWorkers(newBaker2);

            foreach (Baker baker in newEnt.Workers)
            {
                Console.WriteLine("FirstName: " + baker.FirstName);
                Console.WriteLine("LastName: " + baker.LastName);
            }
        }
    }
}
