namespace Library_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LR_Books", "artCat_id", c => c.Int());
            CreateIndex("dbo.LR_Books", "artCat_id");
            AddForeignKey("dbo.LR_Books", "artCat_id", "dbo.LR_ArticleCategory", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LR_Books", "artCat_id", "dbo.LR_ArticleCategory");
            DropIndex("dbo.LR_Books", new[] { "artCat_id" });
            DropColumn("dbo.LR_Books", "artCat_id");
        }
    }
}
