using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySimulation
{
    public class SubGroup
    {
        private readonly List<Student> _students;

        // якщо хтось відрахувався — LabWork підпишеться і заблокує заняття
        public event EventHandler SizeChanged;

        public SubGroup(List<Student> students)
        {
            if (students.Count < 10)
                throw new ArgumentException("Підгрупа не може мати менше 10 студентів.");

            _students = new List<Student>(students);
        }

        public IReadOnlyList<Student> Students
        {
            get { return _students; }
        }

        public int Count
        {
            get { return _students.Count; }
        }

        public void RemoveStudent(Student student)
        {
            _students.Remove(student);

            if (SizeChanged != null)
                SizeChanged(this, EventArgs.Empty);
        }

        public bool IsValid()
        {
            return _students.Count >= 10;
        }
    }
}
