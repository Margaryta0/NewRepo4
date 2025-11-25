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

        public List<Student> SearchByGroupId(int groupId)
        {
            var group = _groupService.GetGroupById(groupId);
            var studentIdsInGroup = group.StudentIDs;

            var allStudents = _studentService.GetAllStudents();

            var students = allStudents
                .Where(s => studentIdsInGroup.Contains(s.StudentID))
                .ToList();

            return students;
        }

        public List<Student> SearchByDormitory(int dormitoryId)
        {
            var dormitory = _dormitoryService.GetDormitoryById(dormitoryId);

            var occupantIds = dormitory.Rooms.SelectMany(r => r.OccupantIDs).Distinct().ToList();

            var allStudents = _studentService.GetAllStudents();

            var students = allStudents
                .Where(s => occupantIds.Contains(s.StudentID))
                .ToList();

            return students;
        }
    }
}
