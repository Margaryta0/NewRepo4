using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySimulation
{
    public class Student
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool HasLaptop { get; private set; }

        private readonly List<SubmittedWork> _submittedWorks = new List<SubmittedWork>();
        public IReadOnlyList<SubmittedWork> SubmittedWorks
        {
            get { return _submittedWorks; }
        }

        public event EventHandler<SubmittedWork> WorkSubmitted;

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

            if (WorkSubmitted != null)
                WorkSubmitted(this, work);
        }

        public bool HasSubmitted(WorkType type)
        {
            foreach (var work in _submittedWorks)
                if (work.Type == type) return true;
            return false;
        }

        public void SetLaptop(bool hasLaptop)
        {
            HasLaptop = hasLaptop;
        }

        public bool CanAttendControl(WorkType requiredWork)
        {
            return HasSubmitted(requiredWork);
        }

        public string GetAdmissionBlockReason(WorkType requiredWork)
        {
            if (!HasSubmitted(requiredWork))
                return "не здав " + (requiredWork == WorkType.LabWork
                    ? "лабораторні роботи"
                    : "курсову роботу");
            return null;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
