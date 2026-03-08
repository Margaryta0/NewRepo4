using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySimulation
{
    public enum ActivityStatus
    {
        NotStarted,  
        InProgress,  
        Completed    
    }

    public abstract class Activity
    {
        public string Name { get; protected set; }
        public int Hours { get; protected set; }
        public Teacher ResponsibleTeacher { get; private set; }

        public ActivityStatus Status { get; private set; }

        public event EventHandler StatusChanged;

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

        public void Start()
        {
            if (Status != ActivityStatus.NotStarted) return;
            Status = ActivityStatus.InProgress;
            if (StatusChanged != null)
                StatusChanged(this, EventArgs.Empty);
        }

        public void Complete()
        {
            if (Status != ActivityStatus.InProgress) return;
            Status = ActivityStatus.Completed;
            if (StatusChanged != null)
                StatusChanged(this, EventArgs.Empty);
        }

        public void Reset()
        {
            Status = ActivityStatus.NotStarted;
            if (StatusChanged != null)
                StatusChanged(this, EventArgs.Empty);
        }

        public virtual string GetBlockReason(Group group)
        {
            if (ResponsibleTeacher == null)
                return "не призначено викладача";
            return null;
        }

        public bool CanStart(Group group)
        {
            return GetBlockReason(group) == null;
        }
    }
}
