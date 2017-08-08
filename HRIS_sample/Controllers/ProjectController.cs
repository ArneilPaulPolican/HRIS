using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace HRIS_sample.Controllers
{
    public class ProjectController : Controller
    {
        private Data.HRISDataContext db = new Data.HRISDataContext();
        // GET: Project
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Application()
        {
            return View();
        }
        public ActionResult ApplicationDetail(Int32? Id)
        {

            return View();
        }

    }
}