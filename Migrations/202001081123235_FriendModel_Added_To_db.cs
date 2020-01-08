namespace LuvDating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FriendModel_Added_To_db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        FriendUserID = c.String(),
                        IsFriend = c.Boolean(nullable: false),
                        pendingRequest = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendModels", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.FriendModels", new[] { "UserID" });
            DropTable("dbo.FriendModels");
        }
    }
}
