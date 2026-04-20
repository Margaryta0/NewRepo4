using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_3
{
    class Student
    {
        public string LastName { get; set; }   
        public string FirstName { get; set; }   
        public int Course { get; set; }   
        public string Group { get; set; }  
        public uint StudentID { get; set; }   
        public string Language { get; set; }   

        public Student(string lastName, string firstName, int course,
                       string group, uint studentID, string language)
        {
            LastName  = lastName;
            FirstName = firstName;
            Course    = course;
            Group     = group;
            StudentID = studentID;
            Language  = language;
        }

        public override string ToString()
            => $"{StudentID,8} | {LastName,-12} | {FirstName,-10} | " +
               $"{Course,5} | {Group,-6} | {Language}";
    }
}
