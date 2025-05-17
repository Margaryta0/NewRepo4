using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    struct Student
    {
        public string name;
        public string lastname;
        public string patronymic;

        public Student(string _name, string _lastname, string _patronymic)
        {
            name = _name;
            lastname = _lastname;
            patronymic = _patronymic;
        }
    }
    public class TableOfStudents
    {
        private Student[] students = new Student[]
        {
            new Student("Kate", "Petrochyk", "Andriivna"),
            new Student("Anne", "Muzychuk", "Oleksandrivna"),
            new Student("Olena", "Smal", "Pavlivna"),
            new Student("Ivan", "Nechay", "Petrovych")
        };

        public string[] this[string columnName]
        {
            get
            {
                switch (columnName.ToLower())
                {
                    case "name":
                        return students.Select(s => s.name).ToArray();
                    case "lastname":
                        return students.Select(s => s.lastname).ToArray();
                    case "patronymic":
                        return students.Select(s => s.patronymic).ToArray();
                    default:
                        throw new ArgumentException("Invalid name of column!");
                }
            }
        }

        public int CountOfNechay
        {
            get
            {

                return students.Count(s => s.lastname.Equals("Nechay", StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}