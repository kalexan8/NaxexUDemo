using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaxexUDemo.Models
{
    public class EnrollmentViewModel
    {
        public EnrollmentViewModel() { }

        public EnrollmentViewModel(Course course)
        {
            Title = course.Title;
            Credits = course.Credits;
            NumberEnrolled = course.NumberEnrolled;
            CourseCapacity = course.CourseCapacity;
        }

        public string Title { get; set; }

        public int Credits { get; set; }

        public int NumberEnrolled { get; set; }

        public int CourseCapacity { get; set; }
    }
}
