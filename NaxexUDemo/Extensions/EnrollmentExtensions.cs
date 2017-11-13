using NaxexUDemo.Data;
using NaxexUDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaxexUDemo.Extensions
{
    public static class EnrollmentExtensions
    {
        public static bool StudentCanEnroll(this ApplicationDbContext dbContext, ApplicationUser user, int courseId)
        {
            var enrollment = dbContext.Enrollments.SingleOrDefault(x => x.CourseId == courseId && x.ApplicationUserId == user.Id);
            var course = dbContext.Courses.SingleOrDefault(x => x.CourseId == courseId && (x.NumberEnrolled + 1 <= x.CourseCapacity));
            if (enrollment == null && course != null && user.EnrolledCredits + course.Credits < user.MaxCredits)
            {
                return true;
            }

            return false;
        }
    }
}
