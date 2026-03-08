using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySimulation
{
    public class Teacher
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        // null = вільний, значення = зайнятий цією дисципліною
        public Discipline CurrentDiscipline { get; private set; }

        public event EventHandler AssignmentChanged;

        public Teacher(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public bool IsAvailableFor(Discipline discipline)
        {
            return CurrentDiscipline == null ||
                   CurrentDiscipline == discipline;
        }

        public bool AssignToDiscipline(Discipline discipline)
        {
            if (!IsAvailableFor(discipline)) return false;

            CurrentDiscipline = discipline;

            if (AssignmentChanged != null)
                AssignmentChanged(this, EventArgs.Empty);

            return true;
        }

        public void RemoveFromDiscipline(Discipline discipline)
        {
            if (CurrentDiscipline == discipline)
            {
                CurrentDiscipline = null;

                if (AssignmentChanged != null)
                    AssignmentChanged(this, EventArgs.Empty);
            }
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
