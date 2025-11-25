using BLL;
using BLL.Entities;
using BLL.Services;
using DAL;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        private StudentService _service;
        private Mock<IRepositiry<Student, string>> _mockRepository;
        private List<Student> _studentsInMemory;

        [SetUp]
        public void Setup()
        {
            _studentsInMemory = new List<Student>();
            _mockRepository = new Mock<IRepositiry<Student, string>>();

            _mockRepository.Setup(r => r.GetAll())
                         .Returns(_studentsInMemory);

            _mockRepository.Setup(r => r.GetById(It.IsAny<string>()))
                           .Returns<string>(id => _studentsInMemory.FirstOrDefault(s => s.StudentID.Equals(id, System.StringComparison.OrdinalIgnoreCase)));

            _mockRepository.Setup(r => r.Add(It.IsAny<Student>()))
                           .Callback<Student>(student => _studentsInMemory.Add(student));

            _mockRepository.Setup(r => r.Delete(It.IsAny<string>()))
                           .Callback<string>(id =>
                           {
                               var studentToRemove = _studentsInMemory.FirstOrDefault(s => s.StudentID.Equals(id, System.StringComparison.OrdinalIgnoreCase));
                               if (studentToRemove != null)
                               {
                                   _studentsInMemory.Remove(studentToRemove);
                               }
                           });

            _mockRepository.Setup(r => r.Update(It.IsAny<Student>()))
                           .Callback<Student>(updatedStudent =>
                           {
                               var existingStudent = _studentsInMemory.FirstOrDefault(s => s.StudentID.Equals(updatedStudent.StudentID, System.StringComparison.OrdinalIgnoreCase));
                               if (existingStudent != null)
                               {
                                   int index = _studentsInMemory.IndexOf(existingStudent);
                                   _studentsInMemory[index] = updatedStudent;
                               }
                           });

            _mockRepository.Setup(r => r.GetNextId())
                           .Returns(default(string));

            _service = new StudentService(_mockRepository.Object);
        }

        [Test]
        public void AddStudent_ValidData_ShouldAddStudent()
        {
            string studentID = "AB123456";
            string lastName = "Петренко";
            string firstName = "Марія";
            int course = 2;

            _service.AddStudent(studentID, lastName, firstName, course);

            var students = _service.GetAllStudents();
            Assert.That(students.Count, Is.EqualTo(1), "Має бути додано 1 студента");
            Assert.That(students[0].StudentID, Is.EqualTo(studentID), "ID має співпадати");
            Assert.That(students[0].LastName, Is.EqualTo(lastName), "Прізвище має співпадати");

            _mockRepository.Verify(r => r.Add(It.IsAny<Student>()), Times.Once);
        }

        [Test]
        public void AddStudent_EmptyID_ShouldThrowException()
        {
            string studentID = "";
            string lastName = "Петренко";
            string firstName = "Марія";
            int course = 2;

            Assert.Throws<StudentInvalidDataException>(() =>
                _service.AddStudent(studentID, lastName, firstName, course)
            );

            _mockRepository.Verify(r => r.Add(It.IsAny<Student>()), Times.Never);
        }

        [Test]
        public void AddStudent_InvalidIDFormat_ShouldThrowException()
        {
            string studentID = "12345678";
            string lastName = "Петренко";
            string firstName = "Марія";
            int course = 2;

            Assert.Throws<StudentInvalidDataException>(() =>
                _service.AddStudent(studentID, lastName, firstName, course)
            );
        }

        [Test]
        public void AddStudent_EmptyLastName_ShouldThrowException()
        {
            string studentID = "AB123456";
            string lastName = "";
            string firstName = "Марія";
            int course = 2;

            Assert.Throws<StudentInvalidDataException>(() =>
                _service.AddStudent(studentID, lastName, firstName, course)
            );
        }

        [Test]
        public void AddStudent_InvalidCourse_ShouldThrowException()
        {
            string studentID = "AB123456";
            string lastName = "Петренко";
            string firstName = "Марія";
            int course = 7;

            Assert.Throws<StudentInvalidDataException>(() =>
                _service.AddStudent(studentID, lastName, firstName, course)
            );
        }

        [Test]
        public void DeleteStudent_NonExistentStudent_ShouldThrowException()
        {
            string studentID = "AB999999";

            Assert.Throws<StudentNotFoundException>(() =>
                _service.DeleteStudent(studentID)
            );
        }

        [Test]
        public void DeleteStudent_ExistingStudent_ShouldDeleteStudent()
        {
            _studentsInMemory.Add(new Student("AB123456", "Петренко", "Марія", 2));
            _mockRepository.Invocations.Clear();

            _service.DeleteStudent("AB123456");

            Assert.That(_service.GetAllStudents().Count, Is.EqualTo(0));
            _mockRepository.Verify(r => r.Delete("AB123456"), Times.Once);
        }

        [Test]
        public void UpdateStudent_NonExistentStudent_ShouldThrowException()
        {
            string studentID = "AB999999";

            Assert.Throws<StudentNotFoundException>(() =>
                _service.UpdateStudent(studentID, "Іваненко", "Іван", 2)
            );
        }

        [Test]
        public void UpdateStudent_ExistingStudent_ShouldUpdateStudent()
        {
            _studentsInMemory.Add(new Student("AB123456", "Петренко", "Марія", 2));
            _mockRepository.Invocations.Clear();

            _service.UpdateStudent("AB123456", "Іваненко", "Іван", 3);
            var updatedStudent = _service.GetStudentByID("AB123456");

            Assert.That(updatedStudent.LastName, Is.EqualTo("Іваненко"));
            Assert.That(updatedStudent.Course, Is.EqualTo(3));
            _mockRepository.Verify(r => r.Update(It.IsAny<Student>()), Times.Once);
        }

        [Test]
        public void GetStudentByID_NonExistentStudent_ShouldThrowException()
        {
            string studentID = "AB999999";

            Assert.Throws<StudentNotFoundException>(() =>
                _service.GetStudentByID(studentID)
            );
        }

        [Test]
        public void SearchStudents_NoMatches_ShouldReturnEmptyList()
        {
            _studentsInMemory.Add(new Student("AB123456", "Петренко", "Марія", 2));

            var results = _service.SearchStudents("Іваненко");

            Assert.That(results.Count, Is.EqualTo(0), "Має повернути порожній список");
        }

        [Test]
        public void GetStudentCount_NoStudents_ShouldReturnZero()
        {
            int count = _service.GetStudentCount();

            Assert.That(count, Is.EqualTo(0), "Кількість студентів має бути 0");
        }

        [Test]
        [TestCase("AB123456", true)]
        [TestCase("XY999999", true)]
        [TestCase("ab123456", false)]
        [TestCase("ABC12345", false)]
        [TestCase("A1234567", false)]
        [TestCase("AB12345", false)]
        [TestCase("1234567", false)]
        [TestCase("", false)]
        public void IsValidStudentID_VariousFormats_ShouldReturnExpectedResult(string studentID, bool expected)
        {
            bool result = Student.IsValidStudentID(studentID);

            Assert.That(result, Is.EqualTo(expected), $"ID '{studentID}' має бути {(expected ? "валідним" : "невалідним")}");
        }

        [Test]
        public void AddStudent_ShouldCallAddOnce()
        {
            string studentID = "AB123456";

            _service.AddStudent(studentID, "Петренко", "Марія", 2);

            _mockRepository.Verify(r => r.Add(It.IsAny<Student>()), Times.Once);
        }

        [Test]
        public void GetAllStudents_ShouldCallGetAllOnce()
        {
            _studentsInMemory.Add(new Student("AB123456", "Петренко", "Марія", 2));
            _mockRepository.Invocations.Clear();

            var students = _service.GetAllStudents();

            _mockRepository.Verify(r => r.GetAll(), Times.Once);

            _mockRepository.Verify(r => r.Update(It.IsAny<Student>()), Times.Never);
        }
    }
}