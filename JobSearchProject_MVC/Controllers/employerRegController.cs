using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSearchProject_MVC.Models;

namespace JobSearchProject_MVC.Controllers
{
    public class employerRegController : Controller
    {
        // GET: employerReg
        project_mvcEntities mv = new project_mvcEntities();
        public ActionResult employerReg_pageload()
        {
            return View();
        }
        public ActionResult employerReg_click(RegistrationViewModel obj)
        {
            obj.Employer.role = "Employer";
            if (ModelState.IsValid)
            {
                var getmaxid = mv.sp_getmaxid().FirstOrDefault();
                int mid = Convert.ToInt32(getmaxid);
                int regid = 0;
                if (mid == 0)
                {
                    regid = 1;
                }
                else
                {
                    regid = mid + 1;
                }
                mv.sp_EmployerReg(regid, obj.Employer.name, obj.Employer.companyname, obj.Employer.email, obj.Employer.phone);
                mv.sp_loginsert(regid, obj.Employer.username, obj.Employer.password, obj.Employer.role);

                return RedirectToAction("login_pageload", "login");
            }
            obj.Employer.empmsg = "Employer Registered Unsuccessfully";
            return View("~/Views/Registration/Index.cshtml", obj);
        }
    }
}