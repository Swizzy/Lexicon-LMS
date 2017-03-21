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
            var courses = new Course[]
            {
                new Course() { Name = ".NET Vår 2017", Description = "" },
                new Course() { Name = "Java Höst 2017", Description="" }
            };
            context.Courses.AddOrUpdate(c => c.Name, courses);
            context.SaveChanges();

            var modules = new Module[]
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
            context.Modules.AddOrUpdate(m => m.CourseId, modules);
            context.SaveChanges();

        }
    }
}
