using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSearchProject_MVC.Models;

namespace JobSearchProject_MVC.Controllers
{
    public class jobseekerRegController : Controller
    {
        // GET: jobseekerReg
        project_mvcEntities mv = new project_mvcEntities();
        public ActionResult jobseekerReg_pageload()
        {
            return View();
        }
        public ActionResult jobseekerReg_click(RegistrationViewModel obj, HttpPostedFileBase file)
        {
            obj.JobSeeker.role = "JobSeeker";
            if (ModelState.IsValid)
            {
                if (file!=null&&file.ContentLength > 0)
                {
                    string fname = Path.GetFileName(file.FileName);
                    var s = Server.MapPath("~/file");
                    string pa = Path.Combine(s, fname);
                    file.SaveAs(pa);
                    var fullpath = Path.Combine("~\\file", fname);
                    obj.JobSeeker.resume = fullpath;
                }
                var maxid = mv.sp_getmaxid().FirstOrDefault();
                int mid = Convert.ToInt32(maxid);
                int regid = 0;
                if (mid == 0)
                {
                    regid = 1;
                }
                else
                {
                    regid = mid + 1;
                }
                mv.sp_JobSeekerReg(regid, obj.JobSeeker.name, obj.JobSeeker.email, obj.JobSeeker.phone,obj.JobSeeker.resume);
                mv.sp_loginsert(regid, obj.JobSeeker.username, obj.JobSeeker.password, obj.JobSeeker.role);

                return RedirectToAction("login_pageload", "login");
            }
            obj.JobSeeker.usermsg = "Registration Unsuccessful";
            return View("~/Views/Registration/Index.cshtml", obj);
        }
    }
}