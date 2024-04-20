using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using Cumulative1.Models;

namespace Cumulative1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher

            public ActionResult Index()
            {
                return View();
            }

            //GET : /Teacher/List
            public ActionResult List()
            {
                TeacherDataController controller = new TeacherDataController();
                IEnumerable<Teacher> Teachers = controller.ListTeachers();
                return View(Teachers);
            }

            //GET : /Teacher/Show/{id}
            public ActionResult Show(int id)
            {
                TeacherDataController controller = new TeacherDataController();
                Teacher NewTeacher = controller.FindTeacher(id);


                return View(NewTeacher);
            }
        //GET : /Teacher/New
        public ActionResult New()
        {

            //shows the New teacher info
            return View();
        }


        //  POST : localhost:xxx/Teacher/Create 
        public ActionResult Create(string teacherfname, string teacherlname, string employeenumber ,decimal salary)
        {

            //debugging statements to confirm successful create method
            Debug.WriteLine(teacherfname);
            Debug.WriteLine(teacherlname);

            TeacherDataController teachercontroller = new TeacherDataController();
            Teacher NewTeacher = new Teacher();
            NewTeacher.teacherfname = teacherfname;
            NewTeacher.teacherlname = teacherlname;
            NewTeacher.employeenumber = employeenumber;
           
            NewTeacher.salary = salary;

            teachercontroller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}

        [HttpPost]
        public ActionResult Delete(int id)
        {
           TeacherDataController teachercontroller = new TeacherDataController();
            teachercontroller.DeleteTeacher(id);
            return RedirectToAction("List");
        }
        public ActionResult Ajax_New()
        {
            return View();
        }




        /// <summary>
        /// Routes to a dynamically generated "teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the teacher</param>
        /// <returns>A dynamic "Update teacher" webpage which provides the current information of the teacher and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/Update/12</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
           Teacher Selectedteacher = controller.FindTeacher(id);

            return View(Selectedteacher);
        }

        public ActionResult Ajax_Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher Selectedteacher = controller.FindTeacher(id);

            return View(Selectedteacher);
        }


        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the teacher to update</param>
        /// <param name="teacherFname">The updated first name of the teacher</param>
        /// <param name="teacherLname">The updated last name of the teacher</param>
        /// <param name="teacherBio">The updated bio of the teacher.</param>
        /// <param name="teacherEmail">The updated email of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /teacher/Update/12
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"teacherFname":"Ritika",
        ///	"teacherLname":"Mehta",
        ///	"employeenumber" : "34",
        ///	"hiredate" : "2024-04-18",
        ///	"salary":"200"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string teacherfname, string teacherlname, string employeenumber, DateTime hiredate, decimal salary)
        {
            Teacher teacherInfo = new Teacher();
            teacherInfo.teacherfname = teacherfname;
            teacherInfo.teacherlname = teacherlname;
            teacherInfo.employeenumber = employeenumber;
            teacherInfo.hiredate = hiredate;
            teacherInfo.salary = salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, teacherInfo);

            return RedirectToAction("Show/" + id);
        }


    }

}