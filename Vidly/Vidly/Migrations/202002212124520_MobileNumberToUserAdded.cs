namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MobileNumberToUserAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Mobile", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Mobile");
        }
    }
}
