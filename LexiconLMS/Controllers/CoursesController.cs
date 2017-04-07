using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;
using MvcBreadCrumbs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        public CoursesController()
        {
            BreadCrumb.Clear();
            BreadCrumb.Add("/", "Home");
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses
        public ActionResult Index()
        {
            if (User.IsInRole("Teacher"))
            {
                return View(db.Courses.ToList().Select(c => new CourseViewModel(c)));
            }
            var user = db.Users.Find(User.Identity.GetUserId());
            if (user == null)
            {
                return RedirectToAction("LogOff", "Account");
            }
            return RedirectToAction("Details");
        }


        // GET: Courses/Details/5
        public ActionResult Details()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            if (user == null)
            {
                return RedirectToAction("LogOff", "Account");
            }
            var course = db.Courses.Find(user.CourseId);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(new CourseDetailsViewModel(course));
        }

        // GET: Courses/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(CourseCreateViewModel course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(new Course(course));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(new CourseCreateViewModel(course));
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id, CourseCreateViewModel course)
        {
            if (ModelState.IsValid)
            {
                var dbCourse = db.Courses.Find(id);
                if (dbCourse != null)
                {
                    dbCourse.Update(course);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(new CourseDeleteViewModel(course));
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteConfirmed(int id)
        {
            var course = db.Courses.Find(id);
            if (course != null)
            {
                foreach (var doc in course.Documents.ToList())
                {
                    db.Documents.Remove(doc);
                }
                foreach (var module in course.Modules.ToList())
                {
                    foreach (var activity in module.Activities.ToList())
                    {
                        foreach (var doc in activity.Documents.ToList())
                        {
                            db.Documents.Remove(doc);
                        }
                        db.Activities.Remove(activity);
                    }
                    foreach (var doc in module.Documents.ToList())
                    {
                        db.Documents.Remove(doc);
                    }
                    db.Modules.Remove(module);
                }
                db.Courses.Remove(course);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Clone(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(new CourseCloneViewModel(course));
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Clone(int id, CourseCloneViewModel course)
        {
            if (ModelState.IsValid)
            {
                // Find old course
                var oldCourse = db.Courses.FirstOrDefault(c => c.Id == id);
                if (oldCourse == null)
                {
                    return HttpNotFound();
                }

                // Calculate the difference between the old and new start date in days
                var diff = (course.StartDate - oldCourse.StartDate)?.Days ?? 0;
                if (diff > 0)
                    diff += 1;

                // Create and add the new course
                var courseClone = new Course(oldCourse, course);
                db.Courses.Add(courseClone);
                // Save the DB changes to get the ID for the new course
                db.SaveChanges();

                foreach (var document in oldCourse.Documents)
                {
                    db.Documents.Add(new Document(document, courseClone));
                }

                // Fetch modules related to this course
                var modulesClone = oldCourse.Modules.Select(m => new Module(m, courseClone.Id)).ToArray();
                // Store all the old IDs
                var oldModules = oldCourse.Modules.ToArray();

                // Add the new modules
                db.Modules.AddRange(modulesClone);
                // Save the DB changes to get the IDs for the new modules
                db.SaveChanges();

                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                // Loop over the existing ModuleIDs
                for (int i = 0; i < oldModules.Length; i++)
                {
                    // Add Module Documents
                    foreach (var document in oldModules[i].Documents)
                    {
                        db.Documents.Add(new Document(document, modulesClone[i]));
                    }

                    var newModuleId = modulesClone[i].Id;
                    // Fetch all activities related to the ModuleID we're currently working on
                    foreach (var activity in oldModules[i].Activities)
                    {
                        // Create a new Activity with the time difference specified linked to the new module
                        var newActivity = new Activity(activity, newModuleId, diff);
                        // Add the new activity to the database
                        db.Activities.Add(newActivity);
                        // Save the changes so we get the new ActivityId
                        db.SaveChanges();
                        // Fetch all documents associated to the previous ActivityID
                        foreach (var document in activity.Documents)
                        {
                            // We are only interested in teacher uploaded documents, other documents are likely assignments
                            if (userManager.IsInRole(document.UserId, "Teacher")) {
                                db.Documents.Add(new Document(document, newActivity));
                            }
                        }
                    }
                    db.SaveChanges();
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
