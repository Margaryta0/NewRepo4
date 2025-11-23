using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Entrepreneur : Human, ISpeciality
    {
        private int _countOfJumps = 2;
        public int CountOfJumps => _countOfJumps;

        public List<Baker> Workers { get; set; } = new List<Baker>();

        public Entrepreneur(string firstName, string lastName) : base(firstName, lastName)
        {
        }

        public void ParachuteJump()
        {
            _countOfJumps++;
        }

        public void AddWorker(Baker baker)
        {
            Workers.Add(baker);
        }

        public string GetWorkerList()
        {
            if (!Workers.Any())
            {
                return "Do not have workers.";
            }
            return $"Workers: {string.Join(", ", Workers.Select(w => w.LastName))}";
        }
    }
}
