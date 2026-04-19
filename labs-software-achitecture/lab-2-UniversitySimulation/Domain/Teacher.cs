using System;
using UniversitySimulation.Domain.Events;

namespace UniversitySimulation.Domain
{
    public sealed class Teacher
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public Discipline CurrentDiscipline { get; private set; }

        public event EventHandler<TeacherAssignmentChangedEventArgs> AssignmentChanged;

        public Teacher(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public bool IsAvailableFor(Discipline discipline) =>
            CurrentDiscipline == null || CurrentDiscipline == discipline;

        public ValidationResult AssignToDiscipline(Discipline discipline)
        {
            if (!IsAvailableFor(discipline))
                return ValidationResult.Failure($"{this} вже веде іншу дисципліну");

            CurrentDiscipline = discipline;
            AssignmentChanged?.Invoke(this,
                new TeacherAssignmentChangedEventArgs(ToString(), discipline.Name, true));
            return ValidationResult.Success();
        }

        public void RemoveFromDiscipline(Discipline discipline)
        {
            if (CurrentDiscipline != discipline) return;
            CurrentDiscipline = null;
            AssignmentChanged?.Invoke(this,
                new TeacherAssignmentChangedEventArgs(ToString(), discipline.Name, false));
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
