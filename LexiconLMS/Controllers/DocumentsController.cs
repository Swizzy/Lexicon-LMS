using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LexiconLMS.Models;
using Microsoft.AspNet.Identity;
using MvcBreadCrumbs;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        private Tuple<Course, Module, Activity> MakeBreadCrumbs(int? courseId, int? moduleId, int? activityId)
        {
            BreadCrumb.Clear();
            BreadCrumb.Add("/", "Home");

            // Fetch DB Activity, Module and Course
            var activity = db.Activities.Find(activityId);
            var module = db.Modules.Find(moduleId);
            var course = db.Courses.Find(courseId);

            // Create return object
            var ret = new Tuple<Course, Module, Activity>(course, module, activity);

            // Make sure we have a course, fall back on module.Course or activity.Module.Course if course is null
            course = course ?? module?.Course ?? activity?.Module?.Course;
            if (course != null)
            {
                BreadCrumb.Add(Url.Action("Index", "Modules", new { courseId = course.Id }), course.Name);
                // Make sure we have a module, fall back on activity.Module if module is null
                module = module ?? activity?.Module;
                if (module != null)
                {
                    BreadCrumb.Add(Url.Action("Index", "Activities", new { moduleId = module.Id }), module.Name);
                    if (activity != null)
                    {
                        BreadCrumb.Add(Url.Action("Index", "Activities", new { moduleId = activity.ModuleId }), activity.Name);
                    }
                }
            }
            return ret;
        }

        // GET: Documents
        public ActionResult Index(int? courseId, int? moduleId, int? activityId)
        {
            var ret = MakeBreadCrumbs(courseId, moduleId, activityId);
            if (ret.Item1 != null)
            {
                var documents = db.Documents.Where(d => d.CourseId == courseId).ToList().Select(d => new DocumentViewModel(d));
                return View("CourseDocumentsIndex", new CourseDocumentsViewModel(ret.Item1, documents));
            }
            if (ret.Item2 != null)
            {
                var documents = db.Documents.Where(d => d.ModuleId == moduleId).ToList().Select(d => new DocumentViewModel(d));
                return View("ModuleDocumentsIndex", new ModuleDocumentsViewModel(ret.Item2, documents));
            }
            if (ret.Item3 != null)
            {
                var documents = db.Documents.Where(d => d.ActivityId == activityId).ToList().Select(d => new DocumentViewModel(d));
                return View("ActivityDocumentsIndex", new ActivityDocumentsViewModel(ret.Item3, documents));
            }
            if (courseId != null || moduleId != null || activityId != null)
            {
                return HttpNotFound();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Documents/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateFile(int? courseId, int? moduleId, int? activityId)
        {
            MakeBreadCrumbs(courseId, moduleId, activityId);

            if (courseId != null)
            {
                var course = db.Courses.Find(courseId);
                if (course == null)
                    return HttpNotFound();
                return View("CourseCreateFile", new CourseCreateDocumentFileViewModel(course));
            }
            if (moduleId != null)
            {
                var module = db.Modules.Find(moduleId);
                if (module == null)
                    return HttpNotFound();
                return View("ModuleCreateFile", new ModuleCreateDocumentFileViewModel(module));
            }
            if (activityId != null)
            {
                var activity = db.Activities.Find(activityId);
                if (activity == null)
                    return HttpNotFound();
                return View("ActivityCreateFile", new ActivityCreateDocumentFileViewModel(activity));
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Course Documents/CreateFile
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateCourseFile(CourseCreateDocumentFileViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(new Document(model, User.Identity.GetUserId())
                {
                    CourseId = model.CourseId
                });
                db.SaveChanges();
                return RedirectToAction("Index", new { model.CourseId });
            }
            return View(model);
        }

        // POST: Module Documents/CreateFile
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateModuleFile(ModuleCreateDocumentFileViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(new Document(model, User.Identity.GetUserId())
                {
                    ModuleId = model.ModuleId
                });
                db.SaveChanges();
                return RedirectToAction("Index", new { model.ModuleId });
            }
            return View(model);
        }

        // POST: Activity Documents/CreateFile
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateActivityFile(ActivityCreateDocumentFileViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(new Document(model, User.Identity.GetUserId())
                {
                    ActivityId = model.ActivityId
                });
                db.SaveChanges();
                return RedirectToAction("Index", new {model.ActivityId});
            }
            return View(model);
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult CreateLink(int? courseId, int? moduleId, int? activityId)
        {
            MakeBreadCrumbs(courseId, moduleId, activityId);

            if (courseId != null)
            {
                var course = db.Courses.Find(courseId);
                if (course == null)
                    return HttpNotFound();
                return View("CourseCreateLink", new CourseCreateDocumentLinkViewModel(course));
            }
            if (moduleId != null)
            {
                var module = db.Modules.Find(moduleId);
                if (module == null)
                    return HttpNotFound();
                return View("ModuleCreateLink", new ModuleCreateDocumentLinkViewModel(module));
            }
            if (activityId != null)
            {
                var activity = db.Activities.Find(activityId);
                if (activity == null)
                    return HttpNotFound();
                return View("ActivityCreateLink", new ActivityCreateDocumentLinkViewModel(activity));
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Course Documents/CreateLink
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateCourseLink(CourseCreateDocumentLinkViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(new Document(model, User.Identity.GetUserId())
                {
                    CourseId = model.CourseId
                });
                db.SaveChanges();
                return RedirectToAction("Index", new { model.CourseId });
            }
            return View(model);
        }

        // POST: Module Documents/CreateLink
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateModuleLink(ModuleCreateDocumentLinkViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(new Document(model, User.Identity.GetUserId())
                {
                    ModuleId = model.ModuleId
                });
                db.SaveChanges();
                return RedirectToAction("Index", new { model.ModuleId });
            }
            return View(model);
        }

        // POST: Activity Documents/CreateLink
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateActivityLink(ActivityCreateDocumentLinkViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(new Document(model, User.Identity.GetUserId())
                {
                    ActivityId = model.ActivityId
                });
                db.SaveChanges();
                return RedirectToAction("Index", new { model.ActivityId });
            }
            return View(model);
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Download(int id)
        {
            var document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            if (!string.IsNullOrWhiteSpace(document.Link))
            {
                return Redirect(document.Link);
            }
            return File(document.Content, document.ContentType, document.FileName);
                
        }

        // GET: Documents/Edit/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }

            MakeBreadCrumbs(document.CourseId, document.ModuleId, document.ActivityId);

            if (!string.IsNullOrWhiteSpace(document.Link))
            {
                return View("EditLink", new DocumentEditLinkViewModel(document));
            }

            return View("EditFile", new DocumentEditFileViewModel(document));
        }

        // POST: Documents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult EditFile(DocumentEditFileViewModel document)
        {
            if (ModelState.IsValid)
            {
                var doc = db.Documents.Find(document.Id);
                doc?.Update(document);
                db.SaveChanges();
                return RedirectToAction("Index", new {
                    CourseId = doc?.CourseId,
                    ModuleId = doc?.ModuleId,
                    ActivityId = doc?.ActivityId
                });
            }
            return View(document);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult EditLink(DocumentEditLinkViewModel document)
        {
            if (ModelState.IsValid)
            {
                var doc = db.Documents.Find(document.Id);
                doc?.Update(document);
                db.SaveChanges();
                return RedirectToAction("Index", new {
                    CourseId = doc?.CourseId,
                    ModuleId = doc?.ModuleId,
                    ActivityId = doc?.ActivityId
                });
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(new DocumentDeleteViewModel(document));
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteConfirmed(int id)
        {
            var document = db.Documents.Find(id);
            if (document == null)
                return HttpNotFound();
            var routingObject = new
            {
                CourseId = document.CourseId,
                ModuleId = document.ModuleId,
                ActivityId = document.ActivityId
            };
            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Index", routingObject);
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

