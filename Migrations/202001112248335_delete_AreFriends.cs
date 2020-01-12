namespace LuvDating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_AreFriends : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FriendModels", "AreFriends");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FriendModels", "AreFriends", c => c.Boolean(nullable: false));
        }
    }
}
