using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobSearchProject_MVC.Models
{
    public class RegistrationViewModel
    {
        public jobseekerReg JobSeeker { get; set; }
        public employerReg Employer { get; set; }
    }
}