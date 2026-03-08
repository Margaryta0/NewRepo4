using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySimulation
{
    class Program
    {
        static List<Student> allStudents = new List<Student>();
        static List<Teacher> allTeachers = new List<Teacher>();
        static List<Group> allGroups = new List<Group>();
        static List<Discipline> allDisciplines = new List<Discipline>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            SeedData(); 

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n╔══════════════════════════════╗");
                Console.WriteLine("║   Університетська система    ║");
                Console.WriteLine("╠══════════════════════════════╣");
                Console.WriteLine("║ 1. Переглянути дисципліни    ║");
                Console.WriteLine("║ 2. Переглянути групи         ║");
                Console.WriteLine("║ 3. Здати роботу              ║");
                Console.WriteLine("║ 4. Запустити активність      ║");
                Console.WriteLine("║ 5. Призначити викладача      ║");
                Console.WriteLine("║ 6. Додати активність         ║");
                Console.WriteLine("║ 7. Завершити активність      ║");
                Console.WriteLine("║ 0. Вихід                     ║");
                Console.WriteLine("╚══════════════════════════════╝");
                Console.Write("Ваш вибір: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1": ShowDisciplines(); break;
                    case "2": ShowGroups(); break;
                    case "3": SubmitWorkMenu(); break;
                    case "4": StartActivityMenu(); break;
                    case "5": AssignTeacherMenu(); break;
                    case "6": AddActivityMenu(); break;
                    case "7": CompleteActivityMenu(); break;
                    case "0": running = false; break;
                    default:
                        Console.WriteLine("Невідома команда. Спробуйте ще раз.");
                        break;
                }
            }
        }


        static void SeedData()
        {
            // Студенти
            for (int i = 1; i <= 15; i++)
            {
                var s = new Student(i, "Студент" + i, "Прізвище" + i, hasLaptop: true);
                s.WorkSubmitted += OnWorkSubmitted;
                allStudents.Add(s);
            }

            // Група
            var group1 = new Group(1, "Б-121-24-3-ПІ", courseYear: 1);
            foreach (var s in allStudents)
                group1.AddStudent(s);
            allGroups.Add(group1);

            // Викладачі
            var t1 = new Teacher(1, "Олег", "Сич");
            var t2 = new Teacher(2, "Марія", "Коваль");
            t1.AssignmentChanged += OnTeacherAssignmentChanged;
            t2.AssignmentChanged += OnTeacherAssignmentChanged;
            allTeachers.Add(t1);
            allTeachers.Add(t2);

            var prog = new Discipline("Основи програмування", group1, new int[] { 1 });
            prog.ActivityChanged += OnActivityChanged;

            var lec = new Lecture(32);
            var lab = new LabWork(20);
            var mkr = new ModularTest(4);
            var crd = new Credit(8);
            foreach (var a in new Activity[] { lec, lab, mkr, crd })
                a.StatusChanged += OnActivityStatusChanged;

            prog.AddActivity(lec);
            prog.AddActivity(lab);
            prog.AddActivity(mkr);
            prog.AddActivity(crd);

            string divideError = lab.DivideIntoSubGroups(group1, 2, OnSubGroupSizeChanged);
            if (divideError != null)
                Console.WriteLine("Помилка поділу: " + divideError);


            prog.AssignTeacher(lec, t1);  
            prog.AssignTeacher(lab, t2);
            prog.AssignTeacher(mkr, t1);

            allDisciplines.Add(prog);

            var oop = new Discipline("ООП", group1, new int[] { 1, 2 });
            oop.ActivityChanged += OnActivityChanged;
            allDisciplines.Add(oop);
        }


        static void ShowDisciplines()
        {
            Console.WriteLine("=== Дисципліни ===");
            foreach (var d in allDisciplines)
            {
                Console.WriteLine("\nДисципліна: " + d.Name);
                Console.WriteLine("  Група: " + d.StudyGroup.Name);
                Console.WriteLine("  Годин: " + d.TotalHours);
                string validationReport = d.GetValidationReport();
                if (validationReport == null)
                    Console.WriteLine("  Валідна: так");
                else
                    Console.WriteLine("  Валідна: ні" + validationReport);
                Console.WriteLine("  Активності:");
                foreach (var a in d.Activities)
                {
                    string teacher = a.ResponsibleTeacher != null
                        ? a.ResponsibleTeacher.ToString()
                        : "не призначено";
                    Console.WriteLine("    - " + a.Name +
                        " (" + a.Hours + " год)" +
                        " | " + teacher +
                        " | " + a.Status);  
                }
            }
        }

        static void ShowGroups()
        {
            Console.WriteLine("=== Групи ===");
            foreach (var g in allGroups)
            {
                Console.WriteLine("\n" + g);
                Console.WriteLine("  Студентів: " + g.Students.Count);
                foreach (var s in g.Students)
                {
                    string laptop = s.HasLaptop ? "є ноутбук" : "немає ноутбука";
                    Console.WriteLine("    " + s.Id + ". " + s + " | " + laptop);
                }
            }
        }

        static void SubmitWorkMenu()
        {
            Console.WriteLine("=== Здати роботу ===");

            Student student = SelectStudent();
            if (student == null) return;

            Console.WriteLine("Тип роботи:");
            Console.WriteLine("  1. Лабораторна робота");
            Console.WriteLine("  2. Курсова робота");
            Console.Write("Вибір: ");
            string typeInput = Console.ReadLine();

            WorkType type;
            if (typeInput == "1") type = WorkType.LabWork;
            else if (typeInput == "2") type = WorkType.CourseWork;
            else { Console.WriteLine("Невірний тип."); return; }

            Console.Write("Опис роботи: ");
            string desc = Console.ReadLine();

            student.SubmitWork(type, desc);
            // Повідомлення виводиться через подію OnWorkSubmitted
        }

        static void StartActivityMenu()
        {
            Console.WriteLine("=== Запустити активність ===");

            Discipline discipline = SelectDiscipline();
            if (discipline == null) return;

            if (discipline.Activities.Count == 0)
            {
                Console.WriteLine("У цій дисципліні немає активностей.");
                return;
            }

            Console.WriteLine("Оберіть активність:");
            for (int i = 0; i < discipline.Activities.Count; i++)
            {
                Activity a = discipline.Activities[i];
                Console.WriteLine("  " + (i + 1) + ". " + a.Name +
                    " [" + a.Status + "]");
            }

            Console.Write("Вибір: ");
            int idx;
            if (!int.TryParse(Console.ReadLine(), out idx) ||
                idx < 1 || idx > discipline.Activities.Count)
            {
                Console.WriteLine("Невірний вибір.");
                return;
            }

            Activity activity = discipline.Activities[idx - 1];
            string blockReason = discipline.StartActivity(activity);
            // Подія OnActivityStatusChanged спрацює автоматично

            if (blockReason != null)
            {
                Console.WriteLine("\n✗ Неможливо розпочати \"" + activity.Name + "\".");
                Console.WriteLine("  Причина: " + blockReason);
                return;
            }

            Console.WriteLine("\n✓ Активність \"" + activity.Name + "\" розпочата.");

            List<Student> admitted = discipline.GetAdmittedStudents(activity);
            List<Student> all = new List<Student>(discipline.StudyGroup.Students);

            if (admitted.Count == all.Count)
            {
                Console.WriteLine("Всі " + all.Count + " студентів допущені.");
            }
            else
            {
                Console.WriteLine("Допущено: " + admitted.Count + " з " + all.Count);
                foreach (var student in all)
                {
                    bool isAdmitted = admitted.Contains(student);
                    if (isAdmitted)
                    {
                        Console.WriteLine("  ✓ " + student);
                    }
                    else
                    {
                        string reason = "";
                        if (activity is ModularTest)
                            reason = student.GetAdmissionBlockReason(WorkType.LabWork);
                        else if (activity is Exam)
                        {
                            var reasons = new List<string>();
                            string r1 = student.GetAdmissionBlockReason(WorkType.LabWork);
                            string r2 = student.GetAdmissionBlockReason(WorkType.CourseWork);
                            if (r1 != null) reasons.Add(r1);
                            if (r2 != null) reasons.Add(r2);
                            reason = string.Join(", ", reasons.ToArray());
                        }
                        Console.WriteLine("  ✗ " + student + " — " + reason);
                    }
                }
            }
        }

        static void CompleteActivityMenu()
        {
            Console.WriteLine("=== Завершити активність ===");

            Discipline discipline = SelectDiscipline();
            if (discipline == null) return;

            var inProgress = new List<Activity>();
            foreach (var a in discipline.Activities)
                if (a.Status == ActivityStatus.InProgress)
                    inProgress.Add(a);

            if (inProgress.Count == 0)
            {
                Console.WriteLine("Немає активностей що зараз тривають.");
                return;
            }

            Console.WriteLine("Оберіть активність для завершення:");
            for (int i = 0; i < inProgress.Count; i++)
                Console.WriteLine("  " + (i + 1) + ". " + inProgress[i].Name);

            Console.Write("Вибір: ");
            int idx;
            if (!int.TryParse(Console.ReadLine(), out idx) ||
                idx < 1 || idx > inProgress.Count)
            {
                Console.WriteLine("Невірний вибір.");
                return;
            }

            string reason = discipline.CompleteActivity(inProgress[idx - 1]);

            if (reason == null)
                Console.WriteLine("✓ Активність завершена.");
            else
                Console.WriteLine("✗ " + reason);
        }

        static void AssignTeacherMenu()
        {
            Console.WriteLine("=== Призначити викладача ===");

            Discipline discipline = SelectDiscipline();
            if (discipline == null) return;

            if (discipline.Activities.Count == 0)
            {
                Console.WriteLine("Спочатку додайте активності.");
                return;
            }

            Console.WriteLine("Оберіть активність:");
            for (int i = 0; i < discipline.Activities.Count; i++)
            {
                string current = discipline.Activities[i].ResponsibleTeacher != null
                    ? " (зараз: " + discipline.Activities[i].ResponsibleTeacher + ")"
                    : " (не призначено)";
                Console.WriteLine("  " + (i + 1) + ". " +
                    discipline.Activities[i].Name + current);
            }

            Console.Write("Вибір активності: ");
            int aIdx;
            if (!int.TryParse(Console.ReadLine(), out aIdx) ||
                aIdx < 1 || aIdx > discipline.Activities.Count)
            {
                Console.WriteLine("Невірний вибір.");
                return;
            }

            AssignTeacherToActivity(discipline, discipline.Activities[aIdx - 1]);
        }

        static void AddActivityMenu()
        {
            Console.WriteLine("=== Додати активність ===");

            Discipline discipline = SelectDiscipline();
            if (discipline == null) return;

            Console.WriteLine("Тип активності:");
            Console.WriteLine("  1. Лекція");
            Console.WriteLine("  2. Лабораторна робота");
            Console.WriteLine("  3. МКР");
            Console.WriteLine("  4. Екзамен");
            Console.WriteLine("  5. Курсова робота");
            Console.Write("Вибір: ");
            string typeInput = Console.ReadLine();

            Console.Write("Кількість годин: ");
            int hours;
            if (!int.TryParse(Console.ReadLine(), out hours) || hours <= 0)
            {
                Console.WriteLine("Невірна кількість годин.");
                return;
            }

            Activity newActivity;
            switch (typeInput)
            {
                case "1": newActivity = new Lecture(hours); break;
                case "2": newActivity = new LabWork(hours); break;
                case "3": newActivity = new ModularTest(hours); break;
                case "4": newActivity = new Exam(hours); break;
                case "5": newActivity = new CourseWork(hours); break;
                default: Console.WriteLine("Невірний тип."); return;
            }

            discipline.AddActivity(newActivity);

            if (!(newActivity is Credit))
            {
                Console.WriteLine("\nБажаєте одразу призначити викладача? (1 - так, інше - пропустити)");
                if (Console.ReadLine() == "1")
                    AssignTeacherToActivity(discipline, newActivity);
            }

            // Якщо це лабораторна — пропонуємо одразу додати підгрупи
            if (newActivity is LabWork)
            {
                Console.WriteLine("Бажаєте одразу поділити групу на підгрупи? (1 - так)");
                if (Console.ReadLine() == "1")
                {
                    Console.Write("На скільки підгруп поділити? (мін. 2): ");
                    int sgCount;
                    if (!int.TryParse(Console.ReadLine(), out sgCount) || sgCount < 2)
                    {
                        Console.WriteLine("Мінімум 2 підгрупи.");
                    }
                    else
                    {
                        string divError = ((LabWork)newActivity).DivideIntoSubGroups(
                            discipline.StudyGroup, sgCount, OnSubGroupSizeChanged);

                        if (divError != null)
                            Console.WriteLine("✗ Неможливо поділити: " + divError);
                        else
                            Console.WriteLine("✓ Групу поділено на " + sgCount + " підгрупи.");
                    }
                }
            }
        }

        static void AssignTeacherToActivity(Discipline discipline, Activity activity)
        {
            if (allTeachers.Count == 0)
            {
                Console.WriteLine("Немає доступних викладачів.");
                return;
            }

            Console.WriteLine("\nОберіть викладача:");
            for (int i = 0; i < allTeachers.Count; i++)
            {
                string status = allTeachers[i].IsAvailableFor(discipline)
                    ? "вільний"
                    : "веде: " + allTeachers[i].CurrentDiscipline.Name;
                Console.WriteLine("  " + (i + 1) + ". " + allTeachers[i] + " | " + status);
            }

            Console.Write("Вибір: ");
            int tIdx;
            if (!int.TryParse(Console.ReadLine(), out tIdx) ||
                tIdx < 1 || tIdx > allTeachers.Count)
            {
                Console.WriteLine("Невірний вибір — викладача не призначено.");
                return;
            }

            Teacher teacher = allTeachers[tIdx - 1];
            string reason = discipline.AssignTeacher(activity, teacher);

            if (reason == null)
                Console.WriteLine("✓ " + teacher + " призначений на \"" + activity.Name + "\".");
            else
                Console.WriteLine("✗ " + reason);
        }

        static Student SelectStudent()
        {
            Console.WriteLine("Оберіть студента:");
            for (int i = 0; i < allStudents.Count; i++)
                Console.WriteLine("  " + (i + 1) + ". " + allStudents[i]);

            Console.Write("Вибір: ");
            int idx;
            if (!int.TryParse(Console.ReadLine(), out idx) || idx < 1 || idx > allStudents.Count)
            {
                Console.WriteLine("Невірний вибір.");
                return null;
            }
            return allStudents[idx - 1];
        }

        static Discipline SelectDiscipline()
        {
            Console.WriteLine("Оберіть дисципліну:");
            for (int i = 0; i < allDisciplines.Count; i++)
                Console.WriteLine("  " + (i + 1) + ". " + allDisciplines[i].Name);

            Console.Write("Вибір: ");
            int idx;
            if (!int.TryParse(Console.ReadLine(), out idx) || idx < 1 || idx > allDisciplines.Count)
            {
                Console.WriteLine("Невірний вибір.");
                return null;
            }
            return allDisciplines[idx - 1];
        }

        //Обробники подій

        static void OnWorkSubmitted(object sender, SubmittedWork work)
        {
            Console.WriteLine("[ПОДІЯ] " + sender + " здав: " + work.Type + " — " + work.Description);
        }

        static void OnSubGroupSizeChanged(object sender, EventArgs e)
        {
            SubGroup sg = (SubGroup)sender;
            Console.WriteLine("[ПОДІЯ] Підгрупа змінила розмір: " + sg.Count + " студентів");
            if (!sg.IsValid())
                Console.WriteLine("[УВАГА] Підгрупа замала — лабораторні заблоковані!");
        }

        static void OnTeacherAssignmentChanged(object sender, EventArgs e)
        {
            Console.WriteLine("[ПОДІЯ] Розклад викладача оновлено: " + sender);
        }

        static void OnActivityChanged(object sender, EventArgs e)
        {
            Discipline d = (Discipline)sender;
            Console.WriteLine("[ПОДІЯ] Активності дисципліни \"" + d.Name + "\" змінено. Годин: " + d.TotalHours);
        }

        static void OnActivityStatusChanged(object sender, EventArgs e)
        {
            Activity a = (Activity)sender;
            Console.WriteLine("[ПОДІЯ] Активність \"" + a.Name +
                "\" змінила статус: " + a.Status);
        }
    }
}
