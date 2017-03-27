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
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documents
        public ActionResult Index(int? courseId, int? moduleId, int? activityId)
        {
            IQueryable<Document> documents = db.Documents;

            if (courseId != null)
            {
                documents = documents.Where(d => d.CourseId == courseId).Include(d => d.Course);
                var docList = documents.ToList().Select(d => new DocumentViewModel(d));
                var course = db.Courses.Find(courseId);
                if (course == null)
                    return HttpNotFound();
                return View("CourseDocumentsIndex", new CourseDocumentsViewModel(course, docList));
            }
            if (moduleId != null)
            {
                documents = documents.Where(d => d.ModuleId == moduleId).Include(d => d.Module);
                var docList = documents.ToList().Select(d => new DocumentViewModel(d));
                var module = db.Modules.Find(moduleId);
                if (module == null)
                    return HttpNotFound();
                return View("ModuleDocumentsIndex", new ModuleDocumentsViewModel(module, docList));
            }
            if (activityId != null)
            {
                documents = documents.Where(d => d.ActivityId == activityId).Include(d => d.Activity);
                var docList = documents.ToList().Select(d => new DocumentViewModel(d));
                var activity = db.Activities.Find(activityId);
                if (activity == null)
                    return HttpNotFound();
                return View("ActivityDocumentsIndex", new ActivityDocumentsViewModel(activity, docList));
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        public ActionResult CreateFile(int? courseId, int? moduleId, int? activityId)
        {
            if (courseId != null)
            {
                var course = db.Courses.Find(courseId);
                if (course == null)
                    return HttpNotFound();
                return View("CourseCreateFile", new CreateCourseDocumentFileViewModel(course));
            }
            if (moduleId != null)
            {
                var module = db.Modules.Find(moduleId);
                if (module == null)
                    return HttpNotFound();
                return View("ModuleCreateFile", new CreateModuleDocumentFileViewModel(module));
            }
            if (activityId != null)
            {
                var activity = db.Activities.Find(activityId);
                if (activity == null)
                    return HttpNotFound();
                return View("ActivityCreateFile", new CreateActivityDocumentFileViewModel(activity));
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Documents/CreateFile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourseFile(CreateCourseDocumentFileViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var br = new System.IO.BinaryReader(model.Upload.InputStream))
                {
                    var doc = new Document()
                    {
                        Name = model.Name,
                        FileName = model.Upload.FileName,
                        ContentType = model.Upload.ContentType,
                        Content = br.ReadBytes(model.Upload.ContentLength),
                        CreateDate = DateTime.Now,
                        UserId = User.Identity.GetUserId(),
                        CourseId = model.CourseId
                    };
                    db.Documents.Add(doc);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { model.CourseId });
                }
            }
            return View(model);
        }

        // POST: Documents/CreateFile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateModuleFile(CreateModuleDocumentFileViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var br = new System.IO.BinaryReader(model.Upload.InputStream))
                {
                    var doc = new Document()
                    {
                        Name = model.Name,
                        FileName = model.Upload.FileName,
                        ContentType = model.Upload.ContentType,
                        Content = br.ReadBytes(model.Upload.ContentLength),
                        CreateDate = DateTime.Now,
                        UserId = User.Identity.GetUserId(),
                        ModuleId = model.ModuleId
                    };
                    db.Documents.Add(doc);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { model.ModuleId });
                }
            }
            return View(model);
        }

        // POST: Documents/CreateFile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateActivityFile(CreateActivityDocumentFileViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var br = new System.IO.BinaryReader(model.Upload.InputStream))
                {
                    var doc = new Document()
                    {
                        Name = model.Name,
                        FileName = model.Upload.FileName,
                        ContentType = model.Upload.ContentType,
                        Content = br.ReadBytes(model.Upload.ContentLength),
                        CreateDate = DateTime.Now,
                        UserId = User.Identity.GetUserId(),
                        ActivityId = model.ActivityId
                    };
                    db.Documents.Add(doc);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { model.ActivityId });
                }
            }
            return View(model);
        }

        public ActionResult CreateLink(int? courseId, int? moduleId, int? activityId)
        {
            if (courseId != null)
            {
                var course = db.Courses.Find(courseId);
                if (course == null)
                    return HttpNotFound();
                return View("CourseCreateLink", new CreateCourseDocumentLinkViewModel(course));
            }
            if (moduleId != null)
            {
                var module = db.Modules.Find(moduleId);
                if (module == null)
                    return HttpNotFound();
                return View("ModuleCreateLink", new CreateModuleDocumentLinkViewModel(module));
            }
            if (activityId != null)
            {
                var activity = db.Activities.Find(activityId);
                if (activity == null)
                    return HttpNotFound();
                return View("ActivityCreateLink", new CreateActivityDocumentLinkViewModel(activity));
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Documents/CreateLink
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourseLink(CreateCourseDocumentLinkViewModel model)
        {
            if (ModelState.IsValid)
            {
                    var doc = new Document()
                    {
                        Name = model.Name,
                        Link = model.Link,
                        CreateDate = DateTime.Now,
                        UserId = User.Identity.GetUserId(),
                        CourseId = model.CourseId
                    };
                    db.Documents.Add(doc);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { model.CourseId });
            }
            return View(model);
        }

        // POST: Documents/CreateLink
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateModuleLink(CreateModuleDocumentLinkViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doc = new Document()
                {
                    Name = model.Name,
                    Link = model.Link,
                    CreateDate = DateTime.Now,
                    UserId = User.Identity.GetUserId(),
                    ModuleId = model.ModuleId
                };
                db.Documents.Add(doc);
                db.SaveChanges();
                return RedirectToAction("Index", new { model.ModuleId });
            }
            return View(model);
        }

        // POST: Documents/CreateLink
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateActivityLink(CreateActivityDocumentLinkViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doc = new Document()
                {
                    Name = model.Name,
                    Link = model.Link,
                    CreateDate = DateTime.Now,
                    UserId = User.Identity.GetUserId(),
                    ActivityId = model.ActivityId
                };
                db.Documents.Add(doc);
                db.SaveChanges();
                return RedirectToAction("Index", new { model.ActivityId });
            }
            return View(model);
        }

        public ActionResult Download(int id)
        {
            var document = db.Documents.Find(id);
            if (!string.IsNullOrWhiteSpace(document.Link))
            {
                return Redirect(document.Link);
            }
            return File(document.Content, document.ContentType, document.FileName);
                
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", document.ActivityId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", document.CourseId);
            ViewBag.ModuleId = new SelectList(db.Modules, "Id", "Name", document.ModuleId);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,FileName,ContentType,Content,FileType,UserId,CreateDate,CourseId,ModuleId,ActivityId")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", document.ActivityId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", document.CourseId);
            ViewBag.ModuleId = new SelectList(db.Modules, "Id", "Name", document.ModuleId);
            return View(document);
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
            db.SaveChanges();
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
