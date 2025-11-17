using BLL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class StudentService
	{
		private List<Student> _students;
		private IRepositiry<Student> _repository;

		public StudentService()
		{
			_repository = new JsonRepository<Student>("students.json");
			_students = _repository.LoadAll();
		}

		public StudentService(IRepositiry<Student> repository)
		{
			_repository = repository;
			_students = _repository.LoadAll();
		}

		public void AddStudent(string studentID, string lastName, string firstName, int course)
		{
			if (string.IsNullOrWhiteSpace(studentID))
				throw new StudentInvalidDataException("ID не може бути пустим!");

			if (!Student.IsValidStudentID(studentID))
				throw new StudentInvalidDataException("Неправильний формат ID");

			if (string.IsNullOrWhiteSpace(lastName))
				throw new StudentInvalidDataException("Прізвище не може бути порожнім");

			if (string.IsNullOrWhiteSpace(firstName))
				throw new StudentInvalidDataException("Ім'я не може бути порожнім");

			if (course < 1 || course > 6)
				throw new StudentInvalidDataException("Курс має бути від 1 до 6");

			// Перевірка на дублікат (правильний виняток)
			if (_students.Any(s => s.StudentID.Equals(studentID, StringComparison.OrdinalIgnoreCase)))
				throw new BLL.StudentInvalidDataException($"Студент з ID {studentID} вже існує");

			var student = new Student
			{
				StudentID = studentID.ToUpper(),
				LastName = lastName.Trim(),
				FirstName = firstName.Trim(),
				Course = course
			};

			_students.Add(student);
			_repository.SaveAll(_students);
		}

		public void DeleteStudent(string studentID)
		{
			var student = _students.FirstOrDefault(s => s.StudentID.Equals(studentID, StringComparison.OrdinalIgnoreCase));
			if (student == null)
				throw new StudentNotFoundException($"Студента з ID {studentID} не знайдено");

			_students.Remove(student);
			_repository.SaveAll(_students);
		}

		public Student GetStudentByID(string studentID)
		{
			var student = _students.FirstOrDefault(s => s.StudentID.Equals(studentID, StringComparison.OrdinalIgnoreCase));
			if (student == null)
				throw new StudentNotFoundException($"Студента з ID {studentID} не знайдено");

			return student;
		}

		public void UpdateStudent(string studentID, string lastName, string firstName, int course)
		{
			var student = GetStudentByID(studentID);

			if (string.IsNullOrWhiteSpace(lastName))
				throw new StudentInvalidDataException("Прізвище не може бути порожнім");

			if (string.IsNullOrWhiteSpace(firstName))
				throw new StudentInvalidDataException("Ім'я не може бути порожнім");

			if (course < 1 || course > 6)
				throw new StudentInvalidDataException("Курс має бути від 1 до 6");


			student.LastName = lastName.Trim();
			student.FirstName = firstName.Trim();
			student.Course = course;

			_repository.SaveAll(_students);
		}

		public List<Student> GetAllStudents()
		{
			return _students;
		}

		public List<Student> SearchStudents(string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
				return _students;

			searchTerm = searchTerm.ToLower();

			return _students.Where(s =>
				s.FirstName.ToLower().Contains(searchTerm) ||
				s.LastName.ToLower().Contains(searchTerm) ||
				s.StudentID.ToLower().Contains(searchTerm)
			).ToList();
		}

		public List<Student> GetStudentsByGroup(int groupId)
		{
			return _students.Where(s => s.GroupID == groupId).ToList();
		}

		public List<Student> GetStudentsInDormitory()
		{
			return _students.Where(s => s.DormitoryRoomID.HasValue).ToList();
		}

		public void AssignStudentToGroup(string studentID, int groupId)
		{
			var student = GetStudentByID(studentID);

			if (student.GroupID.HasValue)
				throw new StudentAlreadyInGroupException($"Студент {studentID} вже у групі {student.GroupID}");

			student.GroupID = groupId;
			_repository.SaveAll(_students);
		}

		public void RemoveStudentFromGroup(string studentID)
		{
			var student = GetStudentByID(studentID);

			if (!student.GroupID.HasValue)
				throw new InvalidOperationException($"Студент {studentID} не належить до жодної групи");

			student.GroupID = null;
			_repository.SaveAll(_students);
		}

		public int GetStudentCount()
		{
			return _students.Count;
		}

	}
}
