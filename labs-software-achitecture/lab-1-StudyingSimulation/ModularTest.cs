using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySimulation
{
    public class ModularTest : Activity
    {
        public ModularTest(int hours) : base("МКР", hours) { }

        public List<Student> GetAdmittedStudents(Group group)
        {
            var admitted = new List<Student>();
            foreach (var student in group.Students)
            {
                if (student.CanAttendControl(WorkType.LabWork))
                    admitted.Add(student);
            }
            return admitted;
        }
    }
}
