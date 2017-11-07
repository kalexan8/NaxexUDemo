using NaxexUDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaxexUDemo.Data
{
    public interface ICourseRepository
    {
        bool CourseExists(int id);

        Task<List<Course>> GetCourses();
        Task<Course> GetCourseById(int? courseId);
        Task AddCourse(Course course);
        Task UpdateCourse(Course course);
        Task DeleteCourse(Course course);


    }
}
