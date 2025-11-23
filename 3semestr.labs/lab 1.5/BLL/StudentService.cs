using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StudentService
    {
        public List<Student> GetSpringBorn4thCourseStudents(IEnumerable<Student> students)
        {
            return students
                .Where(s => s.Course == 4)
                .Where(s => s.BirthDate.Month >= 3 && s.BirthDate.Month <= 5)
                .ToList();
        }

        public bool IsTodayBirthday(Student student)
        {
            return student.BirthDate.Month == DateTime.Today.Month &&
                   student.BirthDate.Day == DateTime.Today.Day;
        }

        public bool TryAddWorker(Entrepreneur entrepreneur, Baker baker)
        {
            if (baker != null)
            {
                entrepreneur.AddWorker(baker);
                return true;
            }
            return false;
        }
    }
}
