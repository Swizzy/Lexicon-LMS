using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

        public ApplicationUserManager UserManager => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        // GET: Users
        public ActionResult Index()
        {
            var users =
                db.Users.ToList().Where(u => UserManager.IsInRole(u.Id, "Teacher")).Select(u => new UserViewModel(u));
            return View(users);
        }

        //
        // GET: /Users/RegisterTeacher
        [Authorize(Roles = "Teacher")]
        public ActionResult RegisterTeacher()
        {
            return View();
        }

        //
        // POST: /Users/RegisterTeacher
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> RegisterTeacher(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser(model);
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user.Id, "Teacher");
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    AddErrors(result);
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Users/RegisterStudent
        [Authorize(Roles = "Teacher")]
        public ActionResult RegisterStudent()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
            return View();
        }

        //
        // POST: /Users/RegisterStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> RegisterStudent(RegisterStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser(model);
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index", new { studentsOnly = true, courseId = model.CourseId });
                AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
            return View(model);
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // GET: Users/EditTeacher/5
        [Authorize(Roles = "Teacher")]
        public ActionResult EditTeacher(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();
            return View(new EditTeacherViewModel(user));
        }

        // POST: Users/EditTeacher/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult EditTeacher(string id, EditTeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(id);
                if (user == null)
                    return HttpNotFound();
                user.Update(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Users/EditStudent/5
        [Authorize(Roles = "Teacher")]
        public ActionResult EditStudent(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Redirect to edit teacher if it's a teacher
            if (UserManager.IsInRole(id, "Teacher"))
                return RedirectToAction(nameof(EditTeacher), new { id });

            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", user.CourseId);
            return View(new EditStudentViewModel(user));
        }

        // POST: Users/EditStudent/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult EditStudent(string id, EditStudentViewModel model)
        {
            // Redirect to edit teacher if it's a teacher
            if (UserManager.IsInRole(id, "Teacher"))
                return RedirectToAction(nameof(EditTeacher), new { id });

            if (ModelState.IsValid)
            {
                var user = db.Users.Find(id);
                if (user == null)
                    return HttpNotFound();
                user.Update(model);
                db.SaveChanges();
                return RedirectToAction("Index", new { studentsOnly = true, courseId = model.CourseId });
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", model.CourseId);
            return View(model);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(string id)
        {
            var cuser = db.Users.Find(User.Identity.GetUserId());
            if (cuser == null || id == cuser.Id)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();
            return View(new UserViewModel(user, UserManager.IsInRole(user.Id, "Teacher")));
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();
            db.Users.Remove(user);
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
