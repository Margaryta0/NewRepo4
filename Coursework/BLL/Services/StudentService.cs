using BLL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class StudentService
    {
        private readonly IRepositiry<Student, string> _repository;

        public StudentService()
        {
            _repository = new JsonRepository<Student, string>("students.json");
        }

        public StudentService(IRepositiry<Student, string> repository)
        {
            _repository = repository;
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

            if (_repository.GetById(studentID.ToUpper()) != null)
                throw new StudentInvalidDataException($"Студент з ID {studentID} вже існує");

            var student = new Student
            {
                StudentID = studentID.ToUpper(),
                LastName = lastName.Trim(),
                FirstName = firstName.Trim(),
                Course = course
            };

            _repository.Add(student);
        }

        public void DeleteStudent(string studentID)
        {
            var student = GetStudentByID(studentID);

            _repository.Delete(student.StudentID);
        }

        public Student GetStudentByID(string studentID)
        {
            var student = _repository.GetById(studentID.ToUpper());
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

            _repository.Update(student);
        }

        public List<Student> GetAllStudents()
        {
            return _repository.GetAll();
        }

        public List<Student> SearchStudents(string searchTerm)
        {
            var students = GetAllStudents(); 

            if (string.IsNullOrWhiteSpace(searchTerm))
                return students;

            searchTerm = searchTerm.ToLower();

            return students.Where(s =>
                s.FirstName.ToLower().Contains(searchTerm) ||
                s.LastName.ToLower().Contains(searchTerm) ||
                s.StudentID.ToLower().Contains(searchTerm)
            ).ToList();
        }

        public int GetStudentCount()
        {
            return GetAllStudents().Count;
        }
    }
}
