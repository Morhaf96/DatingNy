namespace LuvDating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keychange_friends100 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId" }, "dbo.FriendModels");
            DropIndex("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId" });
            DropPrimaryKey("dbo.FriendModels");
            DropPrimaryKey("dbo.ApplicationUserFriendModels");
            AddColumn("dbo.ApplicationUserFriendModels", "FriendModel_pendingRequest", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.FriendModels", new[] { "FriendRequestReciever", "FriendId", "pendingRequest" });
            AddPrimaryKey("dbo.ApplicationUserFriendModels", new[] { "ApplicationUser_Id", "FriendModel_FriendRequestReciever", "FriendModel_FriendId", "FriendModel_pendingRequest" });
            CreateIndex("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId", "FriendModel_pendingRequest" });
            AddForeignKey("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId", "FriendModel_pendingRequest" }, "dbo.FriendModels", new[] { "FriendRequestReciever", "FriendId", "pendingRequest" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId", "FriendModel_pendingRequest" }, "dbo.FriendModels");
            DropIndex("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId", "FriendModel_pendingRequest" });
            DropPrimaryKey("dbo.ApplicationUserFriendModels");
            DropPrimaryKey("dbo.FriendModels");
            DropColumn("dbo.ApplicationUserFriendModels", "FriendModel_pendingRequest");
            AddPrimaryKey("dbo.ApplicationUserFriendModels", new[] { "ApplicationUser_Id", "FriendModel_FriendRequestReciever", "FriendModel_FriendId" });
            AddPrimaryKey("dbo.FriendModels", new[] { "FriendRequestReciever", "FriendId" });
            CreateIndex("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId" });
            AddForeignKey("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "FriendModel_FriendId" }, "dbo.FriendModels", new[] { "FriendRequestReciever", "FriendId" }, cascadeDelete: true);
        }
    }
}
