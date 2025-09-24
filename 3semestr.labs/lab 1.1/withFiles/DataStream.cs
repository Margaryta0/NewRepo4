using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using withEnteties;
using System.Threading;

namespace withFiles
{
    public class DataStream
    {
        private Student[] students;
        public DataStream(Student[] students)
        {
            this.students = students;
        }
        public void CreateFile(string fileName)
        {
            using (StreamWriter ListOfStudents = new StreamWriter(fileName))
            {
                foreach (Student s in students)
                {
                    if (!s.StudentID.IsValidStudentID())
                    {
                        throw new Exception("Invalid student ID!");
                    }
                    ListOfStudents.WriteLine($"Student {s.FirstName} {s.LastName}");
                    ListOfStudents.WriteLine("{");
                    ListOfStudents.WriteLine($"\"firstname\": \"{s.FirstName}\",");
                    ListOfStudents.WriteLine($"\"lastname\": \"{s.LastName}\",");
                    ListOfStudents.WriteLine($"\"studentId\": \"{s.StudentID.FullID}\",");
                    ListOfStudents.WriteLine($"\"birthDate\": \"{s.BirthDate:yyyy-MM-dd}\",");
                    ListOfStudents.WriteLine($"\"course\": {s.Course}");
                    ListOfStudents.WriteLine("}");
                }
            }
        }
        public int ReadStudents(string fileName, Student[] studentsArray)
        {
            int Count = 0;

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                string firstName = "", lastName = "";
                DateTime birthDate = DateTime.MinValue;
                StudentID id = null;
                int course = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (line.StartsWith("\"firstname\""))
                        firstName = line.Split('"')[3];
                    else if (line.StartsWith("\"lastname\""))
                        lastName = line.Split('"')[3];
                    else if (line.StartsWith("\"studentId\""))
                        id = new StudentID(line.Split('"')[3].Substring(0, 2),
                                           int.Parse(line.Split('"')[3].Substring(2)));
                    else if (line.StartsWith("\"birthDate\""))
                        birthDate = DateTime.Parse(line.Split('"')[3]);
                    else if (line.StartsWith("\"course\""))
                        course = int.Parse(line.Split(':')[1].Trim().TrimEnd(','));

                    if (line == "}")
                    {
                        if (course == 4 && birthDate.Month >= 3 && birthDate.Month <= 5)
                        {

                            if (studentsArray != null)
                            {
                                studentsArray[Count] = new Student(firstName, lastName, id, birthDate, course);
                            }
                            Count++;
                        }

                        firstName = lastName = "";
                        birthDate = DateTime.MinValue;
                        id = null;
                        course = 0;
                    }
                }
            }

            return Count;
        }
        public Student[] ReturnSpring4Students(string fileName)
        {
            int count = ReadStudents(fileName, null);
            Student[] ss = new Student[count];
            ReadStudents(fileName, ss);
            return ss;
        }
    }
}
