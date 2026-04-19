using System;
using System.Collections.Generic;
using System.Text;
using UniversitySimulation.Domain;
using UniversitySimulation.Services;
using UniversitySimulation.UI;

namespace UniversitySimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var students = new List<Student>();
            var teachers = new List<Teacher>();
            var groups = new List<Group>();
            var disciplines = new List<Discipline>();

            var service = new DisciplineService(disciplines, teachers, groups, students);

            var observer = new ConsoleObserver(service);

            DataSeeder.Seed(students, teachers, groups, disciplines, service);

            var ui = new ConsoleUI(service);
            ui.Run();

            observer.Unsubscribe();
        }
    }
}
