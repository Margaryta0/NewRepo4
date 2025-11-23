using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        private const string FileName = "students.txt";

        // DI
        private readonly IDataStream _dataStream; 
        private readonly StudentService _studentService; 

        public Program(IDataStream dataStream, StudentService studentService)
        {
            _dataStream = dataStream;
            _studentService = studentService;
        }

        static void Main(string[] args)
        {
            IDataStream dal = new DataStream();
            StudentService bll = new StudentService();

            // DI
            Program app = new Program(dal, bll);
            app.Run();
        }

        public void Run()
        {
            List<Student> initialStudents = GenerateInitialStudents();

            try
            {
                _dataStream.CreateFile(FileName, initialStudents);
                Console.WriteLine($"File '{FileName}' successfully created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating file: {ex.Message}");
                return;
            }

            List<Student> allStudents;
            try
            {
                allStudents = _dataStream.ReadAllStudents(FileName);
                Console.WriteLine($"\nRead {allStudents.Count} students.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return;
            }

            List<Student> spring4thCourseStudents = _studentService.GetSpringBorn4thCourseStudents(allStudents);
            Console.WriteLine($"Found {spring4thCourseStudents.Count} :");
            foreach (var s in spring4thCourseStudents)
            {
                Console.WriteLine($"{s.LastName} {s.FirstName}, Date: {s.BirthDate:dd.MM.yyyy}");
            }

            Console.WriteLine("\nChecking for Birthday:");
            foreach (var s in allStudents)
            {
                if (_studentService.IsTodayBirthday(s))
                {
                    Console.WriteLine($"Birthday of {s.LastName} ({s.CalculateAge() + 1} years old");
                }
            }

            Baker baker1 = new Baker("Oled", "Bagin");
            Entrepreneur entrepreneur = new Entrepreneur("Natali", "Car");

            baker1.ParachuteJump();

            _studentService.TryAddWorker(entrepreneur, baker1);
        }

        private List<Student> GenerateInitialStudents()
        {
            return new List<Student>
            {
                new Student("Megi", "Smal", new StudentID("IK", 123456), new DateTime(2002, 4, 15), 4),
                new Student("Olena", "Smal", new StudentID("OO", 987654), new DateTime(2002, 11, 1), 4),
                new Student("Petro", "Dykin", new StudentID("MM", 112233), new DateTime(2002, 3, 20), 4),
                new Student("Ivan", "Dykin", new StudentID("PP", 654321), new DateTime(2003, 5, 10), 3),
                new Student("Ruvim", "Smal", new StudentID("CC", 777777), DateTime.Today.AddYears(-20), 2)
            };
        }
    }
}
