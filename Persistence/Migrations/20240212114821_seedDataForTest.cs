using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class seedDataForTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransaction_Category_CategoryId",
                table: "FinancialTransaction");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "User");

            migrationBuilder.InsertData(
                table: "Budget",
                columns: new[] { "Id", "AppUserId", "CreatedDate", "DeletedDate", "EndDate", "StartDate", "TotalAmount", "UpdatedDate" },
                values: new object[] { 1, 2, new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(9441), null, new DateTime(2024, 3, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(9421), new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(9418), 3000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryType", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3506), null, "Salary", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 0, new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3511), null, "Investment", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3513), null, "Groceries", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3514), null, "Rent", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3515), null, "Utilities", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3516), null, "Entertainment", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 1, new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3517), null, "Transportation", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 1, new DateTime(2024, 2, 12, 14, 48, 20, 684, DateTimeKind.Local).AddTicks(3518), null, "Healthcare", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "BudgetCategory",
                columns: new[] { "Id", "AllocatedAmount", "BudgetId", "CategoryId", "CreatedDate", "DeletedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1000m, 1, 1, new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(5403), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 500m, 1, 2, new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(5416), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 300m, 1, 3, new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(5418), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1000m, 1, 4, new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(5419), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 200m, 1, 5, new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(5420), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 200m, 1, 6, new DateTime(2024, 2, 12, 14, 48, 20, 683, DateTimeKind.Local).AddTicks(5421), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransaction_Category_CategoryId",
                table: "FinancialTransaction",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransaction_Category_CategoryId",
                table: "FinancialTransaction");

            migrationBuilder.DeleteData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BudgetCategory",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Budget",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "AppUser");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransaction_Category_CategoryId",
                table: "FinancialTransaction",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
