using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySimulation
{
    public class Discipline
    {
        public string Name { get; private set; }
        private readonly int[] _allowedCourses;
        public Group StudyGroup { get; private set; }
        private bool _isCompleted = false;

        private readonly List<Activity> _activities = new List<Activity>();
        public IReadOnlyList<Activity> Activities
        {
            get { return _activities; }
        }

        public event EventHandler ActivityChanged;

        public Discipline(string name, Group group, int[] allowedCourses)
        {
            Name = name;
            StudyGroup = group;
            _allowedCourses = allowedCourses;
        }

        public int TotalHours
        {
            get
            {
                int total = 0;
                foreach (var a in _activities)
                    total += a.Hours;
                return total;
            }
        }

        public List<Teacher> GetTeachers()
        {
            var teachers = new List<Teacher>();
            foreach (var a in _activities)
            {
                if (a.ResponsibleTeacher != null && !teachers.Contains(a.ResponsibleTeacher))
                    teachers.Add(a.ResponsibleTeacher);
            }
            return teachers;
        }

        public string GetBlockReason(Group group)
        {
            if (_isCompleted)
                return "дисципліна вже завершена";

            if (group.CourseYear > 2)
                return "група на " + group.CourseYear +
                       " курсі — дисципліна тільки для 1-2 курсу";

            bool courseAllowed = false;
            foreach (var c in _allowedCourses)
                if (c == group.CourseYear) { courseAllowed = true; break; }
            if (!courseAllowed)
                return "дисципліна не передбачена для " +
                       group.CourseYear + " курсу";

            if (!group.AllStudentsHaveLaptops())
                return "не всі студенти мають ноутбук";

            return null;
        }

        public bool CanBeStudiedBy(Group group)
        {
            return GetBlockReason(group) == null;
        }

        public string AssignTeacher(Activity activity, Teacher teacher)
        {
            if (!_activities.Contains(activity))
                return "активність не належить цій дисципліні";

            if (!teacher.IsAvailableFor(this))
                return teacher + " вже веде іншу дисципліну";

            if (activity.ResponsibleTeacher != null)
            {
                Teacher oldTeacher = activity.ResponsibleTeacher;

                bool stillNeeded = false;
                foreach (var a in _activities)
                {
                    if (a != activity && a.ResponsibleTeacher == oldTeacher)
                    {
                        stillNeeded = true;
                        break;
                    }
                }

                if (!stillNeeded)
                    oldTeacher.RemoveFromDiscipline(this);
            }

            teacher.AssignToDiscipline(this);
            activity.AssignTeacher(teacher);
            return null; 
        }

        public bool IsValid()
        {
            if (TotalHours < 64) return false;

            var teachers = GetTeachers();
            if (teachers.Count < 1) return false;

            int subGroupCount = 0;
            foreach (var a in _activities)
            {
                if (a is LabWork)
                    subGroupCount += ((LabWork)a).SubGroups.Count;
            }

            if (teachers.Count > subGroupCount + 1) return false;
            return true;
        }

        public void AddActivity(Activity activity)
        {
            _activities.Add(activity);
            if (ActivityChanged != null)
                ActivityChanged(this, EventArgs.Empty);
        }

        public void RemoveActivity(Activity activity)
        {
            _activities.Remove(activity);
            if (ActivityChanged != null)
                ActivityChanged(this, EventArgs.Empty);
        }

        public void ReplaceActivity(Activity oldActivity, Activity newActivity)
        {
            int index = _activities.IndexOf(oldActivity);
            if (index >= 0)
            {
                _activities[index] = newActivity;
                if (ActivityChanged != null)
                    ActivityChanged(this, EventArgs.Empty);
            }
        }

        public void MarkAsCompleted()
        {
            _isCompleted = true;
        }

        public string StartActivity(Activity activity)
        {
            if (!_activities.Contains(activity))
                return "активність не належить цій дисципліні";

            if (activity.Status == ActivityStatus.InProgress)
                return "активність вже триває";

            if (!CanBeStudiedBy(StudyGroup))
                return GetBlockReason(StudyGroup);

            if (!activity.CanStart(StudyGroup))
                return activity.GetBlockReason(StudyGroup);

            if (activity.Status == ActivityStatus.Completed)
                activity.Reset();

            activity.Start();
            return null;
        }

        public string CompleteActivity(Activity activity)
        {
            if (!_activities.Contains(activity))
                return "активність не належить цій дисципліні";

            if (activity.Status == ActivityStatus.NotStarted)
                return "активність ще не розпочата";

            if (activity.Status == ActivityStatus.Completed)
                return "активність вже завершена";

            activity.Complete();
            return null;
        }

        public List<Student> GetAdmittedStudents(Activity activity)
        {
            if (activity is ModularTest)
                return ((ModularTest)activity).GetAdmittedStudents(StudyGroup);

            if (activity is Exam)
                return ((Exam)activity).GetAdmittedStudents(StudyGroup);

            // Для лекцій, лабораторних  — всі студенти допущені
            return new List<Student>(StudyGroup.Students);

        }

        public string GetValidationReport()
        {
            var issues = new List<string>();

            if (TotalHours < 64)
                issues.Add("недостатньо годин: " + TotalHours + " з 64");

            var teachers = GetTeachers();
            if (teachers.Count < 1)
                issues.Add("немає жодного викладача");

            int subGroupCount = 0;
            foreach (var a in _activities)
                if (a is LabWork)
                    subGroupCount += ((LabWork)a).SubGroups.Count;

            if (teachers.Count > subGroupCount + 1)
                issues.Add("забагато викладачів: " + teachers.Count +
                           ", максимум: " + (subGroupCount + 1));

            if (issues.Count == 0) return null;

            string result = "";
            foreach (var issue in issues)
                result += "\n    - " + issue;
            return result;
        }
    }
}
