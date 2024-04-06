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

    }
    
}