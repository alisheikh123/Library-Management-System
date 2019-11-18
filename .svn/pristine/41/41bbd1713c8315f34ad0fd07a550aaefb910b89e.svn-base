namespace Library_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LR_Books", "status", c => c.String());
            AddColumn("dbo.LR_Books", "article_category", c => c.String());
            AddColumn("dbo.LR_Books", "type_category", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LR_Books", "type_category");
            DropColumn("dbo.LR_Books", "article_category");
            DropColumn("dbo.LR_Books", "status");
        }
    }
}
