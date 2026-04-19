using System;
using System.Collections.Generic;
using UniversitySimulation.Domain;
using UniversitySimulation.Services;

namespace UniversitySimulation.UI
{
    public sealed class ConsoleUI
    {
        private readonly DisciplineService _service;

        public ConsoleUI(DisciplineService service)
        {
            _service = service;
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                PrintMainMenu();
                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1": ShowDisciplines(); break;
                    case "2": ShowGroups(); break;
                    case "3": SubmitWorkMenu(); break;
                    case "4": StartActivityMenu(); break;
                    case "5": CompleteActivityMenu(); break;
                    case "6": AssignTeacherMenu(); break;
                    case "7": AddActivityMenu(); break;
                    case "8": DivideSubGroupsMenu(); break;
                    case "0": running = false; break;
                    default:
                        PrintError("Невідома команда. Спробуйте ще раз.");
                        break;
                }
            }
        }

        private void PrintMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("╔═══════════════════════════════╗");
            Console.WriteLine("║    Університетська система    ║");
            Console.WriteLine("╠═══════════════════════════════╣");
            Console.WriteLine("║ 1. Переглянути дисципліни     ║");
            Console.WriteLine("║ 2. Переглянути групи          ║");
            Console.WriteLine("║ 3. Здати роботу (студент)     ║");
            Console.WriteLine("║ 4. Запустити активність       ║");
            Console.WriteLine("║ 5. Завершити активність       ║");
            Console.WriteLine("║ 6. Призначити викладача       ║");
            Console.WriteLine("║ 7. Додати активність          ║");
            Console.WriteLine("║ 8. Поділити на підгрупи       ║");
            Console.WriteLine("║ 0. Вихід                      ║");
            Console.WriteLine("╚═══════════════════════════════╝");
            Console.Write("Ваш вибір: ");
        }

        private void ShowDisciplines()
        {
            Console.WriteLine("══════════ ДИСЦИПЛІНИ ══════════");
            foreach (var d in _service.Disciplines)
            {
                Console.WriteLine($"\n {d.Name}  |  Група: {d.StudyGroup.Name}  |  Годин: {d.TotalHours}");

                var issues = d.GetValidationIssues();
                if (issues.Count == 0)
                    PrintSuccess("  Валідна: так");
                else
                {
                    PrintError("  Валідна: ні");
                    foreach (var issue in issues)
                        PrintError($"    - {issue}");
                }

                Console.WriteLine("  Активності:");
                foreach (var a in d.Activities)
                {
                    string teacher = a.ResponsibleTeacher != null
                        ? a.ResponsibleTeacher.ToString()
                        : "не призначено";
                    string status = StatusLabel(a.Status);
                    Console.WriteLine($"    • {a.Name,-20} {a.Hours,3} год  |  {teacher,-20}  |  {status}");

                    // Якщо це лабораторна — показати підгрупи
                    if (a is LabWork)
                    {
                        LabWork labActivity = (LabWork)a;
                        for (int i = 0; i < labActivity.SubGroups.Count; i++)
                            Console.WriteLine(
                                $"        Підгрупа {i + 1}: {labActivity.SubGroups[i].Count} студентів" +
                                (labActivity.SubGroups[i].IsValid() ? "" : " ⚠"));
                    }
                }
            }
        }

        private void ShowGroups()
        {
            Console.WriteLine("══════════ ГРУПИ ══════════");
            foreach (var g in _service.Groups)
            {
                Console.WriteLine($"\n👥 {g}  |  Студентів: {g.Students.Count}");
                foreach (var s in g.Students)
                {
                    string laptop = s.HasLaptop ? "💻" : "❌";
                    int labCount  = CountWorks(s, WorkType.LabWork);
                    int cwCount   = CountWorks(s, WorkType.CourseWork);
                    Console.WriteLine(
                        $"  {s.Id,2}. {s,-20} {laptop}  | ЛР: {labCount}  КР: {cwCount}");
                }
            }
        }

        private void SubmitWorkMenu()
        {
            Console.WriteLine("══════════ ЗДАТИ РОБОТУ ══════════");

            var student = SelectStudent();
            if (student == null) return;

            Console.WriteLine("Тип роботи:");
            Console.WriteLine("  1. Лабораторна робота");
            Console.WriteLine("  2. Курсова робота");
            Console.Write("Вибір: ");
            string typeInput = Console.ReadLine();

            WorkType type;
            if      (typeInput == "1") type = WorkType.LabWork;
            else if (typeInput == "2") type = WorkType.CourseWork;
            else { PrintError("Невірний тип."); return; }

            Console.Write("Опис роботи: ");
            string desc = Console.ReadLine();
            if (string.IsNullOrEmpty(desc)) desc = "без опису";

            student.SubmitWork(type, desc);
        }

        private void StartActivityMenu()
        {
            Console.WriteLine("══════════ ЗАПУСТИТИ АКТИВНІСТЬ ══════════");

            var discipline = SelectDiscipline();
            if (discipline == null) return;

            var activity = SelectActivity(discipline, showStatus: true);
            if (activity == null) return;

            var result = _service.StartActivity(discipline, activity);

            if (!result.IsSuccess)
            {
                PrintError($"✗ Неможливо розпочати \"{activity.Name}\".");
                PrintError($"  Причина: {result.Reason}");
                return;
            }

            // Статус змінився → ConsoleObserver вже вивів подію
            // Тепер показуємо допуск студентів

            var admissions = _service.GetAdmissionResults(discipline, activity);
            int admitted = 0;
            foreach (var (s, r) in admissions) if (r.IsSuccess) admitted++;

            if (admitted == admissions.Count)
            {
                PrintSuccess($"  Всі {admitted} студентів допущені.");
            }
            else
            {
                Console.WriteLine($"  Допущено: {admitted} з {admissions.Count}");
                foreach (var (s, r) in admissions)
                {
                    if (r.IsSuccess)
                        Console.WriteLine($"  ✓ {s}");
                    else
                        PrintError($"  ✗ {s} — {r.Reason}");
                }
            }
        }

        private void CompleteActivityMenu()
        {
            Console.WriteLine("══════════ ЗАВЕРШИТИ АКТИВНІСТЬ ══════════");

            var discipline = SelectDiscipline();
            if (discipline == null) return;

            // Показуємо тільки активності що зараз тривають
            var inProgress = new List<Activity>();
            foreach (var a in discipline.Activities)
                if (a.Status == ActivityStatus.InProgress)
                    inProgress.Add(a);

            if (inProgress.Count == 0)
            {
                PrintError("Немає активностей що зараз тривають.");
                return;
            }

            Console.WriteLine("Активності що тривають:");
            for (int i = 0; i < inProgress.Count; i++)
                Console.WriteLine($"  {i + 1}. {inProgress[i].Name}");

            Console.Write("Вибір: ");
            if (!TryReadIndex(inProgress.Count, out int idx)) return;

            var result = _service.CompleteActivity(discipline, inProgress[idx]);
            if (result.IsSuccess)
                PrintSuccess("✓ Активність завершена.");
            else
                PrintError($"✗ {result.Reason}");
        }

        private void AssignTeacherMenu()
        {
            Console.WriteLine("══════════ ПРИЗНАЧИТИ ВИКЛАДАЧА ══════════");

            var discipline = SelectDiscipline();
            if (discipline == null) return;

            var activity = SelectActivity(discipline, showStatus: false);
            if (activity == null) return;

            AssignTeacherToActivity(discipline, activity);
        }

        private void AssignTeacherToActivity(Discipline discipline, Activity activity)
        {
            if (_service.Teachers.Count == 0)
            {
                PrintError("Немає доступних викладачів.");
                return;
            }

            Console.WriteLine("\nВикладачі:");
            for (int i = 0; i < _service.Teachers.Count; i++)
            {
                var t = _service.Teachers[i];
                string status = t.IsAvailableFor(discipline)
                    ? "вільний"
                    : $"веде: {t.CurrentDiscipline?.Name}";
                Console.WriteLine($"  {i + 1}. {t} | {status}");
            }

            Console.Write("Вибір: ");
            if (!TryReadIndex(_service.Teachers.Count, out int tIdx)) return;

            var teacher = _service.Teachers[tIdx];
            var result  = _service.AssignTeacher(discipline, activity, teacher);

            if (result.IsSuccess)
                PrintSuccess($"✓ {teacher} призначений на \"{activity.Name}\".");
            else
                PrintError($"✗ {result.Reason}");
        }

        private void AddActivityMenu()
        {
            Console.WriteLine("══════════ ДОДАТИ АКТИВНІСТЬ ══════════");

            var discipline = SelectDiscipline();
            if (discipline == null) return;

            Console.WriteLine("Тип активності:");
            Console.WriteLine("  1. Лекція");
            Console.WriteLine("  2. Лабораторна робота");
            Console.WriteLine("  3. МКР");
            Console.WriteLine("  4. Екзамен");
            Console.WriteLine("  5. Курсова робота");
            Console.WriteLine("  6. Залік");
            Console.Write("Вибір: ");
            string typeCode = Console.ReadLine();

            Console.Write("Кількість годин: ");
            if (!int.TryParse(Console.ReadLine(), out int hours) || hours <= 0)
            {
                PrintError("Невірна кількість годин.");
                return;
            }

            Activity newActivity = ActivityFactory.Create(typeCode, hours);

            if (newActivity == null)
            {
                PrintError("Невідомий тип активності.");
                return;
            }

            _service.AddActivity(discipline, newActivity);

            Console.Write("\nПризначити викладача одразу? (1 - так): ");
            if (Console.ReadLine()?.Trim() == "1")
                AssignTeacherToActivity(discipline, newActivity);

            if (newActivity is LabWork)
            {
                LabWork newLabWork = (LabWork)newActivity;
                Console.Write("Поділити на підгрупи одразу? (1 - так): ");
                if (Console.ReadLine()?.Trim() == "1")
                    DoDivideSubGroups(newLabWork, discipline.StudyGroup);
            }
        }

        private void DivideSubGroupsMenu()
        {
            Console.WriteLine("══════════ ПОДІЛ НА ПІДГРУПИ ══════════");

            var discipline = SelectDiscipline();
            if (discipline == null) return;

            var labs = new List<LabWork>();
            foreach (var a in discipline.Activities)
                if (a is LabWork)
                    labs.Add((LabWork)a);

            if (labs.Count == 0)
            {
                PrintError("У цій дисципліні немає лабораторних робіт.");
                return;
            }

            Console.WriteLine("Лабораторні роботи:");
            for (int i = 0; i < labs.Count; i++)
                Console.WriteLine($"  {i + 1}. {labs[i].Name} " +
                    $"(підгруп: {labs[i].SubGroups.Count})");

            Console.Write("Вибір: ");
            if (!TryReadIndex(labs.Count, out int idx)) return;

            DoDivideSubGroups(labs[idx], discipline.StudyGroup);
        }

        private void DoDivideSubGroups(LabWork labWork, Group group)
        {
            Console.Write("На скільки підгруп поділити? (мін. 2): ");
            if (!int.TryParse(Console.ReadLine(), out int count) || count < 2)
            {
                PrintError("Мінімум 2 підгрупи.");
                return;
            }

            var result = _service.DivideLabWorkIntoSubGroups(labWork, group, count);

            if (result.IsSuccess)
                PrintSuccess($"✓ Групу поділено на {count} підгрупи.");
            else
                PrintError($"✗ {result.Reason}");
        }

        private Discipline SelectDiscipline()
        {
            Console.WriteLine("Оберіть дисципліну:");
            for (int i = 0; i < _service.Disciplines.Count; i++)
                Console.WriteLine($"  {i + 1}. {_service.Disciplines[i].Name}");

            Console.Write("Вибір: ");
            if (!TryReadIndex(_service.Disciplines.Count, out int idx))
                return null;

            return _service.Disciplines[idx];
        }

        private Activity SelectActivity(Discipline discipline, bool showStatus)
        {
            if (discipline.Activities.Count == 0)
            {
                PrintError("У цій дисципліні немає активностей.");
                return null;
            }

            Console.WriteLine("Оберіть активність:");
            for (int i = 0; i < discipline.Activities.Count; i++)
            {
                var a = discipline.Activities[i];
                string extra = showStatus ? $" [{StatusLabel(a.Status)}]" : "";
                Console.WriteLine($"  {i + 1}. {a.Name}{extra}");
            }

            Console.Write("Вибір: ");
            if (!TryReadIndex(discipline.Activities.Count, out int idx))
                return null;

            return discipline.Activities[idx];
        }

        private Student SelectStudent()
        {
            Console.WriteLine("Оберіть групу:");
            for (int i = 0; i < _service.Groups.Count; i++)
                Console.WriteLine("  " + (i + 1) + ". " + _service.Groups[i]);

            Console.Write("Вибір групи: ");
            if (!TryReadIndex(_service.Groups.Count, out int gIdx))
                return null;

            var group = _service.Groups[gIdx];

            if (group.Students.Count == 0)
            {
                PrintError("У цій групі немає студентів.");
                return null;
            }

            Console.WriteLine("Оберіть студента з групи " + group.Name + ":");
            for (int i = 0; i < group.Students.Count; i++)
            {
                var s = group.Students[i];
                string laptop = s.HasLaptop ? "є ноутбук" : "немає ноутбука";
                Console.WriteLine("  " + (i + 1) + ". " + s + " | " + laptop);
            }

            Console.Write("Вибір студента: ");
            if (!TryReadIndex(group.Students.Count, out int sIdx))
                return null;

            return group.Students[sIdx];
        }


        private bool TryReadIndex(int count, out int zeroBasedIndex)
        {
            zeroBasedIndex = -1;
            if (!int.TryParse(Console.ReadLine(), out int raw) ||
                raw < 1 || raw > count)
            {
                PrintError("Невірний вибір.");
                return false;
            }
            zeroBasedIndex = raw - 1;
            return true;
        }

        private static void PrintSuccess(string msg)
        {
            Console.WriteLine(msg);
        }

        private static void PrintError(string msg)
        {
            Console.WriteLine(msg);
        }

        private static string StatusLabel(ActivityStatus s)
        {
            switch (s)
            {
                case ActivityStatus.NotStarted: return "Не розпочата";
                case ActivityStatus.InProgress: return "Триває";
                case ActivityStatus.Completed:  return "Завершена";
                default:                        return s.ToString();
            }
        }

        private static int CountWorks(Student s, WorkType type)
        {
            int count = 0;
            foreach (var w in s.SubmittedWorks)
                if (w.Type == type) count++;
            return count;
        }
    }
}
