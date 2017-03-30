using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(2000)]
        public string Link { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }

        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int? ActivityId { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(250)]
        public string CourseDescription { get; set; }

        // Navigation properties
        public virtual Course Course { get; set; }
        public virtual Module Module { get; set; }
        public virtual Activity Activity { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        internal void Update(DocumentEditFileViewModel document)
        {
            Name = document.Name;
        }

        internal void Update(DocumentEditLinkViewModel document)
        {
            Name = document.Name;
            Link = document.Link;
        }
    }

    public class DocumentEditLinkViewModel
    {
        public DocumentEditLinkViewModel()
        {
        }

        public DocumentEditLinkViewModel(Document document)
        {
            Id = document.Id;
            Name = document.Name;
            Link = document.Link;
            CreatedBy = document.ApplicationUser.FullName;
            CreateDate = document.CreateDate;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Url)]
        public string Link { get; set; }

        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Created")]
        public DateTime CreateDate { get; set; }
    }

    public class DocumentEditFileViewModel
    {
        public DocumentEditFileViewModel()
        {
        }

        public DocumentEditFileViewModel(Document document)
        {
            Id = document.Id;
            Name = document.Name;
            FileName = document.FileName;
            CreatedBy = document.ApplicationUser.FullName;
            CreateDate = document.CreateDate;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "File")]
        public string FileName { get; set; }

        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Created")]
        public DateTime CreateDate { get; set; }
    }

    public class DocumentDeleteViewModel
    {
        public DocumentDeleteViewModel()
        {
        }

        public DocumentDeleteViewModel(Document document)
        {
            Name = document.Name;
            FileName = document.FileName;
            Link = document.Link;
            CreatedBy = document.ApplicationUser.FullName;
            CreateDate = document.CreateDate;
        }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "File")]
        public string FileName { get; set; }

        [Display(Name = "Link")]
        public string Link { get; set; }

        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Created")]
        public DateTime CreateDate { get; set; }

    }

    public class CreateDocumentFileViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public HttpPostedFileBase Upload { get; set; }
    }

    public class CreateDocumentLinkViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string Link { get; set; }
    }

    public class CreateCourseDocumentFileViewModel : CreateDocumentFileViewModel
    {
        public CreateCourseDocumentFileViewModel()
        {
        }

        public CreateCourseDocumentFileViewModel(Course course)
        {
            CourseName = course.Name;
            CourseId = course.Id;
        }

        [Required]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }

    public class CreateCourseDocumentLinkViewModel : CreateDocumentLinkViewModel
    {
        public CreateCourseDocumentLinkViewModel()
        {
        }

        public CreateCourseDocumentLinkViewModel(Course course)
        {
            CourseName = course.Name;
            CourseId = course.Id;
        }

        [Required]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }

    public class CreateModuleDocumentFileViewModel : CreateDocumentFileViewModel
    {
        public CreateModuleDocumentFileViewModel()
        {
        }

        public CreateModuleDocumentFileViewModel(Module module)
        {
            ModuleName = module.Name;
            ModuleId = module.Id;
        }

        [Required]
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
    }

    public class CreateModuleDocumentLinkViewModel : CreateDocumentLinkViewModel
    {
        public CreateModuleDocumentLinkViewModel()
        {
        }

        public CreateModuleDocumentLinkViewModel(Module module)
        {
            ModuleName = module.Name;
            ModuleId = module.Id;
        }

        [Required]
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
    }

    public class CreateActivityDocumentFileViewModel : CreateDocumentFileViewModel
    {
        public CreateActivityDocumentFileViewModel()
        {
        }

        public CreateActivityDocumentFileViewModel(Activity activity)
        {
            ActivityName = activity.Name;
            ActivityId = activity.Id;
        }

        [Required]
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
    }

    public class CreateActivityDocumentLinkViewModel : CreateDocumentLinkViewModel
    {
        public CreateActivityDocumentLinkViewModel()
        {
        }

        public CreateActivityDocumentLinkViewModel(Activity activity)
        {
            ActivityName = activity.Name;
            ActivityId = activity.Id;
        }

        [Required]
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
    }

    public class CourseDocumentsViewModel
    {
        public CourseDocumentsViewModel(Course course, IEnumerable<DocumentViewModel> documents)
        {
            CourseName = course.Name;
            CourseId = course.Id;
            Documents = documents;
            CourseDescription = course.Description;
        }

        public string CourseName { get; }
        public int CourseId { get; }

        [DataType(DataType.MultilineText)]
        [StringLength(250)]
        public string CourseDescription { get;}
        public IEnumerable<DocumentViewModel> Documents { get; }
    }

    public class ModuleDocumentsViewModel
    {
        public ModuleDocumentsViewModel(Module module, IEnumerable<DocumentViewModel> documents)
        {
            ModuleName = module.Name;
            ModuleId = module.Id;
            Documents = documents;
        }

        public string ModuleName { get; }
        public int ModuleId { get; }
        public IEnumerable<DocumentViewModel> Documents { get;}
    }

    public class ActivityDocumentsViewModel
    {
        public ActivityDocumentsViewModel(Activity activity, IEnumerable<DocumentViewModel> documents)
        {
            ActivityName = activity.Name;
            ActivityId = activity.Id;
            Documents = documents;
        }

        public string ActivityName { get; }
        public int ActivityId { get; }
        public IEnumerable<DocumentViewModel> Documents { get; }
    }

    public class DocumentViewModel
    {
        public DocumentViewModel(Document document)
        {
            Id = document.Id;
            Name = document.Name;
            UploadedBy = document.ApplicationUser.FullName;
            CreateDate = document.CreateDate;
           
        }

        public int Id { get; }
        public string Name { get; }

        [Display(Name = "Created by")]
        public string UploadedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Created")]
        public DateTime CreateDate { get; set; }

    }
   }