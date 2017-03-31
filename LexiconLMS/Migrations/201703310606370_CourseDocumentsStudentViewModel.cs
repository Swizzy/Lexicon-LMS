namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseDocumentsStudentViewModel : DbMigration
    {
        public override void Up()
        {
           // DropColumn("dbo.Documents", "CourseDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "CourseDescription", c => c.String(maxLength: 250));
        }
    }
}
