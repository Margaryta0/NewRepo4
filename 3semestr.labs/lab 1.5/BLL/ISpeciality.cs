using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ISpeciality
    {
        int CountOfJumps { get; }
        void ParachuteJump();
    }
}
