﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazor.SankoreAPI.Migrations
{
    public partial class RefactorDataContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1aa58b46-8208-479f-a26e-5b78035c280f", "3a4d344e-158e-46b2-baad-4890f2d26fd1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4f7af9a4-5221-422c-a10f-0817e81b84a5", "c487f4ae-ca04-40ea-a4ec-c241179cdac4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1aa58b46-8208-479f-a26e-5b78035c280f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f7af9a4-5221-422c-a10f-0817e81b84a5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a4d344e-158e-46b2-baad-4890f2d26fd1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c487f4ae-ca04-40ea-a4ec-c241179cdac4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a1590a1f-25bb-4846-ab4b-d3f7c0cbdef2", "676c19fc-b985-4001-9d86-603330909d21", "Administrator", "ADMINISTRATOR" },
                    { "cfe1e0ab-8511-4424-abdb-8ff82cf0c9f0", "b37e44a4-404e-4d03-b583-fffe7cdde03f", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "207384ff-8b8e-4dba-a68d-565cb63d40e5", 0, "8cc0a736-3123-468a-b746-4a82a5087ae1", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAENrqEyCAgeoNvhkjjS32duwW7vyEV6YPPHAoV1jB+VGYmgNjl6YQL/mtKnDjGk90VQ==", null, false, "92058efa-bc05-432b-b78a-75b0b5b256eb", false, "user@bookstore.com" },
                    { "df66f44f-f920-4592-9aed-ceab29956b39", 0, "ae481a3b-18df-4242-93f1-9933881b8c4f", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEKPqJ9IcS+UX/7jtwdY2kbmnNAiwG5kN0tU91ceLXYm0i0T86LOOJmj3PgeBjZ6piQ==", null, false, "5df4af95-4425-4148-b02f-849dd99a078f", false, "admin@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cfe1e0ab-8511-4424-abdb-8ff82cf0c9f0", "207384ff-8b8e-4dba-a68d-565cb63d40e5" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a1590a1f-25bb-4846-ab4b-d3f7c0cbdef2", "df66f44f-f920-4592-9aed-ceab29956b39" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cfe1e0ab-8511-4424-abdb-8ff82cf0c9f0", "207384ff-8b8e-4dba-a68d-565cb63d40e5" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a1590a1f-25bb-4846-ab4b-d3f7c0cbdef2", "df66f44f-f920-4592-9aed-ceab29956b39" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1590a1f-25bb-4846-ab4b-d3f7c0cbdef2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfe1e0ab-8511-4424-abdb-8ff82cf0c9f0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "207384ff-8b8e-4dba-a68d-565cb63d40e5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "df66f44f-f920-4592-9aed-ceab29956b39");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1aa58b46-8208-479f-a26e-5b78035c280f", "bcea014c-4856-4561-a230-bc47c29fa22d", "User", "USER" },
                    { "4f7af9a4-5221-422c-a10f-0817e81b84a5", "3aae8d43-6e26-4998-a2c2-034bcbf655c9", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3a4d344e-158e-46b2-baad-4890f2d26fd1", 0, "a94082ac-cc2c-4c48-a278-705dd58ed33e", "admin@sankorebookstore.com", false, "System", "Admin", false, null, "ADMIN@SANKOREBOOKSTORE.COM", "ADMIN@SANKOREBOOKSTORE.COM", "AQAAAAEAACcQAAAAEDDuVSdyUG4GrNpLArzRpM547rOadMvHnjFLvOfdlYQnFtH83FreSa/E9edOUmTj9w==", null, false, "999142c3-d42a-4495-9901-166f7fdcb6aa", false, "admin@sankorebookstore.com" },
                    { "c487f4ae-ca04-40ea-a4ec-c241179cdac4", 0, "18e21362-c90a-4de5-b9b3-3a7a476af0a5", "user@sankorebookstore.com", false, "System", "User", false, null, "USER@SANKOREBOOKSTORE.COM", "USER@SANKOREBOOKSTORE.COM", "AQAAAAEAACcQAAAAEFsB+xGHXzM1GHYp8xtRcrOG2r3DC9jVgnvxLkoeHogJ0Fdiq8NURRu9BistYFaYCw==", null, false, "f59c891f-3114-4221-8dd3-f0ed7b25d579", false, "user@sankorebookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1aa58b46-8208-479f-a26e-5b78035c280f", "3a4d344e-158e-46b2-baad-4890f2d26fd1" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4f7af9a4-5221-422c-a10f-0817e81b84a5", "c487f4ae-ca04-40ea-a4ec-c241179cdac4" });
        }
    }
}
