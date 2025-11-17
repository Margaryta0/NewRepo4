using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SearchService
    {
        private readonly StudentService _studentService;
        private readonly GroupService _groupService;
        private readonly DormitoryService _dormitoryService;

        public SearchService(StudentService studentService, GroupService groupService, DormitoryService dormitoryService)
        {
            _studentService = studentService;
            _groupService = groupService;
            _dormitoryService = dormitoryService;
        }

        public List<Student> SearchByName(string searchTerm)
        {
            return _studentService.SearchStudents(searchTerm);
        }

        public List<Student> SearchByGroupName(string groupName)
        {
            var groups = _groupService.GetAllGroups();
            var group = groups.FirstOrDefault(g => g.Name.Equals(groupName, System.StringComparison.OrdinalIgnoreCase));

            if (group == null)
                return new List<Student>();

            return _groupService.GetStudentsInGroup(group.Id);
        }

        public List<Student> SearchByGroupId(int groupId)
        {
            return _studentService.GetStudentsByGroup(groupId);
        }

        public List<Student> SearchStudentsInDormitory()
        {
            return _studentService.GetStudentsInDormitory();
        }

        public List<Student> SearchByDormitory(int dormitoryId)
        {
            return _dormitoryService.GetOccupants(dormitoryId);
        }

        public List<Student> SearchByDormitoryRoom(int dormitoryId, string roomNumber)
        {
            return _dormitoryService.GetRoomOccupants(dormitoryId, roomNumber);
        }

        public List<Student> SearchByCourse(int course)
        {
            var allStudents = _studentService.GetAllStudents();
            return allStudents.Where(s => s.Course == course).ToList();
        }

        public List<Student> ComplexSearch(string searchTerm, int? course = null, int? groupId = null, bool? inDormitory = null)
        {
            var results = _studentService.GetAllStudents();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                results = results.Where(s =>
                    s.FirstName.ToLower().Contains(searchTerm) ||
                    s.LastName.ToLower().Contains(searchTerm) ||
                    s.StudentID.ToLower().Contains(searchTerm)
                ).ToList();
            }

            if (course.HasValue)
            {
                results = results.Where(s => s.Course == course.Value).ToList();
            }

            if (groupId.HasValue)
            {
                results = results.Where(s => s.GroupID == groupId.Value).ToList();
            }

            if (inDormitory.HasValue)
            {
                if (inDormitory.Value)
                    results = results.Where(s => s.DormitoryRoomID.HasValue).ToList();
                else
                    results = results.Where(s => !s.DormitoryRoomID.HasValue).ToList();
            }

            return results;
        }
    }
}
