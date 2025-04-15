using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Shape
    {
        virtual public double GetArea()
        {
            return 0;
        }

        virtual public double GetPerimeter()
        {
            return 0;
        }
    }
}