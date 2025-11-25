using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class Dormitory : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<DormitoryRoom> Rooms { get; set; }

        public Dormitory()
        {
            Rooms = new List<DormitoryRoom>();
        }

        public Dormitory(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
            Rooms = new List<DormitoryRoom>();
        }

        public void AddRoom(DormitoryRoom room)
        {
            if (Rooms.Any(r => r.RoomNumber == room.RoomNumber))
                throw new InvalidOperationException($"Кімната {room.RoomNumber} вже існує");

            Rooms.Add(room);
        }

        public void RemoveRoom(string roomNumber)
        {
            var room = Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room == null)
                throw new InvalidOperationException($"Кімнату {roomNumber} не знайдено");

            Rooms.Remove(room);
        }

        public DormitoryRoom GetRoom(string roomNumber)
        {
            return Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
        }

        public List<DormitoryRoom> GetAvailableRooms()
        {
            return Rooms.Where(r => r.HasAvailableSpace()).ToList();
        }

        public int GetTotalRooms()
        {
            return Rooms.Count;
        }

        public int GetTotalOccupants()
        {
            return Rooms.Sum(r => r.GetOccupantCount());
        }

        public int GetTotalCapacity()
        {
            return Rooms.Sum(r => r.MaxCapacity);
        }
    }
}
