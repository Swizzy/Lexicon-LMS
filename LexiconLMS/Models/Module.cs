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

        public DateTime? StartDate
        {
            get { return Activities.OrderBy(a => a.StartDate).FirstOrDefault()?.StartDate; }
        }

        public DateTime? EndDate
        {
            get { return Activities.OrderBy(a => a.EndDate).LastOrDefault()?.EndDate; }
        }

        // Navigation properties
        public virtual Course Course { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
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
        [Required]
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
            {
                ActivityCount = module.Activities.Count;

            StartDate = module.StartDate;
            EndDate = module.EndDate;
                if (!HasDocuments)
                {
                    DocumentCount = module.Documents.Count;
                    //var sortedDocuments = module.Documents.OrderByDescending(d => d.CreateDate);
                }
            }

        [Display(Name = "Course")]
        public string CourseName { get; }
        public int CourseId { get; }
        public string Name { get; }
        public string Description { get; }

        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; }

        [Display(Name = "Activities")]
        public int ActivityCount { get; }
        public bool HasActivities { get; }

        [Display(Name = "Module Document")]
        public int DocumentCount { get; }
        public bool HasDocuments { get; }


    }

    public class ModuleViewModel
    {
        public ModuleViewModel(Module module)
        {
            Id = module.Id;
            Name = module.Name;
            Description = module.Description;

            HasActivities = module.Activities.Any();
            if (HasActivities)
            {
                ActivityCount = module.Activities.Count;

            StartDate = module.StartDate;
            EndDate = module.EndDate;
                if (HasDocuments)
                {
                    DocumentCount = module.Documents.Count;
                    //var sortedDocuments = module.Documents.OrderByDescending(d => d.CreateDate);
                }
            }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; }

        [Display(Name = "Activities")]
        public int ActivityCount { get; }
        public bool HasActivities { get; }

        [Display(Name = "Module Document")]
        public int DocumentCount { get; }
        public bool HasDocuments { get; }

    }

}