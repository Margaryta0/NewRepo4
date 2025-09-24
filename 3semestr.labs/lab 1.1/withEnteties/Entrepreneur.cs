using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace withEnteties
{
    public class Entrepreneur : Human, ISpeciality
    {
        private int _countOfJumpes = 2;
        public int CountOfJumps => _countOfJumpes;
        public List<Baker> Workers { get; set; } = new List<Baker>();
        public Entrepreneur(string firstName, string lastName) : base(firstName, lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public void ParachuteJump()
        {
            _countOfJumpes++;
        }

        public void AddWorkers(Baker baker)
        {
            Workers.Add(baker);
        }

    }

}