using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(250)]
        public string Description { get; set; }
        [Display(Name = "Start Date")]
        public DateTime? StartDate {
            get
            {
                var sortedActivities = Modules.SelectMany(m => m.Activities)
                                              .OrderBy(a => a.StartDate);
                return sortedActivities.FirstOrDefault()?.StartDate;
            }
        }
        [Display(Name = "End Date")]
        public DateTime? EndDate
        {
            get
            {
                var sortedActivities = Modules.SelectMany(m => m.Activities)
                                              .OrderBy(a => a.StartDate);
                return sortedActivities.LastOrDefault()?.EndDate;
            }
        }

        //Navigation Properties
        public virtual ICollection<Module> Modules { get; set; }
    }
}