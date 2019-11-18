namespace Library_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LR_Books", "ISSN", c => c.Int(nullable: false));
            AddColumn("dbo.LR_Books", "Language", c => c.String());
            AddColumn("dbo.LR_Books", "Company", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LR_Books", "Company");
            DropColumn("dbo.LR_Books", "Language");
            DropColumn("dbo.LR_Books", "ISSN");
        }
    }
}
