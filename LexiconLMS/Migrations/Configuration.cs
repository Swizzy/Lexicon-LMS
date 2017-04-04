namespace LexiconLMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
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
                new Module { Name = "Java fund", Description = "Java Fundamentals", CourseId = courses[1].Id }
            };
            context.Modules.AddOrUpdate(m => new { m.Name, m.CourseId }, modules);
            context.SaveChanges();

            var activityTypes = new[]
            {
                new ActivityType { Name = "Föreläsning" },
                new ActivityType { Name = "E-Learning" },
                new ActivityType { Name = "Övning", IsAssignment = true }
            };
            context.ActivityTypes.AddOrUpdate(at => at.Name, activityTypes);
            context.SaveChanges();

            var activities = new[]
            {
                new Activity
                {
                    Name = "Intro",
                    Description = "C# (pronounced C sharp) is a simple, modern, object-oriented, and type-safe programming language. It will immediately be familiar to C and C++ programmers. C# combines the high productivity of Rapid Application Development (RAD) languages and the raw power of C++.",
                    ModuleId = modules[0].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-09 08:30"),
                    EndDate = DateTime.Parse("2017-01-09 17:00")
                },
                new Activity
                {
                    Name = "Grund",
                    Description = "Visual C# .NET is Microsoft's C# development tool. It includes an interactive development environment, visual designers for building Windows and Web applications, a compiler, and a debugger.",
                    ModuleId = modules[0].Id,
                    ActivityTypeId = activityTypes[1].Id,
                    StartDate = DateTime.Parse("2017-01-10 08:30"),
                    EndDate = DateTime.Parse("2017-01-10 17:00")
                },
                new Activity
                {
                    Name = "Övning 1",
                    Description = "C# övning - Flöde via loopar och strängmanipulation. Resultatet av övningen skall visas för lärare och godkännas innan den kan anses vara genomförd.",
                    ModuleId = modules[0].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-01-11 08:30"),
                    EndDate = DateTime.Parse("2017-01-11 17:00")
                },
                new Activity
                {
                    Name = "OOP",
                    Description = "Object-oriented programming (OOP) is a programming language model organized around objects rather than \"actions\" and data rather than logic.",
                    ModuleId = modules[0].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-12 08:30"),
                    EndDate = DateTime.Parse("2017-01-12 17:00")
                },
                new Activity
                {
                    Name = "HTML",
                    Description = "Hypertext Markup Language (HTML) is the standard markup language for creating web pages and web applications. With Cascading Style Sheets (CSS) and JavaScript it forms a triad of cornerstone technologies for the World Wide Web.",
                    ModuleId = modules[1].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-13 08:30"),
                    EndDate = DateTime.Parse("2017-01-13 12:00")
                },
                new Activity
                {
                    Name = "CSS",
                    Description = "Cascading Style Sheets, fondly referred to as CSS, is a simple design language intended to simplify the process of making web pages presentable.",
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
                    Description = "SQL (Structured Query Language) is a standardized programming language used for managing relational databases and performing various operations on the data in them.",
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
                }
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
                }
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

            var students = new[]
            {
                new ApplicationUser
                {
                    FirstName = "Lukas",
                    LastName = "Brodérus",
                    Email = "lukas.broderus@gmail.com",
                    UserName = "lukas.broderus@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Olle",
                    LastName = "Eriksson",
                    Email = "olle.eriksson@gmail.com",
                    UserName = "olle.eriksson@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Alexander",
                    LastName = "Esping",
                    Email = "alexander.esping@gmail.com",
                    UserName = "alexander.esping@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Gustav",
                    LastName = "Ewing",
                    Email = "gustav.ewing@gmail.com",
                    UserName = "gustav.ewing@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Alva",
                    LastName = "Gustafsson",
                    Email = "alva.gustafsson@gmail.com",
                    UserName = "alva.gustafsson@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Filip",
                    LastName = "Haglén",
                    Email = "filip.haglen@gmail.com",
                    UserName = "filip.haglen@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Oliver",
                    LastName = "Hultgren",
                    Email = "oliver.hultgren@gmail.com",
                    UserName = "oliver.hultgren@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Ellinor",
                    LastName = "Nichols Jutterdal",
                    Email = "ellinor.nichols.jutterdal@gmail.com",
                    UserName = "ellinor.nichols.jutterdal@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Agust",
                    LastName = "Linder",
                    Email = "agust.linder@gmail.com",
                    UserName = "agust.linder@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Samuel",
                    LastName = "Lipka",
                    Email = "samuel.lipka@gmail.com",
                    UserName = "samuel.lipka@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Malin",
                    LastName = "Lund",
                    Email = "malin.lund@gmail.com",
                    UserName = "malin.lund@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Anton",
                    LastName = "Nilsson",
                    Email = "anton.nilsson@gmail.com",
                    UserName = "anton.nilsson@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Ida",
                    LastName = "Olsson",
                    Email = "ida.olsson@gmail.com",
                    UserName = "ida.olsson@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Otilia",
                    LastName = "Pettersson Esping",
                    Email = "otilia.pettersson.esping@gmail.com",
                    UserName = "otilia.pettersson.esping@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Rasmus",
                    LastName = "Pettersson",
                    Email = "rasmus.pettersson@gmail.com",
                    UserName = "rasmus.pettersson@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Esther",
                    LastName = "Rosell",
                    Email = "esther.rosell@gmail.com",
                    UserName = "esther.rosell@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Frida",
                    LastName = "Rydell",
                    Email = "frida.rydell@gmail.com",
                    UserName = "frida.rydell@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Houriya",
                    LastName = "Shahin",
                    Email = "houriya.shahin@gmail.com",
                    UserName = "houriya.shahin@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Gabriella",
                    LastName = "Sjöholm",
                    Email = "gabriella.sjoholm@gmail.com",
                    UserName = "gabriella.sjoholm@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Elin",
                    LastName = "Törn",
                    Email = "elin.torn@gmail.com",
                    UserName = "elin.torn@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Edina",
                    LastName = "Wernetorp Sjölin",
                    Email = "edina.wernetorp.sjolin@gmail.com",
                    UserName = "edina.wernetorp.sjolin@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Emelinn",
                    LastName = "Wiberg",
                    Email = "emelinn.wiberg@gmail.com",
                    UserName = "emelinn.wiberg@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    FirstName = "William",
                    LastName = "Wärnström",
                    Email = "william.warnstrom@gmail.com",
                    UserName = "william.warnstrom@gmail.com",
                    CourseId = courses[0].Id,
                },
                new ApplicationUser
                {
                    LastName = "Adolfsson",
                    FirstName = "Vendela",
                    Email = "vendela.adolfsson@gmail.com",
                    UserName = "vendela.adolfsson@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Mohammed - Baraa",
                    LastName = "Al Deek",
                    Email = "mohammed.baraa.al.deek@gmail.com",
                    UserName = "mohammed.baraa.al.deek@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Maja",
                    LastName = "Allvin",
                    Email = "maja.allvin@gmail.com",
                    UserName = "maja.allvin@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Leo",
                    LastName = "Backheden",
                    Email = "leo.backheden@gmail.com",
                    UserName = "leo.backheden@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Rebecca",
                    LastName = "Chewell Ståhl",
                    Email = "rebecca.chewell.stahl@gmail.com",
                    UserName = "rebecca.chewell.stahl@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Elinor",
                    LastName = "Dahlman",
                    Email = "elinor.dahlman@gmail.com",
                    UserName = "elinor.dahlman@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Isac",
                    LastName = "Green",
                    Email = "isac.green@gmail.com",
                    UserName = "isac.green@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Albin",
                    LastName = "Gustafsson",
                    Email = "albin.gustafsson@gmail.com",
                    UserName = "albin.gustafsson@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Kim",
                    LastName = "Gustafsson",
                    Email = "kim.gustafsson@gmail.com",
                    UserName = "kim.gustafsson@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Fatema",
                    LastName = "Hasan",
                    Email = "fatema.hasan@gmail.com",
                    UserName = "fatema.hasan@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Felicia",
                    LastName = "Hedberg Eriksson",
                    Email = "felicia.hedberg.eriksson@gmail.com",
                    UserName = "felicia.hedberg.eriksson@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Elin",
                    LastName = "Henriksson",
                    Email = "elin.henriksson@gmail.com",
                    UserName = "elin.henriksson@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Elvira",
                    LastName = "Henriksson",
                    Email = "elvira.henriksson@gmail.com",
                    UserName = "elvira.henriksson@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Sebastian",
                    LastName = "Karlsson",
                    Email = "sebastian.karlsson@gmail.com",
                    UserName = "sebastian.karlsson@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Sara",
                    LastName = "Kus",
                    Email = "sara.kus@gmail.com",
                    UserName = "sara.kus@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Oskar",
                    LastName = "Linder",
                    Email = "oskar.linder@gmail.com",
                    UserName = "oskar.linder@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Joshan",
                    LastName = "Mohsini",
                    Email = "joshan.mohsini@gmail.com",
                    UserName = "joshan.mohsini@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Joel",
                    LastName = "Nilsson",
                    Email = "joel.nilsson@gmail.com",
                    UserName = "joel.nilsson@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Linus",
                    LastName = "Nilsson",
                    Email = "linus.nilsson@gmail.com",
                    UserName = "linus.nilsson@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Isabelle",
                    LastName = "Norberg",
                    Email = "isabelle.norberg@gmail.com",
                    UserName = "isabelle.norberg@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Taunita",
                    LastName = "Shahini",
                    Email = "taunita.shahini@gmail.com",
                    UserName = "taunita.shahini@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Abdul Hakim",
                    LastName = "Shanab",
                    Email = "abdul.hakim.shanab@gmail.com",
                    UserName = "abdul.hakim.shanab@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Joakim",
                    LastName = "Storm",
                    Email = "joakim.storm@gmail.com",
                    UserName = "joakim.storm@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Viktor",
                    LastName = "Svensson",
                    Email = "viktor.svensson@gmail.com",
                    UserName = "viktor.svensson@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Abdulrahman",
                    LastName = "Taha",
                    Email = "abdulrahman.taha@gmail.com",
                    UserName = "abdulrahman.taha@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Pontus",
                    LastName = "Thorén",
                    Email = "pontus.thoren@gmail.com",
                    UserName = "pontus.thoren@gmail.com",
                    CourseId = courses[1].Id,
                },
                new ApplicationUser
                {
                    FirstName = "Anton",
                    LastName = "Vigren",
                    Email = "anton.vigren@gmail.com",
                    UserName = "anton.vigren@gmail.com",
                    CourseId = courses[1].Id,
                }
            };

            foreach (var user in students)
            {
                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    var result = userManager.Create(user, "lexicon");
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                }
            }

            var documents = new[]
           {
                //Word
                new Document {
                    Name = "Dressyr_LA",
                    FileName = "Dressyr_LA.docx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Content = DocumentSeedData.Dressyr_LA,//System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Dressyr_LA.docx"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 09:42"),
                    CourseId = courses[0].Id,
                    ModuleId = null,
                    ActivityId = null
                },
                //Word
                new Document {
                    Name = "Dressyr_LB",
                    FileName = "Dressyr_LB.docx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Content = DocumentSeedData.Dressyr_LB,//System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Dressyr_LB.docx"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 09:42"),
                    CourseId = courses[0].Id,
                    ModuleId = null,
                    ActivityId = null
                },
                //Word
                new Document {
                    Name = "Dressyr_LC",
                    FileName = "Dressyr_LC.docx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Content = DocumentSeedData.Dressyr_LC,//System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Dressyr_LC.docx"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 09:42"),
                    CourseId = null,
                    ModuleId = modules[0].Id,
                    ActivityId = null
                },
                //Word
                new Document {
                    Name = "Dressyr_msv",
                    FileName = "Dressyr_msv.docx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Content = DocumentSeedData.Dressyr_msv,//System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Dressyr_msv.docx"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 09:42"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[0].Id
                },
                //Excel
                new Document {
                    Name = "Ekipage_LA",
                    FileName = "Ekipage_LA.xlsx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    Content = DocumentSeedData.Ekipage_LA,//System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Ekipage_LA.xlsx"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = courses[0].Id,
                    ModuleId = null,
                    ActivityId = null
                },
                //Excel
                new Document {
                    Name = "Ekipage_LB",
                    FileName = "Ekipage_LB.xlsx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    Content = DocumentSeedData.Ekipage_LB,//System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Ekipage_LB.xlsx"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = courses[0].Id,
                    ModuleId = null,
                    ActivityId = null
                },
                //Excel
                new Document {
                    Name = "Ekipage_LA",
                    FileName = "Ekipage_LA.xlsx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    Content = DocumentSeedData.Ekipage_LA,//System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Ekipage_LA.xlsx"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[1].Id,
                    ActivityId = null
                },
                //Excel
                new Document {
                    Name = "Ekipage_LA",
                    FileName = "Ekipage_LA.xlsx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    Content = DocumentSeedData.Ekipage_LA,//System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Ekipage_LA.xlsx"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[2].Id,
                    ActivityId = null
                },
                //PDF
                new Document {
                    Name = "Övning 5",
                    FileName = "Övning 5 - Garage 1.pdf",
                    Link = null,
                    ContentType = "application/pdf",
                    Content = DocumentSeedData._vning_5___Garage_1,//System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Övning 5 - Garage 1.pdf"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = courses[1].Id,
                    ModuleId = null,
                    ActivityId = null
                },
                //PDF
                new Document {
                    Name = "Övning 8",
                    FileName = "Övning 8- Webb - Front-end och JavaScript.pdf",
                    Link = null,
                    ContentType = "application/pdf",
                    Content = DocumentSeedData._vning_8__Webb___Front_end_och_JavaScript,//System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Övning 8- Webb - Front-end och JavaScript.pdf"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[2].Id,
                    ActivityId = null
                },
                //PDF
                new Document {
                    Name = "Övning 7",
                    FileName = "Övning 7- CSS - Lite skinn på skelettet.pdf",
                    Link = null,
                    ContentType = "application/pdf",
                    Content = DocumentSeedData._vning_7__CSS___Lite_skinn_p__skelettet,//System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Övning 7- CSS - Lite skinn på skelettet.pdf"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[3].Id,
                    ActivityId = null
                },
                //PDF
                new Document {
                    Name = "Övning 5",
                    FileName = "Övning 5 - Garage 1.pdf",
                    Link = null,
                    ContentType = "application/pdf",
                    Content = DocumentSeedData._vning_5___Garage_1,//System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Övning 5 - Garage 1.pdf"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[3].Id
                },
                //PDF
                new Document {
                    Name = "Övning 10",
                    FileName = "Övning 10- Bootstrap 3 - Kaffeparty.pdf",
                    Link = null,
                    ContentType = "application/pdf",
                    Content = DocumentSeedData._vning_10__Bootstrap_3___Kaffeparty,//System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Övning 10- Bootstrap 3 - Kaffeparty.pdf"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[4].Id
                },
                //PDF
                new Document {
                    Name = "Övning 12",
                    FileName = "Övning 12- Garage 2.pdf",
                    Link = null,
                    ContentType = "application/pdf",
                    Content = DocumentSeedData._vning_12__Garage_2,//System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Övning 12- Garage 2.pdf"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[5].Id
                },
                //Link
                new Document {
                    Name = "DN",
                    FileName = null,
                    Link = "http://www.dn.se/",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = courses[1].Id,
                    ModuleId = null,
                    ActivityId = null
                },
                //Link
                new Document {
                    Name = "Marathon",
                    FileName = null,
                    Link = "http://www.marathon.se/",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = courses[1].Id,
                    ModuleId = null,
                    ActivityId = null
                },
                //Link
                new Document {
                    Name = "C# Fundamentals",
                    FileName = null,
                    Link = "https://app.pluralsight.com/library/courses/c-sharp-fundamentals-with-visual-studio-2015",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[1].Id,
                    ActivityId = null
                },
                //Link
                new Document {
                    Name = "C# Best Practices",
                    FileName = null,
                    Link = "https://app.pluralsight.com/library/courses/csharp-best-practices-collections-generics",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[2].Id,
                    ActivityId = null
                },
                //Link
                new Document {
                    Name = "C# Generics",
                    FileName = null,
                    Link = "https://app.pluralsight.com/library/courses/csharp-generics",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[3].Id,
                    ActivityId = null
                },
                //Link
                new Document {
                    Name = "WEBB Front - end",
                    FileName = null,
                    Link = "https://www.codeschool.com/courses/front-end-formations",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[3].Id,
                    ActivityId = null
                },
                //Link
                new Document {
                    Name = "WEBB JavaScript",
                    FileName = null,
                    Link = "https://app.pluralsight.com/library/courses/jscript-fundamentals",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[4].Id,
                    ActivityId = null
                },
                //Link
                new Document {
                    Name = "MVC Building Applications",
                    FileName = null,
                    Link = "https://app.pluralsight.com/library/courses/mvc4-building",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[5].Id,
                    ActivityId = null
                },
                //Link
                new Document {
                    Name = "SQLBOLT",
                    FileName = null,
                    Link = "https://sqlbolt.com",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[3].Id
                },
                //Link
                new Document {
                    Name = "AppUtv Hacking the User",
                    FileName = null,
                    Link = "https://app.pluralsight.com/library/courses/hacking-user-experience",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[4].Id
                },
                //Link
                new Document {
                    Name = "AppUtv AngularJS",
                    FileName = null,
                    Link = "https://app.pluralsight.com/library/courses/angularjs-get-started",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[4].Id
                },
                //Link
                new Document {
                    Name = "MVC 5 ASP.NET MVC 5",
                    FileName = null,
                    Link = "https://app.pluralsight.com/library/courses/aspdotnet-mvc5-fundamentals",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[5].Id
                }
            };

            context.Documents.AddOrUpdate(d => d.Name, documents);
            context.SaveChanges();

        }
    }
}
