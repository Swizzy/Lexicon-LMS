using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LexiconLMS.Models;
using Microsoft.AspNet.Identity;
using MvcBreadCrumbs;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class ModulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private void MakeBreadCrumbs(Course course, bool index = false)
        {
            BreadCrumb.Clear();
            BreadCrumb.Add("/", "Home");
            if (course != null)
            {
                if (User.IsInRole("Teacher") || !index)
                {
                    BreadCrumb.Add(Url.Action("Index", "Modules", new { courseId = course.Id }), course.Name);
                }
                else
                {
                    BreadCrumb.Add(Url.Action("Index", "Modules"), "Schedule for " + course.Name);
                }
            }
        }

        // GET: Modules
        public ActionResult Index(int? courseId)
        {
            var view = "Index";
            if (User.IsInRole("Teacher"))
            {
                if (courseId == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                if (user == null)
                    return HttpNotFound();
                courseId = user.CourseId;
                view = "StudentIndex";
            }

            var course = db.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var dbmodules = db.Modules.Where(m => m.CourseId == courseId).ToList();
            var modules = dbmodules.Select(m => new ModuleViewModel(m));

            MakeBreadCrumbs(course, index: true);

            if (!User.IsInRole("Teacher")) {
                var activities = dbmodules.SelectMany(m => m.Activities);
                var data = new List<ModuleIndexStudentViewModel>();
                if (course.StartDate != null)
                {
                    var date = (DateTime)course.StartDate;
                    do
                    {
                        var dayActivities = new List<ActivityScheduleViewModel>();
                        foreach (var activity in activities.Where(a => a.StartDate.DayOfYear <= date.DayOfYear && date.DayOfYear <= a.EndDate.DayOfYear))
                        {
                            dayActivities.Add(new ActivityScheduleViewModel(activity));
                        }

                        if (dayActivities.Count != 0)
                        data.Add(new ModuleIndexStudentViewModel(date, dayActivities));
                        date = date.AddDays(1);
                        if (date.DayOfWeek == DayOfWeek.Saturday) {
                            date = date.AddDays(2);
                        }                        
                    } while (date < course.EndDate);
                }

                return View(view, data);
            }            
            return View(view, new ModuleIndexViewModel(course, modules));
        }

        // GET: Modules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }

            MakeBreadCrumbs(module.Course);

            return View(new ModuleDetailsViewModel(module));
        }

        // GET: Modules/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int? courseId)
        {
            if (courseId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = db.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                return HttpNotFound();

            MakeBreadCrumbs(course);

            return View();
        }

        // POST: Modules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int? courseId, ModuleCreateViewModel module)
        {
            if (courseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                db.Modules.Add(new Module(module, courseId.Value));
                db.SaveChanges();
                return RedirectToAction("Index", new { courseId = courseId });
            }

            MakeBreadCrumbs(db.Courses.Find(courseId));

            return View(module);
        }

        // GET: Modules/Edit/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            MakeBreadCrumbs(module.Course);

            return View(new ModuleCreateViewModel(module));
        }

        // POST: Modules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id, ModuleCreateViewModel module)
        {
            if (ModelState.IsValid)
            {
                var dbmodule = db.Modules.Find(id);
                if (dbmodule == null)
                {
                    return HttpNotFound();
                }
                dbmodule.Update(module);
                db.SaveChanges();
                return RedirectToAction("Index", new { courseId = dbmodule.CourseId });
            }

            MakeBreadCrumbs(db.Courses.Find(id));

            return View(module);
        }

        // GET: Modules/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }

            MakeBreadCrumbs(module.Course);

            return View(new ModuleDeleteViewModel(module));
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteConfirmed(int id)
        {
            var module = db.Modules.Find(id);
            if (module == null)
                return HttpNotFound();

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
            db.SaveChanges();
            return RedirectToAction("Index", new { courseId = module.CourseId });
            
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