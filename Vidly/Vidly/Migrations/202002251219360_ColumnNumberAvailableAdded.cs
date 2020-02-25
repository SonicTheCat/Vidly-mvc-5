namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnNumberAvailableAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Number Available", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Number Available");
        }
    }
}
