using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class DormitoryRoom
    {
        public string RoomNumber { get; set; }
        public int MaxCapacity { get; set; }
        public List<string> OccupantIDs { get; set; }
        public DormitoryRoom()
        {
            OccupantIDs = new List<string>();
        }

        public DormitoryRoom(string roomNumber, int maxCapacity)
        {
            RoomNumber = roomNumber;
            MaxCapacity = maxCapacity;
            OccupantIDs = new List<string>();
        }

        public void AddOccupant(string studentID)
        {
            if (string.IsNullOrWhiteSpace(studentID))
                throw new ArgumentException("ID студента не може бути порожнім");

            if (OccupantIDs.Contains(studentID))
                throw new InvalidOperationException("Студент вже живе в цій кімнаті");

            if (OccupantIDs.Count >= MaxCapacity)
                throw new InvalidOperationException($"Кімната {RoomNumber} заповнена (макс: {MaxCapacity})");

            OccupantIDs.Add(studentID);
        }

        public void RemoveOccupant(string studentID)
        {
            if (!OccupantIDs.Contains(studentID))
                throw new InvalidOperationException("Студента немає в цій кімнаті");

            OccupantIDs.Remove(studentID);
        }

        public bool HasAvailableSpace()
        {
            return OccupantIDs.Count < MaxCapacity;
        }

        public int GetAvailableSpaces()
        {
            return MaxCapacity - OccupantIDs.Count;
        }

        public int GetOccupantCount()
        {
            return OccupantIDs.Count;
        }

        public bool HasOccupant(string studentID)
        {
            return OccupantIDs.Contains(studentID);
        }

    }
}