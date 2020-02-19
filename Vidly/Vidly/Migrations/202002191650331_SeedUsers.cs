namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4a7afa0b-b78e-4ea4-bdc2-a0790e113407', N'admin@abv.bg', 0, N'AJ1FIRS5m0Dod5iFi9vNg1Y9m5vX4vxT06XwUPyIBxx5jYZdd0U8vY7Gk3GZLEZjYw==', N'dfdb2322-11d8-4781-a78e-93044726219b', NULL, 0, 0, NULL, 1, 0, N'admin@abv.bg')
    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'71a4d630-2066-4192-bde4-e65177b658ca', N'guest@vidly.com', 0, N'AHYgbpneC3nD6yoTm8yBT6lYLa8dkMCjlRzwDSOjiNSB/KgXYNsgDWONmEmlfBYhwQ==', N'381d3eed-14b2-4d1c-97ca-066ec606d112', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
    INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c93ad766-9f9b-4eee-b6ed-51405a10944a', N'CanManageMovies')
    INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4a7afa0b-b78e-4ea4-bdc2-a0790e113407', N'c93ad766-9f9b-4eee-b6ed-51405a10944a')
             ");


        }

        public override void Down()
        {
        }
    }
}
