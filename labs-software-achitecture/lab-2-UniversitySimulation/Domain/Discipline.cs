using System;
using System.Collections.Generic;
using UniversitySimulation.Domain.Events;

namespace UniversitySimulation.Domain
{
    public sealed class Discipline
    {
        private readonly List<Activity> _activities = new List<Activity>();
        private readonly int[] _allowedCourses;
        private bool _isCompleted = false;

        public string Name { get; }
        public Group StudyGroup { get; }

        public IReadOnlyList<Activity> Activities => _activities;

        public int TotalHours
        {
            get
            {
                int total = 0;
                foreach (var a in _activities) total += a.Hours;
                return total;
            }
        }

        public event EventHandler<DisciplineActivitiesChangedEventArgs> ActivitiesChanged;

        public Discipline(string name, Group group, int[] allowedCourses)
        {
            Name = name;
            StudyGroup = group;
            _allowedCourses = allowedCourses;
        }

        public ValidationResult ValidateForGroup(Group group)
        {
            if (_isCompleted)
                return ValidationResult.Failure("дисципліна вже завершена");

            if (group.CourseYear > 2)
                return ValidationResult.Failure(
                    $"група на {group.CourseYear} курсі — дисципліна тільки для 1-2 курсу");

            bool courseAllowed = false;
            foreach (var c in _allowedCourses)
                if (c == group.CourseYear) { courseAllowed = true; break; }

            if (!courseAllowed)
                return ValidationResult.Failure(
                    $"дисципліна не передбачена для {group.CourseYear} курсу");

            if (!group.AllStudentsHaveLaptops())
                return ValidationResult.Failure("не всі студенти мають ноутбук/комп'ютер");

            return ValidationResult.Success();
        }

        public void AddActivity(Activity activity)
        {
            _activities.Add(activity);
            ActivitiesChanged?.Invoke(this,
                new DisciplineActivitiesChangedEventArgs(Name, TotalHours, activity));
        }

        public void RemoveActivity(Activity activity)
        {
            _activities.Remove(activity);
            ActivitiesChanged?.Invoke(this,
                new DisciplineActivitiesChangedEventArgs(Name, TotalHours, activity));
        }

        public void ReplaceActivity(Activity oldActivity, Activity newActivity)
        {
            int index = _activities.IndexOf(oldActivity);
            if (index < 0) return;
            _activities[index] = newActivity;
            ActivitiesChanged?.Invoke(this,
                new DisciplineActivitiesChangedEventArgs(Name, TotalHours, newActivity));
        }

        public ValidationResult AssignTeacher(Activity activity, Teacher teacher)
        {
            if (!_activities.Contains(activity))
                return ValidationResult.Failure("активність не належить цій дисципліні");

            if (!teacher.IsAvailableFor(this))
                return ValidationResult.Failure($"{teacher} вже веде іншу дисципліну");

            if (activity.ResponsibleTeacher != null)
            {
                Teacher oldTeacher = activity.ResponsibleTeacher;
                bool stillNeeded = false;
                foreach (var a in _activities)
                    if (a != activity && a.ResponsibleTeacher == oldTeacher)
                    { stillNeeded = true; break; }

                if (!stillNeeded)
                    oldTeacher.RemoveFromDiscipline(this);
            }

            var assignResult = teacher.AssignToDiscipline(this);
            if (!assignResult.IsSuccess) return assignResult;

            activity.AssignTeacher(teacher);
            return ValidationResult.Success();
        }

        public List<Teacher> GetTeachers()
        {
            var teachers = new List<Teacher>();
            foreach (var a in _activities)
                if (a.ResponsibleTeacher != null && !teachers.Contains(a.ResponsibleTeacher))
                    teachers.Add(a.ResponsibleTeacher);
            return teachers;
        }

        public ValidationResult StartActivity(Activity activity)
        {
            if (!_activities.Contains(activity))
                return ValidationResult.Failure("активність не належить цій дисципліні");

            if (activity.Status == ActivityStatus.InProgress)
                return ValidationResult.Failure("активність вже триває");

            var disciplineCheck = ValidateForGroup(StudyGroup);
            if (!disciplineCheck.IsSuccess) return disciplineCheck;

            var activityCheck = activity.CheckIfCanStart();
            if (!activityCheck.IsSuccess) return activityCheck;

            if (activity.Status == ActivityStatus.Completed)
                activity.Reset();

            activity.Start();
            return ValidationResult.Success();
        }

        public ValidationResult CompleteActivity(Activity activity)
        {
            if (!_activities.Contains(activity))
                return ValidationResult.Failure("активність не належить цій дисципліні");

            if (activity.Status == ActivityStatus.NotStarted)
                return ValidationResult.Failure("активність ще не розпочата");

            if (activity.Status == ActivityStatus.Completed)
                return ValidationResult.Failure("активність вже завершена");

            activity.Complete();
            return ValidationResult.Success();
        }

        public bool IsValid()
        {
            if (TotalHours < 64) return false;
            var teachers = GetTeachers();
            if (teachers.Count < 1) return false;
            int subGroupCount = 0;
            foreach (var a in _activities)
                if (a is LabWork) subGroupCount += ((LabWork)a).SubGroups.Count;
            if (teachers.Count > subGroupCount + 1) return false;
            return true;
        }

        public List<string> GetValidationIssues()
        {
            var issues = new List<string>();

            if (TotalHours < 64)
                issues.Add($"недостатньо годин: {TotalHours} з 64");

            var teachers = GetTeachers();
            if (teachers.Count < 1)
                issues.Add("немає жодного викладача");

            int subGroupCount = 0;
            foreach (var a in _activities)
                if (a is LabWork) subGroupCount += ((LabWork)a).SubGroups.Count;

            if (teachers.Count > subGroupCount + 1)
                issues.Add($"забагато викладачів: {teachers.Count}, максимум: {subGroupCount + 1}");

            return issues;
        }

        public void MarkAsCompleted() => _isCompleted = true;

        public override string ToString() => Name;
    }
}
