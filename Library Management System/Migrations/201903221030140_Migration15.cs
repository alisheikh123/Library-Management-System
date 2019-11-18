namespace Library_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration15 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LR_ReturnBook",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        returnedDate = c.DateTime(nullable: false),
                        issuedBy = c.String(nullable: false),
                        issuedOn = c.DateTime(nullable: false),
                        recievedby = c.String(),
                        recivedDate = c.DateTime(nullable: false),
                        fineDue = c.Decimal(precision: 18, scale: 2),
                        fineRecieved = c.Decimal(precision: 18, scale: 2),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            DropColumn("dbo.LR_Issue", "returnedDate");
            DropColumn("dbo.LR_Issue", "issuedBy");
            DropColumn("dbo.LR_Issue", "issuedOn");
            DropColumn("dbo.LR_Issue", "recievedby");
            DropColumn("dbo.LR_Issue", "recivedDate");
            DropColumn("dbo.LR_Issue", "fineDue");
            DropColumn("dbo.LR_Issue", "fineRecieved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LR_Issue", "fineRecieved", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.LR_Issue", "fineDue", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.LR_Issue", "recivedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LR_Issue", "recievedby", c => c.String());
            AddColumn("dbo.LR_Issue", "issuedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.LR_Issue", "issuedBy", c => c.String(nullable: false));
            AddColumn("dbo.LR_Issue", "returnedDate", c => c.DateTime(nullable: false));
            DropTable("dbo.LR_ReturnBook");
        }
    }
}
