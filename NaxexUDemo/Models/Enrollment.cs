using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaxexUDemo.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public string ApplicationUserId { get; set; }

        public int CourseId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Course Course { get; set; }
    }
}
