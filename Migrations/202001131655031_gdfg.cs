namespace LuvDating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gdfg : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserFriendModels", newName: "FriendModelApplicationUsers");
            DropPrimaryKey("dbo.FriendModelApplicationUsers");
            AddPrimaryKey("dbo.FriendModelApplicationUsers", new[] { "FriendModel_FriendId", "ApplicationUser_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.FriendModelApplicationUsers");
            AddPrimaryKey("dbo.FriendModelApplicationUsers", new[] { "ApplicationUser_Id", "FriendModel_FriendId" });
            RenameTable(name: "dbo.FriendModelApplicationUsers", newName: "ApplicationUserFriendModels");
        }
    }
}
