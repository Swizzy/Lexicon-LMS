using System;
using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "End")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public int ModuleId { get; set; }
        public int ActivityTypeId { get; set; }

        public virtual ActivityType ActivityType { get; set; }
    }

    public class ActivityType
    {
        public int Id { get; set; }
        public bool IsAssignment { get; set; }
        public string Name { get; set; }
    }
}
