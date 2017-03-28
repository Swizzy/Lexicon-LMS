using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;
using MvcBreadCrumbs;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class ModulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private void MakeBreadCrumbs(Course course)
        {
            BreadCrumb.Clear();
            BreadCrumb.Add("/", "Home");
            if (course != null)
                BreadCrumb.Add(Url.Action("Index", "Modules", new { courseId = course.Id }), course.Name);
        }

        // GET: Modules
        public ActionResult Index(int? courseId)
        {
            if (courseId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = db.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            MakeBreadCrumbs(course);

            var modules = db.Modules.Where(m => m.CourseId == courseId)
                                    .ToList()
                                    .Select(m => new ModuleViewModel(m));
            return View(new ModuleIndexViewModel(course, modules));
        }

        // GET: Modules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }

            MakeBreadCrumbs(module.Course);

            return View(new ModuleViewModel(module));
        }

        // GET: Modules/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int? courseId)
        {
            if (courseId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = db.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            MakeBreadCrumbs(course);

            return View(new ModuleSingleViewModel(course));
        }

        // POST: Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(ModuleSingleViewModel module)
        {
            if (ModelState.IsValid)
            {
                db.Modules.Add(new Module()
                {
                    CourseId = module.CourseId,
                    Name = module.Name,
                    Description = module.Description
                });
                db.SaveChanges();
                return RedirectToAction("Index", new { courseId = module.CourseId });
            }

            MakeBreadCrumbs(db.Courses.Find(module.CourseId));

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
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }

            MakeBreadCrumbs(module.Course);

            return View(new ModuleSingleViewModel(module));
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id, ModuleSingleViewModel module)
        {
            if (ModelState.IsValid)
            {
                var dbmodule = db.Modules.Find(id);
                if (dbmodule == null)
                    return HttpNotFound();
                dbmodule.Name = module.Name;
                dbmodule.Description = module.Description;
                db.SaveChanges();
                return RedirectToAction("Index", new { courseId = module.CourseId });
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
            Module module = db.Modules.Find(id);
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