using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaxexUDemo.Data;
using NaxexUDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaxexUDemo.ViewComponents
{
    public class StudentEnrollments : ViewComponent
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManagaer;

        public StudentEnrollments(ApplicationDbContext context, UserManager<ApplicationUser> userManagaer)
        {
            _dbContext = context;
            _userManagaer = userManagaer;
        }

        public async Task<IViewComponentResult> InvokeAsync(string studentId)
        {
            var items = await GetEnrollments(studentId);
            return View(items);
        }

        private Task<List<EnrollmentViewModel>> GetEnrollments(string studentId)
        {

            var result = from enrollment in _dbContext.Enrollments.Where(x => x.ApplicationUserId == studentId)
                         join course in _dbContext.Courses on enrollment.CourseId equals course.CourseId
                         select new EnrollmentViewModel(course);

            return result.ToListAsync();
        }
    }
}
