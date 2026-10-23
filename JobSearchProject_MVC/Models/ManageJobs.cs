using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobSearchProject_MVC.Models
{
    public class ManageJobs
    {
        
        public int job_id { get; set; }

        public string title { get; set; }

        public string location { get; set; }

        public string experience { get; set; }

        public string job_type { get; set; }

        public string salary { get; set; }

        public string skills { get; set; }

        public string description { get; set; }

        public string job_status { get; set; }

        public DateTime? created_date { get; set; }
        }
 }
