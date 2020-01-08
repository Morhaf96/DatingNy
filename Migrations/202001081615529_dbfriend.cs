namespace LuvDating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbfriend : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendModels",
                c => new
                    {
                        FriendRequestReciever = c.String(nullable: false, maxLength: 128),
                        AreFriends = c.Boolean(nullable: false),
                        pendingRequest = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FriendRequestReciever);
            
            CreateTable(
                "dbo.FriendModelApplicationUsers",
                c => new
                    {
                        FriendModel_FriendRequestReciever = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FriendModel_FriendRequestReciever, t.ApplicationUser_Id })
                .ForeignKey("dbo.FriendModels", t => t.FriendModel_FriendRequestReciever, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.FriendModel_FriendRequestReciever)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendModelApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendModelApplicationUsers", "FriendModel_FriendRequestReciever", "dbo.FriendModels");
            DropIndex("dbo.FriendModelApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.FriendModelApplicationUsers", new[] { "FriendModel_FriendRequestReciever" });
            DropTable("dbo.FriendModelApplicationUsers");
            DropTable("dbo.FriendModels");
        }
    }
}
