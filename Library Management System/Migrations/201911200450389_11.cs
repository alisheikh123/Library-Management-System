namespace Library_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LR_Issue", "Label");
            DropColumn("dbo.LR_Issue", "X");
            DropColumn("dbo.LR_Issue", "Y");
            DropColumn("dbo.LR_Issue", "Z");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LR_Issue", "Z", c => c.Double(nullable: false));
            AddColumn("dbo.LR_Issue", "Y", c => c.Double(nullable: false));
            AddColumn("dbo.LR_Issue", "X", c => c.Double(nullable: false));
            AddColumn("dbo.LR_Issue", "Label", c => c.String());
        }
    }
}
