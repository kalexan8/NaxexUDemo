﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NaxexUDemo.Data;
using NaxexUDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace NaxexUDemo.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICourseRepository _courseRepository;
        private readonly EnrollmentService _enrollmentService;

        public CoursesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, ICourseRepository courseRepository, EnrollmentService enrollmentService)
        {
            _courseRepository = courseRepository;
            _context = context;
            _userManager = userManager;
            _enrollmentService = enrollmentService;
        }

        // GET: Courses
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetCourses();
            if (User.IsInRole("Student"))
            {
                var enrollments = _enrollmentService.GetUserEnrollments(User);
                var result = from course in courses
                             join enrollment in enrollments on course.CourseId equals enrollment.CourseId into enrolled
                             from final in enrolled.DefaultIfEmpty()
                             let isEnrolled = final == null ? false : true
                             select new CourseViewModel(course) {StudentIsEnrolled = isEnrolled };
                return View(result);

            }
            return View(courses.Select(x=>new CourseViewModel(x)));
        }

        // GET: Courses/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await _courseRepository.GetCourseById((int)id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,Title,Description,CourseCapacity,NumberEnrolled,Credits")] Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseRepository.AddCourse(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,Title,Description,CourseCapacity,NumberEnrolled,Credits")] Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _courseRepository.UpdateCourse(course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_courseRepository.CourseExists(course.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _courseRepository.GetCourseById(id);
            if (course != null)
            {
                await _courseRepository.DeleteCourse(course);
            }

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public IActionResult GetMyEnrollments()
        {
            string userId = _userManager.GetUserId(User);
            return ViewComponent("StudentEnrollments", new { studentId = _userManager.GetUserId(User) });
        }

    }
}
