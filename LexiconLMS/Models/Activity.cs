using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Models
{
    public class Activity
    {
        public Activity()
        {
        }

        public Activity(ActivityCreateViewModel activity)
        {
            Name = activity.Name;
            ActivityTypeId = activity.ActivityTypeId;
            Description = activity.Description;
            StartDate = activity.StartDate.Value;
            EndDate = activity.EndDate.Value;
            ModuleId = activity.ModuleId;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int ModuleId { get; set; }

        [Required]
        public int ActivityTypeId { get; set; }

        // Navigation Properties
        public virtual ActivityType ActivityType { get; set; }
        public virtual Module Module { get; set; }

        public void Update(ActivityCreateViewModel activity)
        {
            Name = activity.Name;
            Description = activity.Description;
            ActivityTypeId = activity.ActivityTypeId;
            if (activity.StartDate != null)
                StartDate = activity.StartDate.Value;
            if (activity.EndDate != null)
                EndDate = activity.EndDate.Value;
        }
    }

    public class ActivityType
    {
        public int Id { get; set; }
        public bool IsAssignment { get; set; }
        public string Name { get; set; }
    }

    public class ActivityIndexVieWModel
    {
        public ActivityIndexVieWModel(Module module, IEnumerable<ActivityViewModel> activities)
        {
            ModuleName = module.Name;
            ModuleId = module.Id;
            Activities = activities;
        }

        public string ModuleName { get; }
        public int ModuleId { get; }
        public IEnumerable<ActivityViewModel> Activities { get; }
    }

    public class ActivityViewModel
    {
        public ActivityViewModel(Activity activity)
        {
            Id = activity.Id;
            Name = activity.Name;
            Description = activity.Description;
            StartDate = activity.StartDate.ToString("yyyy-MM-dd HH:mm");
            EndDate = activity.EndDate.ToString("yyyy-MM-dd HH:mm");
            ActivityType = activity.ActivityType.Name;
            IsAssignment = activity.ActivityType.IsAssignment;
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }

        [Display(Name = "Start Date")]
        public string StartDate { get; }

        [Display(Name = "End Date")]
        public string EndDate { get; }

        [Display(Name = "Activity Type")]
        public string ActivityType { get; }

        public bool IsAssignment { get; }
    }

    public class ActivityCreateViewModel
    {
        public ActivityCreateViewModel()
        {
        }

        public ActivityCreateViewModel(Module module)
        {
            ModuleId = module.Id;
            ModuleName = module.Name;
            if (StartDate == null)
                StartDate = DateTime.Today.AddHours(value: 8);
            if (EndDate == null)
                EndDate = DateTime.Today.AddHours(value: 17);
        }

        public ActivityCreateViewModel(Activity activity) : this(activity.Module)
        {
            Name = activity.Name;
            Description = activity.Description;
            StartDate = activity.StartDate;
            EndDate = activity.EndDate;
        }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start")]
        public DateTime? StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "End")]
        [GenericCompare(CompareToPropertyName = "StartDate", OperatorName = GenericCompareOperator.GreaterThan,
            ErrorMessage = "The field {0} must be {{0}} Start")]
        public DateTime? EndDate { get; set; }

        [Required]
        public int ModuleId { get; set; }

        [Required]
        public int ActivityTypeId { get; set; }

        public string ModuleName { get; set; }
    }
}
