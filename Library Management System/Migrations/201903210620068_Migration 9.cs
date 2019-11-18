namespace Library_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddBooks",
                c => new
                    {
                        bookid = c.Int(nullable: false, identity: true),
                        isbn = c.Int(nullable: false),
                        bookname = c.String(nullable: false),
                        authorname = c.String(nullable: false),
                        edition = c.String(nullable: false),
                        copies = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.bookid);
            
            CreateTable(
                "dbo.LR_Fine",
                c => new
                    {
                        fineId = c.Int(nullable: false, identity: true),
                        fine = c.Decimal(nullable: false, precision: 18, scale: 2),
                        dayLimit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.fineId);
            
            CreateTable(
                "dbo.LR_Issue",
                c => new
                    {
                        issueid = c.Int(nullable: false, identity: true),
                        issuenceNo = c.Int(nullable: false),
                        isbn = c.Int(nullable: false),
                        stdid = c.Int(nullable: false),
                        staffid = c.Int(),
                        issuedate = c.DateTime(nullable: false),
                        returndate = c.DateTime(nullable: false),
                        isreturned = c.Boolean(nullable: false),
                        reissue = c.DateTime(),
                        reissue2 = c.DateTime(),
                        reissue3 = c.DateTime(),
                        returnedDate = c.DateTime(nullable: false),
                        issuedBy = c.String(nullable: false),
                        issuedOn = c.DateTime(nullable: false),
                        recievedby = c.String(),
                        recivedDate = c.DateTime(nullable: false),
                        fineDue = c.Decimal(precision: 18, scale: 2),
                        fineRecieved = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.issueid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LR_Issue");
            DropTable("dbo.LR_Fine");
            DropTable("dbo.AddBooks");
        }
    }
}
