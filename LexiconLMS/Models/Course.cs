using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LexiconLMS.Models
{
    public class Course
    {
        public Course()
        {
        }

        public Course(CourseCreateViewModel course)
        {
            Update(course);
        }

        public void Update(CourseCreateViewModel course)
        {
            Name = course.Name;
            Description = course.Description;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 30)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength: 250)]
        public string Description { get; set; }

        public DateTime? StartDate {
            get {
                return Activities.OrderBy(a => a.StartDate).FirstOrDefault()?.StartDate;
            }
        }
        public DateTime? EndDate
        {
            get
            {
                return Activities.OrderBy(a => a.EndDate).LastOrDefault()?.EndDate;
            }
        }

        public IEnumerable<Activity> Activities
        {
            get { return Modules.SelectMany(m => m.Activities); }
        }

        //Navigation Properties
        public virtual ICollection<Module> Modules { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<ApplicationUser> Students { get; set; }
    }

    public class CourseCreateViewModel
    {
        public CourseCreateViewModel()
        {
            
        }

        public CourseCreateViewModel(Course course)
        {
            Name = course.Name;
            Description = course.Description;
        }

        [Required]
        [StringLength(maximumLength: 30)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength: 250)]
        public string Description { get; set; }
    }

    public class CourseViewModel
    {
        public CourseViewModel(Course course)
        {
            Id = course.Id;
            Name = course.Name;

            ModulesCount = course.Modules.Count;
            DocumentsCount = course.Documents.Count;

            StartDate = course.StartDate;
            EndDate = course.EndDate;
        }

        public int Id { get; }

        public string Name { get; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? StartDate { get; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? EndDate { get; }

        [Display(Name = "Modules")]
        public int ModulesCount { get; }

        [Display(Name = "Course Documents")]
        public int DocumentsCount { get; }
    }

    public class CourseDeleteViewModel
    {
        public CourseDeleteViewModel(Course course)
        {
            Name = course.Name;
            StartDate = course.StartDate;
            EndDate = course.EndDate;
            CourseDocumentsCount = course.Documents.Count;

            ModulesCount = course.Modules.Count;
            ModuleDocumentsCount = course.Modules.SelectMany(m => m.Documents).Count();

            ActivitiesCount = course.Activities.Count();
            ActivityDocumentsCount = course.Activities.SelectMany(a => a.Documents).Count();
        }

        public string Name { get; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? StartDate { get; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EndDate { get; }

        [Display(Name = "Modules")]
        public int ModulesCount { get; }

        [Display(Name = "Activities")]
        public int ActivitiesCount { get; }

        [Display(Name = "Course Documents")]
        public int CourseDocumentsCount { get; }

        [Display(Name = "Module Documents")]
        public int ModuleDocumentsCount { get; }

        [Display(Name = "Activity Documents")]
        public int ActivityDocumentsCount { get; }

        public bool HasDates => StartDate != null && EndDate != null;
        public bool HasModules => ModulesCount > 0;
        public bool HasActivities => ActivitiesCount > 0;
        public bool HasDocuments => CourseDocumentsCount > 0 || ModuleDocumentsCount > 0 || ActivityDocumentsCount > 0;

        public string DeleteType
        {
            get
            {
                var types = new List<string>();
                if (HasModules)
                {
                    types.Add("Modules");
                    if (HasActivities)
                    {
                        types.Add("Activities");
                    }
                }
                if (HasDocuments)
                {
                    types.Add("Documents");
                }

                var sb = new StringBuilder("Course");
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

    public class CourseDetailsViewModel
    {
        public CourseDetailsViewModel(Course course)
        {
            Name = course.Name;
            Description = course.Description;
            Modules = course.Modules.ToList().OrderBy(m => m.StartDate).Select(m => new ModuleViewModel(m));
            Documents = course.Documents.Select(d => new DocumentViewModel(d));
        }

        public string Name { get; }
        public string Description { get; }
        public IEnumerable<ModuleViewModel> Modules { get; }
        public IEnumerable<DocumentViewModel> Documents { get; }
    }
}
