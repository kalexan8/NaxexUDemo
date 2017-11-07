using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaxexUDemo.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CourceCapacity { get; set; }

        public int NumberEnrolled { get; set; }

        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
