using BLL;
using BLL.Entities;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace PL
{
    class Program
    {
        private static StudentService studentService;
        private static GroupService groupService;
        private static DormitoryService dormitoryService;
        private static SearchService searchService; 

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            InitializeServices();

            bool exit = false;
            while (!exit)
            {
                try
                {
                    ShowMainMenu();
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            StudentMenu();
                            break;
                        case "2":
                            GroupMenu();
                            break;
                        case "3":
                            DormitoryMenu();
                            break;
                        case "4":
                            SearchMenu();
                            break;
                        case "0":
                            exit = true;
                            Console.WriteLine("\nДо побачення!");
                            break;
                        default:
                            Console.WriteLine("\n Невірний вибір!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nПомилка: {ex.Message}");
                }

                if (!exit)
                {
                    Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                    Console.ReadKey();
                }
            }
        }

        static void InitializeServices()
        {
            studentService = new StudentService();
            groupService = new GroupService(studentService);
            dormitoryService = new DormitoryService(studentService);
            searchService = new SearchService(studentService, groupService, dormitoryService);
        }

        static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("---------ЕЛЕКТРОННИЙ ДЕКАНАТ--------\n");
            Console.WriteLine("\nГОЛОВНЕ МЕНЮ:\n");
            Console.WriteLine("1. Управління студентами");
            Console.WriteLine("2. Управління групами");
            Console.WriteLine("3. Управління гуртожитком");
            Console.WriteLine("4. Пошук");
            Console.WriteLine("0. Вихід");
            Console.Write("\nВаш вибір(потрібно натиснути цифру яка стоїть перед пунктом): ");
        }

        static void StudentMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=====УПРАВЛІННЯ СТУДЕНТАМИ=====");
                Console.WriteLine("1. Додати студента");
                Console.WriteLine("2. Видалити студента");
                Console.WriteLine("3. Змінити дані студента");
                Console.WriteLine("4. Переглянути всіх студентів");
                Console.WriteLine("5. Переглянути дані студента");
                Console.WriteLine("0. Назад");
                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddStudent();
                            break;
                        case "2":
                            DeleteStudent();
                            break;
                        case "3":
                            UpdateStudent();
                            break;
                        case "4":
                            ViewAllStudents();
                            break;
                        case "5":
                            ViewStudent();
                            break;
                        case "0":
                            back = true;
                            break;
                        default:
                            Console.WriteLine("\nНевірний вибір!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nПомилка: {ex.Message}");
                }

                if (!back && choice != "0")
                {
                    Console.WriteLine("\nНатисніть будь-яку клавішу...");
                    Console.ReadKey();
                }
            }
        }

        static void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("=== ДОДАТИ СТУДЕНТА ===\n");

            Console.Write("ID студента (формат: AB123456): ");
            string id = Console.ReadLine().ToUpper();

            Console.Write("Прізвище: ");
            string lastName = Console.ReadLine();

            Console.Write("Ім'я: ");
            string firstName = Console.ReadLine();

            Console.Write("Курс (1-6): ");
            if (!int.TryParse(Console.ReadLine(), out int course))
            {
                throw new StudentInvalidDataException("Некоректний формат курсу");
            }

            studentService.AddStudent(id, lastName, firstName, course);
            Console.WriteLine("\n:) Студента успішно додано!");
        }

        static void DeleteStudent()
        {
            Console.Clear();
            Console.WriteLine("=== ВИДАЛИТИ СТУДЕНТА ===\n");

            Console.Write("ID студента: ");
            string id = Console.ReadLine().ToUpper();

            var student = studentService.GetStudentByID(id);
            Console.WriteLine($"\nВидалити студента: {student.FirstName} {student.LastName} ({student.StudentID})");
            Console.Write("Ви впевнені? (так/ні): ");

            if (Console.ReadLine().ToLower() == "так")
            {
                studentService.DeleteStudent(id);
                Console.WriteLine("\nСтудента видалено!");
            }
            else
            {
                Console.WriteLine("\nЛишаємо студента");
            }
        }

        static void UpdateStudent()
        {
            Console.Clear();
            Console.WriteLine("=== ЗМІНИТИ ДАНІ СТУДЕНТА ===\n");

            Console.Write("ID студента: ");
            string id = Console.ReadLine().ToUpper();

            var student = studentService.GetStudentByID(id);
            Console.WriteLine("\nПоточні дані:");
            ShowStudent(student);

            Console.Write($"Прізвище [{student.LastName}]: ");
            string lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName)) lastName = student.LastName;

            Console.Write($"Ім'я [{student.FirstName}]: ");
            string firstName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName)) firstName = student.FirstName;

            Console.Write($"Курс [{student.Course}]: ");
            string courseInput = Console.ReadLine();
            int course = student.Course;
            if (!string.IsNullOrWhiteSpace(courseInput))
            {
                if (!int.TryParse(courseInput, out course))
                    throw new StudentInvalidDataException("Некоректний формат курсу");
            }

            studentService.UpdateStudent(id, lastName, firstName, course);
            Console.WriteLine("\n:) Дані оновлено!");
        }

        static void ViewAllStudents()
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК СТУДЕНТІВ ===\n");

            var students = studentService.GetAllStudents();

            if (students.Count == 0)
            {
                Console.WriteLine("Список порожній");
                return;
            }

            Console.WriteLine($"Всього студентів: {students.Count}\n");
            foreach (var student in students)
            {
                ShowStudent(student);
            }
        }

        static void ViewStudent()
        {
            Console.Clear();
            Console.WriteLine("=== ДАНІ СТУДЕНТА ===\n");

            Console.Write("ID студента: ");
            string id = Console.ReadLine().ToUpper();

            var student = studentService.GetStudentByID(id);
            ShowStudent(student);

            var allGroups = groupService.GetAllGroups();
            var studentGroup = allGroups.FirstOrDefault(g => g.StudentIDs.Contains(student.StudentID));

            var allDorms = dormitoryService.GetAllDormitories();
            var studentDormitory = allDorms.FirstOrDefault(d => d.Rooms.Any(r => r.OccupantIDs.Contains(student.StudentID)));
            var studentRoom = studentDormitory?.Rooms.FirstOrDefault(r => r.OccupantIDs.Contains(student.StudentID));

            Console.WriteLine($"Група: {(studentGroup != null ? $"{studentGroup.Name} (ID: {studentGroup.Id})" : "не призначена")}");
            Console.WriteLine($"Гуртожиток: {(studentRoom != null ? $"{studentDormitory.Name}, Кімната {studentRoom.RoomNumber}" : "не поселений")}");
        }

        static void GroupMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=====УПРАВЛІННЯ ГРУПАМИ=====");
                Console.WriteLine("1. Додати групу");
                Console.WriteLine("2. Видалити групу");
                Console.WriteLine("3. Змінити дані групи");
                Console.WriteLine("4. Переглянути всі групи");
                Console.WriteLine("5. Переглянути дані групи та студентів");
                Console.WriteLine("6. Додати студента до групи");
                Console.WriteLine("7. Видалити студента з групи");
                Console.WriteLine("0. Назад");
                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddGroup();
                            break;
                        case "2":
                            DeleteGroup();
                            break;
                        case "3":
                            UpdateGroup();
                            break;
                        case "4":
                            ViewAllGroups();
                            break;
                        case "5":
                            ViewGroup(); 
                            break;
                        case "6":
                            AddStudentToGroup();
                            break;
                        case "7":
                            RemoveStudentFromGroup();
                            break;
                        case "0":
                            back = true;
                            break;
                        default:
                            Console.WriteLine("\nНевірний вибір!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nПомилка: {ex.Message}");
                }

                if (!back && choice != "0")
                {
                    Console.WriteLine("\nНатисніть будь-яку клавішу...");
                    Console.ReadKey();
                }
            }
        }

        static void AddGroup()
        {
            Console.Clear();
            Console.WriteLine("=== ДОДАТИ ГРУПУ ===\n");

            Console.Write("Назва групи (наприклад: Б-121-24-3-ПІ): ");
            string name = Console.ReadLine();

            Console.Write("Курс (1-6): ");
            if (!int.TryParse(Console.ReadLine(), out int course))
            {
                throw new StudentInvalidDataException("Некоректний формат курсу");
            }

            Console.Write("Спеціальність: ");
            string specialty = Console.ReadLine();

            groupService.AddGroup(name, course, specialty);
            Console.WriteLine("\n:) Групу успішно додано!");
        }

        static void DeleteGroup()
        {
            Console.Clear();
            Console.WriteLine("=== ВИДАЛИТИ ГРУПУ ===\n");

            ViewAllGroups();
            Console.Write("\nID групи: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                throw new StudentInvalidDataException("Некоректний формат ID групи");
            }

            var group = groupService.GetGroupById(id);
            Console.WriteLine($"\nВидалити групу: {group.Name}");
            Console.Write("Ви впевнені? (так/ні): ");

            if (Console.ReadLine().ToLower() == "так")
            {
                groupService.DeleteGroup(id);
                Console.WriteLine("\nГрупу видалено!");
            }
        }

        static void UpdateGroup()
        {
            Console.Clear();
            Console.WriteLine("=== ЗМІНИТИ ДАНІ ГРУПИ ===\n");

            ViewAllGroups();
            Console.Write("\nID групи: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                throw new StudentInvalidDataException("Некоректний формат ID групи");
            }

            var group = groupService.GetGroupById(id);
            Console.WriteLine("\nПоточні дані: ");
            ShowGroup(group);

            Console.Write($"Назва [{group.Name}]: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = group.Name;

            Console.Write($"Курс [{group.Course}]: ");
            string courseInput = Console.ReadLine();
            int course = group.Course;
            if (!string.IsNullOrWhiteSpace(courseInput))
            {
                if (!int.TryParse(courseInput, out course))
                    throw new StudentInvalidDataException("Некоректний формат курсу");
            }

            Console.Write($"Спеціальність [{group.Specialty}]: ");
            string specialty = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(specialty)) specialty = group.Specialty;

            groupService.UpdateGroup(id, name, course, specialty);
            Console.WriteLine("\n:) Дані оновлено!");
        }

        static void ViewAllGroups()
        {
            Console.WriteLine("=== СПИСОК ГРУП ===\n");

            var groups = groupService.GetAllGroups();

            if (groups.Count == 0)
            {
                Console.WriteLine("Список порожній");
                return;
            }

            foreach (var group in groups)
            {
                ShowGroup(group);
            }
        }

        static void ViewGroup()
        {
            Console.Clear();
            ViewAllGroups();
            Console.Write("\nID групи: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                throw new StudentInvalidDataException("Некоректний формат ID групи");
            }

            var group = groupService.GetGroupById(id);
            var students = searchService.SearchByGroupId(id);

            Console.WriteLine($"\nСтуденти групи {group.Name} (ID: {group.Id}):");
            if (students.Count == 0)
            {
                Console.WriteLine("(немає студентів)");
            }
            else
            {
                foreach (var student in students)
                {
                    ShowStudent(student);
                }
            }
        }

        static void AddStudentToGroup()
        {
            Console.Clear();
            Console.WriteLine("=== ДОДАТИ СТУДЕНТА ДО ГРУПИ ===\n");

            ViewAllGroups();
            Console.Write("\nID групи: ");
            if (!int.TryParse(Console.ReadLine(), out int groupId))
            {
                throw new StudentInvalidDataException("Некоректний формат ID групи");
            }

            Console.Write("ID студента: ");
            string studentId = Console.ReadLine().ToUpper();

            groupService.AddStudentToGroup(groupId, studentId);
            Console.WriteLine("\n:) Студента додано до групи!");
        }

        static void RemoveStudentFromGroup()
        {
            Console.Clear();
            Console.WriteLine("=== ВИДАЛИТИ СТУДЕНТА З ГРУПИ ===\n");

            ViewAllGroups();
            Console.Write("\nID групи: ");
            if (!int.TryParse(Console.ReadLine(), out int groupId))
            {
                throw new StudentInvalidDataException("Некоректний формат ID групи");
            }

            Console.Write("ID студента: ");
            string studentId = Console.ReadLine().ToUpper();

            groupService.RemoveStudentFromGroup(groupId, studentId);
            Console.WriteLine("\nСтудента видалено з групи!");
        }

        static void DormitoryMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=====УПРАВЛІННЯ ГУРТОЖИТКОМ=====");
                Console.WriteLine("1. Додати гуртожиток");
                Console.WriteLine("2. Додати кімнату");
                Console.WriteLine("3. Поселити студента");
                Console.WriteLine("4. Виписати студента");
                Console.WriteLine("5. Переглянути гуртожитки та кімнати");
                Console.WriteLine("6. Переглянути вільні кімнати");
                Console.WriteLine("0. Назад");
                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddDormitory();
                            break;
                        case "2":
                            AddRoom();
                            break;
                        case "3":
                            CheckInStudent();
                            break;
                        case "4":
                            CheckOutStudent();
                            break;
                        case "5":
                            ViewDormitories();
                            break;
                        case "6":
                            ViewAvailableRooms();
                            break;
                        case "0":
                            back = true;
                            break;
                        default:
                            Console.WriteLine("\nНевірний вибір!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nПомилка: {ex.Message}");
                }

                if (!back && choice != "0")
                {
                    Console.WriteLine("\nНатисніть будь-яку клавішу...");
                    Console.ReadKey();
                }
            }
        }

        static void AddDormitory()
        {
            Console.Clear();
            Console.WriteLine("=== ДОДАТИ ГУРТОЖИТОК ===\n");

            Console.Write("Назва гуртожитку: ");
            string name = Console.ReadLine();

            Console.Write("Адреса: ");
            string address = Console.ReadLine();

            dormitoryService.AddDormitory(name, address);
            Console.WriteLine("\n:) Гуртожиток додано!");
        }

        static void AddRoom()
        {
            Console.Clear();
            Console.WriteLine("=== ДОДАТИ КІМНАТУ ===\n");

            ViewDormitories();
            Console.Write("\nID гуртожитку: ");
            if (!int.TryParse(Console.ReadLine(), out int dormId))
            {
                throw new StudentInvalidDataException("Некоректний формат ID гуртожитку");
            }

            Console.Write("Номер кімнати: ");
            string roomNumber = Console.ReadLine();

            Console.Write("Максимальна кількість студентів (1-4): ");
            if (!int.TryParse(Console.ReadLine(), out int capacity))
            {
                throw new StudentInvalidDataException("Некоректний формат місткості");
            }

            dormitoryService.AddRoom(dormId, roomNumber, capacity);
            Console.WriteLine("\n:) Кімнату додано!");
        }

        static void CheckInStudent()
        {
            Console.Clear();
            Console.WriteLine("=== ПОСЕЛИТИ СТУДЕНТА ===\n");

            ViewDormitories();
            Console.Write("\nID гуртожитку: ");
            if (!int.TryParse(Console.ReadLine(), out int dormId))
            {
                throw new StudentInvalidDataException("Некоректний формат ID гуртожитку");
            }

            Console.Write("Номер кімнати: ");
            string roomNumber = Console.ReadLine();

            Console.Write("ID студента: ");
            string studentId = Console.ReadLine().ToUpper();

            dormitoryService.CheckInStudent(dormId, roomNumber, studentId);
            Console.WriteLine("\n:) Студента поселено!");
        }

        static void CheckOutStudent()
        {
            Console.Clear();
            Console.WriteLine("=== ВИПИСАТИ СТУДЕНТА ===\n");

            ViewDormitories();
            Console.Write("\nID гуртожитку: ");
            if (!int.TryParse(Console.ReadLine(), out int dormId))
            {
                throw new StudentInvalidDataException("Некоректний формат ID гуртожитку");
            }

            Console.Write("ID студента: ");
            string studentId = Console.ReadLine().ToUpper();

            dormitoryService.CheckOutStudent(dormId, studentId);
            Console.WriteLine("\nСтудента виписано!");
        }

        static void ViewDormitories()
        {
            Console.WriteLine("=== СПИСОК ГУРТОЖИТКІВ ===\n");

            var dorms = dormitoryService.GetAllDormitories();

            if (dorms.Count == 0)
            {
                Console.WriteLine("Список порожній");
                return;
            }

            foreach (var dorm in dorms)
            {
                ShowDormitory(dorm);
                foreach (var room in dorm.Rooms)
                {
                    Console.Write(" - ");
                    ShowRoom(room);
                }
            }
        }

        static void ViewAvailableRooms()
        {
            Console.Clear();
            Console.WriteLine("=== ВІЛЬНІ КІМНАТИ ===\n");

            ViewDormitories();
            Console.Write("\nID гуртожитку: ");
            if (!int.TryParse(Console.ReadLine(), out int dormId))
            {
                throw new StudentInvalidDataException("Некоректний формат ID гуртожитку");
            }

            var rooms = dormitoryService.GetAvailableRooms(dormId);

            if (rooms.Count == 0)
            {
                Console.WriteLine("\nНемає вільних кімнат");
                return;
            }

            Console.WriteLine("\nВільні кімнати:");
            foreach (var room in rooms)
            {
                Console.Write(" - ");
                ShowRoom(room);
            }
        }

        static void SearchMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=====ПОШУК=====");
                Console.WriteLine("1. Пошук за ім'ям/прізвищем/ID");
                Console.WriteLine("2. Пошук студентів групи");
                Console.WriteLine("3. Пошук студентів у гуртожитку");
                Console.WriteLine("0. Назад");
                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            SearchByName();
                            break;
                        case "2":
                            SearchByGroup();
                            break;
                        case "3":
                            SearchInDormitory();
                            break;
                        case "0":
                            back = true;
                            break;
                        default:
                            Console.WriteLine("\nНевірний вибір!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nПомилка: {ex.Message}");
                }

                if (!back && choice != "0")
                {
                    Console.WriteLine("\nНатисніть будь-яку клавішу...");
                    Console.ReadKey();
                }
            }
        }

        static void SearchByName()
        {
            Console.Clear();
            Console.WriteLine("=== ПОШУК ЗА ІМ'ЯМ/ПРІЗВИЩЕМ/ID ===\n");

            Console.Write("Введіть ім'я, прізвище або ID: ");
            string searchTerm = Console.ReadLine();

            var results = searchService.SearchByName(searchTerm);

            if (results.Count == 0)
            {
                Console.WriteLine("\nНічого не знайдено");
                return;
            }

            Console.WriteLine($"\nЗнайдено студентів: {results.Count}\n");
            foreach (var student in results)
            {
                ShowStudent(student);
            }
        }

        static void SearchByGroup()
        {
            Console.Clear();
            Console.WriteLine("=== ПОШУК СТУДЕНТІВ ГРУПИ ===\n");

            ViewAllGroups();
            Console.Write("\nID групи: ");
            if (!int.TryParse(Console.ReadLine(), out int groupId))
            {
                throw new StudentInvalidDataException("Некоректний формат ID групи");
            }

            var results = searchService.SearchByGroupId(groupId);

            if (results.Count == 0)
            {
                Console.WriteLine("\nУ групі немає студентів");
                return;
            }

            Console.WriteLine($"\nСтудентів у групі: {results.Count}\n");
            foreach (var student in results)
            {
                ShowStudent(student);
            }
        }

        static void SearchInDormitory()
        {
            Console.Clear();
            Console.WriteLine("=== ПОШУК У ГУРТОЖИТКУ ===\n");

            ViewDormitories();
            Console.Write("\nID гуртожитку: ");
            if (!int.TryParse(Console.ReadLine(), out int dormId))
            {
                throw new StudentInvalidDataException("Некоректний формат ID гуртожитку");
            }

            var results = searchService.SearchByDormitory(dormId);

            if (results.Count == 0)
            {
                Console.WriteLine("\nУ гуртожитку немає студентів");
                return;
            }

            Console.WriteLine($"\nСтудентів у гуртожитку: {results.Count}\n");
            foreach (var student in results)
            {
                ShowStudent(student);
            }
        }

        static void ShowStudent(Student student)
        {
            Console.WriteLine($"ID: {student.StudentID}, Прізвище: {student.LastName}, Ім'я: {student.FirstName}, Курс: {student.Course}");
        }

        static void ShowGroup(Group group)
        {
            Console.WriteLine($" - ID: {group.Id}, Назва: {group.Name}, Спеціальність: {group.Specialty}, Курс: {group.Course}, Студентів: {group.GetStudentCount()}");
        }

        static void ShowDormitory(Dormitory dormitory)
        {
            Console.WriteLine($"{dormitory.Name} ({dormitory.Address}) - ID: {dormitory.Id}, Кімнат: {dormitory.Rooms.Count}, Мешканців: {dormitory.GetTotalOccupants()}/{dormitory.GetTotalCapacity()}");
        }

        static void ShowRoom(DormitoryRoom room)
        {
            Console.WriteLine($"Кімната {room.RoomNumber}: {room.OccupantIDs.Count}/{room.MaxCapacity} (Вільно: {room.GetAvailableSpaces()})");
        }
    }
}
