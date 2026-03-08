using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySimulation
{
    public class Credit : Activity
    {
        public Credit(int hours) : base("Залік", hours) { }

        public override string GetBlockReason(Group group)
        {
            return null; // залік без умов — завжди можна
        }
    }
}
