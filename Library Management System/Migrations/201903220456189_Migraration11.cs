namespace Library_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migraration11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LR_Books", "Title", c => c.String(nullable: false));
            AddColumn("dbo.LR_Books", "description", c => c.String());
            AddColumn("dbo.LR_Books", "date_year_ofpublication", c => c.DateTime(nullable: false));
            AddColumn("dbo.LR_Books", "edition", c => c.Single(nullable: false));
            AddColumn("dbo.LR_Books", "Impact_Factor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.LR_Books", "Eigen_Factor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.LR_Books", "Bill_num", c => c.Int(nullable: false));
            DropColumn("dbo.LR_Books", "name");
            DropColumn("dbo.LR_Books", "publication_year");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LR_Books", "publication_year", c => c.Int(nullable: false));
            AddColumn("dbo.LR_Books", "name", c => c.String(nullable: false));
            DropColumn("dbo.LR_Books", "Bill_num");
            DropColumn("dbo.LR_Books", "Eigen_Factor");
            DropColumn("dbo.LR_Books", "Impact_Factor");
            DropColumn("dbo.LR_Books", "edition");
            DropColumn("dbo.LR_Books", "date_year_ofpublication");
            DropColumn("dbo.LR_Books", "description");
            DropColumn("dbo.LR_Books", "Title");
        }
    }
}
