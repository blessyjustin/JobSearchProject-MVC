using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobSearchProject_MVC.Models
{
    public class login
    {
        [Required(ErrorMessage = "Enter Username!")]
        public string username { set; get; }
        [Required(ErrorMessage = "Enter Password!")]
        public string password { set; get; }
        public string msg { set; get; }
    }
}