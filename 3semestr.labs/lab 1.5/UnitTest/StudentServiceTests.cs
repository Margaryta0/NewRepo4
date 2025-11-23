using BLL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace UnitTest
{
    public class MockDataStream : IDataStream
    {
        private readonly List<Student> _mockStudents;

        public MockDataStream(List<Student> students)
        {
            _mockStudents = students;
        }

        public List<Student> ReadAllStudents(string fileName)
        {
            return _mockStudents;
        }

        public void CreateFile(string fileName, IEnumerable<Student> students)
        {
        }
    }


    [TestFixture]
    public class StudentServiceTests
    {
        [Test]
        public void GetSpringBorn4thCourseStudents_ShouldReturnCorrectCount()
        {
            // 1. Arrange
            var students = new List<Student>
        {
            new Student("Megi", "Smal", new StudentID("ІК", 123456), new DateTime(2002, 4, 15), 4),
            new Student("Tati", "Moroz", new StudentID("ПП", 654321), new DateTime(2003, 5, 10), 3),
            new Student("Petro", "Dykin", new StudentID("ММ", 112233), new DateTime(2002, 3, 20), 4),
            new Student("Ivan", "Petrov", new StudentID("ОО", 987654), new DateTime(2002, 6, 1), 4),
        };
            var service = new StudentService();

            // 2. Act
            List<Student> result = service.GetSpringBorn4thCourseStudents(students);

            Assert.Throws<Exception>(() => service.GetSpringBorn4thCourseStudents(students));

            // 3. Assert
            Assert.That(result.Count, Is.EqualTo(2), "Expected 2 students");
            Assert.That(result.All(s => s.Course == 4 && s.BirthDate.Month >= 3 && s.BirthDate.Month <= 5), Is.True, "Result have incorrect value");
        }

        
        [Test]
        public void IsValidStudentID_WithValidID_ReturnsTrue()
        {
            var studentID = new StudentID("AB", 123456);

            bool isValid = studentID.IsValidStudentID();

            Assert.That(isValid, Is.True, "Valid ID should return True.");
        }

        [Test]
        public void IsValidStudentID_WithInvalidLetters_ReturnsFalse()
        {
            var studentID = new StudentID("A", 123456); 

            bool isValid = studentID.IsValidStudentID();

            Assert.That(isValid, Is.False, "Invalid ID should return false");
        }

        [Test]
        public void ParachuteJump_Student_IncrementsCount()
        {
            var student = new Student("Lina", "Kryt", new StudentID("ТТ", 111111), DateTime.Today, 1);
            int initialJumps = student.CountOfJumps;

            student.ParachuteJump();

            Assert.That(student.CountOfJumps, Is.EqualTo(initialJumps + 1), "Count of jumps should increase by one");
        }

        [Test]
        public void Entrepreneur_AddWorker_ShouldAddBakerToList()
        {
            var entrepreneur = new Entrepreneur("Sofia", "Kisla");
            var baker = new Baker("Oleg", "Narinan");
            var service = new StudentService();
            int initialWorkers = entrepreneur.Workers.Count;

            service.TryAddWorker(entrepreneur, baker);
             
            Assert.That(entrepreneur.Workers.Count, Is.EqualTo(initialWorkers + 1), "The number of employees should increase.");
            Assert.That(entrepreneur.Workers.Contains(baker), Is.True, "Added baker should be at list.");
        }

    }
}
