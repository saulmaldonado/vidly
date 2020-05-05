namespace vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [Phone], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c8392009-f16b-44b6-a9cf-f941f1963659', N'admin@vidly.com', 0, N'AMoe61hrS7i5AjYiVzzyNa0JWfcsq03PLSlJEy//Xi2PQx2sjMNyFF1h3Rkb9XQNFw==', N'cbd21f72-4d17-4a77-9acc-ebed6d01eb6f', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [Phone], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e15ee75d-b8ad-4f4f-aa0b-f7ec552b9337', N'guset@vidly.com', 0, N'AJAGzGv6ccCZL29Br0pI6iKrexka/S08Vru+T+kQMzgsTwVwxE2M7UTrWjfBSWiBDA==', N'97c70477-5748-4993-a452-81bcead9dbfc', NULL, 0, 0, NULL, 1, 0, N'guset@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'0d4d349b-0c39-48ae-bb18-021c30046c40', N'StoreManager')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c8392009-f16b-44b6-a9cf-f941f1963659', N'0d4d349b-0c39-48ae-bb18-021c30046c40')

");
        }
        
        public override void Down()
        {
        }
    }
}
