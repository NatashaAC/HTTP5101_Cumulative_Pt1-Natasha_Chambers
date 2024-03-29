﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTP5101_Cumulative_Pt1_Natasha_Chambers.Models;

namespace HTTP5101_Cumulative_Pt1_Natasha_Chambers.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class/List
        public ActionResult List()
        {
            // Instantiating
            ClassDataController controller = new ClassDataController();
            IEnumerable<Class> Classes = controller.ListClasses();

            return View(Classes);
        }

        // GET: Class/Show
        public ActionResult Show(int id)
        {
            // Instantiating 
            ClassDataController controller = new ClassDataController();
            Class NewClass = controller.FindClass(id);

            return View(NewClass);
        }
    }
}