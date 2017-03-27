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
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        // GET: Users
        public ActionResult Index(bool? studentsOnly, int? courseId)
        {
            IEnumerable<ApplicationUser> users = null;
            string view;
            if (User.IsInRole("Teacher"))
            {
                if (studentsOnly != true && courseId == null)
                {
                    users = db.Users.ToList();
                    // Filter out any non-teachers
                    users = users.Where(u => UserManager.IsInRole(u.Id, "Teacher"));
                    view = "TeacherIndexTeachers";
                }
                else
                {
                    if (courseId != null)
                        users = db.Users.Where(u => u.CourseId == courseId).ToList();
                    else
                        users = db.Users.ToList();
                    // Filter out any teachers, we don't want them here
                    users = users.Where(u => !UserManager.IsInRole(u.Id, "Teacher"));
                    // Create courses list
                    ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
                    view = "TeacherIndexStudents";
                }
            }
            else
            {
                view = "StudentIndex";
                var user = db.Users.Find(User.Identity.GetUserId());
                if (user != null)
                {
                    // Filter out any user not in the current users course
                    users = db.Users.Where(u => u.CourseId == user.CourseId).ToList();
                    // Filter out teachers, we don't want them here
                    users = users.Where(u => !UserManager.IsInRole(u.Id, "Teacher"));
                }
            }
            if (users == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(view, users.Select(u => new UserViewModel(u, false, u.Id != User.Identity.GetUserId())));
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
            var cuser = db.Users.Find(User.Identity.GetUserId());
            if (cuser == null || id == cuser.Id)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var isTeacher = UserManager.IsInRole(id, "Teacher");
            var user = UserManager.FindById(id);
            if (user == null)
                return HttpNotFound();
            UserManager.Delete(user);
            if (isTeacher)
                return RedirectToAction("Index");
            return RedirectToAction("Index", new { studentsOnly = true });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
