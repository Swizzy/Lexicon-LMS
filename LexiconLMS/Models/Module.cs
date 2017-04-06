using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LexiconLMS.Models
{
    public class Module
    {
        public Module()
        {
        }

        public Module(Module module, int courseId)
        {
            CourseId = courseId;
            Name = module.Name;
            Description = module.Description;
        }

        public Module(ModuleCreateViewModel module, int courseId)
        {
            Update(module);
            CourseId = courseId;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public int CourseId { get; set; }

        public DateTime? StartDate => Activities.OrderBy(a => a.StartDate).FirstOrDefault()?.StartDate;
        public DateTime? EndDate => Activities.OrderBy(a => a.EndDate).LastOrDefault()?.EndDate;

        // Navigation properties
        public virtual Course Course { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Document> Documents { get; set; }

        public void Update(ModuleCreateViewModel module)
        {
            Name = module.Name;
            Description = module.Description;
        }
    }

    public class ModuleIndexStudentViewModel
    {

        public ModuleIndexStudentViewModel(DateTime timestamp, IEnumerable<ActivityScheduleViewModel> activities, string modules)
        {
            Activities = activities;
            Timestamp = timestamp;
            Modules = modules;
        }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Timestamp { get; }
        public string Modules { get; }
        public IEnumerable<ActivityScheduleViewModel> Activities { get; }
        
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

    public class ModuleCreateViewModel
    {
        public ModuleCreateViewModel()
        {
        }

        public ModuleCreateViewModel(Module module)
        {
            Name = module.Name;
            Description = module.Description;
        }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ModuleDeleteViewModel
    {
        public ModuleDeleteViewModel(Module module)
        {
            Name = module.Name;
            StartDate = module.StartDate;
            EndDate = module.EndDate;

            ActivityCount = module.Activities.Count;
            ModuleDocumentsCount = module.Documents.Count;
            ActivityDocumentsCount = module.Activities.SelectMany(a => a.Documents).Count();
        }

        public string Name { get; }

        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; }

        [Display(Name = "Activities")]
        public int ActivityCount { get; }
        
        [Display(Name = "Module Documents")]
        public int ModuleDocumentsCount { get; }

        [Display(Name = "Activity Documents")]
        public int ActivityDocumentsCount { get; }

        public bool HasActivities => ActivityCount > 0;
        public bool HasDocuments => ModuleDocumentsCount > 0 || ActivityDocumentsCount > 0;

        public string DeleteType
        {
            get
            {
                var types = new List<string>();
                if (HasActivities)
                {
                    types.Add("Activities");
                }
                if (HasDocuments)
                {
                    types.Add("Documents");
                }

                var sb = new StringBuilder("Module");
                if (types.Count > 0)
                {
                    sb.Append(" and the related " + types[0]);
                    for (int i = 1; i < types.Count; i++)
                    {
                        if (i < types.Count - 1)
                        {
                            sb.Append(", " + types[i]);
                        }
                        else
                        {
                            sb.Append(" and " + types[i]);
                        }
                    }
                }
                return sb.ToString();
            }
        }

    }

    public class ModuleViewModel
    {
        public ModuleViewModel(Module module)
        {
            Id = module.Id;
            Name = module.Name;
            StartDate = module.StartDate;
            EndDate = module.EndDate;
            DocumentsCount = module.Documents.Count;
            ActivitiesCount = module.Activities.Count;
        }

        public int Id { get; }
        public string Name { get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; }

        [Display(Name = "Activities")]
        public int ActivitiesCount { get; }

        [Display(Name = "Module Documents")]
        public int DocumentsCount { get; }
    }

    public class ModuleDetailsViewModel
    {
        public ModuleDetailsViewModel(Module module)
        {
            Name = module.Name;
            Description = module.Description;
            Activities = module.Activities.Select(a => new ActivityViewModel(a));
            Documents = module.Documents.Select(d => new DocumentViewModel(d));
        }

        public string Name { get; }
        public string Description { get; }
        public IEnumerable<ActivityViewModel> Activities { get; }
        public IEnumerable<DocumentViewModel> Documents { get; }
    }
}