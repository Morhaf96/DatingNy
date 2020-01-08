namespace LuvDating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbfriendtable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FriendModelApplicationUsers", newName: "ApplicationUserFriendModels");
            DropPrimaryKey("dbo.ApplicationUserFriendModels");
            AddPrimaryKey("dbo.ApplicationUserFriendModels", new[] { "ApplicationUser_Id", "FriendModel_FriendRequestReciever" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ApplicationUserFriendModels");
            AddPrimaryKey("dbo.ApplicationUserFriendModels", new[] { "FriendModel_FriendRequestReciever", "ApplicationUser_Id" });
            RenameTable(name: "dbo.ApplicationUserFriendModels", newName: "FriendModelApplicationUsers");
        }
    }
}
