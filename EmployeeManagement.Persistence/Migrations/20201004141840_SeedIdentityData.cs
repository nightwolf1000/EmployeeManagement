using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Persistence.Migrations
{
    public partial class SeedIdentityData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1c58a657-c7a3-4045-bbe6-58a8522f28af', N'guest@example.com', N'GUEST@EXAMPLE.COM', N'guest@example.com', N'GUEST@EXAMPLE.COM', 0, N'AQAAAAEAACcQAAAAEP06CGU1gVBM0dNQh/P4IkE9fCQtotnhUeu0tNoi99/Im04oW5n8i6zRKYnxjAM70w==', N'7PUSQL5CEUJG2LPEPHJIFVIWHYF3VMAA', N'c654399f-b18b-4e43-a9b5-430a738e000a', NULL, 0, 0, NULL, 1, 0)");
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c0a08daa-c486-4a63-904f-ca4ac756eb8d', N'administrator@example.com', N'ADMINISTRATOR@EXAMPLE.COM', N'administrator@example.com', N'ADMINISTRATOR@EXAMPLE.COM', 0, N'AQAAAAEAACcQAAAAEA/qPV7LkyV8599Q/e8i5HcxJ+fFOnPiWSypDx1BtJEdV6oubGm5/fUHqNc4icWmZw==', N'DCLMAX6CQFISJTY6TM7XIUVL2HK6DHYT', N'ee91d2ad-6803-4a41-bee0-cdb5332a766b', NULL, 0, 0, NULL, 1, 0)");
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'ed440eb4-d825-4ea4-b813-e3e2dcbee50b', N'Administrator', N'ADMINISTRATOR', N'd2b54503-ea01-4018-beb5-860ba76860a0')");
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c0a08daa-c486-4a63-904f-ca4ac756eb8d', N'ed440eb4-d825-4ea4-b813-e3e2dcbee50b')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUsers] WHERE [UserName] IN (N'guest@example.com', N'administrator@example.com')");
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetRoles] WHERE [Name] = N'Administrator'");
        }
    }
}
