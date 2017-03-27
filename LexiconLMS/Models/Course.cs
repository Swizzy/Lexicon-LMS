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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? StartDate => Modules.Max(m => m.StartDate);

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? EndDate => Modules.Max(m => m.EndDate);

        //Navigation Properties
        public virtual ICollection<Module> Modules { get; set; }
    }
}