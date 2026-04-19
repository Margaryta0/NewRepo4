using System;
using System.Collections.Generic;
using UniversitySimulation.Domain;
using UniversitySimulation.Domain.Events;

namespace UniversitySimulation.Services
{
    public sealed class DisciplineService
    {
        private readonly List<Discipline> _disciplines;
        private readonly List<Teacher> _teachers;
        private readonly List<Group> _groups;
        private readonly List<Student> _students;

        public event EventHandler<ActivityStatusChangedEventArgs> ActivityStatusChanged;
        public event EventHandler<DisciplineActivitiesChangedEventArgs> DisciplineActivitiesChanged;
        public event EventHandler<WorkSubmittedEventArgs> StudentWorkSubmitted;
        public event EventHandler<TeacherAssignmentChangedEventArgs> TeacherAssignmentChanged;
        public event EventHandler<SubGroupSizeChangedEventArgs> SubGroupSizeChanged;

        public DisciplineService(
            List<Discipline> disciplines,
            List<Teacher> teachers,
            List<Group> groups,
            List<Student> students)
        {
            _disciplines = disciplines;
            _teachers = teachers;
            _groups = groups;
            _students = students;
        }

        public IReadOnlyList<Discipline> Disciplines => _disciplines;
        public IReadOnlyList<Teacher> Teachers => _teachers;
        public IReadOnlyList<Group> Groups => _groups;
        public IReadOnlyList<Student> Students => _students;

        public void SubscribeToDiscipline(Discipline discipline)
        {
            discipline.ActivitiesChanged += OnDisciplineActivitiesChanged;
            foreach (var activity in discipline.Activities)
                SubscribeToActivity(activity);
        }

        public void SubscribeToActivity(Activity activity)
        {
            activity.StatusChanged += OnActivityStatusChanged;

            if (activity is LabWork)
            {
                LabWork labWork = (LabWork)activity;
                foreach (var sg in labWork.SubGroups)
                    SubscribeToSubGroup(sg);
            }
        }

        public void SubscribeToTeacher(Teacher teacher)
        {
            teacher.AssignmentChanged += OnTeacherAssignmentChanged;
        }

        public void SubscribeToStudent(Student student)
        {
            student.WorkSubmitted += OnStudentWorkSubmitted;
        }

        public void SubscribeToSubGroup(SubGroup subGroup)
        {
            subGroup.SizeChanged += OnSubGroupSizeChanged;
        }

        public void UnsubscribeFromDiscipline(Discipline discipline)
        {
            discipline.ActivitiesChanged -= OnDisciplineActivitiesChanged;
            foreach (var activity in discipline.Activities)
                activity.StatusChanged -= OnActivityStatusChanged;
        }

        public ValidationResult StartActivity(Discipline discipline, Activity activity)
        {
            return discipline.StartActivity(activity);
        }

        public ValidationResult CompleteActivity(Discipline discipline, Activity activity)
        {
            return discipline.CompleteActivity(activity);
        }

        public ValidationResult AssignTeacher(Discipline discipline, Activity activity, Teacher teacher)
        {
            return discipline.AssignTeacher(activity, teacher);
        }

        public ValidationResult AddActivity(Discipline discipline, Activity activity)
        {
            SubscribeToActivity(activity);
            discipline.AddActivity(activity);
            return ValidationResult.Success();
        }

        public ValidationResult DivideLabWorkIntoSubGroups(
            LabWork labWork, Group group, int subGroupCount)
        {
            var result = labWork.DivideIntoSubGroups(group, subGroupCount);
            if (result.IsSuccess)
            {
                // Підписуємось на нові підгрупи
                foreach (var sg in labWork.SubGroups)
                    SubscribeToSubGroup(sg);
            }
            return result;
        }

        public List<(Student Student, ValidationResult Admission)>
            GetAdmissionResults(Discipline discipline, Activity activity)
        {
            var results = new List<(Student, ValidationResult)>();
            foreach (var student in discipline.StudyGroup.Students)
                results.Add((student, activity.CheckAdmission(student)));
            return results;
        }

        private void OnActivityStatusChanged(object sender, ActivityStatusChangedEventArgs e)
        {
            ActivityStatusChanged?.Invoke(sender, e);
        }

        private void OnDisciplineActivitiesChanged(object sender, DisciplineActivitiesChangedEventArgs e)
        {
            DisciplineActivitiesChanged?.Invoke(sender, e);
        }

        private void OnStudentWorkSubmitted(object sender, WorkSubmittedEventArgs e)
        {
            StudentWorkSubmitted?.Invoke(sender, e);
        }

        private void OnTeacherAssignmentChanged(object sender, TeacherAssignmentChangedEventArgs e)
        {
            TeacherAssignmentChanged?.Invoke(sender, e);
        }

        private void OnSubGroupSizeChanged(object sender, SubGroupSizeChangedEventArgs e)
        {
            SubGroupSizeChanged?.Invoke(sender, e);
        }
    }
}
