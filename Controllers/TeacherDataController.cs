﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cumulative1.Models;
using MySql.Data.MySqlClient;

namespace Cumulative1.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchooldbContext school = new SchooldbContext();

            //This Controller Will access the Teachers table of our school database.
            /// <summary>
            /// Returns a list of Teachers in the system
            /// </summary>
            /// <example>GET api/TeacherData/ListTeachers</example>
            /// <returns>
            /// A list of Teachers table
            /// </returns>
            [HttpGet]
            [Route("api/teacherdata/listteachers")]
            public IEnumerable<Teacher> ListTeachers ()
            {
                //Create an instance of a connection
                MySqlConnection Conn = school.AccessDatabase();

                //Open the connection between the web server and database
                Conn.Open();

                //Establish a new command (query) for our database
                MySqlCommand cmd = Conn.CreateCommand();

                //SQL QUERY
                cmd.CommandText = "Select * from Teachers";

                //Gather Result Set of Query into a variable
                MySqlDataReader ResultSet = cmd.ExecuteReader();

                //Create an empty list of Teachers
                List<Teacher> Teachers = new List<Teacher> { };

                //Loop Through Each Row the Result Set
                while (ResultSet.Read())
                {
                    //Access Column information by the DB column name as an index
                    int TeacherId = (int)ResultSet["teacherid"];
                    string TeacherFname = ResultSet["teacherfname"].ToString();
                    string TeacherLname = ResultSet["teacherlname"].ToString();
                    string employeenumber = ResultSet["employeenumber"].ToString();
                    DateTime hiredate = (DateTime)ResultSet["hiredate"];
                    decimal salary = (decimal)ResultSet["salary"];


                    Teacher NewTeacher = new Teacher();
                    NewTeacher.teacherid = TeacherId;
                    NewTeacher.teacherfname = TeacherFname;
                    NewTeacher.teacherlname = TeacherLname;
                    NewTeacher.employeenumber = employeenumber;
                    NewTeacher.hiredate = hiredate;
                    NewTeacher.salary = salary;

                    //Add the Teacher Name to the List
                    Teachers.Add(NewTeacher);
                }

                //Close the connection between the MySQL Database and the WebServer
                Conn.Close();

                //Return the final list of Teacher names
                return Teachers;
            }





        /// <summary>
        /// Finds an Teacher in the system given an ID
        /// </summary>
        /// <param name="id">The Teacher primary key</param>
        /// <returns>An Teacher object</returns>
        [HttpGet]
        [Route("api/teacherdata/findteacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where Teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string employeenumber = ResultSet["employeenumber"].ToString();
                DateTime hiredate = (DateTime)ResultSet["hiredate"];
                decimal salary = (decimal)ResultSet["salary"];

                
                NewTeacher.teacherid = TeacherId;
                NewTeacher.teacherfname = TeacherFname;
                NewTeacher.teacherlname = TeacherLname;
                NewTeacher.employeenumber = employeenumber;
                NewTeacher.hiredate = hiredate;
                NewTeacher.salary = salary;
            }


            return NewTeacher;
        }



        // Add Teacher
        /// <summary>
        /// Recieves the teacher info and adds it into the database
        /// </summary>
        /// <returns>
        /// </returns>
        /// <example>
        /// curl -H "Content-Type: application/json" -d @teacher.json http://localhost:55963/api/TeacherData/addteacher
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Ritika",
        ///	"TeacherLname":"Mehta",
        /// </example>

        [HttpPost]
        [Route("api/TeacherData/AddTeacher")]
        public string AddTeacher([FromBody]Teacher NewTeacher)
        {


            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            Debug.WriteLine(NewTeacher.teacherfname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@teacherfname,@teacherlname,@employeenumber, CURRENT_DATE(), @salary)";
            cmd.Parameters.AddWithValue("@teacherfname", NewTeacher.teacherfname);
            cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.teacherlname);
            cmd.Parameters.AddWithValue("@employeenumber", NewTeacher.employeenumber);
            cmd.Parameters.AddWithValue("@hiredate", NewTeacher.hiredate);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.salary);


            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

           
            return "Teacher Added";
        }



        /// <summary>
        /// Deletes an Teacher from the connected MySQL Database if the ID of that Teacher exists. Does NOT maintain relational integrity. 
        /// </summary>
        /// <param name="id">The ID of the Teacher.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/3</example>
        [HttpPost]
        
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from Teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }

    }

    
}
