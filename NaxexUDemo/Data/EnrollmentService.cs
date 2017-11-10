using Microsoft.AspNetCore.Identity;
using NaxexUDemo.Extensions;
using NaxexUDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public async void EnrollInCourse(string userId, int courseId)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Id == userId);
            if (user != null && _dbContext.StudentCanEnroll(userId, courseId))
            {
                try
                {
                    using (var transaction = _dbContext.Database.BeginTransaction())
                    {
                        var course = _dbContext.Courses.SingleOrDefault(x => x.CourseId == courseId);
                        course.NumberEnrolled += 1;
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

            if (enrollment != null && course != null)
            {
                try
                {
                    using (var transaction = _dbContext.Database.BeginTransaction())
                    {
                        course.NumberEnrolled -= 1;
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