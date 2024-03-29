﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HTTP5101_Cumulative_Pt1_Natasha_Chambers.Models;
using MySql.Data.MySqlClient;

namespace HTTP5101_Cumulative_Pt1_Natasha_Chambers.Controllers
{
    public class ClassDataController : ApiController
    {
        // Context Class that allows access to the School Database 
        private SchoolDbContext School = new SchoolDbContext();

        // Objective: 
        // Access the data stored in the classes table
        /// <summary>
        ///     Returns a list of the data within the classes table
        ///     in the school database
        /// </summary>
        /// <returns> A list of Classes (code, name, start date, fnish date)</returns>
        /// <example>
        ///     GET api/ClassData/ListClasses
        /// </example>
        [HttpGet]
        [Route("api/ClassData/ListClasses")]
        public IEnumerable<Class> ListClasses()
        {
            // Instance of Connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection between web server and database
            Conn.Open();

            // Create new query for database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "SELECT * FROM classes";

            // Variable thats stores the results from the SQL Querys
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Empty list of Classes
            List<Class> Classes = new List<Class> { };

            // Loop through Result set rows and add to classes lists
            while (ResultSet.Read())
            {
                // New Class Object
                Class NewClass = new Class();

                // Access the Columns in the Classes tab;es
                NewClass.ClassId = Convert.ToInt32(ResultSet["classid"]);
                NewClass.ClassCode = ResultSet["classcode"].ToString();
                NewClass.ClassName = ResultSet["classname"].ToString();
                NewClass.StartDate = Convert.ToDateTime(ResultSet["startdate"]);
                NewClass.FinishDate = Convert.ToDateTime(ResultSet["finishdate"]);

                // Add the NewClass to empty Classes list
                Classes.Add(NewClass);
            }

            // Close Connection
            Conn.Close();

            // Return populated list 
            return Classes;
        }

        /// <summary>
        ///     Returns the information of a class based on the class id
        /// </summary>
        /// <param name="id">An interger</param>
        /// <returns>A Class</returns>
        /// <example> 
        ///     GET api/ClassData/FindClass/9 
        /// </example>
        /// <example>
        ///     GET api/ClassData/FindClass/3
        /// </example>
        [HttpGet]
        [Route("api/ClassData/FindClass/{id}")]
        public Class FindClass(int id)
        {
            // New Class Object
            Class NewClass = new Class();

            // Access School database
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection
            Conn.Open();

            // New query for School database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "SELECT * FROM classes WHERE classid = " + id;

            // Store Query results
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Loop through rows
            while (ResultSet.Read())
            {
                // Access columns in the Classes table
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassCode = ResultSet["classcode"].ToString();
                string ClassName = ResultSet["classname"].ToString();
                DateTime StartDate = Convert.ToDateTime(ResultSet["startdate"]);
                DateTime FinishDate = Convert.ToDateTime(ResultSet["finishdate"]);

                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.ClassName = ClassName;
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;
            }

            // Close connection
            Conn.Close();

            return NewClass;
        }
    }
}
