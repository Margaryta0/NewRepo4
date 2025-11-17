using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StudentNotFoundException : Exception
    {
        public StudentNotFoundException() : base("Студента не знайдено") { }
        public StudentNotFoundException(string message) : base(message) { }
        public StudentNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class GroupNotFoundException : Exception
    {
        public GroupNotFoundException() : base("Групу не знайдено") { }
        public GroupNotFoundException(string message) : base(message) { }
        public GroupNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class RoomFullException : Exception
    {
        public RoomFullException() : base("Кімната заповнена") { }
        public RoomFullException(string message) : base(message) { }
        public RoomFullException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class DormitoryNotFoundException : Exception
    {
        public DormitoryNotFoundException() : base("Гуртожиток не знайдено") { }
        public DormitoryNotFoundException(string message) : base(message) { }
        public DormitoryNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class StudentAlreadyInGroupException : Exception
    {
        public StudentAlreadyInGroupException() : base("Студент вже у групі") { }
        public StudentAlreadyInGroupException(string message) : base(message) { }
        public StudentAlreadyInGroupException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class StudentInvalidDataException : Exception
    {
        public StudentInvalidDataException() : base("Невалідні дані") { }
        public StudentInvalidDataException(string message) : base(message) { }
        public StudentInvalidDataException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
