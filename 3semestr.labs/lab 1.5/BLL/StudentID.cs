using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class StudentID
    {
        public string IDletters { get; set; }
        public int IDnumbers { get; set; }
        public string FullID { get; }

        public StudentID(string IDletters, int IDnumbers)
        {
            this.IDletters = IDletters;
            this.IDnumbers = IDnumbers;

            FullID = IDletters + IDnumbers.ToString("D6"); 
        }

        public bool IsValidStudentID()
        {
            return Regex.IsMatch(FullID, @"^[A-Z]{2}\d{6}$");
        }
    }
}
