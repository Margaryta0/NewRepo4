using System.Collections.Generic;

namespace UniversitySimulation.Domain
{
    public sealed class Group
    {
        private readonly List<Student> _students = new List<Student>();

        public int Id { get; }
        public string Name { get; }
        public int CourseYear { get; }

        public IReadOnlyList<Student> Students => _students;

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

        public void RemoveStudent(Student student) =>
            _students.Remove(student);

        public bool AllStudentsHaveLaptops()
        {
            foreach (var s in _students)
                if (!s.HasLaptop) return false;
            return true;
        }

        public override string ToString() => $"Група {Name} ({CourseYear} курс)";
    }
}
