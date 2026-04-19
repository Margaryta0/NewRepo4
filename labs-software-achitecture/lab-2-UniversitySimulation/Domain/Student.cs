using System;
using System.Collections.Generic;
using UniversitySimulation.Domain.Events;

namespace UniversitySimulation.Domain
{
    public enum WorkType { LabWork, CourseWork }

    public sealed class SubmittedWork
    {
        public WorkType Type { get; }
        public string Description { get; }

        public SubmittedWork(WorkType type, string description)
        {
            Type = type;
            Description = description;
        }

        public override string ToString() =>
            $"{(Type == WorkType.LabWork ? "Лабораторна" : "Курсова")}: {Description}";
    }

    public sealed class Student
    {
        private readonly List<SubmittedWork> _submittedWorks = new List<SubmittedWork>();

        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public bool HasLaptop { get; private set; }

        public IReadOnlyList<SubmittedWork> SubmittedWorks => _submittedWorks;

        public event EventHandler<WorkSubmittedEventArgs> WorkSubmitted;

        public Student(int id, string firstName, string lastName, bool hasLaptop)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            HasLaptop = hasLaptop;
        }

        public void SubmitWork(WorkType type, string description)
        {
            var work = new SubmittedWork(type, description);
            _submittedWorks.Add(work);
            WorkSubmitted?.Invoke(this, new WorkSubmittedEventArgs(work));
        }

        public bool HasSubmitted(WorkType type)
        {
            foreach (var w in _submittedWorks)
                if (w.Type == type) return true;
            return false;
        }

        public void SetLaptop(bool hasLaptop) => HasLaptop = hasLaptop;

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
