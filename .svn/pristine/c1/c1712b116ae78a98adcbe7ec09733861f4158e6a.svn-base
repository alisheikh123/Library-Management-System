namespace Library_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration122 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LR_Books", "name", c => c.String(nullable: false));
            DropColumn("dbo.LR_Books", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LR_Books", "Title", c => c.String(nullable: false));
            DropColumn("dbo.LR_Books", "name");
        }
    }
}
