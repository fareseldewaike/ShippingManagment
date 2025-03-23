using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shipping.repo.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "DateTime", "Name", "isDeleted", "status" },
                values: new object[] { 1, new DateTime(2024, 3, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), "الفرع الرئيسي", false, true });

            migrationBuilder.InsertData(
                table: "DeliverToVillages",
                columns: new[] { "Id", "AdditionalCost" },
                values: new object[] { 1, 10.0 });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Date", "IsDeleted", "Name" },
                values: new object[] { 1, new DateTime(2024, 3, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), false, "ادمن" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Branch" },
                    { 2, "City" },
                    { 3, "Governorate" },
                    { 4, "Employee" },
                    { 5, "Representative" },
                    { 6, "Merchant" },
                    { 7, "Order" },
                    { 8, "OrderReports" },
                    { 9, "Group" },
                    { 10, "ReasonsRefusalType" },
                    { 11, "ShippingType" },
                    { 12, "DeliverToVillage" },
                    { 13, "Weight" }
                });

            migrationBuilder.InsertData(
                table: "Weights",
                columns: new[] { "Id", "AdditionalPrice", "DefaultWeight" },
                values: new object[] { 1, 5.0, 10.0 });

            migrationBuilder.InsertData(
                table: "GroupPermissions",
                columns: new[] { "id", "Action", "GroupId", "PermissionId" },
                values: new object[,]
                {
                    { 1, "Add", 1, 1 },
                    { 2, "Edit", 1, 1 },
                    { 3, "Delete", 1, 1 },
                    { 4, "Show", 1, 1 },
                    { 5, "Add", 1, 2 },
                    { 6, "Edit", 1, 2 },
                    { 7, "Delete", 1, 2 },
                    { 8, "Show", 1, 2 },
                    { 9, "Add", 1, 3 },
                    { 10, "Edit", 1, 3 },
                    { 11, "Delete", 1, 3 },
                    { 12, "Show", 1, 3 },
                    { 13, "Add", 1, 4 },
                    { 14, "Edit", 1, 4 },
                    { 15, "Delete", 1, 4 },
                    { 16, "Show", 1, 4 },
                    { 17, "Add", 1, 5 },
                    { 18, "Edit", 1, 5 },
                    { 19, "Delete", 1, 5 },
                    { 20, "Show", 1, 5 },
                    { 21, "Add", 1, 6 },
                    { 22, "Edit", 1, 6 },
                    { 23, "Delete", 1, 6 },
                    { 24, "Show", 1, 6 },
                    { 25, "Add", 1, 7 },
                    { 26, "Edit", 1, 7 },
                    { 27, "Delete", 1, 7 },
                    { 28, "Show", 1, 7 },
                    { 29, "Add", 1, 8 },
                    { 30, "Edit", 1, 8 },
                    { 31, "Delete", 1, 8 },
                    { 32, "Show", 1, 8 },
                    { 33, "Add", 1, 9 },
                    { 34, "Edit", 1, 9 },
                    { 35, "Delete", 1, 9 },
                    { 36, "Show", 1, 9 },
                    { 37, "Add", 1, 10 },
                    { 38, "Edit", 1, 10 },
                    { 39, "Delete", 1, 10 },
                    { 40, "Show", 1, 10 },
                    { 41, "Add", 1, 11 },
                    { 42, "Edit", 1, 11 },
                    { 43, "Delete", 1, 11 },
                    { 44, "Show", 1, 11 },
                    { 45, "Add", 1, 12 },
                    { 46, "Edit", 1, 12 },
                    { 47, "Delete", 1, 12 },
                    { 48, "Show", 1, 12 },
                    { 49, "Add", 1, 13 },
                    { 50, "Edit", 1, 13 },
                    { 51, "Delete", 1, 13 },
                    { 52, "Show", 1, 13 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeliverToVillages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumn: "id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Weights",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13);
        }
    }
}
