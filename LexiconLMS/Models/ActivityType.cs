using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LexiconLMS.Models
{
    public class ActivityType
    {
        public ActivityType()
        {            
        }

        public ActivityType(ActivityTypeCreateViewModel activityType)
        {
            Update(activityType);
        }

        public int Id { get; set; }
        public bool IsAssignment { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }

        public void Update(ActivityTypeCreateViewModel model)
        {
            Name = model.Name;
            IsAssignment = model.IsAssignment;
        }
    }

    public class ActivityTypeDeleteViewModel
    {

        public ActivityTypeDeleteViewModel(ActivityType activityType)
        {
            Name = activityType.Name;
            IsAssignment = activityType.IsAssignment ? "Yes" : "No";
            ActivitiesCount = activityType.Activities.Count;
            ActivityDocumentsCount = activityType.Activities.SelectMany(a => a.Documents).Count();
        }

        public string Name { get; }

        [Display(Name = "Assignment")]
        public string IsAssignment { get; }

        [Display(Name = "Activities")]
        public int ActivitiesCount { get; }

        [Display(Name = "Activity Documents")]
        public int ActivityDocumentsCount { get; }

        public bool HasActivities => ActivitiesCount > 0;

        public bool HasActivityDocuments => ActivityDocumentsCount > 0;

        public string DeleteType
        {
            get
            {
                var types = new List<string>();
                if (HasActivities)
                {
                    types.Add("Activities");
                }
                if (HasActivityDocuments)
                {
                    types.Add("Documents");
                }

                var sb = new StringBuilder("Activity Type");
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

    public class ActivityTypeDetailViewModel
    {

        public ActivityTypeDetailViewModel(ActivityType activityType, IEnumerable<ActivityTypeActivityViewModel> activities)
        {
            Name = activityType.Name;
            IsAssignment = activityType.IsAssignment ? "Yes" : "No";
            Activities = activities;
        }

        public string Name { get; }

        [Display(Name = "Assignment")]
        public string IsAssignment { get; }

        public IEnumerable<ActivityTypeActivityViewModel> Activities { get; }
    }

    public class ActivityTypeViewModel {

        public ActivityTypeViewModel(ActivityType activityType)
        {
            Id = activityType.Id;
            Name = activityType.Name;
            IsAssignment = activityType.IsAssignment ? "Yes" : "No";
            ActivitiesCount = activityType.Activities.Count;
            CanBeModified = activityType.Activities.Count == 0;
        }

        public int Id { get; }

        public string Name { get; }

        [Display(Name = "Assignment")]
        public string IsAssignment { get; }

        [Display(Name = "Activities")]
        public int ActivitiesCount { get; }

        public bool CanBeModified { get; }
    }

    public class ActivityTypeCreateViewModel
    {
        public ActivityTypeCreateViewModel()
        {
        }

        public ActivityTypeCreateViewModel(ActivityType activityType)
        {
            Name = activityType.Name;
            IsAssignment = activityType.IsAssignment;
        }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Assignment")]
        public bool IsAssignment { get; set; }
    }

    public class ActivityTypeEditViewModel : ActivityTypeCreateViewModel
    {
        public ActivityTypeEditViewModel()
        {
        }

        public ActivityTypeEditViewModel(ActivityType activityType) : base(activityType)
        {
            DisabledString = activityType.Activities.Count == 0 ? "" : "disabled";
        }

        public string DisabledString { get; }
    }
}