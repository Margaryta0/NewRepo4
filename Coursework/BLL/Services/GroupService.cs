using BLL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GroupService
    {
        private readonly IRepositiry<Group, int> _repository;
        private readonly StudentService _studentService;

        public GroupService(StudentService studentService)
        {
            _repository = new JsonRepository<Group, int>("groups.json");
            _studentService = studentService;
        }

        public void AddGroup(string name, int course, string specialty)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new StudentInvalidDataException("Назва групи не може бути порожньою");

            if (course < 1 || course > 6)
                throw new StudentInvalidDataException("Курс має бути від 1 до 6");

            if (string.IsNullOrWhiteSpace(specialty))
                throw new StudentInvalidDataException("Спеціальність не може бути порожньою");

            var allGroups = _repository.GetAll();
            if (allGroups.Any(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                throw new StudentInvalidDataException($"Група з назвою {name} вже існує");

            int newId = (int)(object)_repository.GetNextId();

            var group = new Group
            {
                Id = newId,
                Name = name.Trim(),
                Course = course,
                Specialty = specialty.Trim()
            };

            _repository.Add(group);
        }

        public void DeleteGroup(int groupId)
        {
            var group = GetGroupById(groupId);

            if (group.StudentIDs.Count > 0)
            {
            }

            _repository.Delete(groupId);
        }

        public void UpdateGroup(int groupId, string name, int course, string specialty)
        {
            var group = GetGroupById(groupId);

            if (string.IsNullOrWhiteSpace(name))
                throw new StudentInvalidDataException("Назва групи не може бути порожньою");

            if (course < 1 || course > 6)
                throw new StudentInvalidDataException("Курс має бути від 1 до 6");

            if (string.IsNullOrWhiteSpace(specialty))
                throw new StudentInvalidDataException("Спеціальність не може бути порожньою");

            var allGroups = _repository.GetAll();
            if (allGroups.Any(g => g.Id != groupId && g.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                throw new StudentInvalidDataException($"Група з назвою {name} вже існує");


            group.Name = name.Trim();
            group.Course = course;
            group.Specialty = specialty.Trim();

            _repository.Update(group);
        }

        public void AddStudentToGroup(int groupId, string studentID)
        {
            var group = GetGroupById(groupId);
            _studentService.GetStudentByID(studentID);

            var isAlreadyInGroup = _repository.GetAll()
                .Any(g => g.StudentIDs.Contains(studentID));

            if (isAlreadyInGroup)
                throw new StudentAlreadyInGroupException($"Студент {studentID} вже у іншій групі");

            group.AddStudent(studentID);

            _repository.Update(group);

        }

        public void RemoveStudentFromGroup(int groupId, string studentID)
        {
            var group = GetGroupById(groupId);

            _studentService.GetStudentByID(studentID);

            group.RemoveStudent(studentID);

            _repository.Update(group);
        }

        public Group GetGroupById(int groupId)
        {
            var group = _repository.GetById(groupId);
            if (group == null)
                throw new GroupNotFoundException($"Групу з ID {groupId} не знайдено");

            return group;
        }

        public List<Group> GetAllGroups()
        {
            return _repository.GetAll();
        }


        public int GetGroupCount()
        {
            return _repository.GetAll().Count;
        }
    }
}
