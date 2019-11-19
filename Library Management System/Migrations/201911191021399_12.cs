namespace Library_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LR_AssignRolestoUsers", "StudentName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LR_AssignRolestoUsers", "StudentName");
        }
    }
}
