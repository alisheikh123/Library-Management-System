namespace Library_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LR_Books", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LR_Books", "Status");
        }
    }
}
