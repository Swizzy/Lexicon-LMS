using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LexiconLMS.Models;
using MvcBreadCrumbs;
using Microsoft.AspNet.Identity;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private void MakeBreadCrumbs(Module module)
        {
            BreadCrumb.Clear();
            BreadCrumb.Add("/", "Home");
            if (module != null)
            {
                BreadCrumb.Add(Url.Action("Index", "Modules", new { courseId = module.CourseId}), module.Course.Name);
                BreadCrumb.Add(Url.Action("Index", "Activities", new {moduleId = module.Id}), module.Name);
            }
        }

        // GET: Activities
        public ActionResult Index(int? moduleId)
        {
            var view = "Index";
            if (User.IsInRole("Teacher"))
            {
                if (moduleId == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                if (user == null)
                    return HttpNotFound();
                view = "StudentIndex";
            }
            var module = db.Modules.FirstOrDefault(m => m.Id == moduleId);
            if (module == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var activities = db.Activities.Where(a => a.ModuleId == moduleId)
                                          .Include(a => a.ActivityType)
                                          .ToList()
                                          .Select(a => new ActivityViewModel(a));
            MakeBreadCrumbs(module);

            return View(view, new ActivityIndexVieWModel(module, activities));
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            MakeBreadCrumbs(activity.Module);
            return View(activity);
        }

        // GET: Activities/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int? moduleId)
        {
            if (moduleId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var module = db.Modules.Find(moduleId);
            if (module == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            MakeBreadCrumbs(module);
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "Name");
            return View(new ActivityCreateViewModel(module));
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(ActivityCreateViewModel activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(new Activity(activity));
                db.SaveChanges();
                return RedirectToAction("Index", new { activity.ModuleId });
            }
            MakeBreadCrumbs(db.Modules.Find(activity.ModuleId));
            return View(activity);
        }

        // GET: Activities/Edit/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            MakeBreadCrumbs(activity.Module);
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "Name", activity.ActivityTypeId);
            return View(new ActivityCreateViewModel(activity));
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id, ActivityCreateViewModel activity)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(activity).State = EntityState.Modified;
                Activity dbactivity = db.Activities.Find(id);
                if (dbactivity == null)
                {
                    return HttpNotFound();
                }
                dbactivity.Update(activity);
                db.SaveChanges();
                return RedirectToAction("Index", new { activity.ModuleId });
            }
            MakeBreadCrumbs(db.Activities.Find(id)?.Module);
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "Name", activity.ActivityTypeId);
            return View(activity);
        }

        // GET: Activities/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            MakeBreadCrumbs(activity.Module);
            return View(new ActivityDeleteViewModel(activity));
        }

        // POST: Activities/Delete/5
        [Authorize(Roles = "Teacher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            db.Activities.Remove(activity);
            db.SaveChanges();
            return RedirectToAction("Index", new { activity.ModuleId });
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

