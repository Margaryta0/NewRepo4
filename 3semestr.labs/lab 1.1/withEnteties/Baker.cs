using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace withEnteties
{
    public class Baker : Human, ISpeciality
    {
        private int _countOfJumpes = 10;
        public int CountOfJumps => _countOfJumpes;
        public Baker(string firstName, string lastName) : base(firstName, lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public void ParachuteJump()
        {
            _countOfJumpes++;
        }
    }
}
