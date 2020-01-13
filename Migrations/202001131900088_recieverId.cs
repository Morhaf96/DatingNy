namespace LuvDating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recieverId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatMessages", "RecieverId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChatMessages", "RecieverId");
        }
    }
}
