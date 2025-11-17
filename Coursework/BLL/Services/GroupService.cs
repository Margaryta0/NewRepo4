using BLL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GroupService
    {
        private List<Group> _groups;
        private readonly JsonRepository<Group> _repository;
        private readonly StudentService _studentService;

        public GroupService(StudentService studentService)
        {
            _repository = new JsonRepository<Group>("groups.json");
            _groups = _repository.LoadAll();
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

            if (_groups.Any(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                throw new StudentInvalidDataException($"Група з назвою {name} вже існує");

            int newId = _groups.Any() ? _groups.Max(g => g.Id) + 1 : 1;

            var group = new Group
            {
                Id = newId,
                Name = name.Trim(),
                Course = course,
                Specialty = specialty.Trim()
            };

            _groups.Add(group);
            _repository.SaveAll(_groups);
        }

        public void DeleteGroup(int groupId)
        {
            var group = GetGroupById(groupId);

            if (group.StudentIDs.Count > 0)
            {
                foreach (var studentID in group.StudentIDs.ToList())
                {

                    _studentService.RemoveStudentFromGroup(studentID);
                }
            }

            _groups.Remove(group);
            _repository.SaveAll(_groups);
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

            if (_groups.Any(g => g.Id != groupId && g.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                throw new StudentInvalidDataException($"Група з назвою {name} вже існує");

            group.Name = name.Trim();
            group.Course = course;
            group.Specialty = specialty.Trim();

            _repository.SaveAll(_groups);
        }

        public void AddStudentToGroup(int groupId, string studentID)
        {
            var group = GetGroupById(groupId);
            var student = _studentService.GetStudentByID(studentID);

            if (student.GroupID.HasValue)
                throw new StudentAlreadyInGroupException($"Студент {studentID} вже у групі {student.GroupID}");

            group.AddStudent(studentID);
            _repository.SaveAll(_groups);

            _studentService.AssignStudentToGroup(studentID, groupId);
        }

        public void RemoveStudentFromGroup(int groupId, string studentID)
        {
            var group = GetGroupById(groupId);

            group.RemoveStudent(studentID);
            _repository.SaveAll(_groups);

            _studentService.RemoveStudentFromGroup(studentID);
        }

        public Group GetGroupById(int groupId)
        {
            var group = _groups.FirstOrDefault(g => g.Id == groupId);
            if (group == null)
                throw new GroupNotFoundException($"Групу з ID {groupId} не знайдено");

            return group;
        }

        public List<Group> GetAllGroups()
        {
            return _groups;
        }

        public List<Student> GetStudentsInGroup(int groupId)
        {
            var group = GetGroupById(groupId);
            var students = new List<Student>();

            foreach (var studentID in group.StudentIDs)
            {
                try
                {
                    students.Add(_studentService.GetStudentByID(studentID));
                }
                catch (StudentNotFoundException)
                {
                }
            }

            return students;
        }

        public int GetGroupCount()
        {
            return _groups.Count;
        }
    }
}
