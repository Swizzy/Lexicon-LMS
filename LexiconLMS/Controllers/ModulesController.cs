using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;
using Microsoft.AspNet.Identity;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class ModulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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

            var modules = db.Modules.Where(m => m.CourseId == courseId)
                                    .ToList()
                                    .Select(m => new ModuleViewModel(m));

            return View(view, new ModuleIndexViewModel(course, modules));
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
                dbmodule.Name = module.Name;
                dbmodule.Description = module.Description;
                db.SaveChanges();
                return RedirectToAction("Index", new { courseId = module.CourseId });
            }
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