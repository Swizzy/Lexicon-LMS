using System;
using System.Data.Entity.Migrations;
using System.Linq;
using LexiconLMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Text;

namespace LexiconLMS.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = false;
            ContextKey = "ApplicationDbContext";
        }

        private string RandomPhoneNumber(Random random)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 7; i++)
                sb.Append(random.Next(0, 9));
            return sb.ToString();
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var courses = new[]
            {
                new Course {Name = ".NET Vår 2017", Description = "The .NET framework helps you create mobile, desktop, and web applications that run on Windows PCs, devices and servers"},
                new Course {Name = "Java Höst 2017", Description = "Java is a widely used programming language expressly designed for use in the distributed environment of the internet"}
            };
            context.Courses.AddOrUpdate(c => c.Name, courses);
            context.SaveChanges();

            var modules = new[]
            {
                new Module {Name = "C#", Description = "C# is an elegant and type-safe object-oriented language that enables developers to build a variety of secure and robust applications that run on the .NET Framework", CourseId = courses[0].Id},
                new Module {Name = "Webb", Description = "Build beautiful, interactive websites by learning the fundamentals of HTML, CSS, and JavaScript — three common coding languages on which all modern websites are built", CourseId = courses[0].Id},
                new Module {Name = "MVC", Description = "The Model-View-Controller (MVC) architectural pattern separates an application into three main components: the model, the view, and the controller", CourseId = courses[0].Id},
                new Module {Name = "Database", Description = "SQL (Structured Query Language) is a standardized programming language used for managing relational databases and performing various operations on the data in them", CourseId = courses[0].Id},
                new Module {Name = "Testing", Description = "ISTQB are responsible for developing and maintaining the various software testing syllabi and exams", CourseId = courses[0].Id},
                new Module {Name = "App.Utv.", Description = "Mobile application development is the set of processes and procedures involved in writing software for small, wireless computing devices such as smartphones or tablets", CourseId = courses[0].Id},
                new Module {Name = "MVC fördj", Description = "The ASP.NET MVC framework is a lightweight, highly testable presentation framework that (as with Web Forms-based applications) is integrated with existing ASP.NET features, such as master pages and membership-based authentication", CourseId = courses[0].Id},
                new Module {Name = "Java", Description = "Java Platform, Enterprise Edition or Java EE is a widely used computing platform for development and deployment of enterprise software", CourseId = courses[1].Id},
                new Module {Name = "Java II", Description = "This course extends the basic concepts learnt in Java I and is taught along similar lines.  Topics covered include exception handlers using try, catch and finally blocks, introduction to inheritance, interfaces, abstract and final classes", CourseId = courses[1].Id},
                new Module {Name = "Database", Description = "SQL (Structured Query Language) is a standardized programming language used for managing relational databases and performing various operations on the data in them", CourseId = courses[1].Id},
                new Module {Name = "Cert", Description = "The Foundation Level exam tests for knowledge, not skill. It provides information about the certificate holder’s level of familiarity with the most common concepts of software testing and the associated terminology", CourseId = courses[1].Id},
                new Module {Name = "Testing", Description = "ISTQB are responsible for developing and maintaining the various software testing syllabi and exams", CourseId = courses[1].Id},
                new Module {Name = "Java adv", Description = "Introduction to event handling, the MouseListener interface and the MouseAdapter class, use of the FlowLayout, BorderLayout, GridLayout and GridBagLayout layout managers with both the Abstract Windowing ", CourseId = courses[1].Id},
                new Module {Name = "Java fund", Description = "Toolkit (AWT) and the Swing Toolkit, using dialog windows with applications with more than one frame, adding components to the graphical frame. Assessed work will include the creation of a number of applications with a graphical user interface (GUI)", CourseId = courses[1].Id}
            };
            context.Modules.AddOrUpdate(m => new {m.Name, m.CourseId}, modules);
            context.SaveChanges();

            var activityTypes = new[]
            {
                new ActivityType {Name = "Föreläsning"},
                new ActivityType {Name = "E-Learning"},
                new ActivityType {Name = "Övning", IsAssignment = true}
            };
            context.ActivityTypes.AddOrUpdate(at => at.Name, activityTypes);
            context.SaveChanges();

            var activities = new[]
            {
                new Activity
                {
                    Name = "Intro",
                    Description =
                        "C# (pronounced C sharp) is a simple, modern, object-oriented, and type-safe programming language. It will immediately be familiar to C and C++ programmers. C# combines the high productivity of Rapid Application Development (RAD) languages and the raw power of C++.",
                    ModuleId = modules[0].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-09 08:30"),
                    EndDate = DateTime.Parse("2017-01-09 17:00")
                },
                new Activity
                {
                    Name = "Grund",
                    Description =
                        "Visual C# .NET is Microsoft's C# development tool. It includes an interactive development environment, visual designers for building Windows and Web applications, a compiler, and a debugger.",
                    ModuleId = modules[0].Id,
                    ActivityTypeId = activityTypes[1].Id,
                    StartDate = DateTime.Parse("2017-01-10 08:30"),
                    EndDate = DateTime.Parse("2017-01-10 17:00")
                },
                new Activity
                {
                    Name = "Övning 1",
                    Description =
                        "C# övning - Flöde via loopar och strängmanipulation. Resultatet av övningen skall visas för lärare och godkännas innan den kan anses vara genomförd.",
                    ModuleId = modules[0].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-01-11 08:30"),
                    EndDate = DateTime.Parse("2017-01-11 17:00")
                },
                new Activity
                {
                    Name = "OOP",
                    Description =
                        "Object-oriented programming (OOP) is a programming language model organized around objects rather than \"actions\" and data rather than logic.",
                    ModuleId = modules[0].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-12 08:30"),
                    EndDate = DateTime.Parse("2017-01-12 17:00")
                },
                new Activity
                {
                    Name = "HTML",
                    Description =
                        "Hypertext Markup Language (HTML) is the standard markup language for creating web pages and web applications. With Cascading Style Sheets (CSS) and JavaScript it forms a triad of cornerstone technologies for the World Wide Web.",
                    ModuleId = modules[1].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-13 08:30"),
                    EndDate = DateTime.Parse("2017-01-13 12:00")
                },
                new Activity
                {
                    Name = "CSS",
                    Description =
                        "Cascading Style Sheets, fondly referred to as CSS, is a simple design language intended to simplify the process of making web pages presentable.",
                    ModuleId = modules[2].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-13 13:00"),
                    EndDate = DateTime.Parse("2017-01-13 17:00")
                },
                new Activity
                {
                    Name = "Final Project",
                    Description = "Final Project",
                    ModuleId = modules[6].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-03-17 08:30"),
                    EndDate = DateTime.Parse("2017-04-11 17:00")
                },
                new Activity
                {
                    Name = "Final Project Sprint I Planning",
                    Description = "Final Project Sprint Planning",
                    ModuleId = modules[6].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-03-17 10:00"),
                    EndDate = DateTime.Parse("2017-03-17 17:00")
                },
                new Activity
                {
                    Name = "Final Project Demo Sprint I",
                    Description = "Final Project Demo Sprint I",
                    ModuleId = modules[6].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-03-24 10:00"),
                    EndDate = DateTime.Parse("2017-03-24 12:00")
                },
                new Activity
                {
                    Name = "Final Project Sprint II Planning",
                    Description = "Final Project Sprint II Planning",
                    ModuleId = modules[6].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-03-24 13:00"),
                    EndDate = DateTime.Parse("2017-03-24 17:00")
                },
                new Activity
                {
                    Name = "Final Project Demo Sprint II",
                    Description = "Final Project Demo Sprint II",
                    ModuleId = modules[6].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-03-31 10:00"),
                    EndDate = DateTime.Parse("2017-03-31 15:00")
                },
                new Activity
                {
                    Name = "Final Project Sprint III Planning",
                    Description = "Final Project Sprint III Planning",
                    ModuleId = modules[6].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-03-31 15:00"),
                    EndDate = DateTime.Parse("2017-03-31 17:00")
                },
                new Activity
                {
                    Name = "Final Project Demo Sprint III",
                    Description = "Final Project Demo Sprint III",
                    ModuleId = modules[6].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-04-07 10:00"),
                    EndDate = DateTime.Parse("2017-04-07 14:00")
                },
                new Activity
                {
                    Name = "Final Project Redovisioning",
                    Description = "Final Project Redovisioning",
                    ModuleId = modules[6].Id,
                    ActivityTypeId = activityTypes[2].Id,
                    StartDate = DateTime.Parse("2017-04-11 10:00"),
                    EndDate = DateTime.Parse("2017-04-11 17:00")
                },
                new Activity
                {
                    Name = "SQL",
                    Description =
                        "SQL (Structured Query Language) is a standardized programming language used for managing relational databases and performing various operations on the data in them.",
                    ModuleId = modules[3].Id,
                    ActivityTypeId = activityTypes[1].Id,
                    StartDate = DateTime.Parse("2017-01-14 08:30"),
                    EndDate = DateTime.Parse("2017-01-14 17:00")
                },
                new Activity
                {
                    Name = "ISTQB",
                    Description = "ISTQB revamps product portfolio and releases roadmap",
                    ModuleId = modules[4].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-15 08:30"),
                    EndDate = DateTime.Parse("2017-01-17 17:00")
                },
                new Activity
                {
                    Name = "AngularJS",
                    Description = "AngularJS is a JavaScript framework. It can be added to an HTML page with a script tag",
                    ModuleId = modules[5].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-01-18 08:30"),
                    EndDate = DateTime.Parse("2017-01-18 17:00")
                },
                new Activity
                {
                    Name = "Intro",
                    Description =
                        "C# (pronounced C sharp) is a simple, modern, object-oriented, and type-safe programming language. It will immediately be familiar to C and C++ programmers. C# combines the high productivity of Rapid Application Development (RAD) languages and the raw power of C++.",
                    ModuleId = modules[7].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-08-18 08:30"),
                    EndDate = DateTime.Parse("2017-08-18 17:00")
                },
                new Activity
                {
                    Name = "Grund",
                    Description =
                        "Visual C# .NET is Microsoft's C# development tool. It includes an interactive development environment, visual designers for building Windows and Web applications, a compiler, and a debugger.",
                    ModuleId = modules[7].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-08-18 08:30"),
                    EndDate = DateTime.Parse("2017-08-18 17:00")
                },
                new Activity
                {
                    Name = "OOP",
                    Description =
                        "Object-oriented programming (OOP) is a programming language model organized around objects rather than \"actions\" and data rather than logic.",
                    ModuleId = modules[7].Id,
                    ActivityTypeId = activityTypes[0].Id,
                    StartDate = DateTime.Parse("2017-08-18 08:30"),
                    EndDate = DateTime.Parse("2017-08-18 17:00")
                }
            };
            context.Activities.AddOrUpdate(at => new {at.Name, at.ModuleId}, activities);
            context.SaveChanges();

            //User Accounts and Roles
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var role = new IdentityRole {Name = "Teacher"};
            if (!context.Roles.Any(r => r.Name == role.Name))
            {
                var result = roleManager.Create(role);
                if (!result.Succeeded)
                    throw new Exception(string.Join("\n", result.Errors));
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
                    UserName = "admin@lexicon.se"
                }
            };

            foreach (var user in users)
            {
                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    var result = userManager.Create(user, "lexicon");
                    if (!result.Succeeded)
                        throw new Exception(string.Join("\n", result.Errors));
                    result = userManager.AddToRole(user.Id, "Teacher");
                    if (!result.Succeeded)
                        throw new Exception(string.Join("\n", result.Errors));
                }
                else
                {
                    user.Id = context.Users.First(u => u.UserName == user.UserName).Id;
                }
            }

            var random = new Random();

            var students = new[]
            {
                new ApplicationUser
                {
                    FirstName = "Lukas",
                    LastName = "Brodérus",
                    Email = "lukas.broderus@gmail.com",
                    UserName = "lukas.broderus@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Olle",
                    LastName = "Eriksson",
                    Email = "olle.eriksson@gmail.com",
                    UserName = "olle.eriksson@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Alexander",
                    LastName = "Esping",
                    Email = "alexander.esping@gmail.com",
                    UserName = "alexander.esping@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Gustav",
                    LastName = "Ewing",
                    Email = "gustav.ewing@gmail.com",
                    UserName = "gustav.ewing@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Alva",
                    LastName = "Gustafsson",
                    Email = "alva.gustafsson@gmail.com",
                    UserName = "alva.gustafsson@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Filip",
                    LastName = "Haglén",
                    Email = "filip.haglen@gmail.com",
                    UserName = "filip.haglen@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Oliver",
                    LastName = "Hultgren",
                    Email = "oliver.hultgren@gmail.com",
                    UserName = "oliver.hultgren@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Ellinor",
                    LastName = "Nichols Jutterdal",
                    Email = "ellinor.nichols.jutterdal@gmail.com",
                    UserName = "ellinor.nichols.jutterdal@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Agust",
                    LastName = "Linder",
                    Email = "agust.linder@gmail.com",
                    UserName = "agust.linder@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Samuel",
                    LastName = "Lipka",
                    Email = "samuel.lipka@gmail.com",
                    UserName = "samuel.lipka@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Malin",
                    LastName = "Lund",
                    Email = "malin.lund@gmail.com",
                    UserName = "malin.lund@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Anton",
                    LastName = "Nilsson",
                    Email = "anton.nilsson@gmail.com",
                    UserName = "anton.nilsson@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Ida",
                    LastName = "Olsson",
                    Email = "ida.olsson@gmail.com",
                    UserName = "ida.olsson@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Otilia",
                    LastName = "Pettersson Esping",
                    Email = "otilia.pettersson.esping@gmail.com",
                    UserName = "otilia.pettersson.esping@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Rasmus",
                    LastName = "Pettersson",
                    Email = "rasmus.pettersson@gmail.com",
                    UserName = "rasmus.pettersson@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Esther",
                    LastName = "Rosell",
                    Email = "esther.rosell@gmail.com",
                    UserName = "esther.rosell@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Frida",
                    LastName = "Rydell",
                    Email = "frida.rydell@gmail.com",
                    UserName = "frida.rydell@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Houriya",
                    LastName = "Shahin",
                    Email = "houriya.shahin@gmail.com",
                    UserName = "houriya.shahin@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Gabriella",
                    LastName = "Sjöholm",
                    Email = "gabriella.sjoholm@gmail.com",
                    UserName = "gabriella.sjoholm@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Elin",
                    LastName = "Törn",
                    Email = "elin.torn@gmail.com",
                    UserName = "elin.torn@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Edina",
                    LastName = "Wernetorp Sjölin",
                    Email = "edina.wernetorp.sjolin@gmail.com",
                    UserName = "edina.wernetorp.sjolin@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Emelinn",
                    LastName = "Wiberg",
                    Email = "emelinn.wiberg@gmail.com",
                    UserName = "emelinn.wiberg@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "William",
                    LastName = "Wärnström",
                    Email = "william.warnstrom@gmail.com",
                    UserName = "william.warnstrom@gmail.com",
                    CourseId = courses[0].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    LastName = "Adolfsson",
                    FirstName = "Vendela",
                    Email = "vendela.adolfsson@gmail.com",
                    UserName = "vendela.adolfsson@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Mohammed - Baraa",
                    LastName = "Al Deek",
                    Email = "mohammed.baraa.al.deek@gmail.com",
                    UserName = "mohammed.baraa.al.deek@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Maja",
                    LastName = "Allvin",
                    Email = "maja.allvin@gmail.com",
                    UserName = "maja.allvin@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Leo",
                    LastName = "Backheden",
                    Email = "leo.backheden@gmail.com",
                    UserName = "leo.backheden@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Rebecca",
                    LastName = "Chewell Ståhl",
                    Email = "rebecca.chewell.stahl@gmail.com",
                    UserName = "rebecca.chewell.stahl@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Elinor",
                    LastName = "Dahlman",
                    Email = "elinor.dahlman@gmail.com",
                    UserName = "elinor.dahlman@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Isac",
                    LastName = "Green",
                    Email = "isac.green@gmail.com",
                    UserName = "isac.green@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Albin",
                    LastName = "Gustafsson",
                    Email = "albin.gustafsson@gmail.com",
                    UserName = "albin.gustafsson@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Kim",
                    LastName = "Gustafsson",
                    Email = "kim.gustafsson@gmail.com",
                    UserName = "kim.gustafsson@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Fatema",
                    LastName = "Hasan",
                    Email = "fatema.hasan@gmail.com",
                    UserName = "fatema.hasan@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Felicia",
                    LastName = "Hedberg Eriksson",
                    Email = "felicia.hedberg.eriksson@gmail.com",
                    UserName = "felicia.hedberg.eriksson@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Elin",
                    LastName = "Henriksson",
                    Email = "elin.henriksson@gmail.com",
                    UserName = "elin.henriksson@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Elvira",
                    LastName = "Henriksson",
                    Email = "elvira.henriksson@gmail.com",
                    UserName = "elvira.henriksson@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Sebastian",
                    LastName = "Karlsson",
                    Email = "sebastian.karlsson@gmail.com",
                    UserName = "sebastian.karlsson@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Sara",
                    LastName = "Kus",
                    Email = "sara.kus@gmail.com",
                    UserName = "sara.kus@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Oskar",
                    LastName = "Linder",
                    Email = "oskar.linder@gmail.com",
                    UserName = "oskar.linder@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Joshan",
                    LastName = "Mohsini",
                    Email = "joshan.mohsini@gmail.com",
                    UserName = "joshan.mohsini@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Joel",
                    LastName = "Nilsson",
                    Email = "joel.nilsson@gmail.com",
                    UserName = "joel.nilsson@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Linus",
                    LastName = "Nilsson",
                    Email = "linus.nilsson@gmail.com",
                    UserName = "linus.nilsson@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Isabelle",
                    LastName = "Norberg",
                    Email = "isabelle.norberg@gmail.com",
                    UserName = "isabelle.norberg@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Taunita",
                    LastName = "Shahini",
                    Email = "taunita.shahini@gmail.com",
                    UserName = "taunita.shahini@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Abdul Hakim",
                    LastName = "Shanab",
                    Email = "abdul.hakim.shanab@gmail.com",
                    UserName = "abdul.hakim.shanab@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Joakim",
                    LastName = "Storm",
                    Email = "joakim.storm@gmail.com",
                    UserName = "joakim.storm@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Viktor",
                    LastName = "Svensson",
                    Email = "viktor.svensson@gmail.com",
                    UserName = "viktor.svensson@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Abdulrahman",
                    LastName = "Taha",
                    Email = "abdulrahman.taha@gmail.com",
                    UserName = "abdulrahman.taha@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Pontus",
                    LastName = "Thorén",
                    Email = "pontus.thoren@gmail.com",
                    UserName = "pontus.thoren@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                },
                new ApplicationUser
                {
                    FirstName = "Anton",
                    LastName = "Vigren",
                    Email = "anton.vigren@gmail.com",
                    UserName = "anton.vigren@gmail.com",
                    CourseId = courses[1].Id,
                    PhoneNumber = "070-" + RandomPhoneNumber(random)
                }
            };

            foreach (var user in students)
            {
                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    var result = userManager.Create(user, "lexicon");
                    if (!result.Succeeded)
                        throw new Exception(string.Join("\n", result.Errors));
                }
                else
                {
                    user.Id = context.Users.First(u => u.UserName == user.UserName).Id;
                }
            }

            var documents = new[]
            {
                //Word .NET course document
                new Document
                {
                    Name = "Getting started with Visual Studio",
                    FileName = "Getting started with Visual Studio.docx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Content = DocumentSeedData.Dressyr_LA,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Dressyr_LA.docx"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 09:42"),
                    CourseId = courses[0].Id,
                    ModuleId = null,
                    ActivityId = null
                },
                //Word .NET course document
                new Document
                {
                    Name = "Building a C# application",
                    FileName = "Building a C# application.docx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Content = DocumentSeedData.Dressyr_LB,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Dressyr_LB.docx"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 09:42"),
                    CourseId = courses[0].Id,
                    ModuleId = null,
                    ActivityId = null
                },
                //Word
                new Document
                {
                    Name = "Developer's Best Practices",
                    FileName = "Developer's Best Practices.docx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Content = DocumentSeedData.Dressyr_LC,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Dressyr_LC.docx"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 09:42"),
                    CourseId = null,
                    ModuleId = modules[0].Id,
                    ActivityId = null
                },
                //Word
                new Document
                {
                    Name = "Building a C# class library",
                    FileName = "Building a C# class library.docx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Content = DocumentSeedData.Dressyr_msv,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Dressyr_msv.docx"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 09:42"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[0].Id
                },
                //Excel .NET course document
                new Document
                {
                    Name = "Learn about GitHub",
                    FileName = "Learn about GitHub.xlsx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    Content = DocumentSeedData.Ekipage_LA,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Ekipage_LA.xlsx"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = courses[0].Id,
                    ModuleId = null,
                    ActivityId = null
                },
                //Excel .NET course document
                new Document
                {
                    Name = ".NET tooling and common infrastructure",
                    FileName = ".NET tooling and common infrastructure.xlsx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    Content = DocumentSeedData.Ekipage_LB,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Ekipage_LB.xlsx"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = courses[0].Id,
                    ModuleId = null,
                    ActivityId = null
                },
                //PDF
                new Document
                {
                    Name = "Övning 5",
                    FileName = "Övning 5 - Garage 1.pdf",
                    Link = null,
                    ContentType = "application/pdf",
                    Content = DocumentSeedData._vning_5___Garage_1,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Övning 5 - Garage 1.pdf"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = courses[1].Id,
                    ModuleId = null,
                    ActivityId = null
                },
                //PDF
                new Document
                {
                    Name = "Övning 8",
                    FileName = "Övning 8- Webb - Front-end och JavaScript.pdf",
                    Link = null,
                    ContentType = "application/pdf",
                    Content = DocumentSeedData._vning_8__Webb___Front_end_och_JavaScript,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Övning 8- Webb - Front-end och JavaScript.pdf"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[2].Id,
                    ActivityId = null
                },
                //PDF
                new Document
                {
                    Name = "Övning 7",
                    FileName = "Övning 7- CSS - Lite skinn på skelettet.pdf",
                    Link = null,
                    ContentType = "application/pdf",
                    Content = DocumentSeedData._vning_7__CSS___Lite_skinn_p__skelettet,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Övning 7- CSS - Lite skinn på skelettet.pdf"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[3].Id,
                    ActivityId = null
                },
                //PDF
                new Document
                {
                    Name = "Övning 3",
                    FileName = "Övning 3 - Inkapsling arv och polymorfism.pdf",
                    Link = null,
                    ContentType = "application/pdf",
                    Content = DocumentSeedData._vning_3_C____Inkapsling__arv_och_polymorfism__2_,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[3].Id
                },
                //PDF
                new Document
                {
                    Name = "Övning 10",
                    FileName = "Övning 10- Bootstrap 3 - Kaffeparty.pdf",
                    Link = null,
                    ContentType = "application/pdf",
                    Content = DocumentSeedData._vning_10__Bootstrap_3___Kaffeparty,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Övning 10- Bootstrap 3 - Kaffeparty.pdf"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[4].Id
                },
                //PDF
                new Document
                {
                    Name = "Övning 12",
                    FileName = "Övning 12- Garage 2.pdf",
                    Link = null,
                    ContentType = "application/pdf",
                    Content = DocumentSeedData._vning_12__Garage_2,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Övning 12- Garage 2.pdf"),
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[5].Id
                },
                //Link
                new Document
                {
                    Name = "Java Tutorial",
                    FileName = null,
                    Link = "https://www.tutorialspoint.com/java/",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = courses[1].Id,
                    ModuleId = null,
                    ActivityId = null
                },
                //Link
                new Document
                {
                    Name = "Java SE Documentation - Tutorials - Oracle",
                    FileName = null,
                    Link = "http://www.oracle.com/technetwork/java/javase/documentation/tutorials-jsp-138802.html",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = courses[1].Id,
                    ModuleId = null,
                    ActivityId = null
                },
                //Link
                new Document
                {
                    Name = "Visual Studio Download",
                    FileName = null,
                    Link = "https://www.visualstudio.com/downloads/",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[1].Id,
                    ActivityId = null
                },
                //Link
                new Document
                {
                    Name = "Building Applications with ASP.NET MVC 4",
                    FileName = null,
                    Link = "https://app.pluralsight.com/library/courses/mvc4-building",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[2].Id,
                    ActivityId = null
                },
                //Link
                new Document
                {
                    Name = "Practical HTML5",
                    FileName = null,
                    Link = "https://app.pluralsight.com/library/courses/practical-html5",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[3].Id,
                    ActivityId = null
                },
                //Link WEBB
                new Document
                {
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
                //Link Testning
                new Document
                {
                    Name = "A Journey to Automated Testing in ASP.NET MVC",
                    FileName = null,
                    Link = "https://www.pluralsight.com/courses/confident-coding-automated-testing-aspdotnet-mvc",
                    ContentType = null,
                    Content = null,
                    UserId = users[0].Id,
                    CreateDate = DateTime.Parse("2017-01-09 08:30"),
                    CourseId = null,
                    ModuleId = modules[4].Id,
                    ActivityId = null
                },
                //Link
                new Document
                {
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
                new Document
                {
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
                new Document
                {
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
                new Document
                {
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
                new Document
                {
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

            context.Documents.RemoveRange(context.Documents.ToArray());
            context.Documents.AddRange(documents);

            var assignments = new[]
            {
                //Assignment in Word
                new Document
                {
                    Name = null,
                    FileName = "Assignment01.docx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Content = DocumentSeedData.Assignment01,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Assignment01.docx"),
                    UserId = students[0].Id,
                    CreateDate = DateTime.Parse("2017-03-30 17:22"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[6].Id // 1, 7, 8, 9, 10, 11, 12, 13, 14
                },
                //Assignment in Word
                new Document
                {
                    Name = null,
                    FileName = "Assignment02.docx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Content = DocumentSeedData.Assignment02,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Assignment02.docx"),
                    UserId = students[3].Id,
                    CreateDate = DateTime.Parse("2017-03-30 17:22"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[6].Id // 1, 7, 8, 9, 10, 11, 12, 13, 14
                },
                //Assignment in Word
                new Document
                {
                    Name = null,
                    FileName = "Assignment03.docx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Content = DocumentSeedData.Assignment03,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Assignment03.docx"),
                    UserId = students[3].Id,
                    CreateDate = DateTime.Parse("2017-03-30 17:22"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[12].Id // 1, 7, 8, 9, 10, 11, 12, 13, 14
                },
                //Assignment in Word
                new Document
                {
                    Name = null,
                    FileName = "Assignment04.docx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Content = DocumentSeedData.Assignment04,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Assignment04.docx"),
                    UserId = students[2].Id,
                    CreateDate = DateTime.Parse("2017-03-30 17:22"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[12].Id // 1, 7, 8, 9, 10, 11, 12, 13, 14
                },
                //Assignment in Word
                new Document
                {
                    Name = null,
                    FileName = "Assignment05.docx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Content = DocumentSeedData.Assignment05,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Assignment05.docx"),
                    UserId = students[4].Id,
                    CreateDate = DateTime.Parse("2017-03-30 17:22"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[13].Id // 1, 7, 8, 9, 10, 11, 12, 13, 14
                },
                //Assignment in Word
                new Document
                {
                    Name = null,
                    FileName = "Assignment06.docx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Content = DocumentSeedData.Assignment06,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Assignment06.docx"),
                    UserId = students[5].Id,
                    CreateDate = DateTime.Parse("2017-03-30 17:22"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[13].Id // 1, 7, 8, 9, 10, 11, 12, 13, 14
                },
                //Assignment in Word
                new Document
                {
                    Name = null,
                    FileName = "Assignment07.docx",
                    Link = null,
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Content = DocumentSeedData.Assignment07,
                    //System.IO.File.ReadAllBytes("C:\\Users\\User\\Desktop\\DocumentSeed\\Assignment07.docx"),
                    UserId = students[7].Id,
                    CreateDate = DateTime.Parse("2017-03-30 17:22"),
                    CourseId = null,
                    ModuleId = null,
                    ActivityId = activities[13].Id // 1, 7, 8, 9, 10, 11, 12, 13, 14
                }
            };
            context.Documents.AddRange(assignments);

            context.SaveChanges();
        }
    }
}
