﻿using System;
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
        [Authorize(Roles = "Teacher")]
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

        // POST: Course Documents/CreateFile
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
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

        // POST: Module Documents/CreateFile
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
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

        // POST: Activity Documents/CreateFile
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
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

        [Authorize(Roles = "Teacher")]
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

        // POST: Course Documents/CreateLink
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
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

        // POST: Module Documents/CreateLink
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
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

        // POST: Activity Documents/CreateLink
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
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

        [Authorize(Roles = "Teacher")]
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
        [Authorize(Roles = "Teacher")]
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
            ViewBag.RoutingObject = new
            {
                CourseId = document.CourseId,
                ModuleId = document.ModuleId,
                ActivityId = document.ActivityId
            };

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
                doc.Update(document);
                db.SaveChanges();
                return RedirectToAction("Index", new {
                    CourseId = doc.CourseId,
                    ModuleId = doc.ModuleId,
                    ActivityId = doc.ActivityId
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
                doc.Update(document);
                db.SaveChanges();
                return RedirectToAction("Index", new {
                    CourseId = doc.CourseId,
                    ModuleId = doc.ModuleId,
                    ActivityId = doc.ActivityId
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
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoutingObject = new
            {
                CourseId = document.CourseId,
                ModuleId = document.ModuleId,
                ActivityId = document.ActivityId
            };
            return View(new DocumentDeleteViewModel(document));
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
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