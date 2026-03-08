using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySimulation
{
    public class LabWork : Activity
    {
        private readonly List<SubGroup> _subGroups = new List<SubGroup>();
        public IReadOnlyList<SubGroup> SubGroups
        {
            get { return _subGroups; }
        }

        private bool _isBlocked = false;

        public LabWork(int hours) : base("Лабораторна робота", hours) { }

        public string DivideIntoSubGroups(Group group, int subGroupCount,
                                           EventHandler externalSizeChanged = null)
        {
            if (group.Students.Count < subGroupCount * 10)
                return "недостатньо студентів. Потрібно мінімум " +
                       (subGroupCount * 10) + ", є " + group.Students.Count;

            foreach (var sg in _subGroups)
            {
                sg.SizeChanged -= OnSubGroupSizeChanged;
                if (externalSizeChanged != null)
                    sg.SizeChanged -= externalSizeChanged;
            }
            _subGroups.Clear();

            var students = new List<Student>(group.Students);
            int baseSize = students.Count / subGroupCount;
            int remainder = students.Count % subGroupCount;
            int startIndex = 0;

            for (int i = 0; i < subGroupCount; i++)
            {
                int size = baseSize + (i < remainder ? 1 : 0);
                var sg = new SubGroup(students.GetRange(startIndex, size));
                startIndex += size;

                sg.SizeChanged += OnSubGroupSizeChanged;

                if (externalSizeChanged != null)
                    sg.SizeChanged += externalSizeChanged;

                _subGroups.Add(sg);
            }

            return null;
        }

        private void OnSubGroupSizeChanged(object sender, EventArgs e)
        {
            _isBlocked = false;
            foreach (var sg in _subGroups)
            {
                if (!sg.IsValid())
                {
                    _isBlocked = true;
                    break;
                }
            }
        }

        public override string GetBlockReason(Group group)
        {
            string baseReason = base.GetBlockReason(group);
            if (baseReason != null) return baseReason;

            if (_subGroups.Count == 0)
                return "не створено жодної підгрупи";

            foreach (var sg in _subGroups)
                if (!sg.IsValid())
                    return "підгрупа має менше 10 студентів (зараз: " + sg.Count + ")";

            if (_isBlocked)
                return "одна з підгруп заблокована через недостатню кількість студентів";

            return null;
        }
    }
}
