using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NaxexUDemo.Data;
using Microsoft.AspNetCore.Identity;
using NaxexUDemo.Models;

namespace NaxexUDemo.Controllers
{
    public class EnrollMentData
    {
        public string UserId { get; set; }
        public int CourseId { get; set; }
    }

   [Produces("application/json")]
   [Route("api/[Controller]")]
    public class EnrollmentController : Controller
    {
        private readonly EnrollmentService _enrollmentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EnrollmentController(EnrollmentService enrollmentService, UserManager<ApplicationUser> userManager)
        {
            _enrollmentService = enrollmentService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("EnrollInCourse")]
        public IActionResult EnrollInCourse([FromBody]EnrollMentData data )
        {
             
            if (data==null)
            {
                return new JsonResult("failiure");
            }
            try
            {
               _enrollmentService.EnrollInCourse(data.UserId, data.CourseId);
                return new  JsonResult("success");
            }
            catch (Exception)
            {
                //todo handle exception
                return new JsonResult("failiure");
            }   
        }

        [HttpPost]
        [Route("DropCourse")]
        public IActionResult DropCourse([FromBody]EnrollMentData data)
        {

            if (data == null)
            {
                return new JsonResult("failiure");
            }
            try
            {
                _enrollmentService.DropCourse(data.UserId, data.CourseId);
                return new JsonResult("success");
            }
            catch (Exception)
            {
                //todo handle exception
                return new JsonResult("failiure");
            }
        }



    }
}