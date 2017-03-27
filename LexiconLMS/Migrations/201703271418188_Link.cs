namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Link : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "Link", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "Link");
        }
    }
}
