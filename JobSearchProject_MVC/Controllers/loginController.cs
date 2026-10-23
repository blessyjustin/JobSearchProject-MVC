using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSearchProject_MVC.Models;

namespace JobSearchProject_MVC.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        project_mvcEntities mv = new project_mvcEntities();
        public ActionResult login_pageload()
        {
            return View();
        }
        public ActionResult login_click(login obj)
        {
            if (ModelState.IsValid)
            {
                var cid = mv.sp_countlogid(obj.username, obj.password).First();
                if (cid >0)
                {
                    var uid = mv.sp_regid(obj.username, obj.password).FirstOrDefault();
                    Session["uid"] = uid;

                    var logty = mv.sp_logtype(obj.username, obj.password).FirstOrDefault();
                    if (logty == "JobSeeker")
                    {
                        Session["jobseeker_id"] = uid;
                        return RedirectToAction("JobSeeker_pageload", "JobSeeker");
                    }
                    else
                    {
                        Session["employer_id"] = uid;
                        return RedirectToAction("Employer_pageload", "Employer");

                    }
                }
                else
                {
                    ModelState.Clear();
                    obj.msg = "Invalid Username and Password";
                    return View("login_pageload", obj);
                }
            }
            else
            {
                ModelState.Clear();
                obj.msg = "Invalid login";
                return View("login_pageload", obj);
            }
        }
    }
}