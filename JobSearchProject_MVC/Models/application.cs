using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobSearchProject_MVC.Models
{
    public class application
    {
        public int JobId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public string Qualification { get; set; }

        public string Specialization { get; set; }

        public int? PassingYear { get; set; }

        public string Experience { get; set; }

        public string Skills { get; set; }

        public HttpPostedFileBase ResumeFile { get; set; }

        public string msg { get; set; }
    }
}
