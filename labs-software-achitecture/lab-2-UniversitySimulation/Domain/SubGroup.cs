using System;
using System.Collections.Generic;
using UniversitySimulation.Domain.Events;

namespace UniversitySimulation.Domain
{
    public sealed class SubGroup
    {
        private readonly List<Student> _students;

        public event EventHandler<SubGroupSizeChangedEventArgs> SizeChanged;

        public SubGroup(List<Student> students)
        {
            if (students == null || students.Count < 10)
                throw new ArgumentException("Підгрупа не може мати менше 10 студентів.");
            _students = new List<Student>(students);
        }

        public IReadOnlyList<Student> Students => _students;
        public int Count => _students.Count;
        public bool IsValid() => _students.Count >= 10;

        public void RemoveStudent(Student student)
        {
            if (!_students.Contains(student)) return;
            _students.Remove(student);
            SizeChanged?.Invoke(this,
                new SubGroupSizeChangedEventArgs(Count, IsValid()));
        }
    }
}
