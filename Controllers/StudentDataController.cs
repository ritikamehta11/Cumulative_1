using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cumulative1.Models;
using MySql.Data.MySqlClient;

namespace Cumulative1.Controllers
{
    public class StudentDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchooldbContext school = new SchooldbContext();

        //This Controller Will access the Students table of our school database.
        /// <summary>
        /// Returns a list of Students in the system
        /// </summary>
        /// <example>GET api/StudentData/ListStudents</example>
        /// <returns>
        /// A list of Students table
        /// </returns>
        [HttpGet]
        [Route("api/StudentData/ListStudents")]
        public IEnumerable<Student> ListStudents()
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Students";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Students
            List<Student> Students = new List<Student> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string studentnumber = ResultSet["studentnumber"].ToString();
                DateTime enroldate = (DateTime)ResultSet["enroldate"];
           


                Student NewStudent = new Student();
                NewStudent.studentid = StudentId;
                NewStudent.studentfname = StudentFname;
                NewStudent.studentlname = StudentLname;
                NewStudent.studentnumber = studentnumber;
                NewStudent.enroldate = enroldate;

                //Add the Student Name to the List
                Students.Add(NewStudent);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Student names
            return Students;
        }




        /// <summary>
        /// Finds an Student in the system given an ID
        /// </summary>
        /// <param name="id">The Student primary key</param>
        /// <returns>An Student object</returns>
        [HttpGet]
        [Route("api/Studentdata/findStudent/{id}")]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Students where Studentid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string studentnumber = ResultSet["studentnumber"].ToString();
                DateTime enroldate = (DateTime)ResultSet["enroldate"];


                NewStudent.studentid = StudentId;
                NewStudent.studentfname = StudentFname;
                NewStudent.studentlname = StudentLname;
                NewStudent.studentnumber = studentnumber;
                NewStudent.enroldate = enroldate;

            }


            return NewStudent;
        }
    }
}
