namespace Library_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration142 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LR_ReturnBook", "student_id", c => c.Int());
            AddColumn("dbo.LR_ReturnBook", "Department", c => c.String());
            AddColumn("dbo.LR_ReturnBook", "book_title", c => c.String());
            AddColumn("dbo.LR_ReturnBook", "ISBN", c => c.Int(nullable: false));
            AddColumn("dbo.LR_ReturnBook", "Issue_Date", c => c.DateTime());
            AddColumn("dbo.LR_ReturnBook", "DueDate", c => c.DateTime());
            AddColumn("dbo.LR_ReturnBook", "fine", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.LR_ReturnBook", "issuedBy");
            DropColumn("dbo.LR_ReturnBook", "issuedOn");
            DropColumn("dbo.LR_ReturnBook", "recievedby");
            DropColumn("dbo.LR_ReturnBook", "recivedDate");
            DropColumn("dbo.LR_ReturnBook", "fineDue");
            DropColumn("dbo.LR_ReturnBook", "fineRecieved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LR_ReturnBook", "fineRecieved", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.LR_ReturnBook", "fineDue", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.LR_ReturnBook", "recivedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LR_ReturnBook", "recievedby", c => c.String());
            AddColumn("dbo.LR_ReturnBook", "issuedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.LR_ReturnBook", "issuedBy", c => c.String(nullable: false));
            DropColumn("dbo.LR_ReturnBook", "fine");
            DropColumn("dbo.LR_ReturnBook", "DueDate");
            DropColumn("dbo.LR_ReturnBook", "Issue_Date");
            DropColumn("dbo.LR_ReturnBook", "ISBN");
            DropColumn("dbo.LR_ReturnBook", "book_title");
            DropColumn("dbo.LR_ReturnBook", "Department");
            DropColumn("dbo.LR_ReturnBook", "student_id");
        }
    }
}
