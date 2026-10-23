using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobSearchProject_MVC.Models
{
    public class postjob
    {
        [Required(ErrorMessage ="Enter Job Title")]
        public string title { get; set; }
        [Required(ErrorMessage = "Enter Job Description")]
        public string description { get; set; }

        [Required(ErrorMessage = "Enter Required Skills")]
        public string skills { get; set; }
        [Required(ErrorMessage = "Enter Required Experience")]
        public string experience { get; set; }
        [Required(ErrorMessage = "Enter Location")]
        public string location { get; set; }
        public string salary { get; set; }
        [Required(ErrorMessage = "Enter Job Type")]
        public string job_type { get; set; }
        public string msg { set; get; }
    }
}
