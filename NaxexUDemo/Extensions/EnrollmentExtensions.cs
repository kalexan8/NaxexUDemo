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
        public static bool StudentCanEnroll(this ApplicationDbContext dbContext, string userId, int courseId)
        {
            var enrollment = dbContext.Enrollments.SingleOrDefault(x => x.CourseId == courseId && x.ApplicationUserId == userId);
            var course = dbContext.Courses.SingleOrDefault(x => x.CourseId == courseId && (x.NumberEnrolled + 1 <= x.CourceCapacity));
            if (enrollment == null && course != null)
            {
                return true;
            }

            return false;
        }
    }
}
