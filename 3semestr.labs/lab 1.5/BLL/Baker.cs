using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Baker : Human, ISpeciality
    {
        private int _countOfJumps = 10;
        public int CountOfJumps => _countOfJumps;

        public string BakeCake(string ingredient)
        {
            return $"{FirstName} {LastName} bake cake, using {ingredient}.";
        }

        public Baker(string firstName, string lastName) : base(firstName, lastName)
        {
        }

        public void ParachuteJump()
        {
            _countOfJumps++;
        }
    }
}
