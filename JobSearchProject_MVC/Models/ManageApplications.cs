using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobSearchProject_MVC.Models
{
    public class ManageApplications
    {
        public int application_id { get; set; }

        public int job_id { get; set; }

        public int jobseeker_id { get; set; }

        public string applicant_name { get; set; }

        public string job_title { get; set; }

        public DateTime? applied_date { get; set; }

        public string application_status { get; set; }

        public string applicant_email { get; set; }

        public string applicant_phone { get; set; }

        public string applicant_gender { get; set; }

        public string applicant_address { get; set; }

        public string applicant_qualification { get; set; }

        public string applicant_specialization { get; set; }

        public int? applicant_passing_year { get; set; }

        public string applicant_experience { get; set; }

        public string applicant_skills { get; set; }

        public string application_resume { get; set; }

    }
}