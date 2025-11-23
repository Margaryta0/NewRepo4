using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataStream : IDataStream
    {
        public void CreateFile(string fileName, IEnumerable<Student> students)
        {
            using (StreamWriter ListOfStudents = new StreamWriter(fileName))
            {
                foreach (Student s in students)
                {
                    if (!s.StudentID.IsValidStudentID())
                    {
                        throw new Exception($"Invalid student ID for {s.LastName}!");
                    }

                    ListOfStudents.WriteLine("Student {");
                    ListOfStudents.WriteLine($"\"firstname\": \"{s.FirstName}\",");
                    ListOfStudents.WriteLine($"\"lastname\": \"{s.LastName}\",");
                    ListOfStudents.WriteLine($"\"studentId\": \"{s.StudentID.FullID}\",");
                    ListOfStudents.WriteLine($"\"birthDate\": \"{s.BirthDate:yyyy-MM-dd}\",");
                    ListOfStudents.WriteLine($"\"course\": {s.Course}");
                    ListOfStudents.WriteLine("}");
                }
            }
        }

        public List<Student> ReadAllStudents(string fileName)
        {
            List<Student> allStudents = new List<Student>();

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"File {fileName} not found.");
            }

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
                    {
                        string fullId = line.Split('"')[3];
                        id = new StudentID(fullId.Substring(0, 2), int.Parse(fullId.Substring(2)));
                    }
                    else if (line.StartsWith("\"birthDate\""))
                        birthDate = DateTime.Parse(line.Split('"')[3]);
                    else if (line.StartsWith("\"course\""))
                        course = int.Parse(line.Split(':')[1].Trim().TrimEnd(','));

                    if (line == "}")
                    {
                        if (id != null)
                        {
                            allStudents.Add(new Student(firstName, lastName, id, birthDate, course));
                        }

                        firstName = lastName = "";
                        birthDate = DateTime.MinValue;
                        id = null;
                        course = 0;
                    }
                }
            }

            return allStudents;
        }
    }
}
