using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NaxexUDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaxexUDemo.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger)
        {
            if (context.Courses.Any())
            {
                return;
            }
            context.Courses.AddRange(
              new Course[] {
                   new Course{ Title="PE", CourseCapacity=40, Credits=2, Description = "COurse Description"},
                   new Course{ Title="Fundamentals of Software ENgineering", CourseCapacity=30, Credits=2, Description = "COirse Description"},
                   new Course{ Title="Intro to C#", CourseCapacity=20, Credits=3, Description = "COirse Description"},
                   new Course{ Title="Algorithm Design", CourseCapacity=10, Credits=4, Description = "COirse Description"},
                   new Course{ Title="Semantic Web", CourseCapacity=30, Credits=4, Description = "Course Description"},
                   new Course{ Title="Intro to OOP", CourseCapacity=30, Credits=2, Description = "Basic Concepts of OOP" }
              }
               );

            await CreateDefaultUserAndRoleForApplication(userManager, roleManager, logger);
            context.SaveChanges();            
        }


        private static async Task CreateDefaultUserAndRoleForApplication(UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm, ILogger<DbInitializer> logger)
        {
            const string administratorRole = "Administrator";
            const string email = "noreply@your-domain.com";

            await CreateDefaultRole(rm, logger, administratorRole);
            await CreateDefaultRole(rm, logger, "Student");
            var user = await CreateDefaultUser(um, logger, email);           
            await AddDefaultRoleToDefaultUser(um, logger, email, administratorRole, user);
        }

        private static async Task CreateDefaultRole(RoleManager<IdentityRole> rm, ILogger<DbInitializer> logger, string role)
        {
            logger.LogInformation($"Create the role `{role}` for application");
            var ir = await rm.CreateAsync(new IdentityRole(role));
            if (ir.Succeeded)
            {
                logger.LogDebug($"Created the role `{role}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default role `{role}` cannot be created");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static async Task<ApplicationUser> CreateDefaultUser(UserManager<ApplicationUser> um, ILogger<DbInitializer> logger, string email)
        {
            logger.LogInformation($"Create default user with email `{email}` for application");
            var user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString("D"),
                UserName = email,
                NormalizedUserName = um.KeyNormalizer.Normalize(email),
                FirstName = "Xesus",
                LastName = "Krustev",
                Email = email,
                NormalizedEmail = um.KeyNormalizer.Normalize(email),
                EmailConfirmed = true,
                MaxCredits = 999,
                LockoutEnabled = false,
                 
            };
            var passwordHasher = new PasswordHasher<ApplicationUser>();           
            user.PasswordHash = passwordHasher.HashPassword(user, "password");
            var ir = await um.CreateAsync(user);
            if (ir.Succeeded)
            {
                logger.LogDebug($"Created default user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default user `{email}` cannot be created");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }

            var createdUser = await um.FindByEmailAsync(email);
            return createdUser;
        }

        private static async Task AddDefaultRoleToDefaultUser(UserManager<ApplicationUser> um, ILogger<DbInitializer> logger, string email, string administratorRole, ApplicationUser user)
        {
            logger.LogInformation($"Add default user `{email}` to role '{administratorRole}'");
            var ir = await um.AddToRoleAsync(user, administratorRole);
            if (ir.Succeeded)
            {
                logger.LogDebug($"Added the role '{administratorRole}' to default user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"The role `{administratorRole}` cannot be set for the user `{email}`");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static string GetIdentiryErrorsInCommaSeperatedList(IdentityResult ir)
        {
            string errors = null;
            foreach (var identityError in ir.Errors)
            {
                errors += identityError.Description;
                errors += ", ";
            }
            return errors;
        }
    }
}
