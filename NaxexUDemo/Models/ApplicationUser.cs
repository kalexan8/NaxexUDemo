using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaxexUDemo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser():base()
        {

        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int MaxCredits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
