using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Menu
    {
        private readonly EntityService _service;

        public Menu(EntityService service)
        {
            _service = service;
        }

        public void MainMenu()
        {

            Console.WriteLine("Save text data in fale...");
            _service.SeedDataAndSave();
            Console.WriteLine("Data saves.");

            Console.WriteLine("All students in system:");
            List<Student> students = _service.GetAllStudents();
            foreach (var s in students)
            {
                Console.WriteLine(s);
            }

            int count = _service.CountSpringFourthYearStudents();

            Console.WriteLine($"Students of 4 course, born in spring: {count}");
        }
    }
}
