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
		private Mock<IRepositiry<Student>> _mockRepository;
		private List<Student> _studentsInMemory;

		[SetUp]
		public void Setup()
		{
			_studentsInMemory = new List<Student>();
			_mockRepository = new Mock<IRepositiry<Student>>();

			_mockRepository.Setup(r => r.LoadAll())
						   .Returns(_studentsInMemory);

			_mockRepository.Setup(r => r.SaveAll(It.IsAny<List<Student>>()))
						   .Callback<List<Student>>(students =>
						   {
							   _studentsInMemory.Clear();
							   _studentsInMemory.AddRange(students);
						   });

			_service = new StudentService(_mockRepository.Object);
		}

		[Test]
		public void AddStudent_ValidData_ShouldAddStudent()
		{
			string studentID = "AB123456";
			string lastName = "Петренко";
			string firstName = "Марія";
			int course = 2;

			// Act
			_service.AddStudent(studentID, lastName, firstName, course);

			// Assert
			var students = _service.GetAllStudents();
			Assert.That(students.Count, Is.EqualTo(1), "Має бути додано 1 студента");
			Assert.That(students[0].StudentID, Is.EqualTo(studentID), "ID має співпадати");
			Assert.That(students[0].LastName, Is.EqualTo(lastName), "Прізвище має співпадати");
		}

		[Test]
		public void AddStudent_EmptyID_ShouldThrowException()
		{
			string studentID = "";
			string lastName = "Петренко";
			string firstName = "Марія";
			int course = 2;

			// Act & Assert
			Assert.Throws<StudentInvalidDataException>(() =>
				_service.AddStudent(studentID, lastName, firstName, course)
			);

			_mockRepository.Verify(r => r.SaveAll(It.IsAny<List<Student>>()), Times.Never);
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
		public void UpdateStudent_NonExistentStudent_ShouldThrowException()
		{
			string studentID = "AB999999";

			Assert.Throws<StudentNotFoundException>(() =>
				_service.UpdateStudent(studentID, "Іваненко", "Іван", 2)
			);
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
			_service.AddStudent("AB123456", "Петренко", "Марія", 2);

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
		public void AddStudent_ShouldCallSaveAllOnce()
		{
			string studentID = "AB123456";

			_service.AddStudent(studentID, "Петренко", "Марія", 2);

			_mockRepository.Verify(r => r.SaveAll(It.IsAny<List<Student>>()), Times.Once);
		}

		[Test]
		public void GetAllStudents_ShouldNotCallSaveAll()
		{
			_service.AddStudent("AB123456", "Петренко", "Марія", 2);
			_mockRepository.Invocations.Clear();

			var students = _service.GetAllStudents();

			_mockRepository.Verify(r => r.SaveAll(It.IsAny<List<Student>>()), Times.Never);
		}
	}
}