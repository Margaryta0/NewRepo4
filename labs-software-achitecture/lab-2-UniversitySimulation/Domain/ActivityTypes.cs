using UniversitySimulation.Domain.Strategies;
using System.Collections.Generic;

namespace UniversitySimulation.Domain
{

    public sealed class Lecture : Activity
    {
        public Lecture(int hours) : base("Лекція", hours)
        {
            AdmissionStrategy = new AllowAllAdmissionStrategy();
        }
    }

    public sealed class CourseWork : Activity
    {
        public CourseWork(int hours) : base("Курсова робота", hours)
        {
            AdmissionStrategy = new AllowAllAdmissionStrategy();
        }
    }

    public sealed class ModularTest : Activity
    {
        public ModularTest(int hours) : base("МКР", hours)
        {
            AdmissionStrategy = new LabWorkAdmissionStrategy();
        }
    }

    public sealed class Exam : Activity
    {
        public Exam(int hours) : base("Екзамен", hours)
        {
            AdmissionStrategy = new ExamAdmissionStrategy();
        }
    }

    public sealed class Credit : Activity
    {
        public Credit(int hours) : base("Залік", hours)
        {
            AdmissionStrategy = new AllowAllAdmissionStrategy();
        }
    }

    public sealed class LabWork : Activity
    {
        private readonly List<SubGroup> _subGroups =
            new List<SubGroup>();

        public IReadOnlyList<SubGroup> SubGroups => _subGroups;

        private bool _isBlockedBySubGroups = false;

        public LabWork(int hours) : base("Лабораторна робота", hours)
        {
            AdmissionStrategy = new AllowAllAdmissionStrategy();
        }

        public ValidationResult DivideIntoSubGroups(Group group, int subGroupCount)
        {
            if (subGroupCount < 2)
                return ValidationResult.Failure("мінімум 2 підгрупи");

            if (group.Students.Count < subGroupCount * 10)
                return ValidationResult.Failure(
                    $"недостатньо студентів. Потрібно мінімум {subGroupCount * 10}, є {group.Students.Count}");

            // Відписуємось від старих підгруп
            foreach (var sg in _subGroups)
                sg.SizeChanged -= OnSubGroupSizeChanged;

            _subGroups.Clear();

            var students = new List<Student>(group.Students);
            int baseSize = students.Count / subGroupCount;
            int remainder = students.Count % subGroupCount;
            int startIndex = 0;

            for (int i = 0; i < subGroupCount; i++)
            {
                int size = baseSize + (i < remainder ? 1 : 0);
                var sg = new SubGroup(students.GetRange(startIndex, size));
                startIndex += size;
                sg.SizeChanged += OnSubGroupSizeChanged;
                _subGroups.Add(sg);
            }

            _isBlockedBySubGroups = false;
            return ValidationResult.Success();
        }

        private void OnSubGroupSizeChanged(object sender,
            UniversitySimulation.Domain.Events.SubGroupSizeChangedEventArgs e)
        {
            // Перевіряємо чи є хоч одна невалідна підгрупа
            _isBlockedBySubGroups = false;
            foreach (var sg in _subGroups)
            {
                if (!sg.IsValid())
                {
                    _isBlockedBySubGroups = true;
                    break;
                }
            }
        }
        public override ValidationResult CheckIfCanStart()
        {
            var baseCheck = base.CheckIfCanStart();
            if (!baseCheck.IsSuccess) return baseCheck;

            if (_subGroups.Count == 0)
                return ValidationResult.Failure("не створено жодної підгрупи");

            foreach (var sg in _subGroups)
                if (!sg.IsValid())
                    return ValidationResult.Failure(
                        $"підгрупа має менше 10 студентів (зараз: {sg.Count})");

            if (_isBlockedBySubGroups)
                return ValidationResult.Failure(
                    "одна з підгруп заблокована через недостатню кількість студентів");

            return ValidationResult.Success();
        }
    }
}
