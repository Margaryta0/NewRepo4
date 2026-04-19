using System;
using UniversitySimulation.Domain;
using UniversitySimulation.Domain.Events;
using UniversitySimulation.Services;

namespace UniversitySimulation.UI
{
    public sealed class ConsoleObserver
    {
        private readonly DisciplineService _service;

        public ConsoleObserver(DisciplineService service)
        {
            _service = service;

            _service.ActivityStatusChanged += OnActivityStatusChanged;
            _service.DisciplineActivitiesChanged += OnDisciplineActivitiesChanged;
            _service.StudentWorkSubmitted += OnStudentWorkSubmitted;
            _service.TeacherAssignmentChanged += OnTeacherAssignmentChanged;
            _service.SubGroupSizeChanged += OnSubGroupSizeChanged;
        }

        private void OnActivityStatusChanged(object sender,
            ActivityStatusChangedEventArgs e)
        {
            Console.WriteLine(
                $"  [ПОДІЯ] Активність \"{e.ActivityName}\": " +
                $"{StatusLabel(e.OldStatus)} → {StatusLabel(e.NewStatus)}");
            Console.ResetColor();
        }

        private void OnDisciplineActivitiesChanged(object sender,
            DisciplineActivitiesChangedEventArgs e)
        {
            Console.WriteLine(
                $"  [ПОДІЯ] Дисципліна \"{e.DisciplineName}\": " +
                $"змінено активності, годин тепер: {e.TotalHours}");
        }

        private void OnStudentWorkSubmitted(object sender,
            WorkSubmittedEventArgs e)
        {
            Console.WriteLine(
                $"  [ПОДІЯ] Студент \"{sender}\" здав роботу: {e.Work}");
        }

        private void OnTeacherAssignmentChanged(object sender,
            TeacherAssignmentChangedEventArgs e)
        {
            string action = e.IsAssigned
                ? $"призначений на \"{e.DisciplineName}\""
                : $"звільнений з \"{e.DisciplineName}\"";
            Console.WriteLine($"  [ПОДІЯ] Викладач {e.TeacherName} {action}");
        }

        private void OnSubGroupSizeChanged(object sender,
            SubGroupSizeChangedEventArgs e)
        {
            Console.WriteLine(
                $"  [ПОДІЯ] Підгрупа змінила розмір: {e.NewCount} студентів" +
                (e.IsValid ? "" : " ⚠ ЗАМАЛА (мін. 10)!"));
        }


        private static string StatusLabel(ActivityStatus s)
        {
            switch (s)
            {
                case ActivityStatus.NotStarted: return "Не розпочата";
                case ActivityStatus.InProgress: return "Триває";
                case ActivityStatus.Completed:  return "Завершена";
                default:                               return s.ToString();
            }
        }

        public void Unsubscribe()
        {
            _service.ActivityStatusChanged -= OnActivityStatusChanged;
            _service.DisciplineActivitiesChanged -= OnDisciplineActivitiesChanged;
            _service.StudentWorkSubmitted -= OnStudentWorkSubmitted;
            _service.TeacherAssignmentChanged -= OnTeacherAssignmentChanged;
            _service.SubGroupSizeChanged -= OnSubGroupSizeChanged;
        }
    }
}
