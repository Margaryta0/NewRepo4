using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4
{
    class DNode
    {
        public Student Data { get; set; }
        public DNode Prev { get; set; }   
        public DNode Next { get; set; }   
        public DNode(Student d) { Data = d; }
    }
}
