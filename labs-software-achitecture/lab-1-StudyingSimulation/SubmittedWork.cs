using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySimulation
{
    public enum WorkType
    {
        LabWork,
        CourseWork
    }

    public class SubmittedWork
    {
        public WorkType Type { get; private set; }
        public string Description { get; private set; }

        public SubmittedWork(WorkType type, string description)
        {
            Type = type;
            Description = description;
        }
    }
}
