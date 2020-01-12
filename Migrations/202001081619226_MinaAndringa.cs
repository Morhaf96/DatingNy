namespace LuvDating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MinaAndringa : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProfilePostViewModels", newName: "PostModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PostModels", newName: "ProfilePostViewModels");
        }
    }
}
