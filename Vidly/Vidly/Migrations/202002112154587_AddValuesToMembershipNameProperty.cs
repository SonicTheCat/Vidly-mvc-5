namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValuesToMembershipNameProperty : DbMigration
    {
        public override void Up()
        {
            Sql(@"update dbo.MembershipTypes
                set Name = 'Pay as you go'
                where id = 1");

            Sql(@"update dbo.MembershipTypes
                set Name = 'Montly'
                where id = 2");

            Sql(@"update dbo.MembershipTypes
                set Name = 'Quaterly'
                where id = 3");

            Sql(@"update dbo.MembershipTypes
                set Name = 'Anualy'
                where id = 4");
        }
        
        public override void Down()
        {
        }
    }
}
