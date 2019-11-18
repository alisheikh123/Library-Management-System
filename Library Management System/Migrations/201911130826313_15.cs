namespace Library_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LR_Issue", "Label", c => c.String());
            AddColumn("dbo.LR_Issue", "X", c => c.Double(nullable: false));
            AddColumn("dbo.LR_Issue", "Y", c => c.Double(nullable: false));
            AddColumn("dbo.LR_Issue", "Z", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LR_Issue", "Z");
            DropColumn("dbo.LR_Issue", "Y");
            DropColumn("dbo.LR_Issue", "X");
            DropColumn("dbo.LR_Issue", "Label");
        }
    }
}
