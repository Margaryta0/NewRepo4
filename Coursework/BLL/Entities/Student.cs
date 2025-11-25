using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class Student : IEntity<string>
    {
        public string StudentID { get; set; }

        public string Id { get => StudentID; set => StudentID = value; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Course { get; set; }

        public Student() { }

        public Student(string studentID, string lastName, string firstName, int course)
        {
            StudentID = studentID;
            LastName = lastName;
            FirstName = firstName;
            Course = course;
        }

        public static bool IsValidStudentID(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || id.Length != 8)
                return false;

            if (!char.IsUpper(id[0]) || !char.IsUpper(id[1]))
                return false;

            for (int i = 2; i < 8; i++)
            {
                if (!char.IsDigit(id[i]))
                    return false;
            }

            return true;
        }
    }
}
