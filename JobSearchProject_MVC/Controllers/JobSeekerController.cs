using System;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using JobSearchProject_MVC.Models;

namespace JobSearchProject_MVC.Controllers
{
    public class JobSeekerController : Controller
    {
        project_mvcEntities mv = new project_mvcEntities();
        public ActionResult JobSeeker_pageload(string msg,string title,string experience,string salary,string location)
        {
            if (string.IsNullOrWhiteSpace(title) &&string.IsNullOrWhiteSpace(experience) && string.IsNullOrWhiteSpace(salary) && string.IsNullOrWhiteSpace(location))
            {
                ViewBag.alljobs = mv.sp_GetAllJobs().ToList();
            }
            else
            {
                ViewBag.alljobs = mv.sp_SearchJobs(title,experience,salary,location).ToList();
            }

            application model = new application();
            model.msg = msg;

            return View(model);
        }

        public ActionResult ApplyNow(int id)
        {
            if (Session["jobseeker_id"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }
            int jobseekerId = Convert.ToInt32(Session["jobseeker_id"]);
            var job = mv.sp_GetAllJobs().FirstOrDefault(j => j.job_id == id);
            if (job == null)
            {
                return RedirectToAction("JobSeeker_pageload");
            }
            var seeker = mv.jobseeker_tbl.FirstOrDefault(s => s.id == jobseekerId);
            if (seeker == null)
            {
                return RedirectToAction("JobSeeker_pageload");
            }
            var model = new application();
            model.JobId = id;
            model.Name = seeker.Name;
            model.Email = seeker.Email;
            model.Phone = seeker.Phone;

            ViewBag.Job = job;
            ViewBag.JobSeeker = seeker;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitApplication(application model)
        {
            if (Session["jobseeker_id"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }
            int jobseekerId = Convert.ToInt32(Session["jobseeker_id"]);
            var job = mv.sp_GetAllJobs().FirstOrDefault(j => j.job_id == model.JobId);
            var seeker = mv.jobseeker_tbl.FirstOrDefault(s => s.id == jobseekerId);
            if (job == null || seeker == null)
            {
                return RedirectToAction("JobSeeker_pageload");
            }

            ViewBag.Job = job;
            ViewBag.JobSeeker = seeker;

            if (!ModelState.IsValid)
            {
                return View("ApplyNow", model);
            }
            int? count = mv.sp_duplicate(model.JobId, jobseekerId).FirstOrDefault();
            if (count > 0)
            {
                model.msg = "You have already applied for this job.";
                return View("ApplyNow", model);
            }
            string resumePath = null;
            if (model.ResumeFile != null && model.ResumeFile.ContentLength > 0)
            {
                string folder = Server.MapPath("~/UploadedResumes");
                string fileName = Path.GetFileName(model.ResumeFile.FileName);
                string filePath = Path.Combine(folder, fileName);
                model.ResumeFile.SaveAs(filePath);
                resumePath = "~/UploadedResumes/" + fileName;
            }
            if (string.IsNullOrEmpty(resumePath))
            {
                model.msg = "Please upload your resume.";
                return View("ApplyNow", model);
            }
            mv.sp_SubmitApplication(model.JobId, jobseekerId, model.Name, model.Email, model.Phone, model.Gender, model.Address, model.Qualification, model.Specialization, model.PassingYear, model.Experience, model.Skills, resumePath);
            model.msg = "Your application has been submitted successfully!";
            return View("ApplyNow", model);
        }

        public ActionResult MyApplications()
        {
            if (Session["jobseeker_id"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }
            int jobseekerId = Convert.ToInt32(Session["jobseeker_id"]);
            ViewBag.myApplications = mv.sp_viewApplications(jobseekerId).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult CancelApplication(int application_id)
        {
            if (Session["jobseeker_id"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }
            int jobseekerId = Convert.ToInt32(Session["jobseeker_id"]);
            mv.sp_CancelApplication(application_id, jobseekerId);
            return RedirectToAction("MyApplications");
        }

        public new ActionResult Profile()
        {
            if (Session["jobseeker_id"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }
            int jobseekerId = Convert.ToInt32(Session["jobseeker_id"]);
            ViewBag.profile = mv.sp_ViewJobSeekerProfile(jobseekerId).FirstOrDefault();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public new ActionResult Profile( string Name, string Email, string Phone, string Resume, string ProfessionalSummary, string Location, string Skills,string Qualification)
        {
            if (Session["jobseeker_id"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }
            int jobseekerId = Convert.ToInt32(Session["jobseeker_id"]);
            mv.sp_UpdateJobSeekerProfile(jobseekerId,Name, Email, Phone, Resume, ProfessionalSummary, Location, Skills,Qualification );
            return RedirectToAction("Profile");
        }

        public ActionResult ChangePassword(string username,string oldpass,string newpass,string confirmpass)
        {
            if (Session["jobseeker_id"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }
            if (Request.HttpMethod == "POST")
            {
                if (newpass != confirmpass)
                {
                    ViewBag.Message = "Passwords do not match";
                    return View();
                }
                if (newpass == oldpass)
                {
                    ViewBag.Message = "New password must be different from old password";
                    return View();
                }
                ObjectParameter op =new ObjectParameter("status", typeof(int));
                mv.sp_changepass(username, oldpass, newpass, op);
                int status = Convert.ToInt32(op.Value);
                if (status == 1)
                {
                    ViewBag.Message = "Password Changed Successfully";
                    ViewBag.Success = true;
                }
                else
                {
                    ViewBag.Message = "Incorrect old password";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("login_pageload", "login");
        }
    }
}
