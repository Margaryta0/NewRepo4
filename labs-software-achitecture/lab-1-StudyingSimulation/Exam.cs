using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySimulation
{
    public class Exam : Activity
    {
        public Exam(int hours) : base("Екзамен", hours) { }

        public List<Student> GetAdmittedStudents(Group group)
        {
            var admitted = new List<Student>();
            foreach (var student in group.Students)
            {
                if (student.CanAttendControl(WorkType.LabWork) &&
                    student.CanAttendControl(WorkType.CourseWork))
                    admitted.Add(student);
            }
            return admitted;
        }

    }
}
