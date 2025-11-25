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
    public class DormitoryService
    {
        private readonly IRepositiry<Dormitory, int> _repository;
        private readonly StudentService _studentService;

        public DormitoryService(StudentService studentService)
        {
            _repository = new JsonRepository<Dormitory, int>("dormitories.json");
            _studentService = studentService;
        }

        public void AddDormitory(string name, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new StudentInvalidDataException("Назва гуртожитку не може бути порожньою");

            if (string.IsNullOrWhiteSpace(address))
                throw new StudentInvalidDataException("Адреса не може бути порожньою");

            var allDormitories = _repository.GetAll();
            if (allDormitories.Any(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                throw new StudentInvalidDataException($"Гуртожиток з назвою {name} вже існує");

            int newId = (int)(object)_repository.GetNextId();

            var dormitory = new Dormitory
            {
                Id = newId,
                Name = name.Trim(),
                Address = address.Trim()
            };

            _repository.Add(dormitory);
        }

        public void DeleteDormitory(int dormitoryId)
        {
            var dormitory = GetDormitoryById(dormitoryId);

            foreach (var room in dormitory.Rooms)
            {
                room.OccupantIDs.Clear();
            }

            _repository.Delete(dormitoryId);
        }

        public void UpdateDormitory(int dormitoryId, string name, string address)
        {
            var dormitory = GetDormitoryById(dormitoryId);

            if (string.IsNullOrWhiteSpace(name))
                throw new StudentInvalidDataException("Назва гуртожитку не може бути порожньою");

            if (string.IsNullOrWhiteSpace(address))
                throw new StudentInvalidDataException("Адреса не може бути порожньою");

            var allDormitories = _repository.GetAll();
            if (allDormitories.Any(d => d.Id != dormitoryId && d.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                throw new StudentInvalidDataException($"Гуртожиток з назвою {name} вже існує");

            dormitory.Name = name.Trim();
            dormitory.Address = address.Trim();

            _repository.Update(dormitory);
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

            _repository.Update(dormitory);
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

            _repository.Update(dormitory);
        }

        public void CheckInStudent(int dormitoryId, string roomNumber, string studentID)
        {
            var dormitory = GetDormitoryById(dormitoryId);
            var room = dormitory.GetRoom(roomNumber);

            if (room == null)
                throw new DormitoryNotFoundException($"Кімнату {roomNumber} не знайдено в гуртожитку {dormitory.Name}");

            var student = _studentService.GetStudentByID(studentID);

            var isAlreadyOccupant = _repository.GetAll()
                .Any(d => d.Rooms.Any(r => r.OccupantIDs.Contains(studentID)));

            if (isAlreadyOccupant)
                throw new InvalidOperationException($"Студент {studentID} вже поселений в іншому гуртожитку/кімнаті");

            room.AddOccupant(studentID);

            _repository.Update(dormitory);

        }


        public void CheckOutStudent(int dormitoryId, string studentID)
        {
            var dormitory = GetDormitoryById(dormitoryId);

            _studentService.GetStudentByID(studentID); 

            var room = dormitory.Rooms.FirstOrDefault(r => r.OccupantIDs.Contains(studentID));

            if (room == null)
                throw new DormitoryNotFoundException($"Студента {studentID} не знайдено в гуртожитку {dormitory.Name}");

            room.RemoveOccupant(studentID);

            _repository.Update(dormitory);
        }

        public Dormitory GetDormitoryById(int dormitoryId)
        {
            var dormitory = _repository.GetById(dormitoryId);
            if (dormitory == null)
                throw new DormitoryNotFoundException($"Гуртожиток з ID {dormitoryId} не знайдено");

            return dormitory;
        }

        public List<Dormitory> GetAllDormitories()
        {
            return _repository.GetAll();
        }

        public List<DormitoryRoom> GetAvailableRooms(int dormitoryId)
        {
            var dormitory = GetDormitoryById(dormitoryId);
            return dormitory.GetAvailableRooms();
        }
    }
}
