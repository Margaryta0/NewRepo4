using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EntityService
    {
        private readonly EntityContext _context;

        public EntityService(EntityContext context)
        {
            _context = context;
        }

        public int CountSpringFourthYearStudents()
        {
            List<Student> students = _context.LoadStudents();

            int count = students.Count(s => s.Course == 4 && IsSpringBirth(s.DateOfBirth));

            return count;
        }

        private bool IsSpringBirth(DateTime dob)
        {
            return dob.Month >= 3 && dob.Month <= 5;
        }

        public void SeedDataAndSave()
        {
            _context.SaveStudents(EntityContext.GetSampleStudents());
        }

        public List<Student> GetAllStudents()
        {
            return _context.LoadStudents();
        }
    }
}