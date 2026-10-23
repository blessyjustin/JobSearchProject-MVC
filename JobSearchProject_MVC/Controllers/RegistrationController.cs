using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSearchProject_MVC.Models;

namespace JobSearchProject_MVC.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            var model = new RegistrationViewModel();
            model.JobSeeker = new jobseekerReg();
            model.Employer = new employerReg();
            return View(model);
        }
    }
}