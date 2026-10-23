using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSearchProject_MVC.Models;

namespace JobSearchProject_MVC.Controllers
{
    public class postjobController : Controller
    {
        // GET: postjob
        project_mvcEntities mv = new project_mvcEntities();
        public ActionResult postjob_pageload()
        {
            return View();
        }
        public ActionResult postjob_click(postjob obj)
        { 
            if(ModelState.IsValid)
            {
                int empId = Convert.ToInt32(Session["employer_id"]);
                mv.sp_postJob(empId, obj.title, obj.description, obj.skills,obj.experience, obj.location,obj.salary,obj.job_type);
                obj.msg = "Job Posted Successfully";
            }
            else
            {
                obj.msg = "Error";
            }
            return View("postjob_pageload",obj);
        }
    }
}