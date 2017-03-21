using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LexiconLMS.Models;

namespace LexiconLMS.Controllers
{
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Activities
        public ActionResult Index(int? moduleId)
        {
            if (moduleId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var module = db.Modules.FirstOrDefault(m => m.Id == moduleId);
            if (module == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var activities = db.Activities.Where(a => a.ModuleId == moduleId)
                                          .Include(a => a.ActivityType)
                                          .ToList()
                                          .Select(a => new ActivityViewModel(a));

            return View(new ActivityIndexVieWModel(module, activities));
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
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create(int? moduleId)
        {
            if (moduleId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var module = db.Modules.Find(moduleId);
            if (module == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "Name");
            return View(new ActivityCreateViewModel(module));
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityCreateViewModel activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(new Activity(activity));
                db.SaveChanges();
                return RedirectToAction("Index", new { activity.ModuleId });
            }
            return View(activity);
        }

        // GET: Activities/Edit/5
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
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "Name", activity.ActivityTypeId);
            return View(new ActivityCreateViewModel(activity));
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,EndDate,ModuleId,ActivityTypeId")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "Name", activity.ActivityTypeId);
            return View(new ActivityCreateViewModel(activity));
        }

        // GET: Activities/Delete/5
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
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
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
