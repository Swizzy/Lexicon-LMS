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
            var modules = new Module[]
            {
                new Module { Name = "C#", Description = ".NET", CourseId = 1  },
                new Module { Name = "Webb", Description = "Webbutveckling", CourseId = 1 },
                new Module { Name = "MVC", Description = "ASP.NET", CourseId = 1 },
                new Module { Name = "Databas", Description = "SQL", CourseId = 1 },
                new Module { Name = "Testning", Description = "Test och ledning", CourseId = 1 },
                new Module { Name = "App.Utv.", Description = "Applikationsutveckling", CourseId = 1 },
                new Module { Name = "MVC fördj", Description = "MVC fördjupning", CourseId = 1 },

                new Module { Name = "Java", Description = "Java Enterprice", CourseId = 2 },
                new Module { Name = "Java II", Description = "Java Enterprice Final Project", CourseId = 2  },
                new Module { Name = "Database", Description = "Database Management", CourseId = 2 },
                new Module { Name = "Cert", Description = "Certification Training", CourseId = 2 },
                new Module { Name = "Testning", Description = "Test och ledning", CourseId = 2 },
                new Module { Name = "Java adv", Description = "Java Advanced", CourseId = 2 },
                new Module { Name = "Java fund", Description = "Java Fundamentals", CourseId = 2 },
            };

            context.Modules.AddRange(modules);
            context.SaveChanges();

        }
    }
}
