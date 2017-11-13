using Microsoft.AspNetCore.Mvc;
using NaxexUDemo.Data;
using NaxexUDemo.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaxexUDemo.ViewComponents
{
    public class CoursesList : ViewComponent
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesList(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var items = await _courseRepository.GetCourses();
            return View(await _courseRepository.GetCourses());
        }
    }
}
