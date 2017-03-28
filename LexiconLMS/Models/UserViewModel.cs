using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Models
{
    public class UserViewModel
    {
        public UserViewModel(ApplicationUser user, bool isTeacher = false, bool canBeDeleted = true)
        {
            Id = user.Id;
            Name = user.FullName;
            EMail = user.Email;
            Phone = user.PhoneNumber;
            Course = user.Course?.Name;
            IsTeacher = isTeacher;
            CanBeDeleted = canBeDeleted;
        }

        public string Id { get; }

        public string Name { get; }

        [Display(Name = "E-Mail")]
        public string EMail { get; }

        public string Phone { get; }

        public string Course { get; }

        public bool IsTeacher { get; }

        public bool CanBeDeleted { get; }
    }
}
