using System.Linq;
using System.Net;
using System.Web.Mvc;
using LexiconLMS.Models;
using MvcBreadCrumbs;
using Microsoft.AspNet.Identity;

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
                return HttpNotFound();
            return RedirectToAction("Details");
        }


        // GET: Courses/Details/5
        public ActionResult Details()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var course = db.Courses.Find(user?.CourseId);
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
                foreach (var doc in course.Documents)
                {
                    db.Documents.Remove(doc);
                }
                foreach (var module in course.Modules)
                {
                    foreach (var activity in module.Activities)
                    {
                        foreach (var doc in activity.Documents)
                        {
                            db.Documents.Remove(doc);
                        }
                        db.Activities.Remove(activity);
                    }
                    foreach (var doc in module.Documents)
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
