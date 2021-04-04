using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HTTP5101_Cumulative_Pt1_Natasha_Chambers.Models;
using MySql.Data.MySqlClient;

namespace HTTP5101_Cumulative_Pt1_Natasha_Chambers.Controllers
{
    public class StudentDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        // Objective:
        // Access the data stored in the students table
        /// <summary>
        ///     Returns list of students in the students table
        ///     within the school database
        /// </summary>
        /// <returns> List of students </returns>
        /// <example>
        ///     GET api/StudentData/ListStudents
        /// </example>
        [HttpGet]
        [Route("api/StudentData/ListStudents")]
        public IEnumerable<Student> ListStudents()
        {
            // Instance of Connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection
            Conn.Open();
            
            // Create new Query for database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "SELECT * FROM students";

            // Store results from Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Empty List of Students
            List<Student> Students = new List<Student> { };

            // Loop through Result Set rows
            while (ResultSet.Read())
            {
                // New Student Object
                Student NewStudent = new Student();

                // Access the Columns in the Students table
                NewStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                NewStudent.StudentFname = ResultSet["studentfname"].ToString();
                NewStudent.StudentLname = ResultSet["studentlname"].ToString();
                NewStudent.StudentNumber = ResultSet["studentnumber"].ToString();
                NewStudent.EnrollDate = Convert.ToDateTime(ResultSet["enroldate"]);

                // Add students to empty list
                Students.Add(NewStudent);
            }

            // Close connection
            Conn.Close();

            return Students;
        }

        /// <summary>
        ///     Returnsthe information of a student based on the student id
        /// </summary>
        /// <param name="id">an interger</param>
        /// <returns> a student</returns>
        /// <example>
        ///     GET api/StudentData/FindStudent/5
        /// </example>
        [HttpGet]
        [Route("api/StudentData/FindStudent/{id}")]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM students WHERE studentid = " + id;

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while(ResultSet.Read())
            {
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                DateTime EnrollDate = Convert.ToDateTime(ResultSet["enroldate"]);

                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrollDate = EnrollDate;
            }

            Conn.Close();

            return NewStudent; 
        }
    }
}
