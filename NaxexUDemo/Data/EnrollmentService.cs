using Microsoft.AspNetCore.Identity;
using NaxexUDemo.Extensions;
using NaxexUDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;

namespace NaxexUDemo.Data
{
    public class EnrollmentService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICourseRepository _courseRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public EnrollmentService(ApplicationDbContext applicationDbContext, ICourseRepository courseRepository, UserManager<ApplicationUser> userManager)
        {
            _dbContext = applicationDbContext;
            _courseRepository = courseRepository;
            _userManager = userManager;
        }

        public IEnumerable<Enrollment> GetUserEnrollments(ClaimsPrincipal user)
        {
            var userId = user.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return _dbContext.Enrollments.Where(x => x.ApplicationUserId == userId);
        }

        public async void EnrollInCourse(string userId, int courseId)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Id == userId);
            var course = _dbContext.Courses.SingleOrDefault(x => x.CourseId == courseId);

            if (user != null && _dbContext.StudentCanEnroll(user, courseId))
            {
                try
                {
                    using (var transaction = _dbContext.Database.BeginTransaction())
                    {
                        user.EnrolledCredits += course.Credits;
                        course.NumberEnrolled += 1;
                        _dbContext.Users.Update(user);
                        _dbContext.Courses.Update(course);
                        await _dbContext.AddAsync(new Enrollment { CourseId = courseId, ApplicationUserId = userId });
                        _dbContext.SaveChanges();
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {

                    //TODO handle error
                }

            }
        }

        public void DropCourse(string userId, int courseId)
        {
            var enrollment = _dbContext.Enrollments.SingleOrDefault(x => x.ApplicationUserId == userId && x.CourseId == courseId);
            var course = _dbContext.Courses.SingleOrDefault(x => x.CourseId == courseId);
            var user = _dbContext.Users.SingleOrDefault(x => x.Id == userId);

            if (enrollment != null && course != null)
            {
                try
                {
                    using (var transaction = _dbContext.Database.BeginTransaction())
                    {
                        user.EnrolledCredits -= course.Credits;
                        course.NumberEnrolled -= 1;
                        _dbContext.Users.Update(user);
                        _dbContext.Courses.Update(course);
                        _dbContext.Remove(enrollment);
                        _dbContext.SaveChanges();
                        transaction.Commit();
                    }
                }
                catch
                {
                    //TODO handle error
                }
            }
        }
    }
}