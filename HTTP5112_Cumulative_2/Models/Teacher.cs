using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTP5112_Cumulative_1.Models
{
    public class Teacher
    {
        public int TeacherId; 
        public string TeacherFname;
        public string TeacherLname;
        public string EmployeeNumber;
        public string HireDate;
        public decimal Salary;
        public List<Class> CourseTaught;
    }
}

