using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Serializable]
    public class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Course { get; set; }
        public string StudentID { get; set; }
        public DateTime DateOfBirth { get; set; }

        public override string ToString()
        {
            return $"Students: {LastName} {FirstName}, Course: {Course}, DataOfBirth: {DateOfBirth:dd.MM.yyyy}";
        }
    }
}
