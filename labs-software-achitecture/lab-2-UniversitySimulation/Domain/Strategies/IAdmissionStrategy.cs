using System.Collections.Generic;

namespace UniversitySimulation.Domain.Strategies
{
    public interface IAdmissionStrategy
    {
        ValidationResult CheckAdmission(Student student);

        string Description { get; }
    }

    public sealed class AllowAllAdmissionStrategy : IAdmissionStrategy
    {
        public string Description => "Всі студенти допускаються";

        public ValidationResult CheckAdmission(Student student) =>
            ValidationResult.Success();
    }

    public sealed class LabWorkAdmissionStrategy : IAdmissionStrategy
    {
        public string Description => "Потрібні здані лабораторні роботи";

        public ValidationResult CheckAdmission(Student student)
        {
            if (!student.HasSubmitted(WorkType.LabWork))
                return ValidationResult.Failure("не здав лабораторні роботи");
            return ValidationResult.Success();
        }
    }
    public sealed class ExamAdmissionStrategy : IAdmissionStrategy
    {
        public string Description => "Потрібні здані лабораторні та курсова робота";

        public ValidationResult CheckAdmission(Student student)
        {
            var issues = new List<string>();

            if (!student.HasSubmitted(WorkType.LabWork))
                issues.Add("не здав лабораторні роботи");
            if (!student.HasSubmitted(WorkType.CourseWork))
                issues.Add("не здав курсову роботу");

            return issues.Count == 0
                ? ValidationResult.Success()
                : ValidationResult.Failure(string.Join(", ", issues));
        }
    }
}
