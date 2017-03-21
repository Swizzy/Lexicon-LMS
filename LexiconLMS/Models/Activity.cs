﻿using System;
using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
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
        [GenericCompare(CompareToPropertyName = "StartDate", OperatorName = GenericCompareOperator.GreaterThan, ErrorMessage = "The field {0} must be {{0}} Start")]
        public DateTime EndDate { get; set; }

        [Required]
        public int ModuleId { get; set; }

        [Required]
        public int ActivityTypeId { get; set; }

        public virtual ActivityType ActivityType { get; set; }
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
}
