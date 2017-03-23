namespace LexiconLMS.Migrations
{
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
                new Course { Name = ".NET V�r 2017", Description = "" },
                new Course { Name = "Java H�st 2017", Description="" }
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
                new Module { Name = "MVC f�rdj", Description = "MVC f�rdjupning", CourseId = courses[0].Id },

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
                new ActivityType { Name = "F�rel�sning" },
                new ActivityType { Name = "E-Learning" },
                new ActivityType { Name = "�vning", IsAssignment = true },
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
                    Name = "�vning 1",
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
                    ModuleId = modules[1].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-13 13:00"),
                    EndDate = DateTime.Parse("2017-01-13 17:00")
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
        }
    }
}
