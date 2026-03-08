using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySimulation
{
    public class Group
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public int CourseYear { get; private set; }

        private readonly List<Student> _students = new List<Student>();

        public IReadOnlyList<Student> Students
        {
            get { return _students; }
        }

        public Group(int id, string name, int courseYear)
        {
            Id = id;
            Name = name;
            CourseYear = courseYear;
        }

        public void AddStudent(Student student)
        {
            if (!_students.Contains(student))
                _students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            _students.Remove(student);
        }

        public bool AllStudentsHaveLaptops()
        {
            foreach (var student in _students)
                if (!student.HasLaptop) return false;
            return true;
        }

        public override string ToString()
        {
            return "Група " + Name + " (" + CourseYear + " курс)";
        }
    }
}
