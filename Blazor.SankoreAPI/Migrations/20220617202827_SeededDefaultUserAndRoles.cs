using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazor.SankoreAPI.Migrations
{
    public partial class SeededDefaultUserAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1aa58b46-8208-479f-a26e-5b78035c280f", "bcea014c-4856-4561-a230-bc47c29fa22d", "User", "USER" },
                    { "4f7af9a4-5221-422c-a10f-0817e81b84a5", "3aae8d43-6e26-4998-a2c2-034bcbf655c9", "Administrator", "ADMINISTRATOR" }
                });

            _ = migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3a4d344e-158e-46b2-baad-4890f2d26fd1", 0, "a94082ac-cc2c-4c48-a278-705dd58ed33e", "admin@sankorebookstore.com", false, "System", "Admin", false, null, "ADMIN@SANKOREBOOKSTORE.COM", "ADMIN@SANKOREBOOKSTORE.COM", "AQAAAAEAACcQAAAAEDDuVSdyUG4GrNpLArzRpM547rOadMvHnjFLvOfdlYQnFtH83FreSa/E9edOUmTj9w==", null, false, "999142c3-d42a-4495-9901-166f7fdcb6aa", false, "admin@sankorebookstore.com" },
                    { "c487f4ae-ca04-40ea-a4ec-c241179cdac4", 0, "18e21362-c90a-4de5-b9b3-3a7a476af0a5", "user@sankorebookstore.com", false, "System", "User", false, null, "USER@SANKOREBOOKSTORE.COM", "USER@SANKOREBOOKSTORE.COM", "AQAAAAEAACcQAAAAEFsB+xGHXzM1GHYp8xtRcrOG2r3DC9jVgnvxLkoeHogJ0Fdiq8NURRu9BistYFaYCw==", null, false, "f59c891f-3114-4221-8dd3-f0ed7b25d579", false, "user@sankorebookstore.com" }
                });

            _ = migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1aa58b46-8208-479f-a26e-5b78035c280f", "3a4d344e-158e-46b2-baad-4890f2d26fd1" });

            _ = migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4f7af9a4-5221-422c-a10f-0817e81b84a5", "c487f4ae-ca04-40ea-a4ec-c241179cdac4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1aa58b46-8208-479f-a26e-5b78035c280f", "3a4d344e-158e-46b2-baad-4890f2d26fd1" });

            _ = migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4f7af9a4-5221-422c-a10f-0817e81b84a5", "c487f4ae-ca04-40ea-a4ec-c241179cdac4" });

            _ = migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1aa58b46-8208-479f-a26e-5b78035c280f");

            _ = migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f7af9a4-5221-422c-a10f-0817e81b84a5");

            _ = migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a4d344e-158e-46b2-baad-4890f2d26fd1");

            _ = migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c487f4ae-ca04-40ea-a4ec-c241179cdac4");
        }
    }
}
