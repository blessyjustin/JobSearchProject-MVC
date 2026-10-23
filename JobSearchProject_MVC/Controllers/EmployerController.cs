using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSearchProject_MVC.Models;


namespace JobSearchProject_MVC.Controllers
{
    public class EmployerController : Controller
    {
        // GET: Employer
        project_mvcEntities mv = new project_mvcEntities();

        public ActionResult Employer_pageload()
        {
            int employerId = Convert.ToInt32(Session["employer_id"]);

            var company = mv.sp_EmployerCompanyName(employerId).FirstOrDefault();
            ViewBag.CompanyName = company;

            ViewBag.TotalJobsPosted = mv.sp_EmployerTotalJobs(employerId).FirstOrDefault();
            ViewBag.TotalApplications = mv.sp_EmployerApplications(employerId).FirstOrDefault();
            ViewBag.ActiveJobs =mv.sp_EmployerActiveJobs(employerId).FirstOrDefault();
            ViewBag.JobsOnHold = mv.sp_EmployerJobsOnHold(employerId).FirstOrDefault();
            ViewBag.RecentJobs = mv.sp_EmployerRecentJobs(employerId).ToList();
            ViewBag.RecentApplications = mv.sp_EmployerRecentApplications(employerId).ToList();

            return View();
        }
        public ActionResult ManageJobs()
        {
            int empId = Convert.ToInt32(Session["employer_id"]);
            var result = mv.sp_EmployerManageJobs(empId).ToList();
            List<ManageJobs> jobs = new List<ManageJobs>();
            foreach (var j in result)
            {
                ManageJobs obj = new ManageJobs();
                obj.job_id = j.job_id;
                obj.title = j.title;
                obj.location = j.location;
                obj.experience = j.experience;
                obj.job_type = j.job_type;
                obj.salary = j.salary;
                obj.skills = j.skills;
                obj.description = j.description;
                obj.job_status = j.job_status;
                obj.created_date = j.created_date;
                jobs.Add(obj);
            }
            return View(jobs);

        }
        public ActionResult EditJob(int id)
        {
            if (Session["employer_id"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }
            int empId = Convert.ToInt32(Session["employer_id"]);
            var result = mv.sp_EmployerGetJobById(id, empId).FirstOrDefault();
            ManageJobs obj = new ManageJobs();
            obj.job_id = result.job_id;
            obj.title = result.title;
            obj.location = result.location;
            obj.experience = result.experience;
            obj.job_type = result.job_type;
            obj.salary = result.salary;
            obj.skills = result.skills;
            obj.description = result.description;
            obj.job_status = result.job_status;

            return View(obj);
        }

       
        public ActionResult EditJob_click(ManageJobs obj)
        {
            if (Session["employer_id"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }
            if (ModelState.IsValid)
            {
                int empId = Convert.ToInt32(Session["employer_id"]);
                mv.sp_EmployerUpdateJob(obj.job_id, empId,obj.title,obj.description,obj.skills,obj.experience, obj.location,obj.salary,obj.job_type,obj.job_status);
                return RedirectToAction("ManageJobs");
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteJob(int id)
        {
            if (Session["employer_id"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }
            int empId = Convert.ToInt32(Session["employer_id"]);
            mv.sp_EmployerDeleteJob(id, empId);
            return RedirectToAction("ManageJobs");
        }

        public ActionResult ManageApplications()
        {
            if (Session["employer_id"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }   
            int empId = Convert.ToInt32(Session["employer_id"]);
            var result = mv.sp_EmployerManageApplications(empId).ToList();
            List<ManageApplications> applications = new List<ManageApplications>();
            foreach (var a in result)
            {
                ManageApplications obj = new ManageApplications();

                obj.application_id = a.application_id;
                obj.job_id = a.job_id;
                obj.jobseeker_id = a.jobseeker_id;
                obj.applicant_name = a.applicant_name;
                obj.job_title = a.job_title;
                obj.applied_date = a.applied_date;
                obj.application_status = a.application_status;

                applications.Add(obj);
            }
            return View(applications);
        }

        public ActionResult ApplicationDetails(int id)
        {
            if (Session["employer_id"] == null)
                return RedirectToAction("login_pageload", "login");

            int empId = Convert.ToInt32(Session["employer_id"]);
            var result = mv.sp_EmployerApplicationDetails(id, empId).FirstOrDefault();
            ManageApplications obj = new ManageApplications();

            obj.application_id = result.application_id;
            obj.job_id = result.job_id;
            obj.jobseeker_id = result.jobseeker_id;
            obj.job_title = result.job_title;
            obj.applicant_name = result.applicant_name;
            obj.applicant_email = result.applicant_email;
            obj.applicant_phone = result.applicant_phone;
            obj.applicant_gender = result.applicant_gender;
            obj.applicant_address = result.applicant_address;
            obj.applicant_qualification = result.applicant_qualification;
            obj.applicant_specialization = result.applicant_specialization;
            obj.applicant_passing_year = result.applicant_passing_year;
            obj.applicant_experience = result.applicant_experience;
            obj.applicant_skills = result.applicant_skills;
            obj.application_resume = result.application_resume;
            obj.applied_date = result.applied_date;
            obj.application_status = result.application_status;

            return View(obj);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("login_pageload", "login");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateApplicationStatus(int id, string application_status)
        {
            if (Session["employer_id"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }
            int empId = Convert.ToInt32(Session["employer_id"]);
            mv.sp_EmployerUpdateApplicationStatus(id,empId,application_status );
            return RedirectToAction("ApplicationDetails", new { id = id });
        }


        public ActionResult ViewResume(int id)
        {
            if (Session["employer_id"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }
            int empId = Convert.ToInt32(Session["employer_id"]);
            var result = mv.sp_EmployerApplicationDetails(id, empId).FirstOrDefault();
            if (result == null ||String.IsNullOrWhiteSpace(result.application_resume))
            {
                return HttpNotFound("Resume not found.");
            }
            string resumePath = result.application_resume.Replace('\\', '/');
            string fullPath = Server.MapPath(resumePath);
            if (!System.IO.File.Exists(fullPath))
            {
                return Content("Resume file not found: " + fullPath);
            }
            return File(fullPath, "application/pdf");
        }

    }

}
