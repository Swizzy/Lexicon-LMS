using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Models
{
    public class UserViewModel
    {
        public UserViewModel(ApplicationUser user)
        {
            Id = user.Id;
            Name = user.FullName;
            EMail = user.Email;
            Phone = user.PhoneNumber;
        }

        public string Id { get; }

        public string Name { get; }

        [Display(Name = "E-Mail")]
        public string EMail { get; }

        public string Phone { get; }
    }
}
