using System.Collections.Generic;
using UniversitySimulation.Domain;

namespace UniversitySimulation.Services
{
    public static class DataSeeder
    {
        public static void Seed(
            List<Student> students,
            List<Teacher> teachers,
            List<Group> groups,
            List<Discipline> disciplines,
            DisciplineService service)
        {
            for (int i = 1; i <= 14; i++)
            {
                var s = new Student(i, "Студент" + i, "Прізвище" + i, hasLaptop: true);
                students.Add(s);
                service.SubscribeToStudent(s);
            }
            var noLaptop = new Student(15, "Студент15", "Прізвище15", hasLaptop: false);
            students.Add(noLaptop);
            service.SubscribeToStudent(noLaptop);

            var group1 = new Group(1, "Б-121-24-3-ПІ", courseYear: 1);
            foreach (var s in students)
                group1.AddStudent(s);
            groups.Add(group1);

            var group3 = new Group(2, "Б-121-22-1-ПІ", courseYear: 2);
            for (int i = 1; i <= 12; i++)
            {
                var s = new Student(100 + i, "Ст3к" + i, "Прізв" + i, hasLaptop: true);
                group3.AddStudent(s);
                students.Add(s);         
                service.SubscribeToStudent(s);
            }
            groups.Add(group3);

            var t1 = new Teacher(1, "Олег", "Сич");
            var t2 = new Teacher(2, "Марія", "Коваль");
            var t3 = new Teacher(3, "Іван", "Петренко");
            teachers.Add(t1); teachers.Add(t2); teachers.Add(t3);
            service.SubscribeToTeacher(t1);
            service.SubscribeToTeacher(t2);
            service.SubscribeToTeacher(t3);

            var prog = new Discipline("Основи програмування", group1, new int[] { 1 });
            service.SubscribeToDiscipline(prog);
            disciplines.Add(prog);

            var lec1 = new Lecture(32);
            var lab1 = new LabWork(20);
            var mkr1 = new ModularTest(4);
            var crd1 = new Credit(8);

            service.AddActivity(prog, lec1);
            service.AddActivity(prog, lab1);
            service.AddActivity(prog, mkr1);
            service.AddActivity(prog, crd1);

            service.DivideLabWorkIntoSubGroups(lab1, group1, 2);

            prog.AssignTeacher(lec1, t1);
            prog.AssignTeacher(lab1, t2);
            prog.AssignTeacher(mkr1, t1);

            var oop = new Discipline("ООП", group1, new int[] { 1, 2 });
            service.SubscribeToDiscipline(oop);
            disciplines.Add(oop);

            var lec2 = new Lecture(32);
            var lab2 = new LabWork(20);
            var mkr2 = new ModularTest(4);
            var ex2  = new Exam(8);

            service.AddActivity(oop, lec2);
            service.AddActivity(oop, lab2);
            service.AddActivity(oop, mkr2);
            service.AddActivity(oop, ex2);

            service.DivideLabWorkIntoSubGroups(lab2, group1, 2);

            oop.AssignTeacher(lec2, t1);
            oop.AssignTeacher(lab2, t2);
            oop.AssignTeacher(mkr2, t1);
            oop.AssignTeacher(ex2, t1);

            var algo = new Discipline(
                "Алгоритми та структури даних", group3, new int[] { 2 });
            service.SubscribeToDiscipline(algo);
            disciplines.Add(algo);

            for (int i = 0; i < 10; i++)
                students[i].SubmitWork(WorkType.LabWork, "ЛР №1 — " + students[i]);

            for (int i = 0; i < 5; i++)
                students[i].SubmitWork(WorkType.CourseWork, "КР — " + students[i]);
        }
    }
}
