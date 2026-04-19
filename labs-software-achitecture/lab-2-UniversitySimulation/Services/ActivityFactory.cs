using UniversitySimulation.Domain;

namespace UniversitySimulation.Services
{
    public static class ActivityFactory
    {
        public enum ActivityType
        {
            Lecture = 1,
            LabWork = 2,
            ModularTest = 3,
            Exam = 4,
            CourseWork = 5,
            Credit = 6
        }

        public static Activity Create(ActivityType type, int hours)
        {
            switch (type)
            {
                case ActivityType.Lecture:     return new Lecture(hours);
                case ActivityType.LabWork:     return new LabWork(hours);
                case ActivityType.ModularTest: return new ModularTest(hours);
                case ActivityType.Exam:        return new Exam(hours);
                case ActivityType.CourseWork:  return new CourseWork(hours);
                case ActivityType.Credit:      return new Credit(hours);
                default:                       return null;
            }
        }

        public static Activity Create(string typeCode, int hours)
        {
            int code;
            if (!int.TryParse(typeCode, out code)) return null;
            if (!System.Enum.IsDefined(typeof(ActivityType), code)) return null;
            return Create((ActivityType)code, hours);
        }
    }
}
