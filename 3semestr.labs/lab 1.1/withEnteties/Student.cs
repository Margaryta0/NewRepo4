using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace withEnteties
{
    public class Student : Human, ISpeciality
    {
        private StudentID _studentID;
        public StudentID StudentID => _studentID;

        public DateTime BirthDate { get; set; }
        private int _countOfJumps = 0;
        public int CountOfJumps => _countOfJumps;

        public int Course { get; set; }

        public Student(string firstName, string lastName, StudentID studentID, DateTime birthDate, int course) :
            base(firstName, lastName)
        {
            _studentID = studentID;
            BirthDate = birthDate;
            Course = course;
        }

        public void ParachuteJump()
        {
            _countOfJumps++;
        }

    }
}
