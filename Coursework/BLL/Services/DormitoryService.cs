using BLL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DormitoryService
    {
        private List<Dormitory> _dormitories;
        private readonly JsonRepository<Dormitory> _repository;
        private readonly StudentService _studentService;

        public DormitoryService(StudentService studentService)
        {
            _repository = new JsonRepository<Dormitory>("dormitories.json");
            _dormitories = _repository.LoadAll();
            _studentService = studentService;
        }

        public void AddDormitory(string name, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new StudentInvalidDataException("Назва гуртожитку не може бути порожньою");

            if (string.IsNullOrWhiteSpace(address))
                throw new StudentInvalidDataException("Адреса не може бути порожньою");

            if (_dormitories.Any(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                throw new StudentInvalidDataException($"Гуртожиток з назвою {name} вже існує");

            int newId = _dormitories.Any() ? _dormitories.Max(d => d.Id) + 1 : 1;

            var dormitory = new Dormitory
            {
                Id = newId,
                Name = name.Trim(),
                Address = address.Trim()
            };

            _dormitories.Add(dormitory);
            _repository.SaveAll(_dormitories);
        }

        public void DeleteDormitory(int dormitoryId)
        {
            var dormitory = GetDormitoryById(dormitoryId);

            foreach (var room in dormitory.Rooms)
            {
                foreach (var studentID in room.OccupantIDs.ToList())
                {
                    CheckOutStudent(dormitoryId, studentID);

                }
            }

            _dormitories.Remove(dormitory);
            _repository.SaveAll(_dormitories);
        }

        public void UpdateDormitory(int dormitoryId, string name, string address)
        {
            var dormitory = GetDormitoryById(dormitoryId);

            if (string.IsNullOrWhiteSpace(name))
                throw new StudentInvalidDataException("Назва гуртожитку не може бути порожньою");

            if (string.IsNullOrWhiteSpace(address))
                throw new StudentInvalidDataException("Адреса не може бути порожньою");

            dormitory.Name = name.Trim();
            dormitory.Address = address.Trim();

            _repository.SaveAll(_dormitories);
        }

        public void AddRoom(int dormitoryId, string roomNumber, int maxCapacity)
        {
            var dormitory = GetDormitoryById(dormitoryId);

            if (string.IsNullOrWhiteSpace(roomNumber))
                throw new StudentInvalidDataException("Номер кімнати не може бути порожнім");

            if (maxCapacity < 1 || maxCapacity > 4)
                throw new StudentInvalidDataException("Місткість кімнати має бути від 1 до 4");

            var room = new DormitoryRoom(roomNumber.Trim(), maxCapacity);
            dormitory.AddRoom(room);

            _repository.SaveAll(_dormitories);
        }

        public void RemoveRoom(int dormitoryId, string roomNumber)
        {
            var dormitory = GetDormitoryById(dormitoryId);
            var room = dormitory.GetRoom(roomNumber);

            if (room == null)
                throw new DormitoryNotFoundException($"Кімнату {roomNumber} не знайдено");

            if (room.OccupantIDs.Count > 0)
                throw new InvalidOperationException($"Не можна видалити кімнату {roomNumber}, оскільки в ній живуть студенти");

            dormitory.RemoveRoom(roomNumber);
            _repository.SaveAll(_dormitories);
        }

        public void CheckInStudent(int dormitoryId, string roomNumber, string studentID)
        {
            var dormitory = GetDormitoryById(dormitoryId);
            var room = dormitory.GetRoom(roomNumber);

            if (room == null)
                throw new DormitoryNotFoundException($"Кімнату {roomNumber} не знайдено в гуртожитку {dormitory.Name}");

            var student = _studentService.GetStudentByID(studentID);

            if (student.DormitoryRoomID.HasValue)
                throw new InvalidOperationException($"Студент {studentID} вже поселений у кімнаті");

            if (!room.HasAvailableSpace())
                throw new RoomFullException($"Кімната {roomNumber} заповнена (макс: {room.MaxCapacity})");

            room.AddOccupant(studentID);
            _repository.SaveAll(_dormitories);

            student.DormitoryRoomID = GetRoomId(dormitoryId, roomNumber);
        }


        public void CheckOutStudent(int dormitoryId, string studentID)
        {
            var dormitory = GetDormitoryById(dormitoryId);
            var student = _studentService.GetStudentByID(studentID);

            var room = dormitory.Rooms.FirstOrDefault(r => r.OccupantIDs.Contains(studentID));

            if (room == null)
                throw new DormitoryNotFoundException($"Студента {studentID} не знайдено в гуртожитку {dormitory.Name}");

            room.RemoveOccupant(studentID);
            _repository.SaveAll(_dormitories);

            student.DormitoryRoomID = null;
        }

        public Dormitory GetDormitoryById(int dormitoryId)
        {
            var dormitory = _dormitories.FirstOrDefault(d => d.Id == dormitoryId);
            if (dormitory == null)
                throw new DormitoryNotFoundException($"Гуртожиток з ID {dormitoryId} не знайдено");

            return dormitory;
        }

        public List<Dormitory> GetAllDormitories()
        {
            return _dormitories;
        }

        public List<DormitoryRoom> GetAvailableRooms(int dormitoryId)
        {
            var dormitory = GetDormitoryById(dormitoryId);
            return dormitory.GetAvailableRooms();
        }

        /// Отримує інформацію про проживаючих у гуртожитку
        public List<Student> GetOccupants(int dormitoryId)
        {
            var dormitory = GetDormitoryById(dormitoryId);
            var occupants = new List<Student>();

            foreach (var room in dormitory.Rooms)
            {
                foreach (var studentID in room.OccupantIDs)
                {
                    try
                    {
                        occupants.Add(_studentService.GetStudentByID(studentID));
                    }
                    catch (StudentNotFoundException)
                    {
                    }
                }
            }

            return occupants;
        }

        public List<Student> GetRoomOccupants(int dormitoryId, string roomNumber)
        {
            var dormitory = GetDormitoryById(dormitoryId);
            var room = dormitory.GetRoom(roomNumber);

            if (room == null)
                throw new DormitoryNotFoundException($"Кімнату {roomNumber} не знайдено");

            var occupants = new List<Student>();
            foreach (var studentID in room.OccupantIDs)
            {
                try
                {
                    occupants.Add(_studentService.GetStudentByID(studentID));
                }
                catch (StudentNotFoundException)
                {
                }
            }

            return occupants;
        }


        private int GetRoomId(int dormitoryId, string roomNumber)
        {
            return dormitoryId * 10000 + int.Parse(roomNumber);
        }
    }
}
