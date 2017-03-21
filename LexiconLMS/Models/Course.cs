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
        [Display(Name = "Course Name")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}