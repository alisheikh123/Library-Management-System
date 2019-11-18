namespace Library_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration121 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ArticleCategories", newName: "LR_ArticleCategory");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.LR_ArticleCategory", newName: "ArticleCategories");
        }
    }
}
