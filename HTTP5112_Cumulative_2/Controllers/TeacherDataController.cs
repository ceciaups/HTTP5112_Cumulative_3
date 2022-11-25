using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HTTP5112_Cumulative_1.Models;
using MySql.Data.MySqlClient;

namespace HTTP5112_Cumulative_1.Controllers
{
    public class TeacherDataController : Controller
    {
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// This controller will access the teachers table of school database and return a list of teachers in the school database.
        /// </summary>
        /// <returns>A list of teachers (first names and last names)</returns>
        /// <example>GET api/TeacherData/ListTeachers</example>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //cmd.CommandText = "SELECT * FROM teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";
            cmd.CommandText = "SELECT * FROM teachers where lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Teacher> Teachers = new List<Teacher> { };

            while (ResultSet.Read())
            {
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                string HireDate = ResultSet["hiredate"].ToString();
                decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                Teacher MyTeacher = new Teacher();
                MyTeacher.TeacherId = TeacherId;
                MyTeacher.TeacherFname = TeacherFname;
                MyTeacher.TeacherLname = TeacherLname;
                MyTeacher.EmployeeNumber = EmployeeNumber;
                MyTeacher.HireDate = HireDate;
                MyTeacher.Salary = Salary;

                Teachers.Add(MyTeacher);
            }

            Conn.Close();

            return Teachers;
        }

        /// <summary>
        /// This controller will find a teacher in the system given an ID
        /// </summary>
        /// <param name="id">The teacher primary key</param>
        /// <returns>A teacher object</returns>
        /// <example>GET api/TeacherData/FindTeacher</example>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher")]
        public Teacher FindTeacher(int id)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM teachers LEFT OUTER JOIN classes ON classes.teacherid = teachers.teacherid WHERE teachers.teacherid = @id";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            Teacher NewTeacher = new Teacher();
            List<Class> NewClasses = new List<Class> { };

            while (ResultSet.Read())
            {
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                string HireDate = ResultSet["hiredate"].ToString();
                decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                Debug.WriteLine(ResultSet["classid"].ToString());

                if (ResultSet["classid"].ToString() != "")
                {
                    Class NewClass = new Class();
                    int ClassId = Convert.ToInt32(ResultSet["classid"]);
                    string ClassCode = ResultSet["classcode"].ToString();
                    string StartDate = ResultSet["startdate"].ToString();
                    string FinishDate = ResultSet["finishdate"].ToString();
                    string ClassName = ResultSet["classname"].ToString();

                    NewClass.ClassId = ClassId;
                    NewClass.ClassCode = ClassCode;
                    NewClass.TeacherId = TeacherId;
                    NewClass.StartDate = StartDate;
                    NewClass.FinishDate = FinishDate;
                    NewClass.ClassName = ClassName;

                    NewClasses.Add(NewClass);
                }
            }

            NewTeacher.CourseTaught = NewClasses;

            Conn.Close();

            return NewTeacher;
        }
    }
}

