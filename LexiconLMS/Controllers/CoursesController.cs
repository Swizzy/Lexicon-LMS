﻿using System.Linq;
using System.Net;
using System.Web.Mvc;
using LexiconLMS.Models;
using MvcBreadCrumbs;

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
            else
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                if (user == null)
                return HttpNotFound();
                //int? courseId = user.CourseId;
                //return RedirectToAction("Index", "Modules");
                return RedirectToAction("Details", "Courses", new { Id = user.CourseId });
            }
        }


        // GET: Courses/Details/5
        public ActionResult Details(int? id)
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
            return View(course);
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
