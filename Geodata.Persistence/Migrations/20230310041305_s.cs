using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Geodata.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class s : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9df22342-667d-47ea-9df3-968dc9067b46");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5e195428-b874-465e-a6a3-4f89eca0dfb0", "f10ec17a-8c8a-44c3-85f4-639ab898d0fb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5efaada7-1916-4833-a239-4835cef570ee", "f10ec17a-8c8a-44c3-85f4-639ab898d0fb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e195428-b874-465e-a6a3-4f89eca0dfb0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5efaada7-1916-4833-a239-4835cef570ee");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f10ec17a-8c8a-44c3-85f4-639ab898d0fb");

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4aa212b6-29df-41f9-9f32-107d855b7ff8", "90120415-c5b9-4083-beef-b69bbf66a186", "Moderator", "MODERATOR" },
                    { "8e66d5b8-62eb-4085-b360-3d8b71418c34", "d701018a-bf65-4252-82cb-03affd7dd6fa", "User", "USER" },
                    { "c064a770-f249-4947-ac8e-f58815696eed", "d37d4a6b-c15b-4ef4-84f3-e6d7ef39a960", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4aa212b6-29df-41f9-9f32-107d855b7ff8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e66d5b8-62eb-4085-b360-3d8b71418c34");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c064a770-f249-4947-ac8e-f58815696eed");

            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5e195428-b874-465e-a6a3-4f89eca0dfb0", "c2531dfd-43f9-479b-9315-0908bc319652", "User", "USER" },
                    { "5efaada7-1916-4833-a239-4835cef570ee", "394097b1-8c55-43b5-8a79-39280c66253e", "Admin", "ADMIN" },
                    { "9df22342-667d-47ea-9df3-968dc9067b46", "ee9ff487-caeb-4f01-9557-90eccda4b5d6", "Moderator", "MODERATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f10ec17a-8c8a-44c3-85f4-639ab898d0fb", 0, "6d03427a-efbd-45c3-b610-9564dcc66c82", "admin@gmail.com", false, false, null, "Admin", "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEFMYSPS9NRq0upUWjXIYJHnOLGPvHrxzMlEh9jntNl1PeUce3M3IO4r50NCZd2WL0Q==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "67a7b03b-fa86-4b6b-a332-174d277b0fb5", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "5e195428-b874-465e-a6a3-4f89eca0dfb0", "f10ec17a-8c8a-44c3-85f4-639ab898d0fb" },
                    { "5efaada7-1916-4833-a239-4835cef570ee", "f10ec17a-8c8a-44c3-85f4-639ab898d0fb" }
                });
        }
    }
}
