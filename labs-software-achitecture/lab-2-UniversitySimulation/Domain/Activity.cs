using System;
using UniversitySimulation.Domain.Events;
using UniversitySimulation.Domain.Strategies;

namespace UniversitySimulation.Domain
{
    public enum ActivityStatus { NotStarted, InProgress, Completed }
    public abstract class Activity
    {
        public string Name { get; protected set; }
        public int Hours { get; protected set; }
        public Teacher ResponsibleTeacher { get; private set; }
        public ActivityStatus Status { get; private set; }

        public event EventHandler<ActivityStatusChangedEventArgs> StatusChanged;

        protected IAdmissionStrategy AdmissionStrategy { get; set; }
            = new AllowAllAdmissionStrategy();

        protected Activity(string name, int hours)
        {
            Name = name;
            Hours = hours;
            Status = ActivityStatus.NotStarted;
        }

        public void AssignTeacher(Teacher teacher)
        {
            ResponsibleTeacher = teacher;
        }

        public virtual ValidationResult CheckIfCanStart()
        {
            if (ResponsibleTeacher == null)
                return ValidationResult.Failure("не призначено викладача");
            return ValidationResult.Success();
        }

        public ValidationResult CheckAdmission(Student student) =>
            AdmissionStrategy.CheckAdmission(student);

        public string AdmissionDescription => AdmissionStrategy.Description;

        public void Start()
        {
            if (Status != ActivityStatus.NotStarted) return;
            var oldStatus = Status;
            Status = ActivityStatus.InProgress;
            StatusChanged?.Invoke(this,
                new ActivityStatusChangedEventArgs(Name, oldStatus, Status));
        }

        public void Complete()
        {
            if (Status != ActivityStatus.InProgress) return;
            var oldStatus = Status;
            Status = ActivityStatus.Completed;
            StatusChanged?.Invoke(this,
                new ActivityStatusChangedEventArgs(Name, oldStatus, Status));
        }

        public void Reset()
        {
            var oldStatus = Status;
            Status = ActivityStatus.NotStarted;
            StatusChanged?.Invoke(this,
                new ActivityStatusChangedEventArgs(Name, oldStatus, Status));
        }
    }
}
