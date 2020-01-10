namespace LuvDating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friendKeys : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendModels",
                c => new
                    {
                        FriendId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FriendRequestReciever = c.String(),
                        AreFriends = c.Boolean(nullable: false),
                        pendingRequest = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FriendId);
            
            CreateTable(
                "dbo.ApplicationUserFriendModels",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        FriendModel_FriendId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.FriendModel_FriendId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.FriendModels", t => t.FriendModel_FriendId, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.FriendModel_FriendId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserFriendModels", "FriendModel_FriendId", "dbo.FriendModels");
            DropForeignKey("dbo.ApplicationUserFriendModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendId" });
            DropIndex("dbo.ApplicationUserFriendModels", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserFriendModels");
            DropTable("dbo.FriendModels");
        }
    }
}
