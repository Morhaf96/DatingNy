namespace LuvDating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keychangex2000 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId", "FriendModel_pendingRequest" }, "dbo.FriendModels");
            DropIndex("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId", "FriendModel_pendingRequest" });
            DropPrimaryKey("dbo.FriendModels");
            DropPrimaryKey("dbo.ApplicationUserFriendModels");
            AddPrimaryKey("dbo.FriendModels", new[] { "FriendRequestReciever", "FriendId" });
            AddPrimaryKey("dbo.ApplicationUserFriendModels", new[] { "ApplicationUser_Id", "FriendModel_FriendRequestReciever", "FriendModel_FriendId" });
            CreateIndex("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId" });
            AddForeignKey("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId" }, "dbo.FriendModels", new[] { "FriendRequestReciever", "FriendId" }, cascadeDelete: true);
            DropColumn("dbo.ApplicationUserFriendModels", "FriendModel_pendingRequest");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUserFriendModels", "FriendModel_pendingRequest", c => c.Int(nullable: false));
            DropForeignKey("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId" }, "dbo.FriendModels");
            DropIndex("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId" });
            DropPrimaryKey("dbo.ApplicationUserFriendModels");
            DropPrimaryKey("dbo.FriendModels");
            AddPrimaryKey("dbo.ApplicationUserFriendModels", new[] { "ApplicationUser_Id", "FriendModel_FriendRequestReciever", "FriendModel_FriendId", "FriendModel_pendingRequest" });
            AddPrimaryKey("dbo.FriendModels", new[] { "FriendRequestReciever", "FriendId", "pendingRequest" });
            CreateIndex("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId", "FriendModel_pendingRequest" });
            AddForeignKey("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId", "FriendModel_pendingRequest" }, "dbo.FriendModels", new[] { "FriendRequestReciever", "FriendId", "pendingRequest" }, cascadeDelete: true);
        }
    }
}
