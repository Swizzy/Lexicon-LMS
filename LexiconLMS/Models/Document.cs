using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Web;

namespace LexiconLMS.Models
{
    public class Document
    {
        public Document()
        {
        }

        private Document(string userId, string name = null)
        {
            CreateDate = DateTime.Now;
            UserId = userId;
            Name = name;
        }

        public Document(CreateDocumentFileViewModel model, string userId) : this(userId, model.Name)
        {
            AddFile(model.Upload);
        }

        public Document(CreateDocumentLinkViewModel model, string userId) : this(userId, model.Name)
        {
            Link = model.Link;
        }

        public Document(UpdateAssignmentViewModel model, string userId) : this(userId)
        {
            AddFile(model.Upload);
        }

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

        // Navigation properties
        public virtual Course Course { get; set; }
        public virtual Module Module { get; set; }
        public virtual Activity Activity { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        private void AddFile(HttpPostedFileBase upload)
        {
            using (var br = new BinaryReader(upload.InputStream))
            {
                FileName = upload.FileName;
                ContentType = upload.ContentType;
                Content = br.ReadBytes(upload.ContentLength);
            }
        }

        internal void Update(DocumentEditFileViewModel document)
        {
            Name = document.Name;
        }

        internal void Update(DocumentEditLinkViewModel document)
        {
            Name = document.Name;
            Link = document.Link;
            CreateDate = DateTime.Now;
        }

        internal void Update(UpdateAssignmentViewModel document)
        {
            CreateDate = DateTime.Now;
            AddFile(document.Upload);
        }
    }

    public abstract class DocumentEditViewModel
    {
        public DocumentEditViewModel()
        {
        }

        public DocumentEditViewModel(Document document)
        {
            Id = document.Id;
            Name = document.Name;
            CreatedBy = document.ApplicationUser.FullName;
            CreateDate = document.CreateDate;
            if (document.ActivityId != null)
                DocumentType = "Activity";
            if (document.ModuleId != null)
                DocumentType = "Module";
            if (document.CourseId != null)
                DocumentType = "Course";
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Created")]
        public DateTime CreateDate { get; set; }

        public string DocumentType { get; }
    }

    public abstract class CreateDocumentFileViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public HttpPostedFileBase Upload { get; set; }
    }

    public abstract class CreateDocumentLinkViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string Link { get; set; }
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
        public string UploadedBy { get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(Name = "Created")]
        public DateTime CreateDate { get; }

    }

    public class DocumentEditLinkViewModel : DocumentEditViewModel
    {
        public DocumentEditLinkViewModel()
        {
        }

        public DocumentEditLinkViewModel(Document document) : base(document)
        {
            Link = document.Link;
        }

        [Required]
        [DataType(DataType.Url)]
        public string Link { get; set; }
    }

    public class DocumentEditFileViewModel : DocumentEditViewModel
    {
        public DocumentEditFileViewModel()
        {
        }

        public DocumentEditFileViewModel(Document document) : base(document)
        {
            FileName = document.FileName;
        }

        [Display(Name = "File")]
        public string FileName { get; set; }

    }

    public class DocumentDeleteViewModel : DocumentEditViewModel
    {
        public DocumentDeleteViewModel()
        {
        }

        public DocumentDeleteViewModel(Document document) : base(document)
        {
            FileName = document.FileName;
            Link = document.Link;
        }

        [Display(Name = "File")]
        public string FileName { get; set; }

        [Display(Name = "Link")]
        public string Link { get; set; }

        public string DeleteType => DocumentType + " " + (string.IsNullOrWhiteSpace(Link) ? "File" : "Link");
    }

    public class CourseCreateDocumentFileViewModel : CreateDocumentFileViewModel
    {
        public CourseCreateDocumentFileViewModel()
        {
        }

        public CourseCreateDocumentFileViewModel(Course course)
        {
            CourseId = course.Id;
        }

        [Required]
        public int CourseId { get; set; }
    }

    public class CourseCreateDocumentLinkViewModel : CreateDocumentLinkViewModel
    {
        public CourseCreateDocumentLinkViewModel()
        {
        }

        public CourseCreateDocumentLinkViewModel(Course course)
        {
            CourseId = course.Id;
        }

        [Required]
        public int CourseId { get; set; }
    }

    public class CourseDocumentsViewModel
    {
        public CourseDocumentsViewModel(Course course, IEnumerable<DocumentViewModel> documents)
        {
            CourseId = course.Id;
            Documents = documents;
        }

        public int CourseId { get; }
        public IEnumerable<DocumentViewModel> Documents { get; }
    }

    public class ModuleCreateDocumentFileViewModel : CreateDocumentFileViewModel
    {
        public ModuleCreateDocumentFileViewModel()
        {
        }

        public ModuleCreateDocumentFileViewModel(Module module)
        {
            ModuleId = module.Id;
        }

        [Required]
        public int ModuleId { get; set; }
    }

    public class ModuleCreateDocumentLinkViewModel : CreateDocumentLinkViewModel
    {
        public ModuleCreateDocumentLinkViewModel()
        {
        }

        public ModuleCreateDocumentLinkViewModel(Module module)
        {
            ModuleId = module.Id;
        }

        [Required]
        public int ModuleId { get; set; }
    }

    public class ModuleDocumentsViewModel
    {
        public ModuleDocumentsViewModel(Module module, IEnumerable<DocumentViewModel> documents)
        {
            ModuleId = module.Id;
            Documents = documents;
        }

        public int ModuleId { get; }
        public IEnumerable<DocumentViewModel> Documents { get; }
    }

    public class ActivityDocumentsViewModel
    {
        public ActivityDocumentsViewModel(Activity activity, IEnumerable<DocumentViewModel> documents)
        {
            ActivityId = activity.Id;
            Documents = documents;
        }

        public int ActivityId { get; }
        public IEnumerable<DocumentViewModel> Documents { get; }
    }

    public class ActivityCreateDocumentFileViewModel : CreateDocumentFileViewModel
    {
        public ActivityCreateDocumentFileViewModel()
        {
        }

        public ActivityCreateDocumentFileViewModel(Activity activity)
        {
            ActivityId = activity.Id;
        }

        [Required]
        public int ActivityId { get; set; }
    }

    public class ActivityCreateDocumentLinkViewModel : CreateDocumentLinkViewModel
    {
        public ActivityCreateDocumentLinkViewModel()
        {
        }

        public ActivityCreateDocumentLinkViewModel(Activity activity)
        {
            ActivityId = activity.Id;
        }

        [Required]
        public int ActivityId { get; set; }
    }
}