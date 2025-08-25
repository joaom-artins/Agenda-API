using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Data.Migrations
{
    public partial class SeedRolesAndAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // IDs fixos para não gerar duplicação em futuras migrations
            var roleAdminId = Guid.NewGuid();
            var roleUserId = Guid.NewGuid();
            var adminUserId = Guid.NewGuid();

            // Inserir roles
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
                    { roleAdminId, "Admin", "ADMIN", Guid.NewGuid().ToString() },
                    { roleUserId, "User", "USER", Guid.NewGuid().ToString() }
                });

            // Inserir usuário admin padrão
            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<IdentityUser<Guid>>();
            var passwordHash = hasher.HashPassword(null, "Admin@123"); // senha padrão

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[]
                {
                    "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail",
                    "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp",
                    "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount",
                    "Name", "Occupation", "Type"
                },
                values: new object[]
                {
                    adminUserId, "admin", "ADMIN", "admin@system.com", "ADMIN@SYSTEM.COM",
                    true, passwordHash, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
                    false, false, true, 0,
                    "Administrator", "System Admin", 2 
                });

            // Vincular admin à role de Admin
            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: ["UserId", "RoleId"],
                values: [adminUserId, roleAdminId]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [UsersRoles]");
            migrationBuilder.Sql("DELETE FROM [Users]");
            migrationBuilder.Sql("DELETE FROM [Roles]");
        }
    }
}
