using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class Group : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Course { get; set; }
        public string Specialty { get; set; }

        public List<string> StudentIDs { get; set; }

        public Group()
        {
            StudentIDs = new List<string>();
        }

        public Group(int id, string name, int course, string specialty)
        {
            Id = id;
            Name = name;
            Course = course;
            Specialty = specialty;
            StudentIDs = new List<string>();
        }

        public void AddStudent(string studentID)
        {
            if (string.IsNullOrWhiteSpace(studentID))
                throw new ArgumentException("ID студента не може бути порожнім");

            if (StudentIDs.Contains(studentID))
                throw new InvalidOperationException("Студент вже в цій групі");

            StudentIDs.Add(studentID);
        }

        public void RemoveStudent(string studentID)
        {
            if (!StudentIDs.Contains(studentID))
                throw new InvalidOperationException("Студента немає в цій групі");

            StudentIDs.Remove(studentID);
        }

        public int GetStudentCount()
        {
            return StudentIDs.Count;
        }

        public bool HasStudent(string studentID)
        {
            return StudentIDs.Contains(studentID);
        }
    }
}
