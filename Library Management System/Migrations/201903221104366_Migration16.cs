namespace Library_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration16 : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.LR_Issue", "issueid", "id");
            AddColumn("dbo.LR_Issue", "student_id", c => c.Int());
            AddColumn("dbo.LR_Issue", "student_name", c => c.String());
            AddColumn("dbo.LR_Issue", "Department", c => c.String());
            AddColumn("dbo.LR_Issue", "batch", c => c.Int(nullable: false));
            AddColumn("dbo.LR_Issue", "book_title", c => c.String());
            AddColumn("dbo.LR_Issue", "Issue_Date", c => c.DateTime());
            AddColumn("dbo.LR_Issue", "ExpiryDate", c => c.DateTime());
            AddColumn("dbo.LR_Issue", "Status", c => c.String());
            DropColumn("dbo.LR_Issue", "issuenceNo");
            DropColumn("dbo.LR_Issue", "stdid");
            DropColumn("dbo.LR_Issue", "staffid");
            DropColumn("dbo.LR_Issue", "issuedate");
            DropColumn("dbo.LR_Issue", "returndate");
            DropColumn("dbo.LR_Issue", "isreturned");
            DropColumn("dbo.LR_Issue", "reissue");
            DropColumn("dbo.LR_Issue", "reissue2");
            DropColumn("dbo.LR_Issue", "reissue3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LR_Issue", "reissue3", c => c.DateTime());
            AddColumn("dbo.LR_Issue", "reissue2", c => c.DateTime());
            AddColumn("dbo.LR_Issue", "reissue", c => c.DateTime());
            AddColumn("dbo.LR_Issue", "isreturned", c => c.Boolean(nullable: false));
            AddColumn("dbo.LR_Issue", "returndate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LR_Issue", "issuedate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LR_Issue", "staffid", c => c.Int());
            AddColumn("dbo.LR_Issue", "stdid", c => c.Int(nullable: false));
            AddColumn("dbo.LR_Issue", "issuenceNo", c => c.Int(nullable: false));
            AddColumn("dbo.LR_Issue", "issueid", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.LR_Issue");
            DropColumn("dbo.LR_Issue", "Status");
            DropColumn("dbo.LR_Issue", "ExpiryDate");
            DropColumn("dbo.LR_Issue", "Issue_Date");
            DropColumn("dbo.LR_Issue", "book_title");
            DropColumn("dbo.LR_Issue", "batch");
            DropColumn("dbo.LR_Issue", "Department");
            DropColumn("dbo.LR_Issue", "student_name");
            DropColumn("dbo.LR_Issue", "student_id");
            DropColumn("dbo.LR_Issue", "id");
            AddPrimaryKey("dbo.LR_Issue", "issueid");
        }
    }
}
