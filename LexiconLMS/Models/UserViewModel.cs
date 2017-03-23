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
            Phone = Phone;
        }

        [Key]
        public string Id { get; }
        public string Name { get; }
        public string EMail { get; }
        public string Phone { get; }

    }
}