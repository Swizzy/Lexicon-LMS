namespace LexiconLMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LexiconLMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = false;
            ContextKey = "ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var courses = new[]
            {
                new Course { Name = ".NET Vår 2017", Description = "" },
                new Course { Name = "Java Höst 2017", Description="" }
            };
            context.Courses.AddOrUpdate(c => c.Name, courses);
            context.SaveChanges();

            var modules = new[]
            {
                new Module { Name = "C#", Description = ".NET", CourseId = courses[0].Id  },
                new Module { Name = "Webb", Description = "Webbutveckling", CourseId = courses[0].Id },
                new Module { Name = "MVC", Description = "ASP.NET", CourseId = courses[0].Id },
                new Module { Name = "Databas", Description = "SQL", CourseId = courses[0].Id },
                new Module { Name = "Testning", Description = "Test och ledning", CourseId = courses[0].Id },
                new Module { Name = "App.Utv.", Description = "Applikationsutveckling", CourseId = courses[0].Id },
                new Module { Name = "MVC fördj", Description = "MVC fördjupning", CourseId = courses[0].Id },

                new Module { Name = "Java", Description = "Java Enterprice", CourseId = courses[1].Id },
                new Module { Name = "Java II", Description = "Java Enterprice Final Project", CourseId = courses[1].Id },
                new Module { Name = "Database", Description = "Database Management", CourseId = courses[1].Id },
                new Module { Name = "Cert", Description = "Certification Training", CourseId = courses[1].Id },
                new Module { Name = "Testning", Description = "Test och ledning", CourseId = courses[1].Id },
                new Module { Name = "Java adv", Description = "Java Advanced", CourseId = courses[1].Id },
                new Module { Name = "Java fund", Description = "Java Fundamentals", CourseId = courses[1].Id },
            };
            context.Modules.AddOrUpdate(m => new { m.Name, m.CourseId }, modules);
            context.SaveChanges();

            var activityTypes = new[]
            {
                new ActivityType { Name = "Föreläsning" },
                new ActivityType { Name = "E-Learning" },
                new ActivityType { Name = "Övning", IsAssignment = true },
            };
            context.ActivityTypes.AddOrUpdate(at => at.Name, activityTypes);
            context.SaveChanges();

            var activities = new[]
            {
                new Activity
                {
                    Name = "Intro",
                    Description = "",
                    ModuleId = modules[0].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-09 08:30"),
                    EndDate = DateTime.Parse("2017-01-09 17:00")
                },
                new Activity
                {
                    Name = "Grund",
                    Description = "",
                    ModuleId = modules[0].Id,
                    ActivityTypeId = activityTypes[1].Id,
                    StartDate = DateTime.Parse("2017-01-10 08:30"),
                    EndDate = DateTime.Parse("2017-01-10 17:00")
                },
                new Activity
                {
                    Name = "Övning 1",
                    Description = "",
                    ModuleId = modules[0].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-01-11 08:30"),
                    EndDate = DateTime.Parse("2017-01-11 17:00")
                },
                new Activity
                {
                    Name = "OOP",
                    Description = "",
                    ModuleId = modules[0].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-12 08:30"),
                    EndDate = DateTime.Parse("2017-01-12 17:00")
                },
                new Activity
                {
                    Name = "HTML",
                    Description = "",
                    ModuleId = modules[1].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-13 08:30"),
                    EndDate = DateTime.Parse("2017-01-13 12:00")
                },
                new Activity
                {
                    Name = "CSS",
                    Description = "",
                    ModuleId = modules[2].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-13 13:00"),
                    EndDate = DateTime.Parse("2017-01-13 17:00")
                },
                 new Activity
                {
                    Name = "Final Project",
                    Description = "Final Project",
                    ModuleId = modules[2].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-03-17 08:30"),
                    EndDate = DateTime.Parse("2017-04-11 17:00")
                },
                 new Activity
                {
                    Name = "Final Project Sprint I Planning",
                    Description = "Final Project Sprint Planning",
                    ModuleId = modules[2].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-03-17 10:00"),
                    EndDate = DateTime.Parse("2017-03-17 17:00")
                },
                 new Activity
                {
                    Name = "Final Project Demo Sprint I",
                    Description = "Final Project Demo Sprint I",
                    ModuleId = modules[2].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-03-24 10:00"),
                    EndDate = DateTime.Parse("2017-03-24 12:00")
                },
                  new Activity
                {
                    Name = "Final Project Sprint II Planning",
                    Description = "Final Project Sprint II Planning",
                    ModuleId = modules[2].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-03-24 13:00"),
                    EndDate = DateTime.Parse("2017-03-24 17:00")
                },
                 new Activity
                {
                    Name = "Final Project Demo Sprint II",
                    Description = "Final Project Demo Sprint II",
                    ModuleId = modules[2].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-03-31 10:00"),
                    EndDate = DateTime.Parse("2017-03-31 15:00")
                },
                  new Activity
                {
                    Name = "Final Project Sprint III Planning",
                    Description = "Final Project Sprint III Planning",
                    ModuleId = modules[2].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-03-31 15:00"),
                    EndDate = DateTime.Parse("2017-03-31 17:00")
                },
                 new Activity
                {
                    Name = "Final Project Demo Sprint III",
                    Description = "Final Project Demo Sprint III",
                    ModuleId = modules[2].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-04-07 10:00"),
                    EndDate = DateTime.Parse("2017-04-07 14:00")
                },
                  new Activity
                {
                    Name = "Final Project Redovisioning",
                    Description = "Final Project Redovisioning",
                    ModuleId = modules[2].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-04-11 10:00"),
                    EndDate = DateTime.Parse("2017-04-11 17:00")
                },
                new Activity
                {
                    Name = "SQL",
                    Description = "",
                    ModuleId = modules[3].Id,
                    ActivityTypeId = activityTypes[1].Id,
                    StartDate = DateTime.Parse("2017-01-14 08:30"),
                    EndDate = DateTime.Parse("2017-01-14 17:00")
                },
                new Activity
                {
                    Name = "ISTQB",
                    Description = "",
                    ModuleId = modules[4].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-15 08:30"),
                    EndDate = DateTime.Parse("2017-01-17 17:00")
                },
                new Activity
                {
                    Name = "AngularJS",
                    Description = "",
                    ModuleId = modules[5].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-18 08:30"),
                    EndDate = DateTime.Parse("2017-01-18 17:00")
                },
                new Activity
                {
                    Name = "Intro",
                    Description = "",
                    ModuleId = modules[7].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-08-18 08:30"),
                    EndDate = DateTime.Parse("2017-08-18 17:00")
                },
                new Activity
                {
                    Name = "Grund",
                    Description = "",
                    ModuleId = modules[7].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-08-18 08:30"),
                    EndDate = DateTime.Parse("2017-08-18 17:00")
                },
                new Activity
                {
                    Name = "OOP",
                    Description = "",
                    ModuleId = modules[7].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-08-18 08:30"),
                    EndDate = DateTime.Parse("2017-08-18 17:00")
                },
            };
            context.Activities.AddOrUpdate(at => new { at.Name, at.ModuleId } , activities);
            context.SaveChanges();

            //User Accounts and Roles
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var role = new IdentityRole { Name = "Teacher" };
            if (!context.Roles.Any(r => r.Name == role.Name))
            {
                var result = roleManager.Create(role);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors));
                }
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var users = new[]
            {
                new ApplicationUser
                {
                    FirstName = "Adrian",
                    LastName = "Lozano",
                    Email = "zano@gmail.com",
                    UserName = "zano@gmail.com"
                },
                new ApplicationUser
                {
                    FirstName = "Admin",
                    LastName = "",
                    Email = "admin@lexicon.se",
                    UserName = "admin@lexicon.se",
                },
            };

            foreach (var user in users)
            {
                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    var result = userManager.Create(user, "lexicon");
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                    result = userManager.AddToRole(user.Id, "Teacher");
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                }
            }
        }
    }
}
