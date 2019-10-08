namespace AspNetRoleBasedSecurity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inti : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        AutoId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AutoId);
            
            CreateTable(
                "dbo.ArticlesComments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Comments = c.String(),
                        ThisDateTime = c.DateTime(),
                        ArticleId = c.Int(nullable: false),
                        Rating = c.Int(),
                        User_n = c.String(),
                        Status_Com = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ArticlesComments");
            DropTable("dbo.Articles");
        }
    }
}
