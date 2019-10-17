namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'3a442a94-2b71-488b-9eee-9f770ba85c78', N'admin@vidly.com', 0, N'AGHmjJi8lacDVwetSfnw/Ge2NBJonKjI78JyRp5k27HInNhEtUjGo0pXI5C9ChbTug==', N'd201184e-ed4f-48f7-89ea-08ad6f23237c', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'59dbb415-15c6-47f1-841b-e4eb96087b9b', N'guest@vidly.com', 0, N'AHSD7y0jLHGYtXULjNJRXq22S7dv2TDctd+xDWT1LHc0p734ZUtqFYMi7Wm2JmhVxQ==', N'fd88f3f9-2c67-45ad-a75c-10fed484cf79', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f48e5323-08ee-465a-88bc-552d9e0e55f5', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3a442a94-2b71-488b-9eee-9f770ba85c78', N'f48e5323-08ee-465a-88bc-552d9e0e55f5')");

        }
        
        public override void Down()
        {
        }
    }
}
