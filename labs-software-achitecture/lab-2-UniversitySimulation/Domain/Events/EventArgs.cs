using System;

namespace UniversitySimulation.Domain.Events
{
    public sealed class ActivityStatusChangedEventArgs : EventArgs
    {
        public string ActivityName { get; }
        public ActivityStatus OldStatus { get; }
        public ActivityStatus NewStatus { get; }

        public ActivityStatusChangedEventArgs(
            string activityName, ActivityStatus oldStatus, ActivityStatus newStatus)
        {
            ActivityName = activityName;
            OldStatus = oldStatus;
            NewStatus = newStatus;
        }
    }

    public sealed class DisciplineActivitiesChangedEventArgs : EventArgs
    {
        public string DisciplineName { get; }
        public int TotalHours { get; }
        public Activity ChangedActivity { get; }

        public DisciplineActivitiesChangedEventArgs(
            string disciplineName, int totalHours, Activity changedActivity)
        {
            DisciplineName = disciplineName;
            TotalHours = totalHours;
            ChangedActivity = changedActivity;
        }
    }

    public sealed class WorkSubmittedEventArgs : EventArgs
    {
        public SubmittedWork Work { get; }

        public WorkSubmittedEventArgs(SubmittedWork work)
        {
            Work = work;
        }
    }

    public sealed class TeacherAssignmentChangedEventArgs : EventArgs
    {
        public string TeacherName { get; }
        public string DisciplineName { get; }  // null якщо звільнений
        public bool IsAssigned { get; }

        public TeacherAssignmentChangedEventArgs(
            string teacherName, string disciplineName, bool isAssigned)
        {
            TeacherName = teacherName;
            DisciplineName = disciplineName;
            IsAssigned = isAssigned;
        }
    }

    public sealed class SubGroupSizeChangedEventArgs : EventArgs
    {
        public int NewCount { get; }
        public bool IsValid { get; }

        public SubGroupSizeChangedEventArgs(int newCount, bool isValid)
        {
            NewCount = newCount;
            IsValid = isValid;
        }
    }
}
