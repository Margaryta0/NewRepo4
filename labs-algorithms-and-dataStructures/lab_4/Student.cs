using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4
{
    class Student
    {
        public string LastName { get; set; }   // Прізвище
        public string FirstName { get; set; }   // Ім'я
        public uint TaxCode { get; set; }   // Ідентифікаційний код (ключ)
        public string City { get; set; }   // Місце проживання

        public Student(string ln, string fn, uint code, string city)
        {
            LastName  = ln;
            FirstName = fn;
            TaxCode   = code;
            City      = city;
        }

        public override string ToString()
            => $"{TaxCode,12} | {LastName,-12} | {FirstName,-10} | {City}";
    }
}
