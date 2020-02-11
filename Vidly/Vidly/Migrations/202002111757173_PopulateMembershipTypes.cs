namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class PopulateMembershipTypes : DbMigration
    {
        public override void Up()
        {
            //Pay as you go
            Sql(@"insert into MembershipTypes (SignUpFee, DurationInMonths, DiscountRate)
               values 
               (0, 0, 0)");

            //Monthly
            Sql(@"insert into MembershipTypes (SignUpFee, DurationInMonths, DiscountRate)
               values 
               (30, 1, 10)");

            //Quaterly
            Sql(@"insert into MembershipTypes (SignUpFee, DurationInMonths, DiscountRate)
               values 
               (90, 3, 15)");

            //Anualy 
            Sql(@"insert into MembershipTypes (SignUpFee, DurationInMonths, DiscountRate)
               values 
               (300, 12, 20)");
        }

        public override void Down()
        {
        }
    }
}
