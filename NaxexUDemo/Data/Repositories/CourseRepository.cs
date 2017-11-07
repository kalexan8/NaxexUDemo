using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NaxexUDemo.Models;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace NaxexUDemo.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CourseRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public bool CourseExists(int id)
        {
            return _applicationDbContext.Courses.Any(e => e.CourseId == id);
        }

        public Task<List<Course>> GetCourses()
        {
            return _applicationDbContext.Courses.ToListAsync();
        }

        public Task<Course> GetCourseById(int? courseId)
        {
            return _applicationDbContext.Courses.Where(x => x.CourseId == courseId).SingleOrDefaultAsync();
        }

        public Task AddCourse(Course course)
        {             
            _applicationDbContext.Add(course);
            return _applicationDbContext.SaveChangesAsync();
        }

        public Task UpdateCourse(Course course)
        {
            _applicationDbContext.Update(course);
            return _applicationDbContext.SaveChangesAsync();
        }

        public Task DeleteCourse(Course course)
        {
            _applicationDbContext.Remove(course);
            return _applicationDbContext.SaveChangesAsync();
        }
    }
}
