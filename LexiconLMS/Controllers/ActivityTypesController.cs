using System.Linq;
using System.Net;
using System.Web.Mvc;
using LexiconLMS.Models;
using MvcBreadCrumbs;

namespace LexiconLMS.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class ActivityTypesController : Controller
    {
        private void MakeBreadCrumbs()
        {
            BreadCrumb.Clear();
            BreadCrumb.Add("/", "Home");
            BreadCrumb.Add(Url.Action("Index", "ActivityTypes"), "Manage Activity Types");
        }

        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivityTypes
        public ActionResult Index()
        {
            MakeBreadCrumbs();
            return View(db.ActivityTypes.ToList().Select(at => new ActivityTypeViewModel(at)));
        }

        // GET: ActivityTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var activityType = db.ActivityTypes.Find(id);
            if (activityType == null)
                return HttpNotFound();
            var activities = activityType.Activities.Select(a => new ActivityTypeActivityViewModel(a));
            MakeBreadCrumbs();
            return View(new ActivityTypeDetailViewModel(activityType, activities));
        }

        // GET: ActivityTypes/Create
        public ActionResult Create()
        {
            MakeBreadCrumbs();
            return View();
        }

        // POST: ActivityTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityTypeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.ActivityTypes.Add(new ActivityType(model));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            MakeBreadCrumbs();
            return View(model);
        }

        // GET: ActivityTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var activityType = db.ActivityTypes.Find(id);
            if (activityType == null)
                return HttpNotFound();
            MakeBreadCrumbs();
            return View(new ActivityTypeCreateViewModel(activityType));
        }

        // POST: ActivityTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, ActivityTypeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var activityType = db.ActivityTypes.Find(id);
                if (activityType == null)
                    return HttpNotFound();
                activityType.Update(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            MakeBreadCrumbs();
            return View(model);
        }

        // GET: ActivityTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var activityType = db.ActivityTypes.Find(id);
            if (activityType == null)
                return HttpNotFound();
            MakeBreadCrumbs();
            return View(new ActivityTypeDeleteViewModel(activityType));
        }

        // POST: ActivityTypes/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            var activityType = db.ActivityTypes.Find(id);
            if (activityType == null)
                return HttpNotFound();
            foreach (var activity in db.Activities.Where(a => a.ActivityTypeId == id).ToArray())
            {
                foreach (var document in activity.Documents.ToArray())
                {
                    db.Documents.Remove(document);
                }
                db.Activities.Remove(activity);
            }
            db.ActivityTypes.Remove(activityType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
