using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace LexiconLMS.Models
{
    public class TeacherAssignmentsViewModel
    {
        public TeacherAssignmentsViewModel(Activity activity, ApplicationUserManager userManager)
        {
            DueDate = activity.EndDate;

            var completed = activity.Documents.Where(d => !userManager.IsInRole(d.UserId, "Teacher"));
            var pending = activity.Module
                                  .Course
                                  .Students
                                  .Where(s => completed.All(d => d.UserId != s.Id))
                                  .Select(s => new AssignmentViewModel(s, activity));
            Assignments = pending.Concat(completed.Select(d => new AssignmentViewModel(d, activity)));
        }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; }

        public IEnumerable<AssignmentViewModel> Assignments { get; }
    }

    public class StudentAssignmentsViewModel
    {
        public StudentAssignmentsViewModel(Course course, ApplicationUser user)
        {
            Assignments = course.Activities
                                .Where(a => a.ActivityType.IsAssignment)
                                .Select(a => new { a, doc = a.Documents.FirstOrDefault(d => d.UserId == user.Id) })
                                .Select(t => t.doc != null ? new AssignmentViewModel(t.doc, t.a) : new AssignmentViewModel(user, t.a));
        }

        public IEnumerable<AssignmentViewModel> Assignments { get; }
    }

    public class AssignmentViewModel
    {
        public enum StatusValues
        {
            Pending,
            Completed,
            Late
        }

        public AssignmentViewModel(Document document, Activity activity)
        {
            DocumentId = document.Id;
            Student = document.ApplicationUser.FullName;
            CreateDate = document.CreateDate;

            Name = activity.Name;
            ActivityId = activity.Id;
            DueDate = activity.EndDate;
            ModuleId = activity.ModuleId;
            Status = StatusValues.Completed;
            ModuleName = activity.Module.Name;
            if (DateTime.Now < activity.EndDate)
                IsUpdateOk = true;
        }

        public AssignmentViewModel(ApplicationUser user, Activity activity)
        {
            Student = user.FullName;

            Name = activity.Name;
            ActivityId = activity.Id;
            DueDate = activity.EndDate;
            ModuleId = activity.ModuleId;
            ModuleName = activity.Module.Name;
            Status = activity.EndDate > DateTime.Now ? StatusValues.Pending : StatusValues.Late;
            if (DateTime.Now < activity.EndDate)
                IsUploadOk = true;
        }

        public int? DocumentId { get; }
        public int ActivityId { get; }
        public int ModuleId { get; }

        [Display(Name = "Name")]
        public string Name { get; }

        [Display(Name = "Module")]
        public string ModuleName { get; }

        [Display(Name = "Student")]
        public string Student { get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Uploaded")]
        public DateTime? CreateDate { get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; }

        public StatusValues Status { get; }

        // This is for the view, to show or not show the download link
        public bool HasDocument => DocumentId != null;

        // This is for the view, to show or not show the upload link
        public bool IsUploadOk { get; }

        public bool IsUpdateOk { get; }
    }

    public class UpdateAssignmentViewModel
    {
        private bool allowPost;

        public UpdateAssignmentViewModel()
        {
        }

        public UpdateAssignmentViewModel(Document document)
        {
            UpdateAllowPost(document.Activity);
        }

        public void UpdateAllowPost(Activity activity)
        {
            allowPost = activity.EndDate > DateTime.Now;
        }

        [Required]
        public HttpPostedFileBase Upload { get; set; }

        public string Disabled => allowPost ? "" : "disabled";
    }

    public class UploadAssignmentViewModel : UpdateAssignmentViewModel
    {
        public UploadAssignmentViewModel()
        {
        }

        public UploadAssignmentViewModel(Activity activity)
        {
            ActivityId = activity.Id;
            UpdateAllowPost(activity);
        }

        [Required]
        public int ActivityId { get; set; }
    }
}