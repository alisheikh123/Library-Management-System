namespace Library_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration101 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LR_Books", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LR_Books", "Status", c => c.String());
        }
    }
}
