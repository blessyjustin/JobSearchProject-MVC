using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobSearchProject_MVC.Models
{
    public class employerReg
    {
        
        public string role { set; get; }
        [Required(ErrorMessage = "Enter Name")]
        public string name { set; get; }
        [Required(ErrorMessage = "Enter Company Name")]
        public string companyname { set; get; }
        [Required(ErrorMessage = "Enter Phone Number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter Valid Number")]
        public string phone { set; get; }
        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress(ErrorMessage = "Enter Valid Email")]
        public string email { set; get; }
        [Required(ErrorMessage = "Enter Username")]
        public string username { set; get; }
        [Required(ErrorMessage = "Enter Password")]
        public string password { set; get; }
        [Required(ErrorMessage = "Enter Confirm Password")]
        [Compare("password", ErrorMessage = "Password Mismatch")]
        public string confirmpass { set; get; }
        public string empmsg { set; get; }
    }
}