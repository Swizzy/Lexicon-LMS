using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Module
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public string Description { get; set; }
        public int CourseId { get; set; }

        // Navigation properties
        public virtual Course Course { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        //public virtual ICollection<Document> Documents { get; set; }
    }

    public class ModuleSingleViewModel
    {
        public ModuleSingleViewModel()
        {

        }

        public ModuleSingleViewModel(Module module)
        {
            CourseName = module.Course.Name;
            CourseId = module.CourseId;
            Name = module.Name;
            Description = module.Description;
        }

        public ModuleSingleViewModel(Course course)
        {
            CourseName = course.Name;
            CourseId = course.Id;
        }

        public string CourseName { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ModuleIndexViewModel
    {
        public ModuleIndexViewModel(Course course, IEnumerable<ModuleViewModel> modules)
        {
            CourseName = course.Name;
            CourseId = course.Id;
            Modules = modules;
        }

        public string CourseName { get; }
        public int CourseId { get; }
        public IEnumerable<ModuleViewModel> Modules { get; }

    }

    public class ModuleDeleteViewModel
    {
        public ModuleDeleteViewModel()
        {

        }

        public ModuleDeleteViewModel(Module module)
        {
            CourseName = module.Course.Name;
            CourseId = module.CourseId;
            Name = module.Name;
            Description = module.Description;

            HasActivities = module.Activities.Any();
            if (!HasActivities)
                return;

            ActivityCount = module.Activities.Count;

            var sortedActivities = module.Activities.OrderBy(a => a.StartDate);
            StartDate = sortedActivities.First().StartDate;
            EndDate = sortedActivities.Last().EndDate;
        }

        [Display(Name = "Course")]
        public string CourseName { get; }
        public int CourseId { get; }
        public string Name { get; }
        public string Description { get; }

        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; }

        [Display(Name = "Activities")]
        public int ActivityCount { get; }
        public bool HasActivities { get; }


    }

    public class ModuleViewModel
    {
        public ModuleViewModel(Module module)
        {
            Id = module.Id;
            Name = module.Name;
            Description = module.Description;

            ActivityCount = module.Activities.Count;

            var sortedActivities = module.Activities.OrderBy(a => a.StartDate);
            StartDate = sortedActivities.FirstOrDefault()?.StartDate;
            EndDate = sortedActivities.LastOrDefault()?.EndDate;
        }

        public int Id { get; }
        public int ActivityCount { get; set; }
        public string Name { get; }
        public string Description { get; }

        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }
    }

}